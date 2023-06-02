#============================================================
#    Table Data Util
#============================================================
# - author: zhangxuetu
# - datetime: 2023-05-17 19:14:50
# - version: 4.0
#============================================================
class_name TableDataUtil


class SingletonData:
	
	static func get_value(key, default: Callable):
		if not Engine.has_meta(key):
			Engine.set_meta(key, default.call())
		return Engine.get_meta(key)
	
	static func get_child_value(key, child_key, child_default: Callable):
		var data = get_value(key, func(): return {})
		if data.has(child_key):
			return data[child_key]
		else:
			data[child_key] = child_default.call()
			return data[child_key]
	


class Files:
	
	static func load_file(path: String):
		if FileAccess.file_exists(path):
			var bytes = FileAccess.get_file_as_bytes(path)
			return bytes_to_var(bytes)
	
	static func make_dir(dir: String) -> bool:
		if not DirAccess.dir_exists_absolute(dir):
			DirAccess.make_dir_recursive_absolute(dir)
			return true
		return false
	
	static func save_data(path: String, data) -> bool:
		make_dir(path.get_base_dir())
		if not path.is_empty():
			var bytes : PackedByteArray = var_to_bytes(data)
			var writer : FileAccess = FileAccess.open(path, FileAccess.WRITE)
			if writer.get_open_error() != OK:
				printerr("打开文件失败！", writer.get_open_error())
				return false
			
			writer.store_buffer(bytes)
			if writer.get_error() != OK:
				printerr("写入文件失败：", writer.get_error())
				return false
			
			writer = null
			return true
		return false
	
	static func save_as_string(path:String, data):
		make_dir(path.get_base_dir())
		var writer = FileAccess.open(path, FileAccess.WRITE)
		writer.store_string(
			JSON.stringify(data) 
			if not data is String 
			else data
		)
	
	static func read_as_string(path: String) -> String:
		if FileAccess.file_exists(path):
			var reader = FileAccess.open(path, FileAccess.READ)
			return reader.get_as_text()
		return ""
	
	static func read_csv_file(path: String, delim: String = ",") -> Array[PackedStringArray]:
		if FileAccess.file_exists(path):
			var reader = FileAccess.open(path, FileAccess.READ)
			var lines : Array[PackedStringArray]= []
			var line = reader.get_csv_line(delim)
			while line != PackedStringArray([""]):
				lines.append(line)
				line = reader.get_csv_line(delim)
			return lines
		return []
	
	static func get_absolute_path(path: String) -> String:
		var reader = FileAccess.open(path, FileAccess.READ)
		if reader:
			return reader.get_path_absolute()
		return ""
	


class Classes:
	
	static func get_propertys(script: Script) -> Array[String]:
		return SingletonData.get_child_value("TableDataUtil_Classes_propertys", script, func():
			var list : Array[String] = []
			list.append_array(script \
				.get_script_property_list() \
				.map(func(data): return data['name'])
				.filter(func(name: String): return name.find(".") == -1)
			)
			return list
		)
	
	static func set_property_by_dict(object: Object, dict: Dictionary):
		for property in dict:
			if property in object:
				object[property] = dict[property]
	
	static func get_dict_by_property(object: Object) -> Dictionary:
		var dict : Dictionary = {}
		var list = get_propertys(object.get_script())
		for property in get_propertys(object.get_script()):
			dict[property] = object[property]
		return dict
	


class Editor:
	
	## 获取编辑器接口
	static func get_editor_interface() -> EditorInterface:
		if not Engine.is_editor_hint():
			return null
		const KEY = "TableDataUtil_get_editor_interface"
		if Engine.has_meta(KEY):
			return Engine.get_meta(KEY)
		else:
			var plugin = ClassDB.instantiate("EditorPlugin")
			Engine.set_meta(KEY, plugin.get_editor_interface())
			return plugin.get_editor_interface()
	
	## 是否开启了插件
	static func is_enabled() -> bool:
		if not Engine.is_editor_hint():
			return false
		if get_editor_interface() == null:
			return false
		return get_editor_interface().is_plugin_enabled("table_data_editor")
	
	## 当前插件是否是打开的
	static func is_main_node() -> bool:
		var node = get_editor_interface().get_editor_main_screen()
		for child in node.get_children():
			if child is Control and child.visible:
				# 显示的是当前插件的名称
				return child.name == 'table_data_editor'
		return false


