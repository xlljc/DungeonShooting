#============================================================
#    Table Data Set
#============================================================
# - author: zhangxuetu
# - datetime: 2023-05-16 23:22:41
# - version: 4.0
#============================================================
## 表格数据集
##
##专门存储管理表单的数据。处理每个单元格中的数据
class_name TableDataEditor_TableDataSet


enum {
	COLUMN,
	ROW,
}

# 表格数据。以 [code]grid_data[行][列] = 值[/code] 的格式存储数据。
var grid_data : Dictionary = {}
# 列集合。所有行哪些列有值
var column_set : Dictionary = {}


#============================================================
#  SetGet
#============================================================
func get_config_data():
	return {
		"grid_data": grid_data,
		"column_set": column_set,
	}

func set_config_data(data: Dictionary):
	self.grid_data = data.get("grid_data", {})
	self.column_set = data.get("column_set", {})

func get_origin_data() -> Dictionary:
	return grid_data

func is_empty() -> bool:
	return grid_data.is_empty()

func has_value(coords: Vector2i) -> bool:
	return (grid_data.has(coords[ROW]) 
		and grid_data[coords[ROW]].has(coords[COLUMN])
	)

func get_value(coords: Vector2i):
	if has_value(coords):
		var row : int = coords[ROW]
		var column : int = coords[COLUMN]
		return grid_data[row][column]
	return ""

func set_value(coords: Vector2i, value) -> void:
	var row : int = coords[ROW]
	var column : int = coords[COLUMN]
	
	if not grid_data.has(row):
		grid_data[row] = {}
	grid_data[row][column] = value
	
	if not column_set.has(column):
		column_set[column] = 0
	column_set[column] += 1


func get_max_column() -> int:
	if column_set.is_empty():
		return 0
	return column_set.keys().max()

func remove_value(coords: Vector2i) -> bool:
	if has_value(coords):
		var row : int = coords[ROW]
		var column : int = coords[COLUMN]
		grid_data[row].erase(column)
		column_set[column] -= 1
		
		# 没有数据时，进行移除
		if Dictionary(grid_data[row]).is_empty():
			grid_data.erase(row)
		if column_set[column] == 0:
			column_set.erase(column)
		return true
	return false

func get_row_list() -> Array[int]:
	return Array(grid_data.keys(), TYPE_INT, "", null)



#============================================================
#  内置
#============================================================
func _to_string():
	return var_to_str( TableDataUtil.Classes.get_dict_by_property(self) )


func _init(data: Dictionary = {}):
	self.grid_data = data.get("grid_data", {})
	self.column_set = data.get("column_set", {})


