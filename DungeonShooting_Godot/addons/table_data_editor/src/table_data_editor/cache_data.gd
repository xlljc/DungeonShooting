#============================================================
#    Project Data
#============================================================
# - author: zhangxuetu
# - datetime: 2023-05-17 17:37:56
# - version: 4.0
#============================================================
## 项目数据
##
##整个表格项目中的之前的缓存数据，也可视作整个项目数据
##[br]启动这个节点后
class_name TableDataEditor_CacheData


const CACHE_DATA_PATH = "res://.godot/table_data_editor/~json_edit_grid_cache_data.gdata"


## 最后一次操作的文件的路径
var last_operation_path : String = ""
# 最近打开过的文件
var recently_opend_paths = {}:
	set(v):
		if v is Array:
			recently_opend_paths = {}
			for item in v:
				recently_opend_paths[item] = null
		elif v is Dictionary:
			recently_opend_paths = v
		else:
			assert(false, "错误的数据类型")
# 弹窗路径
var dialog_path : String = ""


#============================================================
#  SetGet
#============================================================
func get_recently_opend_paths() -> Array:
	if recently_opend_paths is Array:
		return recently_opend_paths
	return recently_opend_paths.keys()



#============================================================
#  自定义
#============================================================
static func instance() -> TableDataEditor_CacheData:
	var script = TableDataEditor_CacheData as GDScript
	const KEY = "instance"
	if script.has_meta(KEY):
		return script.get_meta(KEY)
	
	else:
		var object = TableDataEditor_CacheData.new()
		var data
		if FileAccess.file_exists(CACHE_DATA_PATH):
			var bytes = FileAccess.get_file_as_bytes(CACHE_DATA_PATH)
			data = bytes_to_var(bytes)
		if data is Dictionary:
			TableDataUtil.Classes.set_property_by_dict(object, data)
		
		script.set_meta(KEY, object)
		return object


## 保存数据
static func save_data() -> void:
	var data = TableDataUtil.Classes.get_dict_by_property(instance())
	TableDataUtil.Files.save_data(CACHE_DATA_PATH, data)


## 存在打开的文件
func exists_opened_path() -> bool:
	return last_operation_path != "" and FileAccess.file_exists(last_operation_path)


func update_last_operation_path(path: String):
	path = path.strip_edges()
	last_operation_path = path
	if path != "":
		if not recently_opend_paths is Dictionary:
			recently_opend_paths = {}
		if FileAccess.file_exists(path):
			recently_opend_paths[path] = null

