#============================================================
#    Serial Number Cell
#============================================================
# - datetime: 2022-11-26 22:33:13
#============================================================
# 序列号表格
@tool
class_name SerialNumberCell
extends BaseCellElement


@onready 
var label := $label as Label


func show_number(v: int):
	if label == null: await ready
	label.text = str(v)

