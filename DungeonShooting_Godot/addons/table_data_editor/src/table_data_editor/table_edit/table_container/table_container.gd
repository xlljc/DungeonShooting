#============================================================
#    Table Container
#============================================================
# - datetime: 2022-11-26 17:01:54
#============================================================
## 表格
##
##这里只进行表格单元格相关管理，不进行数据的处理，仅作为表格样式显示
@tool
class_name TableContainer
extends MarginContainer


## 新增单元格
signal newly_added_cell(coords: Vector2i, new_cell: Control)
## 网格中的单元格大小发生改变
signal grid_cell_size_changed(grid_size: Vector2i)


@export var cell : PackedScene


var __init_node = InjectUtil.auto_inject(self, "_", true)

var _row_container : RowContainer
var _update_row_column_amount_timer : Timer


# 表格单元格数量大小
var _grid_cell_count_size := Vector2i()
# 单个单元格大小
var _tile_size := Vector2i()
# 单元格列表
var _cell_list : Array[Control] = []
# 列对应的单元格
var _column_to_cells_map := {}
# 行对应的单元格
var _row_to_cells_map := {}


#============================================================
#  SetGet
#============================================================
## 获取单元格行列数量大小
func get_grid_row_column_count_size() -> Vector2i:
	return _grid_cell_count_size

## 获取表格的行数量
func get_grid_row_count() -> int:
	return _grid_cell_count_size.y

## 获取表格的列数量
func get_grid_column_count() -> int:
	return _grid_cell_count_size.x

## 获取单元格大小
func get_tile_size() -> Vector2i:
	return _tile_size

## 获取行容器
func get_row_container() -> RowContainer:
	return _row_container

## 获取所有行的单元格列容器
func get_column_container() -> Array[ColumnContainer]:
	return _row_container.get_columns_containers()

## 获取所有单元格
func get_all_cell() -> Array[Control]:
	return _cell_list

## 获取一列的单元格。column 从 0 开始，最大为 [method get_grid_row_column_count_siz] 的 x 值
func get_column_cells(column: int) -> Array:
	return _column_to_cells_map[column]

## 获取这一行的单元格。row 从 0 开始，最大为 [method get_grid_row_column_count_siz] 的 y 值
func get_row_cells(row: int) -> Array:
	return _row_to_cells_map[row]



#============================================================
#  内置
#============================================================
func _ready() -> void:
	assert(cell != null, "还没有设置 cell 属性！")
	
	# cell 的更新与大小
	_row_container.newly_added_line.connect(func(new_line: ColumnContainer):
		new_line.newly_added_cell.connect( func(new_cell: Control): 
			self._cell_list.append(new_cell)
			
			var column : int = new_cell.get_index()
			var row : int = new_line.get_index()
			self.newly_added_cell.emit( Vector2i(column, row), new_cell ) 
			
			if _column_to_cells_map.has(column):
				_column_to_cells_map[column].append(new_cell)
			else:
				_column_to_cells_map[column] = []
				_column_to_cells_map[column].append(new_cell)
			if _row_to_cells_map.has(row):
				_row_to_cells_map[row].append(new_cell)
			else:
				_row_to_cells_map[row] = []
				_row_to_cells_map[row].append(new_cell)
		)
		new_line.item = self.cell
		new_line.update_cell_amount(_grid_cell_count_size.x)
	)
	self.resized.connect(_update_row_column_amount)
	
	# 先创建出来第一个，用以获取最小 cell 大小
	_row_container.update_row_amount(1)
	(_row_container.get_columns_containers()[0] as ColumnContainer).update_cell_amount(1)
	var first_cell = _row_container.get_columns_containers()[0].get_cells()[0] as Control
	_tile_size = first_cell.size
	
	# 更新单元格数量
	_update_row_column_amount_timer.timeout.connect(func():
		var tmp_cell_amount = _grid_cell_count_size
		_grid_cell_count_size.x = int(self.size.x / _tile_size.x) + 1
		_grid_cell_count_size.y = int(self.size.y / _tile_size.y) + 1
		
		if tmp_cell_amount != _grid_cell_count_size:
			print("[ TableContainer ] 单元格小：", _grid_cell_count_size)
			self.grid_cell_size_changed.emit(_grid_cell_count_size)
		
		for line in _row_container.get_columns_containers():
			line = line as ColumnContainer
			line.update_cell_amount(_grid_cell_count_size.x)
		_row_container.update_row_amount(_grid_cell_count_size.y)
	)
	_update_row_column_amount_timer.start()
	_update_row_column_amount_timer.autostart = true
	_update_row_column_amount_timer.one_shot = true
	_update_row_column_amount_timer.timeout.emit()



#============================================================
#  自定义
#============================================================
func _update_row_column_amount():
	_update_row_column_amount_timer.stop()
	_update_row_column_amount_timer.start()

