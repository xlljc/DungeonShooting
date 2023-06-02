#============================================================
#    Check check_box
#============================================================
# - datetime: 2022-07-16 22:30:22
#============================================================
@tool
extends HBoxContainer


signal pressed


@export
var text : String = "":
	set(value):
		text = value
		if check_box == null:
			await ready
		check_box.text = value
	get:
		return check_box.text

@export
var selected : bool = false :
	set(value):
		selected = value
		if check_box == null:
			await ready
		check_box.button_pressed = value
	get:
		return check_box.button_pressed

@export
var color : Color = Color.WHITE :
	set(value):
		color = value
		if check_box == null:
			await ready
		check_box.modulate = color


@onready var check_box = $CheckBox as CheckBox


func _ready():
	check_box.pressed.connect(_pressed)


func _pressed():
	pressed.emit()

