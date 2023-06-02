#============================================================
#    @sub Tem
#============================================================
# - datetime: 2022-07-17 16:32:29
#============================================================
class_name _ScriptMenu_SubItem


const MenuItemBuilder := ScriptCommentMenuConstant.MenuItemBuilder


var _editor_plugin = EditorPlugin.new()
var _util_script : ScriptCommentMenu_ScriptUtil
var _util_script_editor : ScriptCommentMenuConstant.ScriptEditorUtil


#============================================================
#  Set/Get
#============================================================
func get_editor_interface() -> EditorInterface:
	return _editor_plugin.get_editor_interface()

func get_script_editor_util():
	return _util_script_editor

func get_script_util() -> ScriptCommentMenu_ScriptUtil:
	return _util_script


#============================================================
#  自定义
#============================================================
##  外部调用初始化菜单
##[br]
##[br][code]menu_button[/code]  菜单按钮
func init_menu(menu_button: MenuButton) -> void:
	if not menu_button.has_meta("IsInit"):
		_util_script_editor = ScriptCommentMenuConstant.ScriptEditorUtil.new()
		_util_script =  ScriptCommentMenu_ScriptUtil.new()
		menu_button.set_meta("IsInit", {
			"_util_script_editor": _util_script_editor,
			"_util_script": _util_script,
		})
	
	var data : Dictionary = menu_button.get_meta("IsInit")
	for property in data:
		var value = data[property]
		set(property, value)
	
	_init_menu(menu_button)


## 添加分隔符
func add_separator(menu_button: MenuButton):
	(MenuItemBuilder.instance()
		.set_menu_by_menu_button(menu_button)
		.add_separator()
		.build()
	)

## 添加菜单
func add_menu_item(menu_button: MenuButton, name: String, key_map: Dictionary, callable: Callable):
	# 添加菜单
	(MenuItemBuilder.instance()
		.set_menu(menu_button.get_popup())
		.set_item_name(name)
		.set_connect(callable)
		.set_key(key_map.get("key", false))
		.set_ctrl(key_map.get("ctrl", false))
		.set_shift(key_map.get("shift", false))
		.set_alt(key_map.get("alt", false))
		.build()
	)



##  重写方法，初始化菜单
##[br]
##[br][code]menu_button[/code]  菜单按钮
func _init_menu(menu_button: MenuButton) -> void:
	pass


##  卸载子项
func _uninstall():
	pass


