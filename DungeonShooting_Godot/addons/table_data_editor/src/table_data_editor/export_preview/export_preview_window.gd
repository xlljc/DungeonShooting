#============================================================
#    Export Preview
#============================================================
# - author: zhangxuetu
# - datetime: 2022-11-27 17:45:09
# - version: 4.0
#============================================================
# 预览导出
@tool
class_name ExportPreviewWindow
extends Window


## 最大示例数量
const EXAMPLE_COUNT : int = 5


## 导出 json 数据
signal exported(path: String, type, data)


@export_node_path("TableDataEditor") var _table_data_editor : NodePath
@onready var table_data_editor : TableDataEditor = get_node(_table_data_editor)

var __init_node = InjectUtil.auto_inject(self, "_")
var _text_box : TextEdit
var _head_as_key_panel : Control
var _head_line_box : SpinBox
var _save_dialog : FileDialog 
var _compact : CheckBox
var _select_items : Control
var _selected_item_param : Control
var _resource_class_name : LineEdit


## 指定的 head 列数对应的值内容。_head_map[列数] = 值内容
var _head_map : Dictionary = {}
## 类型选项按钮组
var _button_group : ButtonGroup 


#============================================================
#  内置
#============================================================
func _ready() -> void:
	_button_group = _select_items.get_child(0).button_group as ButtonGroup
	_button_group.pressed.connect(func(button):
		for child in _selected_item_param.get_children():
			if child is Control:
				child.visible = false
		
		var item_node : Control = _selected_item_param.get_node_or_null(str(button.name))
		if item_node:
			item_node.visible = true
		
		update_text_box_content()
	)


#============================================================
#  SetGet
#============================================================
## 获取头部字段行。列值对应字段值
func get_head_map() -> Dictionary:
	var head_row_number : int = _head_line_box.value
	var data_set = table_data_editor.get_table_edit().get_data_set()
	var head_row_data : Dictionary = data_set.grid_data.get(head_row_number, {})
	return head_row_data


## 将有值的行的数据进行保存
func get_data_by_head_row() -> Array:
	var result : Array = []
	var head_row_number : int = _head_line_box.value
	var data_set = table_data_editor.get_table_edit().get_data_set()
	var head_row_data : Dictionary = data_set.grid_data.get(head_row_number, {})
	head_row_number += 1
	for row in range(head_row_number, data_set.grid_data.size() + 1):
		var data = {}
		var row_data = data_set.grid_data[row]
		for column in head_row_data:
			data[head_row_data[column]] = row_data.get(column, "")
		result.append(data)
	return result


## 获取 CSV 格式数据
func get_csv_data() -> Array[String]:
	var data_set = table_data_editor.get_table_edit().data_set as TableDataEditor_TableDataSet
	var max_column : int = data_set.get_max_column()
	if max_column == 0:
		return []
	
	var csv_list : Array[String] = []
	for row in data_set.get_row_list():
		var line : Array = []
		for column in range(1, max_column + 1):
			line.append(
				JSON.stringify(data_set.get_value(Vector2i(column, row)))
			)
		csv_list.append(",".join(line))
	
	return csv_list


