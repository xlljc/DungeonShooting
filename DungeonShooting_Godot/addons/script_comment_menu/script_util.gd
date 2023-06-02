#============================================================
#    Scirpt Util
#============================================================
# - datetime: 2022-07-17 17:25:00
#============================================================
## 处理脚本的工具
class_name ScriptCommentMenu_ScriptUtil


const DATA_TYPE_TO_NAME = {
	TYPE_NIL: &"null",
	TYPE_BOOL: &"bool",
	TYPE_INT: &"int",
	TYPE_FLOAT: &"float",
	TYPE_STRING: &"String",
	TYPE_RECT2: &"Rect2",
	TYPE_VECTOR2: &"Vector2",
	TYPE_VECTOR2I: &"Vector2i",
	TYPE_VECTOR3: &"Vector3",
	TYPE_VECTOR3I: &"Vector3i",
	TYPE_TRANSFORM2D: &"Transform2D",
	TYPE_VECTOR4: &"Vector4",
	TYPE_VECTOR4I: &"Vector4i",
	TYPE_PLANE: &"Plane",
	TYPE_QUATERNION: &"Quaternion",
	TYPE_AABB: &"AABB",
	TYPE_BASIS: &"Basis",
	TYPE_TRANSFORM3D: &"Transform3D",
	TYPE_PROJECTION: &"Projection",
	TYPE_COLOR: &"Color",
	TYPE_STRING_NAME: &"StringName",
	TYPE_NODE_PATH: &"NodePath",
	TYPE_RID: &"RID",
	TYPE_OBJECT: &"Object",
	TYPE_CALLABLE: &"Callable",
	TYPE_SIGNAL: &"Signal",
	TYPE_DICTIONARY: &"Dictionary",
	TYPE_ARRAY: &"Array",
	TYPE_PACKED_BYTE_ARRAY: &"PackedByteArray",
	TYPE_PACKED_INT32_ARRAY: &"PackedInt32Array",
	TYPE_PACKED_INT64_ARRAY: &"PackedInt64Array",
	TYPE_PACKED_STRING_ARRAY: &"PackedStringArray",
	TYPE_PACKED_VECTOR2_ARRAY: &"PackedVector2Array",
	TYPE_PACKED_VECTOR3_ARRAY: &"PackedVector3Array",
	TYPE_PACKED_FLOAT32_ARRAY: &"PackedFloat32Array",
	TYPE_PACKED_FLOAT64_ARRAY: &"PackedFloat64Array",
	TYPE_PACKED_COLOR_ARRAY: &"PackedColorArray",
}

const NAME_TO_DATA_TYPE = {
	&"null": TYPE_NIL,
	&"bool": TYPE_BOOL,
	&"int": TYPE_INT,
	&"float": TYPE_FLOAT,
	&"String": TYPE_STRING,
	&"Rect2": TYPE_RECT2,
	&"Vector2": TYPE_VECTOR2,
	&"Vector2i": TYPE_VECTOR2I,
	&"Vector3": TYPE_VECTOR3,
	&"Vector3i": TYPE_VECTOR3I,
	&"Transform2D": TYPE_TRANSFORM2D,
	&"Vector4": TYPE_VECTOR4,
	&"Vector4i": TYPE_VECTOR4I,
	&"Plane": TYPE_PLANE,
	&"Quaternion": TYPE_QUATERNION,
	&"AABB": TYPE_AABB,
	&"Basis": TYPE_BASIS,
	&"Transform3D": TYPE_TRANSFORM3D,
	&"Projection": TYPE_PROJECTION,
	&"Color": TYPE_COLOR,
	&"StringName": TYPE_STRING_NAME,
	&"NodePath": TYPE_NODE_PATH,
	&"RID": TYPE_RID,
	&"Object": TYPE_OBJECT,
	&"Callable": TYPE_CALLABLE,
	&"Signal": TYPE_SIGNAL,
	&"Dictionary": TYPE_DICTIONARY,
	&"Array": TYPE_ARRAY,
	&"PackedByteArray": TYPE_PACKED_BYTE_ARRAY,
	&"PackedInt32Array": TYPE_PACKED_INT32_ARRAY,
	&"PackedInt64Array": TYPE_PACKED_INT64_ARRAY,
	&"PackedStringArray": TYPE_PACKED_STRING_ARRAY,
	&"PackedVector2Array": TYPE_PACKED_VECTOR2_ARRAY,
	&"PackedVector3Array": TYPE_PACKED_VECTOR3_ARRAY,
	&"PackedFloat32Array": TYPE_PACKED_FLOAT32_ARRAY,
	&"PackedFloat64Array": TYPE_PACKED_FLOAT64_ARRAY,
	&"PackedColorArray": TYPE_PACKED_COLOR_ARRAY,
}


