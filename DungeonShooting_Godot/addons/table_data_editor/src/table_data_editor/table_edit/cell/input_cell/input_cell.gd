#============================================================
#    Input Cell
#============================================================
# - datetime: 2022-11-26 18:12:34
#============================================================
##输入单元格
@tool
class_name InputCell
extends MarginContainer


##单击单元格
signal single_clicked
##双击单元格
signal double_clicked
##水平方向拖拽
##[br]
##[br][code]distance[/code] 为当前鼠标位置与第一次按下时鼠标位置的距离
##[br][code]pressed_size[/code] 为点击时的节点的大小
signal h_dragged(distance: float, pressed_node_size: Vector2i)
##垂直方向拖拽
##[br]
##[br][code]distance[/code] 为当前鼠标位置与第一次按下时鼠标位置的距离
##[br][code]pressed_size[/code] 为点击时的节点的大小
signal v_dragged(distance: float, pressed_node_size: Vector2i)


var _data : String
var _latest_v_dragged := false
var _latest_h_dragged := false
var _latest_dragged := false

# 点击时的鼠标位置
var _pressed_mouse_pos: Vector2
# 点击时节点的大小
var _pressed_node_size: Vector2i
# 是否可以拖拽，防止 _event 发送速度过快
var _enabled_dragged := true


@onready 
var _text_edit := %text_edit as TextEdit
@onready 
var _process_timer := %process_timer as Timer


#============================================================
#  SetGet
#============================================================
func set_value(v: String):
	_data = v
	show_value(v)

func get_value() -> String:
	return _data

func show_value(v):
	_text_edit.text = v if v else ""

## 值发生了改变
func is_changed():
	return _text_edit.text != _data


#============================================================
#  内置
#============================================================
func _ready():
#	_text_edit.gui_input.connect(self._gui_input)
	
	_text_edit.focus_entered.connect( func(): self.focus_entered.emit() )
	_text_edit.focus_exited.connect( func(): 
		self.focus_exited.emit() 
		if is_changed():
			set_value(_text_edit.text)
	)
	set_process(false)
	_process_timer.timeout.connect( set_process.bind(false) )
	self.mouse_entered.connect(func():
		set_process(true)
		_process_timer.stop()
	)
	self.mouse_exited.connect(func():
		_process_timer.start()
	)
	


func _process(delta):
	# 更新显示的鼠标图像
	var margin = custom_minimum_size - get_local_mouse_position()
	if margin.x < self["theme_override_constants/margin_right"] and margin.y < self["theme_override_constants/margin_bottom"]:
		self.mouse_default_cursor_shape = Control.CURSOR_MOVE
	elif margin.x <= self["theme_override_constants/margin_right"]:
		self.mouse_default_cursor_shape = Control.CURSOR_HSPLIT
	elif margin.y <= self["theme_override_constants/margin_bottom"]:
		self.mouse_default_cursor_shape = Control.CURSOR_VSPLIT
	else:
		if not(_latest_h_dragged or _latest_v_dragged):
			self.mouse_default_cursor_shape = Control.CURSOR_ARROW
	
	_enabled_dragged = true


func _gui_input(event):
	if event is InputEventMouseMotion:
		if _enabled_dragged:
			var diff = get_local_mouse_position() - _pressed_mouse_pos
			if _latest_h_dragged:
				self.custom_minimum_size.x = _pressed_node_size.x + diff.x
				h_dragged.emit(self.custom_minimum_size.x - _pressed_node_size.x, _pressed_node_size)
				_process_timer.stop()
			if _latest_v_dragged:
				self.custom_minimum_size.y = _pressed_node_size.y + diff.y
				v_dragged.emit(self.custom_minimum_size.y - _pressed_node_size.y, _pressed_node_size)
				_process_timer.stop()
			_enabled_dragged = false
	
	elif event is InputEventMouseButton:
		if event.pressed and event.button_index == MOUSE_BUTTON_LEFT:
			var margin = custom_minimum_size - get_local_mouse_position()
			if event.double_click:
				self.double_clicked.emit()
			else:
				
				_pressed_node_size = self.size
				_pressed_mouse_pos = get_local_mouse_position()
				
				if margin.x <= self["theme_override_constants/margin_right"]:
					_latest_h_dragged = true
				if margin.y <= self["theme_override_constants/margin_bottom"]:
					_latest_v_dragged = true
				
				self.single_clicked.emit()
			
		else:
			self.mouse_default_cursor_shape = Control.CURSOR_ARROW
			_latest_h_dragged = false
			_latest_v_dragged = false
			
			var diff = self.custom_minimum_size - get_local_mouse_position()
			if diff.x < 0 or diff.y < 0:
				_process_timer.start()


