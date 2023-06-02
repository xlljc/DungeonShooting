#============================================================
#    Dialog
#============================================================
# - datetime: 2022-07-17 15:49:30
#============================================================
@tool
extends Control


signal selected_method(method_names : Array)


const SCRIPT_METHOD_LIST_SCRIPT = preload("res://addons/script_comment_menu/sub_item/override/scene/script_method_list.gd")
const CHECK_LABEL = preload("res://addons/script_comment_menu/sub_item/override/scene/check_label.gd")


@onready var confirmation_dialog = find_child("ConfirmationDialog") 
@onready var script_method_list = find_child("ScriptMethodList") as SCRIPT_METHOD_LIST_SCRIPT


#============================================================
#  自定义
#============================================================
##  显示弹窗
func show_popup(script: Script):
	confirmation_dialog.popup_centered_ratio(0.6)
	script_method_list.update_data(script)


#============================================================
#  自定义
#============================================================
func _ready():
	confirmation_dialog.confirmed.connect(_confirmed)
	if not Engine.is_editor_hint():
		confirmation_dialog.popup_centered()


#============================================================
#  连接信号
#============================================================
func _confirmed():
	var selected_items = script_method_list.get_selected_items()
	
	var method_list = {}
	for item in selected_items:
		item = item as CHECK_LABEL
		var method_name : String = item.text
		method_list[method_name] = null
		item.selected = false
	selected_method.emit(method_list.keys())


