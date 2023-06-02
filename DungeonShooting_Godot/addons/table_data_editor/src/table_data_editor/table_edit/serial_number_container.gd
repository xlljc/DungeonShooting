#============================================================
#    Serial Number Container
#============================================================
# - datetime: 2022-11-26 23:09:40
#============================================================
# 显示左上的数字序号的容器
@tool
class_name SerialNumberContainer
extends GridContainer


## 显示序号的单元格场景
@export var serial_number_cell : PackedScene


var __init_node = InjectUtil.auto_inject(self, "_", true)

var _table_container : TableContainer
var _h_serial_number_container : HBoxContainer
var _v_serial_number_container : VBoxContainer
var _space : Control


var _last_top_left : Vector2i



#============================================================
#  自定义
#============================================================
## 更新横竖列数字
##[br]
##[br][code]top_left[/code]  以 top_left 值开始向下更新
func update_serial_number(top_left: Vector2i):
	var serial_number : SerialNumberCell
	for i in _h_serial_number_container.get_child_count():
		serial_number = _h_serial_number_container.get_child(i) as SerialNumberCell
		serial_number.show_number(top_left.x + i)
	for i in _v_serial_number_container.get_child_count():
		serial_number = _v_serial_number_container.get_child(i) as SerialNumberCell
		serial_number.show_number(top_left.y + i)
	_last_top_left = top_left
	
	_space.custom_minimum_size = Vector2(_v_serial_number_container.size.x, _h_serial_number_container.size.y)
	if _space.custom_minimum_size == Vector2(0,0):
		_space.custom_minimum_size = Vector2(27, 35)

## 更新这个行高
func update_row_height(origin_row: int, height: int):
	if _v_serial_number_container.get_child_count() > 0:
		var node = _v_serial_number_container.get_child(origin_row) as Control
		node.custom_minimum_size.y = height

## 更新这个列宽
func update_column_width(origin_column: int, width: int):
	if _h_serial_number_container.get_child_count() > 0:
		var node = _h_serial_number_container.get_child(origin_column) as Control
		node.custom_minimum_size.x = width


## 更新行列数字标题节点的数量
func update_grid_cell_count(grid_size: Vector2i):
	if _table_container == null:
		while _table_container == null:
			await get_tree().process_frame
	
	# 表格数量发生改变时添加序号节点单元格
	var tile_size = _table_container.get_tile_size()
#	print_debug("单元格大小：", tile_size)
	
	# 水平单元格
	if grid_size.x > _h_serial_number_container.get_child_count():
		var diff_count = grid_size.x - _h_serial_number_container.get_child_count()
		for i in diff_count:
			var node = serial_number_cell.instantiate() as Control
			node.custom_minimum_size = tile_size
			_h_serial_number_container.add_child(node)
	
	# 垂直单元格
	if grid_size.y > _v_serial_number_container.get_child_count():
		var diff_count = grid_size.y - _v_serial_number_container.get_child_count()
		for i in diff_count:
			var node = serial_number_cell.instantiate() as Control
			node.custom_minimum_size.y = tile_size.y
			_v_serial_number_container.add_child(node)
	
	update_serial_number(_last_top_left)

