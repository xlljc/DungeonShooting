#============================================================
#    List
#============================================================
# - datetime: 2022-11-26 16:57:09
#============================================================
## 数据行的容器

@tool
class_name RowContainer
extends MarginContainer


signal newly_added_line(line: ColumnContainer)


@export var item : PackedScene


@onready var container = %container as VBoxContainer


func get_columns_containers() -> Array:
	return container.get_children()


## 更新行数量
func update_row_amount(count: int):
	if container == null:
		await self.ready
	var node
	for i in count - container.get_child_count():
		node = item.instantiate()
		container.add_child(node)
		newly_added_line.emit(node)


