#============================================================
#    File Data
#============================================================
# - author: zhangxuetu
# - datetime: 2023-03-23 23:30:46
# - version: 4.0
#============================================================
## 当前文件数据
##
##新建/打开/存储 处理其中的数据。
##[br]文件相关的数据都在这里保存，加载存储获取都在这里，这样保证数据不会乱
class_name TableDataEditor_FileData


## 原始数据
var origin_data : Dictionary
# 数据格式版本
var version:
	get: return 1_3_0
# 数据集
var data_set = TableDataEditor_TableDataSet.new():
	set(v):
		data_set = TableDataEditor_TableDataSet.new(v) \
			if v is Dictionary \
			else v
## 列宽数据
var column_width : Dictionary = {}
## 行高数据
var row_height : Dictionary = {}
## 编辑框大小
var edit_dialog_size : Vector2 = Vector2(100, 50)
## 文件路径
var path : String = ""


#============================================================
#  SetGet
#============================================================
func is_empty() -> bool:
	return origin_data.is_empty()

func is_new_file() -> bool:
	return path.is_empty()


#============================================================
#  内置
#============================================================
func _init(origin_data: Dictionary):
	self.origin_data = origin_data
	
	# 加载旧版本数据
	var version = origin_data.get("version")
	if not ((version is float or version is int) and version >= 1.3):
		var grid_data : Dictionary = origin_data.get("grid_data", {})
		for coords in grid_data:
			self.data_set.set_value(coords, grid_data[coords])
	
	# 根据字典数据设置当前对象属性
	TableDataUtil.Classes.set_property_by_dict(self, origin_data)


#============================================================
#  自定义
#============================================================
## 加载文件。
static func load_file(path: String) -> TableDataEditor_FileData:
	if FileAccess.file_exists(path):
		var data = TableDataUtil.Files.load_file(path)
		if not data is Dictionary: 
			var reader = FileAccess.open(path, FileAccess.READ)
			if reader:
				data = reader.get_var()
		
		if not data is Dictionary:
			data = {}
		
		var file_data = TableDataEditor_FileData.new(data)
		file_data.path = path
		return file_data
	else:
		return TableDataEditor_FileData.new({})


## 保存当前文件的数据
##[br]
##[br][code]saved_path[/code]  保存到的路径，默认不传参数保存为当前路径，
##如果当前没有设置路径则会报错，所以第一次要传入路径进行保存
func save_data(saved_path: String = "") -> bool:
	if saved_path == "":
		saved_path = self.path
	self.path = saved_path
	
	assert(self.path.strip_edges() != "", "当前没有设置文件路径")
	
	# 获取数据
	var data : Dictionary = {}
	var propertys = TableDataUtil.Classes.get_propertys(self.get_script())
	for property in propertys:
		if property in self:
			data[property] = self[property]
	
	data["data_set"] = data_set.get_config_data()
	
	return TableDataUtil.Files.save_data(self.path, data)

