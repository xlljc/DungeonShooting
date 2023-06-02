#============================================================
#    Json Editor
#============================================================
# - datetime: 2022-11-27 01:31:14
#============================================================
## JSON 数据编辑器
##
##这里将各独立的组件组合起来，功能整合
@tool
class_name TableDataEditor
extends MarginContainer


## 没有保存文件时的颜色
const NOT_SAVED_COLOR = Color(1, 0.65, 0.275, 1)
## 保存文件后的颜色
const SAVED_COLOR = Color(1, 1, 1, 0.625)
## 最大显示的最近打开的文件数量
const RECENTLY_OPEND_MAX_COUNT = 10


## 文件弹窗文件过滤
const FILTERS = ["*.gdata; GData"]
## 菜单项数据
const MENU_ITEM : Dictionary = {
	"File": [
		"New", "Open", {"Recently Opened": ["/"]}, "-",
		"Save", "Save As...", "-",
		"Export...",
		"Import...",
	],
	"Edit": [
		"Undo", "Redo", "-",
		"Double click edit"
	],
	"Help": ["Help"],
}
## 菜单快捷键
const MENU_SHORTCUT : Dictionary = {
	"/File/New": { "keycode": KEY_N, "ctrl": true },
	"/File/Open": { "keycode": KEY_O, "ctrl": true },
	"/File/Save": { "keycode": KEY_S, "ctrl": true },
	"/File/Save As...": { "keycode": KEY_S, "ctrl": true, "shift": true },
	"/File/Export...": { "keycode": KEY_E, "ctrl": true },
	"/File/Import...": { "keycode": KEY_I, "ctrl": true },
	"/Edit/Undo": {"keycode": KEY_Z, "ctrl": true},
	"/Edit/Redo": {"keycode": KEY_Z, "ctrl": true, "shift": true},
}
const MENU_CHECKABLE : Array = [
	"/Edit/Double click edit",
]


## 创建新的文件
signal created_file(path: String)


# 保存到的文件路径
var _saved_path : String = "" :
	set(v):
		_saved_path = v
		_file_path_label.text = _saved_path
# 是否已保存
var _saved: bool = true:
	set(v):
		_saved = v
		if _saved_status_label == null:
			await ready
		if _saved:
			_saved_status_label.text = "(saved)"
			_saved_status_label.self_modulate = SAVED_COLOR
		else:
			_saved_status_label.text = "(unsaved)"
			_saved_status_label.self_modulate = NOT_SAVED_COLOR

# 行映射。记录哪些行有数据
var _has_value_row_map := {}
# 列映射。记录哪些列有数据
var _has_value_column_map := {}
# 撤销重做
var _undo_redo : UndoRedo = UndoRedo.new()

# 上次打开的文件路径
var _dialog_path : String = ""
# 是否已加载完成
var _is_reloaded := false :
	set(v):
		if _is_reloaded == false:
			_is_reloaded = v


var __init_node = InjectUtil.auto_inject(self, "_")


var _table_edit : TableEdit # 编辑表格节点
var _menu_list : MenuList # 菜单列表
var _scroll_pos : LineEdit # 滚动条位置输入框
var _pages : ItemList # 切换页面（暂未开始实现功能）
var _export_preview_window : ExportPreviewWindow
var _confirm_dialog : ConfirmationDialog
var _tooltip_dialog : AcceptDialog
var _save_as_dialog : FileDialog
var _open_file_dialog : FileDialog
var _import_dialog : FileDialog
var _saved_status_label : Label
var _file_path_label : Label
var _prompt_message : Label
var _prompt_message_player : AnimationPlayer


var file_data := TableDataEditor_FileData.new({})
var cache_data := TableDataEditor_CacheData.instance()



#============================================================
#  SetGet
#============================================================
## 获取编辑表格对象
func get_table_edit() -> TableEdit:
	return _table_edit


#============================================================
#  内置
#============================================================
func _ready() -> void:
	file_data = TableDataEditor_FileData.new({})
	cache_data = TableDataEditor_CacheData.instance()
	
	(func():
		_saved_path = ""
		
		_init_dialog()
		_init_menu()
		
		_load_last_cache_data()
		
		_is_reloaded = true
		
	).call_deferred()


func _exit_tree():
	if not Engine.is_editor_hint() or TableDataUtil.Editor.is_enabled():
		cache_data.save_data()


#============================================================
#  私有方法
#============================================================
# 新建文件
func _new_file() -> void:
	load_file_path("")


# 初始化菜单列表
func _init_menu():
	# TODO: 最近打开的文件替换增加数据
	_menu_list.init_menu(MENU_ITEM)
	# 设置快捷键
	_menu_list.init_shortcut(MENU_SHORTCUT)
	
	_menu_list.set_menu_disabled_by_path("/Edit/Undo", true)
	_menu_list.set_menu_disabled_by_path("/Edit/Redo", true)
	
	for menu_path in MENU_CHECKABLE:
		_menu_list.set_menu_as_checkable(menu_path, true)
	_menu_list.set_menu_check_by_path("/Edit/Double click edit", true)