static func _get_script_data_cache(script: Script) -> Dictionary:
	return ScriptCommentMenu_DataUtil.get_meta_dict_data("ScriptCommentMenu_ScriptUtil__get_script_data_cache")

##  数据类型名称
##[br][code]type[/code]: 数据类型枚举值
##[br][code]return[/code]: 返回数据类型的字符串
static func get_type_name(type: int) -> StringName:
	return DATA_TYPE_TO_NAME.get(type)

## 获取这个类名的类型
static func get_type_of(_class_name: StringName) -> int:
	return NAME_TO_DATA_TYPE.get(_class_name, -1)

## 是否有这个类型的枚举
static func has_type(type: int) -> bool:
	return DATA_TYPE_TO_NAME.has(type)

## 是否是基础数据类型
static func is_base_data_type(_class_name: StringName) -> bool:
	return NAME_TO_DATA_TYPE.has(_class_name)

##  获取属性列表
##[br]
##[br]返回类似如下格式的数据
##[codeblock]
##{ 
##    "name": "RefCounted", 
##    "class_name": &"", 
##    "type": 0, 
##    "hint": 0, 
##    "hint_string": "", 
##    "usage": 128 
##}
##[/codeblock]
static func get_property_data_list(script: Script) -> Array[Dictionary]:
	if is_instance_valid(script):
		return script.get_script_property_list()
	return Array([], TYPE_DICTIONARY, "Dictionary", null)

##  获取方法列表
static func get_method_data_list(script: Script) -> Array[Dictionary]:
	if is_instance_valid(script):
		return script.get_script_method_list()
	ScriptCommentMenu_DataUtil.get_type_array("int")
	return Array([], TYPE_DICTIONARY, "Dictionary", null)


## 获取方法的参数列表数据
static func get_method_arguments_list(script: Script, method_name: StringName) -> Array[Dictionary]:
	var data = get_method_data(script, method_name)
	if data:
		return data.get("args", ScriptCommentMenu_DataUtil.get_type_array("Dictionary"))
	return ScriptCommentMenu_DataUtil.get_type_array("Dictionary")


##  获取信号列表
static func get_signal_data_list(script: Script) -> Array[Dictionary]:
	if is_instance_valid(script):
		return script.get_script_signal_list()
	return Array([], TYPE_DICTIONARY, "Dictionary", null)

## 获取这个属性名称数据
static func get_property_data(script: Script, property: StringName) -> Dictionary:
	var data = _get_script_data_cache(script)
	var p_cache_data : Dictionary = ScriptCommentMenu_DataUtil.get_value_or_set(data, "propery_data_cache", func():
		var property_data : Dictionary = {}
		for i in script.get_script_property_list():
			property_data[i['name']] = i
		return property_data
	)
	return p_cache_data.get(property, {})

## 获取这个名称的方法的数据
static func get_method_data(script: Script, method_name: StringName) -> Dictionary:
	var data = _get_script_data_cache(script)
	var m_cache_data : Dictionary = ScriptCommentMenu_DataUtil.get_value_or_set(data, "method_data_cache", func():
		var method_data : Dictionary = {}
		for i in script.get_script_method_list():
			method_data[i['name']]=i
		return method_data
	)
	return m_cache_data.get(method_name, {})

