#============================================================
#    Script Method List
#============================================================
# - datetime: 2022-07-17 15:22:58
#============================================================
@tool
extends MarginContainer


const ITEM_LIST_SCRIPT = preload("item_list.gd")


@onready var item_list = $ScrollContainer/ItemList as ITEM_LIST_SCRIPT
@onready var scroll_container = $ScrollContainer


##  获取所有选中的项
func get_selected_items() -> Array: 
	return item_list.get_selected_items()


##  更新数据
##[br]
##[br][code]script[/code]  脚本
func update_data(script: Script):
	# 获取脚本的继承的所有脚本类
	var scripts = []
	script = script.get_base_script()
	while script != null:
		scripts.append(script)
		script = script.get_base_script()
	# 显示脚本的数据
	show_script_list_data(scripts)
	
	# 滚动到下面
#	await get_tree().create_timer(0.1).timeout
#	scroll_container.scroll_vertical = 2000


##  展示脚本列表数据
##[br]
##[br][code]scripts[/code]  
func show_script_list_data(scripts: Array):
	item_list.clear()
	# 已添加过的（防止重复获取）
	var added : Dictionary = {}
	# 开始遍历
	scripts.reverse()
	for script in scripts:
		var path = script.resource_path.get_file()
		item_list.add_label(path)
		
		# 获取数据
		var data = {}
		for method_data in script.get_script_method_list():
			var method_name : String = method_data['name']
			if not added.has(method_name):
				data[method_name] = method_data
				added[method_name] = null
		
		# 排序
		var list = data.keys()
		list.sort()
		for key in list:
			var method_data : Dictionary = data[key]
			var method : String = method_data['name']
			item_list.add_item(method)
	