# 初始化弹窗
func _init_dialog():
	
	# 数据导出预览
	_export_preview_window.close_requested.connect( func(): _export_preview_window.visible = false )
	
	# 添加文件类型var FILTERS = ["*.gdata; GData"]
	_open_file_dialog.filters = FILTERS
	_save_as_dialog.filters = FILTERS
	_import_dialog.filters = ["*.csv; CSV"]
	
	# 打开窗口的路径位置
	var callable = func(dialog: FileDialog):
		if dialog.current_dir != _dialog_path:
			_dialog_path = dialog.current_dir
	_open_file_dialog.visibility_changed.connect(callable.bind(_open_file_dialog))
	_save_as_dialog.visibility_changed.connect(callable.bind(_save_as_dialog))


# 加载上次缓存的数据
func _load_last_cache_data():
	for dialog in [_open_file_dialog, _save_as_dialog, _import_dialog]:
		dialog.current_dir = cache_data.dialog_path
		dialog.visibility_changed.connect(func():
			if not dialog.visible:
				cache_data.dialog_path = dialog.current_dir
		)
	
	if cache_data.exists_opened_path():
		load_file_path(cache_data.last_operation_path)
	
	# 添加打开过的路径
	const RECENTLY_OPEND_MENU = "/File/Recently Opened"
	var list : Array = cache_data.get_recently_opend_paths()
	list.reverse()
	for idx in range(min(list.size(), RECENTLY_OPEND_MAX_COUNT)):
		var path = list[idx]
		if FileAccess.file_exists(path):
			_menu_list.add_menu(path, RECENTLY_OPEND_MENU)


#============================================================
#  自定义
#============================================================
## 显示提示信息
func display_prompt_message(message: String, color: Color = Color.WHITE) -> void:
	_prompt_message.text = message
	_prompt_message.modulate = color
	_prompt_message_player.stop()
	_prompt_message_player.play("flicker")


##  加载路径的数据
##[br]
##[br][code]path[/code]  加载这个路径的数据，如果为空字符串，则是为临时数据，保存时会弹窗保存位置
func load_file_path(path: String):
	# 这个文件的数据
	load_file_data(TableDataEditor_FileData.load_file(path))
	
	_saved_path = path
	
	cache_data.update_last_operation_path(_saved_path)
	cache_data.save_data()


##  加载文件数据
##[br]
##[br][code]file_data[/code]  加载文件数据
func load_file_data(file_data: TableDataEditor_FileData):
	self.file_data = file_data
	
	# 加载到表格中
	(func():
		_table_edit.row_to_height_map = file_data.row_height
		_table_edit.column_to_width_map = file_data.column_width
		_table_edit.data_set = file_data.data_set
		
		_table_edit.get_edit_dialog().box_size = file_data.edit_dialog_size
		_table_edit.update_cell_list()
	).call_deferred()
	
	# 其他
	_saved = true
	_saved_path = ""
	
	_undo_redo.clear_history()
	_menu_list.set_menu_disabled_by_path("/Edit/Undo", true)
	_menu_list.set_menu_disabled_by_path("/Edit/Redo", true)
	
	_table_edit.get_edit_dialog().showed = false


## 保存数据到这个路径中
func save_data_to(path: String):
	if file_data.save_data(path):
		# 保存成功，则进行处理
		self._saved = true
		self._saved_path = path
		cache_data.update_last_operation_path(path)
		cache_data.save_data()
		
		self.created_file.emit(path)
		
		display_prompt_message("保存成功. %s" % [Time.get_datetime_string_from_system().replace("T", " ")])
		print("[ TableDataEditor ] 保存成功 ", Time.get_datetime_string_from_system())
		
	else:
		display_prompt_message("保存失败")
		printerr("[ TableDataEditor ] 保存失败")


## 保存为 JSON
func save_as_json(path: String):
	var data = _table_edit.data_set.get_origin_data()
	TableDataUtil.Files.save_as_string(path, data)
	self.created_file.emit(path)


## 显示保存 Dialog
func show_save_dialog(default_file_name: String = ""):
	if default_file_name != "":
		_save_as_dialog.current_file = default_file_name
	_save_as_dialog.popup_centered_ratio(0.5)


## 导入文件
func import_file(path: String):
	const FILE_TYPE = ["csv"]
	if not path.get_extension() in FILE_TYPE:
		display_prompt_message("错误的文件类型：%s。暂不支持 %s 以外的文件类型" % [ 
			path.get_extension(), FILE_TYPE 
		])
		assert(false, "错误的文件类型")
	
	# 加载 csv数据 到 数据集 中
	var data_set = TableDataEditor_TableDataSet.new()
	var csv_lines = TableDataUtil.Files.read_csv_file(path)
	
	var line : PackedStringArray
	for row in csv_lines.size():
		line = csv_lines[row]
		for column in line.size():
			data_set.set_value(Vector2i(column, row) + Vector2i.ONE, line[column])
	
	# 加载数据
	var tmp_file_data = file_data.load_file("")
	tmp_file_data.data_set = data_set
	load_file_data(tmp_file_data)
	
	cache_data.dialog_path = path
	cache_data.save_data()