## 获取这个名称的信号的数据
static func get_signal_data(script: Script, signal_name: StringName):
	var data = _get_script_data_cache(script)
	var s_cache_data : Dictionary = ScriptCommentMenu_DataUtil.get_value_or_set(data, "script_data_cache", func():
		var signal_data : Dictionary = {}
		for i in script.get_script_signal_list():
			signal_data[i['name']]=i
		return signal_data
	)
	return s_cache_data.get(signal_name, {})


##  获取方法数据
## [br]
## [br][code]script[/code]： 脚本
## [br][code]method[/code]： 要获取的方法数据的方法名
## [br]
## [br][code]return[/code]： 返回脚本的数据信息。
##  包括的 key 有 [code]name[/code], [code]args[/code], [code]default_args[/code]
## , [code]flags[/code], [code]return[/code], [code]id[/code]
func find_method_data(script: Script, method: String) -> Dictionary:
	var method_data = script.get_script_method_list()
	for m in method_data:
		if m['name'] == method:
			return m
	return {}


##  获取扩展脚本链（扩展的所有脚本）
##[br]
##[br][code]script[/code]  Object 对象或脚本
##[br][code]return[/code]  返回继承的脚本路径列表
static func get_extends_link(script: Script) -> PackedStringArray:
	var list := PackedStringArray()
	while script:
		if FileAccess.file_exists(script.resource_path):
			list.push_back(script.resource_path)
		script = script.get_base_script()
	return list


##  获取基础类型继承链类列表
##[br]
##[br][code]_class[/code]  基础类型类名
##[br][code]return[/code]  返回基础的类名列表
static func get_extends_link_base(_class) -> PackedStringArray:
	if _class is Script:
		_class = _class.get_instance_base_type()
	elif _class is Object:
		_class = _class.get_class()
	
	var c = _class
	var list = []
	while c != "":
		list.append(c)
		c = ClassDB.get_parent_class(c)
	return PackedStringArray(list)


##  生成方法代码
##[br]
##[br][code]method_data[/code]  方法数据
##[br][code]return[/code]  返回生成的代码
static func generate_method_code(method_data: Dictionary) -> String:
	var temp := method_data.duplicate(true)
	var args := ""
	for i in temp['args']:
		var arg_name = i['name']
		var arg_type = ( get_type_name(i['type']) if i['type'] != TYPE_NIL else "")
		if arg_type.strip_edges() == "":
			arg_type = str(i['class_name'])
		if arg_type.strip_edges() != "":
			arg_type = ": " + arg_type
		args += "%s%s, " % [arg_name, arg_type]
	temp['args'] = args.trim_suffix(", ")
	if temp['return']['type'] != TYPE_NIL:
		temp['return_type'] = get_type_name(temp['return']['type'])
	
	if temp.has('return_type') and temp['return_type'] != "":
		temp['return_type'] = " -> " + str(temp['return_type'])
		temp['return_sentence'] = "pass\n\treturn super." + temp['name'] + "()"
	else:
		temp['return_type'] = ""
		temp['return_sentence'] = "pass"
	
	return "func {name}({args}){return_type}:\n\t{return_sentence}\n".format(temp)


##  获取对象的脚本
static func get_object_script(object: Object) -> Script:
	if object == null:
		return null
	if object is Script:
		return object
	return object.get_script() as Script


##  对象是否是 tool 状态
##[br]
##[br][code]object[/code]  返回这个对象的脚本是否是开启 tool 的状态
static func is_tool(object: Object) -> bool:
	var script = get_object_script(object)
	return script.is_tool() if script else false


## 获取对象的脚本路径，如果不存在脚本，则返回空的字符串
static func get_object_script_path(object: Object) -> String:
	var script = get_object_script(object)
	return script.resource_path if script else ""


