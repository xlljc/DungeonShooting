#============================================================
#    Comment
#============================================================
# - datetime: 2022-07-17 14:49:15
#============================================================
## 脚本注释
class_name _ScriptMenu_Comments
extends _ScriptMenu_SubItem


const SEPARATE_LENGTH = 60


var util_script_editor := ScriptCommentMenuConstant.ScriptEditorUtil.new()
var util_script := ScriptCommentMenu_ScriptUtil.new()

var regex = RegEx.new()


#============================================================
#  内置
#============================================================
func _init():
	var pattern = "(?<indent>\\s*)(static\\s+)?func\\s+(?<method>[^\\(]+)"
	regex.compile(pattern)


#============================================================
#  自定义
#============================================================
#(override)
func _init_menu(menu_button: MenuButton):
	# 设置添加菜单项
	var menu : PopupMenu = menu_button.get_popup()
	add_menu_item(menu_button, "脚本注释", {}, _script_comment)
	add_separator(menu_button)
	add_menu_item(menu_button, "方法注释", {
		"key": KEY_C,
		"ctrl": true,
		"shift": true,
	}, _func_comment)
	add_menu_item(menu_button, "类别分隔", {
		"key": KEY_SLASH,
		"ctrl": true,
		"shift": true,
	}, _category_comment)


#============================================================
#  功能
#============================================================
##  方法注释
func _func_comment():
	var text_edit : TextEdit = util_script_editor.get_current_code_editor()
	var line : int = text_edit.get_caret_line()
	
	for i in range(line, text_edit.get_line_count()):
		var line_code : String = text_edit.get_line(i)
		var result = regex.search(line_code)
		if result:
			var method = result.get_string("method").strip_edges()
			printt(method
				, util_script_editor.get_current_script()
				, util_script_editor.get_current_script().resource_path
			)
			
			var indent = result.get_string("indent")
			var data = util_script.find_method_data(util_script_editor.get_current_script(), method)
			if data.size() == 0:
				printerr('没有找到', method,'方法的数据，脚本是否还未保存？')
				return
			
			var code : String = "##  %s\n" % data['name']
			if data['args'].size() > 0:
				code += (indent + "##[br]\n")
				for arg in data['args']:
					code += (indent + "##[br][code]%s[/code]  \n" % arg['name'])
			if data['return']['type'] != TYPE_NIL:
				code += (indent + "##[br][code]return[/code]  ")
			code = code.trim_suffix("\n")
			util_script_editor.insert_code_current_pos(code, true)
			break

##  脚本注释
func _script_comment():
	var script = util_script_editor.get_current_script()
	if script == null:
		return
	
	var separa = "=".repeat(SEPARATE_LENGTH)
	
	# 脚本名
	var script_name = script.resource_path.get_file().get_basename().capitalize()
	# 时间
	var datetime = Time.get_datetime_dict_from_system()
	var datetime_str = "%02d-%02d-%02d %02d:%02d:%02d" % [
		datetime['year'], datetime['month'], datetime['day'],
		datetime['hour'], datetime['minute'], datetime['second'],
	]
	
	var code = """#{sep}
#    {name}
#{sep}
# - author: zhangxuetu
# - datetime: {datetime}
# - version: 4.0
#{sep}
""".format({
	"sep": separa,
	"name": script_name,
	"datetime": datetime_str,
})
	# 插入到顶部
	var textedit = util_script_editor.get_current_code_editor()
	textedit.set_caret_line(0)
	textedit.set_caret_column(0)
	textedit.insert_text_at_caret(code)


## 类别分隔
func _category_comment():
	var separa = "=".repeat(SEPARATE_LENGTH)
	var code = """#{sep}
#  
#{sep}""".format({
		"sep": separa,
	})
	
	var textedit = util_script_editor.get_current_code_editor()
	textedit.set_caret_column(0)
	textedit.insert_text_at_caret(code)
	textedit.set_caret_line(textedit.get_caret_line() - 1)


