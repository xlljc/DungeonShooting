#============================================================
#    Item List
#============================================================
# - datetime: 2022-07-17 15:02:44
#============================================================
@tool
extends VBoxContainer


const CHECK_LABEL_SCENE = preload("check_label.tscn")
const CHECK_LABEL_SCRIPT = preload("check_label.gd")


var last_press_check : CHECK_LABEL_SCRIPT


##  添加 Item
##[br]
##[br][code]label[/code]  
##[br][code]color[/code]  
##[br]
##[br][code]return[/code]  
func add_item(label: String, color : Color = Color.WHITE) -> CHECK_LABEL_SCRIPT:
	var check_label = CHECK_LABEL_SCENE.instantiate()
	add_child(check_label)
	check_label.text = label
	check_label.color = color
	# 上一次点击的对象
	check_label.pressed.connect(_pressed.bind(check_label)) 
	return check_label


##  添加组别标签
##[br]
##[br][code]text[/code]  标签名
func add_label(text: String):
	var space = Control.new()
	space.custom_minimum_size.y = 4
	add_child(space)
	var panel = Panel.new()
	panel.custom_minimum_size.y = 1
	add_child(panel)
	var label = Label.new()
	label.text = text
	add_child(label)


##  获取所有项
func get_all_item() -> Array:
	var list = []
	for child in get_children():
		if child is CHECK_LABEL_SCRIPT:
			list.append(child)
	return list


##  获取选中的项
func get_selected_items() -> Array:
	var list = []
	for item in get_all_item():
		item = item as CHECK_LABEL_SCRIPT
		if item.selected:
			list.append(item)
	return list

## 清除所有
func clear():
	for child in get_children():
		child.queue_free()



#============================================================
#  连接信号
#============================================================
func _pressed(check: CHECK_LABEL_SCRIPT):
	# 如果是按着 Shift 键
	var all_item = get_all_item()
	if Input.is_key_pressed(KEY_SHIFT):
		if last_press_check != check:
			var last_index = all_item.find(last_press_check)
			var curr_index = all_item.find(check)
			var selected = check.selected
			for i in range(last_index, curr_index, sign(curr_index - last_index)):
				all_item[i].selected = selected
	
	last_press_check = check

