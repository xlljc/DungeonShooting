extends EditorScript


const EditorUtil_PopupMenu = preload("popup_menu_util.gd")


var json = JSON.new()
var util_popup_menu = EditorUtil_PopupMenu.new()


func _run():
	pass
	
	var menu = MenuButton.new()
	menu.text = "测试菜单"
	add_editor_menu(menu)
	await get_tree().create_timer(2).timeout
	menu.queue_free()



var _top_container: HBoxContainer
func _get_top_container() -> HBoxContainer:
	if _top_container == null:
		for child in get_editor_interface().get_base_control().get_children():
			if child is VBoxContainer:
				_top_container = child.get_child(0)
				break
	return _top_container

var _editor_menu_container : HBoxContainer
func get_editor_menu_container() -> HBoxContainer:
	if _editor_menu_container == null:
		_editor_menu_container = _get_top_container().get_child(0)
	return _editor_menu_container


func add_editor_menu(menu_button: MenuButton):
	get_editor_menu_container().add_child(menu_button)


func get_tree():
	return get_editor_interface().get_tree()


## 添加脚本菜单按钮
func add_script_editor_menu(menu_button: MenuButton, items: Array = []):
	var popup = menu_button.get_popup()
	for item in items:
		if item.begins_with("-"):
			popup.add_separator()
		else:
			while item.begins_with("-"):
				item = item.trim_prefix("-")
			popup.add_item(item)
	
	var menu_container : Control
	while true:
		var tmp = get_editor_interface() \
			.get_script_editor() \
			.get_current_editor()
		if tmp == null:
			await Engine.get_main_loop().create_timer(1).timeout
			continue
		for i in 4:
			tmp = tmp.get_parent_control()
			if tmp == null:
				break
		if tmp == null:
			await Engine.get_main_loop().create_timer(1).timeout
			continue
		menu_container = tmp.get_child(0) as Control
		break
	
	var node_index : int = 0
	for i in range(menu_container.get_child_count() - 1, -1, -1):
		if menu_container.get_child(i) is MenuButton:
			node_index = i + 1
			break
	menu_container.add_child(menu_button)
	menu_container.move_child(menu_button, node_index)


func connect_menu(menu, item_name: String, callable, method: String = ""):
	var popup : PopupMenu
	if menu is MenuButton:
		popup = menu.get_popup()
	elif menu is PopupMenu:
		popup = menu
	if method:
		util_popup_menu.connect_popup_item(menu.get_popup(), item_name, callable, method)
	else:
		util_popup_menu.connect_popup_item(menu.get_popup(), item_name, callable.get_object(), callable.get_method())


static func add_menu_item_shortcut(
	menu: MenuButton
	, item_name: String
	, keycode: int
	, ctrl : bool
	, alt : bool
	, shift : bool
):
	EditorUtil_PopupMenu.add_popup_shortcut(
		menu.get_popup(), item_name, keycode, ctrl, alt, shift
	)