#============================================================
#  连接信号
#============================================================
func _on_table_edit_cell_value_changed(cell: InputCell, coords: Vector2i, previous: String, value: String):
	_saved = false
	
#	print("[ TableDataEditor ] 单元格发生改变")
	
	# 记录存在有数据的行列
	_undo_redo.create_action("修改单元格的值")
	_undo_redo.add_do_method( _table_edit.alter_value.bind(coords, value, false) )
	_undo_redo.add_do_method( _table_edit.update_cell_list )
	_undo_redo.add_undo_method( _table_edit.alter_value.bind(coords, previous, false) )
	_undo_redo.add_undo_method( _table_edit.update_cell_list )
	_undo_redo.commit_action()
	
	# 撤销可用性
	_menu_list.set_menu_disabled_by_path("/Edit/Undo", false)


func _on_table_edit_scroll_changed(coords: Vector2i):
	_scroll_pos.text = str(coords)


func _on_scroll_pos_text_submitted(new_text):
	var re = RegEx.new()
	re.compile("(\\d+)\\s*,\\s*(\\d+)")
	var result = re.search(new_text)
	if result == null:
		return
	
	var pos = str_to_var("Vector2i(%s, %s)" % [result.get_string(1), result.get_string(2)])
	if pos is Vector2i:
		_table_edit.scroll_to(pos)
		print("[ TableDataEditor ] 跳转到位置：", pos)


func _on_menu_list_menu_pressed(idx, menu_path: StringName):
#	print_debug("[ TableDataEditor ] 点击菜单 ", menu_path)
	
	match menu_path:
		"/File/New":
			if not _saved:
				_confirm_dialog.dialog_text = "当前还没有保存，是否要继续创建？"
				_confirm_dialog.popup_centered()
			else:
				_new_file()
		
		"/File/Open":
			_open_file_dialog.popup_centered_ratio(0.5)
		
		"/File/Save":
			if _saved_path == "":
				show_save_dialog()
			else:
				save_data_to(_saved_path)
		
		"/File/Save As...":
			show_save_dialog("new_file.gdata")
		
		"/File/Export...":
			_export_preview_window.popup_centered_ratio(0.5)
			_export_preview_window.update_text_box_content()
		
		"/File/Import...":
			_import_dialog.popup_centered_ratio(0.5)
		
		"/Edit/Undo":
			_undo_redo.undo()
			_menu_list.set_menu_disabled_by_path("/Edit/Undo", not _undo_redo.has_undo())
			_menu_list.set_menu_disabled_by_path("/Edit/Redo", false)
		
		"/Edit/Redo":
			_undo_redo.redo()
			_menu_list.set_menu_disabled_by_path("/Edit/Redo", not _undo_redo.has_redo())
			_menu_list.set_menu_disabled_by_path("/Edit/Undo", false)
		
		"/Edit/Double click edit":
			var status = _menu_list.get_menu_check_by_path("/Edit/Double click edit")
			_menu_list.set_menu_check_by_path("/Edit/Double click edit", not status)
			_table_edit.double_click_edit = not status
		
		"/Help/Help":
			_tooltip_dialog.popup_centered()
		
		_:
			# 最近打开的文件
			if menu_path.contains("/File/Recently Opened"):
				var file_path = menu_path.trim_prefix("/File/Recently Opened/")
				if FileAccess.file_exists(file_path):
					load_file_path(file_path)
					
				else:
					printerr("文件不存在")
					_menu_list.remove_menu(menu_path)


func _on_save_as_dialog_file_selected(path):
	_saved_path = path
	match _saved_path.get_extension():
		"gdata":
			save_data_to( _saved_path )
		"json":
			save_as_json( _saved_path )
		_:
			display_prompt_message("错误的文件类型：%s" % [ _saved_path.get_extension() ])
			printerr("[ TableDataEditor ] <Unknown Type> ", _saved_path.get_extension())


func _on_export_preview_window_exported(path, type, data):
	_export_preview_window.visible = false
	display_prompt_message("已导出 %s 资源" % [type])
	self.created_file.emit(path)


func _on_open_file_dialog_file_selected(path: String):
	cache_data.dialog_path = path.get_base_dir()
	load_file_path(path)


func _on_file_path_label_gui_input(event):
	if event is InputEventMouseButton:
		if event.button_index == MOUSE_BUTTON_LEFT and event.double_click:
			if _saved_path != "" and DirAccess.dir_exists_absolute(_saved_path.get_base_dir()):
				var path = TableDataUtil.Files.get_absolute_path(_saved_path)
				OS.shell_open(path.get_base_dir())


func _on_table_edit_popup_edit_box_size_changed(box_size):
	file_data.edit_dialog_size = box_size

