#============================================================
#    Override
#============================================================
# - datetime: 2022-07-17 15:53:56
#============================================================

## 重写
class_name _ScriptMenu_Overrides
extends _ScriptMenu_SubItem


const DIALOG_SCRIPT = preload("dialog.gd")
const DIALOG_SCENE = preload("dialog.tscn")


var dialog = DIALOG_SCENE.instantiate() as DIALOG_SCRIPT


#============================================================
#  自定义
#============================================================
#(override)
func _init_menu(menu_button: MenuButton):
	# 添加弹窗
	dialog.selected_method.connect(_selected_method)
	get_editor_interface().get_base_control().add_child(dialog)
	dialog.theme = get_editor_interface().get_base_control().theme
	# 添加菜单
	add_separator(menu_button)
	add_menu_item(menu_button, "重写方法", {
		"key": KEY_O,
		"ctrl": true,
		"shift": true,
	}, _show_popup)


#(override)
func _uninstall():
	super._uninstall()
	dialog.queue_free()


#============================================================
#  连接信号
#============================================================
## 显示弹窗
func _show_popup():
	var script = get_script_editor_util().get_current_script() as Script
	dialog.show_popup(script)


const FORMAT = """
#(override)
func {method_name}({arguments}){return_type}:
	{return_value}super.{method_name}({parameters})

"""

func _selected_method(method_names : Array):
	
	var text_edit = get_script_editor_util().get_current_code_editor() as TextEdit
	var script = get_script_editor_util().get_current_script() as Script
	
	var code : String = ""
	
	var added = {}
	for method_data in script.get_script_method_list():
		if added.has(method_data['name']) or not method_data['name'] in method_names:
			continue
		
		added[method_data['name']] = null
		
		var method_name = method_data['name']
		var method_type = method_data.get("type", 0)
		var method_args = method_data['args'] as Array
		var method_return = method_data['return']
		
		# 参数列表
		var arguments : String = ", ".join(method_args.map(func(arg): return arg['name']))
		arguments = arguments.strip_edges().trim_suffix(",")
		
		# 类型
		var return_type : String = ScriptCommentMenu_ScriptUtil.get_type_name(method_type)
		var return_value : String = ""
		if return_type == "null":
			return_type = ""
		if return_type != "":
			return_type = " -> " + return_type
			return_value = "return "
		
		code += FORMAT.format({
			"method_name": method_name,
			"method_type": ScriptCommentMenu_ScriptUtil.get_type_name(method_type),
			"return_type": return_type,
			"return_value": "",
			"arguments": arguments,
			"parameters": arguments,
		})
	
	text_edit.set_caret_column(0)
	text_edit.insert_text_at_caret(code)
	




