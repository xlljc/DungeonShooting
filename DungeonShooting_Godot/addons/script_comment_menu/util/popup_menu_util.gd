extends RefCounted


static func find_popup_menu_id(popup: PopupMenu, item_name: String) -> int:
	for idx in popup.get_item_count():
		# 找到这个菜单
		if popup.get_item_text(idx) == item_name:
			return idx
	return -1


static func add_popup_shortcut(
	popup: PopupMenu
	, item_name: String
	, keycode: int
	, ctrl : bool
	, alt : bool
	, shift : bool
):
	var idx = find_popup_menu_id(popup, item_name)
	if idx > -1:
		var shortcut = Shortcut.new()
		var input = InputEventKey.new()
		input.keycode = keycode
		input.ctrl_pressed = ctrl
		input.alt_pressed = alt
		input.shift_pressed = shift
		shortcut.events.append(input)
		popup.set_item_shortcut(idx, shortcut)
	else:
		printerr("没有这个名称 ", item_name, " 的菜单项")



var _popup_data := {}
func connect_popup_item(popup: PopupMenu, item_name: String, target: Object, method: String) -> int:
	var idx = find_popup_menu_id(popup, item_name)
	if idx > -1:
		if not popup.id_pressed.is_connected(self._popup_id_pressed):
			popup.id_pressed.connect(self._popup_id_pressed.bind(popup))
		if not _popup_data.has(popup):
			_popup_data[popup] = {}
		if not _popup_data[popup].has(idx):
			_popup_data[popup][idx] = []
		# 记录这个菜单的 id 的点击数据
		_popup_data[popup][idx].append({
			"target": target,
			"method": method,
		})
	else:
		printerr("这个菜单 popup 没有 ", item_name, " 的菜单项")
	
	return idx


func _popup_id_pressed(idx: int, popup: PopupMenu):
	if _popup_data.has(popup):
		var connected_data_list : Array = _popup_data[popup][idx]
		for data in connected_data_list:
			var target : Object = data['target']
			var method : String = data['method']
			target.call(method)
