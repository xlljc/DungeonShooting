#============================================================
#    Edit Box Window
#============================================================
# - author: zhangxuetu
# - datetime: 2023-03-19 11:48:30
# - version: 4.0
#============================================================
@tool
class_name PopupEditBox
extends Control


signal popup_hide(text: String)
signal box_size_changed(box_size: Vector2)
signal input_switch_char(character: int)


@export
var text : String = "" :
	set(v):
		text = v
		if not is_inside_tree(): await ready
		if _edit_box.text != text:
			_edit_box.text = text
@export
var showed : bool = true:
	set(v):
		if v != self.visible:
			showed = v
			self.visible = v
@export
var box_size: Vector2 :
	set(v):
		box_size = v
		if not is_inside_tree(): await ready
		if _edit_box.size != box_size:
			_edit_box.size = box_size
	get:
		if _edit_box:
			return _edit_box.size
		return Vector2(0, 0)


@onready var _edit_box := %edit_box as TextEdit
@onready var _scale_rect := %scale_rect as Control


var _resize_pressed : bool = false
var _pressed_size : Vector2 = Vector2(0,0)
var _pressed_pos : Vector2 = Vector2(0,0)


#============================================================
#  SetGet
#============================================================
func get_edit_box() -> TextEdit:
	return _edit_box

func get_text() -> String:
	return _edit_box.text


#============================================================
#   内置
#============================================================
func _ready():
	_edit_box.position = Vector2(0,0)
	
	_scale_rect.gui_input.connect(func(event):
		if event is InputEventMouseMotion:
			if _resize_pressed:
				var diff_v = get_global_mouse_position() - _pressed_pos
				_edit_box.size = _pressed_size + diff_v
			
		elif event is InputEventMouseButton:
			if event.button_index == MOUSE_BUTTON_LEFT:
				_resize_pressed = event.pressed
				if _resize_pressed:
					_pressed_size = _edit_box.size
					_pressed_pos = get_global_mouse_position()
	)


#============================================================
#  自定义
#============================================================
func popup(rect: Rect2 = Rect2()):
	if _edit_box == null: await ready
	
	if rect.position != Vector2():
		_edit_box.global_position = rect.position
	if rect.size != Vector2():
		_edit_box.size = rect.size
	
	# 聚焦编辑
	_edit_box.visible = true
	_edit_box.set_caret_line( _edit_box.get_line_count() )
	_edit_box.set_caret_column( _edit_box.text.length() )
	self.showed = true
	
#	print("[ PopupEditBox ] 弹出窗口")
	
	# 取消焦点时隐藏
	var t = _edit_box.text
	_edit_box.grab_focus()
	_edit_box.focus_exited.connect(func(): 
		if t != _edit_box.text:
			self.popup_hide.emit(_edit_box.text)
		_edit_box.visible = false
#		print("[ PopupEditBox ] 弹窗隐藏")
	, Object.CONNECT_ONE_SHOT)


func _on_edit_box_resized():
	if _edit_box == null: await ready
	self.box_size_changed.emit(_edit_box.size)


func _on_edit_box_gui_input(event):
	if event is InputEventKey:
		if event.pressed:
			if not event.alt_pressed:
				# Enter/Tab 切换单元格
				if event.keycode in [KEY_ENTER, KEY_KP_ENTER]:
					self.input_switch_char.emit(KEY_ENTER)
					get_tree().root.set_input_as_handled()
					
				elif event.keycode in [KEY_TAB]:
					self.input_switch_char.emit(KEY_TAB)
					get_tree().root.set_input_as_handled()
			
			else:
				# Alt+Enter换行
				if event.keycode in [KEY_ENTER, KEY_KP_ENTER]:
					_edit_box.insert_text_at_caret("\n")