##  获取这个对象的这个方法的信息
##[br]
##[br][code]object[/code]  对象
##[br][code]method_name[/code]  方法名
##[br][code]return[/code]  返回方法的信息
static func get_object_method_data(object: Object, method_name: StringName) -> Dictionary:
	if not is_instance_valid(object):
		return {}
	var script = get_object_script(object)
	if script:
		return get_method_data(script, method_name)
	return {}


## 获取这个信号的数据
static func get_object_signal_data(object: Object, signal_name: StringName) -> Dictionary:
	if not is_instance_valid(object):
		return {}
	var script = get_object_script(object)
	if script:
		return get_signal_data(script, signal_name)
	return {}


## 获取对象的属性数据
static func get_object_property_data(object: Object, proprety_name: StringName) -> Dictionary:
	if not is_instance_valid(object):
		return {}
	var script = get_object_script(object)
	if script:
		return get_property_data(script, proprety_name)
	return {}


##  获取内置类名称转为对象。比如将 "Node" 字符串转为 [Node] 这种 GDScriptNativeClass 类型数据
##[br]
##[br][code]_class[/code]  类名称
static func get_built_in_class (_class: StringName):
	if not ClassDB.class_exists(_class):
		return null
	var _class_db = ScriptCommentMenu_DataUtil.get_meta_dict_data("ScriptCommentMenu_ScriptUtil_get_built_in_class")
	return ScriptCommentMenu_DataUtil.get_value_or_set(_class_db, _class, func():
		var script = GDScript.new()
		script.source_code = "var type = " + _class
		if script.reload() == OK:
			var obj = script.new()
			_class_db[_class] = obj.type
			return _class_db[_class]
		else:
			push_error("错误的类名：", _class)
		return null
	)


## 根据类名返回类对象
static func get_script_class(_class: StringName):
	if ClassDB.class_exists(_class):
		return null
	var _class_db = ScriptCommentMenu_DataUtil.get_meta_dict_data("ScriptCommentMenu_ScriptUtil_get_script_class")
	return ScriptCommentMenu_DataUtil.get_value_or_set(_class_db, _class, func():
		var script = GDScript.new()
		script.source_code = "var type = " + _class
		if script.reload() == OK:
			var obj = script.new()
			_class_db[_class] = obj.type
			return _class_db[_class]
		else:
			push_error("错误的类名：", _class)
		return null
	)


## 创建脚本
static func create_script(source_code: String) -> GDScript:
	var data = ScriptCommentMenu_DataUtil.get_meta_dict_data("ScriptCommentMenu_ScriptUtil_create_script")
	return ScriptCommentMenu_DataUtil.get_value_or_set(data, source_code.sha256_text(), func():
		var script := GDScript.new()
		script.source_code = source_code
		script.reload()
		return script
	)


## 获取这个类的场景。这个场景的位置和名称需要和脚本一致，只有后缀名不一样。这个类不能是内部类
static func get_script_scene(script: GDScript) -> PackedScene:
	var data = ScriptCommentMenu_DataUtil.get_meta_dict_data("ScriptCommentMenu_ScriptUtil_get_script_scene")
	if data.has(script):
		return data[script]
	else:
		var path := script.resource_path
		if path == "":
			return null
		
		var ext := path.get_extension()
		var file = path.substr(0, len(path) - len(ext))
		
		var scene: PackedScene
		if FileAccess.file_exists(file + "tscn"):
			scene = ResourceLoader.load(file + "tscn", "PackedScene") as PackedScene
		elif FileAccess.file_exists(file + "scn"):
			scene = ResourceLoader.load(file + "scn", "PackedScene") as PackedScene
		else:
			printerr("这个类目录下没有相同名称的场景文件！")
			return null
		data[script] = scene
		return scene


##  获取对象的类。如果是自定义类返回 [GDScript] 类；如果是内置类，则返回 [GDScriptNativeClass] 类
static func get_object_class(object: Object):
	if object:
		if object is Script:
			return object
		if object.get_script() != null:
			return object.get_script()
		return get_built_in_class (object.get_class())
	return &""