## 获取转为资源的数据
func get_resources_by_path(path: String) -> Array[Resource]:
	var head_row_data : Dictionary = get_head_map()
	var head_row_number : int = _head_line_box.value
	var data_set = table_data_editor.get_table_edit().get_data_set()
	var row_list = data_set.get_row_list()
	for idx in row_list.size():
		var row = row_list[idx]
		if row > head_row_number:
			row_list = row_list.slice(idx, row_list.size())
			break
	
	# 类名
	var r_class_name : String = _resource_class_name.text.strip_edges()
	
	# 属性列表
	var propertys : Array = []
	for column in head_row_data:
		
		# 寻找这个字段有数据的行，判断数据类型
		var value = ""
		for row in row_list:
			value = data_set.grid_data[row].get(column)
			if value != "":
				break
		
		# 判断数据类型
		var type = "String"
		if value != "":
			var json = JSON.parse_string(value)
			match typeof(json):
				TYPE_STRING, TYPE_NIL: type = "String"
				TYPE_INT: type = "int"
				TYPE_FLOAT: type = "float"
				TYPE_BOOL: type = "bool"
				TYPE_ARRAY: type = "Array"
				TYPE_DICTIONARY: type = "Dictionary"
				TYPE_COLOR: type = "Color"
				TYPE_VECTOR2: type = "Vector2"
				TYPE_VECTOR2I: type = "Vector2i"
				TYPE_VECTOR3: type = "Vector3"
				TYPE_VECTOR3I: type = "Vector3i"
				TYPE_VECTOR4: type = "Vector4"
				TYPE_VECTOR4I: type = "Vector4i"
				_: "String"
		
		# 生成 @export s属性
		var property = head_row_data[column]
		printt(column, property, value)
		propertys.append("@export var %s : %s " % [property, type])
	
	# 生成脚本
	var script = GDScript.new()
	script.source_code = """# {ScriptName}
{ClassName}extends Resource

{Propertys}
""".format({
		"ScriptName": path.get_file(),
		"ClassName": ("class_name %s\n" % [r_class_name]) if r_class_name else "",
		"Propertys": "\n".join(propertys),
	})
	ResourceSaver.save(script, path)
	
	# 生成资源
	var resources : Array[Resource] = []
	var new_script = load(path) as GDScript 
	for row in row_list:
		# 生成数据
		var data = {}
		var row_data = data_set.grid_data[row]
		for column in head_row_data:
			data[head_row_data[column]] = row_data.get(column, "")
		
		# 生成资源。避免 new 时跟已有的类冲突造成的报错
		var resource = Resource.new()
		resource.set_script(new_script)
		for property in data:
			resource[property] = data[property]
		resources.append(resource)
	
	return resources



#============================================================
#  自定义
#============================================================
func _data_format(data) -> String:
	return JSON.stringify(data, "" if _compact.button_pressed else "\t")


func _update_by_head_row():
	var data_list = get_data_by_head_row()
	var examples = []
	for i in range(min(data_list.size(), EXAMPLE_COUNT)):
		examples.append(data_list[i])
	_text_box.text = JSON.stringify(examples, "\t")


func _update_by_csv():
	var data_list = get_csv_data()
	var examples = []
	for i in range(min(data_list.size(), EXAMPLE_COUNT)):
		examples.append(data_list[i])
	_text_box.text = "\n".join(examples)


# 更新文本框的内容
func update_text_box_content():
	_text_box.text = ""
	match _button_group.get_pressed_button().name:
		"json", "resource":
			_update_by_head_row()
		"csv":
			_update_by_csv()
			


#============================================================
#  连接信号
#============================================================
func _on_head_line_box_value_changed(value: float) -> void:
	update_text_box_content()


func _on_export_pressed() -> void:
	var extension = str(_button_group.get_pressed_button().name)
	if extension == "resource":
		var r_class_name = _resource_class_name.text.strip_edges()
		if r_class_name == "":
			r_class_name = "res_0"
		_save_dialog.current_file = r_class_name.to_snake_case() + ".gd"
		
	else:
		_save_dialog.current_file = "new_file." + extension
	
	_save_dialog.popup_centered_ratio(0.5)


func _on_save_dialog_file_selected(path: String) -> void:
	print(path)
	var data
	var type = str(_button_group.get_pressed_button().name)
	match type:
		"csv":
			data = "\n".join(get_csv_data())
			TableDataUtil.Files.save_as_string( path, data )
			
			# 导出的文件保持默认文件，不作为翻译文件
			var keep_import_path = path + ".import"
			TableDataUtil.Files.save_as_string(keep_import_path, '[remap]\n\nimporter="keep"\n\n')
			
		"json":
			data = get_data_by_head_row()
			TableDataUtil.Files.save_as_string( path, _data_format(data) )
		
		"resource":
			data = get_resources_by_path(path)
			var idx : int = 0
			var dir = path.get_base_dir()
			var filename = path.get_file().get_basename()
			for resource in data:
				var file_path = dir.path_join("%s_%002d.tres" % [filename, idx])
				while FileAccess.file_exists(file_path):
					idx += 1
					file_path = dir.path_join("%s_%002d.tres" % [filename, idx])
				ResourceSaver.save(resource, file_path)
				idx += 1
	
	_save_dialog.current_path = path
	
	self.hide()
	print("[ ExportPreview ] 保存数据：", path)
	self.exported.emit(path, type, data )


func _on_cancel_pressed():
	self.hide()
