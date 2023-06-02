extends EditorScript


## 当前代码编辑器
func get_current_code_editor() -> TextEdit:
	return (get_editor_interface()
			.get_script_editor()
			.get_current_editor()
			.get_base_editor()) as TextEdit


## 当前脚本的代码
func get_current_script_code() -> String:
	return get_current_code_editor().text


var _script_popup_id : int = -1
var _script_popup := {}
## 添加脚本弹窗
## @return  返回添加的弹窗菜单的[code]id[/code]
func add_script_popup(popup: PopupMenu) -> int:
	_script_popup_id += 1
	_script_popup[_script_popup_id] = popup
	get_editor_interface().get_script_editor().add_child(popup)
	return _script_popup_id


## 弹出菜单
## @id  菜单 [code]id[/code] 为 [method add_script_popup]add_script_popup[/method] 后返回的值
func popup_menu(id: int):
	if _script_popup.has(id):
		var editor = get_current_code_editor() as TextEdit
		var popup : PopupMenu = _script_popup[id]
		popup.position = (get_current_code_editor().global_position
			+ get_current_code_editor().get_caret_draw_pos()
			+ Vector2(0, 50)
		)
		popup.popup()

func get_current_script() -> Script:
	return get_editor_interface() \
		.get_script_editor() \
		.get_current_script()


func insert_code_current_pos(code: String, insert_first: bool = false):
	var textedit = get_current_code_editor()
	if insert_first:
		textedit.set_caret_column(0)
	textedit.insert_text_at_caret(code)


func _run():
	pass
	
	var popup = PopupMenu.new()
	popup.add_item("test_01")
	popup.add_item("test_02")
	popup.add_item("test_03")
	var id = add_script_popup(popup)
	popup_menu(id)
	
	await get_editor_interface().get_tree().create_timer(1).timeout
	popup.queue_free()
	
