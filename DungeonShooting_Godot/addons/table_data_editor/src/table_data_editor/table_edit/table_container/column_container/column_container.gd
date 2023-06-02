#============================================================
#    Line
#============================================================
# - datetime: 2022-11-26 16:57:14
#============================================================
@tool
class_name ColumnContainer
extends MarginContainer


signal newly_added_cell(cell: Node)


@export
var item : PackedScene


@onready 
var container := %container as HBoxContainer


func update_cell_amount(count: int):
	if container == null:
		await self.ready
	if item:
		var node
		for i in count - container.get_child_count():
			node = item.instantiate()
			container.add_child(node)
			newly_added_cell.emit(node)


func get_cells():
	return container.get_children()


func get_cell(idx: int) -> Node:
	return container.get_child(idx)

