#============================================================
#    Data Util
#============================================================
# - datetime: 2022-12-21 21:19:10
#============================================================
## 数据工具
##
##用作全局获取数据使用
class_name ScriptCommentMenu_DataUtil


##  获取场景树 [SceneTree] 对象的 meta 数据作为单例数据，如果返回的数据为 [code]null[/code] 则会在下次继续调用这个 
##default 回调方法，直到返回的数据不为 [code]null[/code] 为止 
##[br]
##[br][code]meta_key[/code]  数据key
##[br][code]default[/code]  如果没有这个key，则默认返回的数据
##[br][code]ignore_null[/code]  忽略 null 值。如果为 true，则在默认值为 null 的时候不记录到元数据，直到有数据为止
static func get_meta_data(meta_key: StringName, default: Callable, ignore_null: bool = true):
	if Engine.has_meta(meta_key) and Engine.get_meta(meta_key) != null:
		return Engine.get_meta(meta_key)
	else:
		var value = default.call()
		if ignore_null:
			if value != null:
				set_meta_data(meta_key, value)
		else:
			set_meta_data(meta_key, value)
		
		return value


##  设置数据
##[br]
##[br][code]meta_key[/code]  数据key
##[br][code]value[/code]  设置的值
static func set_meta_data(meta_key: StringName, value):
	Engine.set_meta(meta_key, value)


## 是否有这个 key 的据
static func has_meta_data(meta_key: StringName) -> bool:
	return  Engine.has_meta(meta_key)


##  移除数据
static func remove_meta_data(meta_key: StringName) -> bool:
	if Engine.has_meta(meta_key):
		Engine.remove_meta(meta_key)
		return true
	return false


## 移除所有meta数据
static func clear_all_meta() -> void:
	for key in Engine.get_meta_list():
		Engine.remove_meta(key)


##  获取 Dictionary 数据
static func get_meta_dict_data(meta_key: StringName, default: Dictionary = {}) -> Dictionary:
	if Engine.has_meta(meta_key):
		return Engine.get_meta(meta_key)
	else:
		Engine.set_meta(meta_key, default)
		return default


##  获取 Array 数据
static func get_meta_array_data(meta_key: StringName, default: Array = []) -> Array:
	if Engine.has_meta(meta_key):
		return Engine.get_meta(meta_key)
	else:
		Engine.set_meta(meta_key, default)
		return default


## 获取目标的默认数据，以目标对象作为基础存储数据
static func get_object_data(object: Object, key: StringName, default: Callable ):
	if object.has_meta(key):
		return object.get_meta(key)
	else:
		var data = default.call()
		object.set_meta(key, data)
		return data


## 获取标 [Dictionary] 类型数据 
static func get_object_dict_data(object: Object, key: StringName, default: Dictionary = {}) -> Dictionary:
	return get_object_data(object, key, func(): return default)


class _ClassInfo:
	var _type : int = TYPE_NIL
	var _class_name : StringName = &""
	var _script : Script = null
	
	func _to_string():
		return str({
			"_type": _type,
			"_class_name": _class_name,
			"_script": _script,
		})
	

## 获取类的数据
##[br]
##[br][code]_class[/code]  类型。这个值可以是类名称，也可以是 [int] 类的数据型枚举的值。最大
## [constant TYPE_MAX]，最小 [constant TYPE_NIL]
##[br][code]return[/code]  返回这个类的信息
static func get_class_info(_class) -> _ClassInfo:
	var map = get_meta_dict_data("DataUtil_get_type_cache_data_for_array", {})
	if map.has(_class):
		return map[_class] as _ClassInfo
		
	else:
		var type : int = TYPE_NIL
		var _class_name : StringName = &""
		var script = null
		if _class is Script:
			type = TYPE_OBJECT
			_class_name = _class.get_instance_base_type()
			script =  _class
		elif _class is int and _class > 0 and _class < TYPE_MAX:
			type = _class
			_class = ScriptCommentMenu_ScriptUtil.get_type_name(_class)
		elif _class is Object:
			var _class_type_ = str(_class)
			if _class_type_.contains("GDScriptNativeClass"):
				var obj = _class.new()
				type = typeof(obj)
				_class_name = obj.get_class()
			else:
				type = TYPE_OBJECT
				_class_name = "Object"
		elif _class is String:
			if ScriptCommentMenu_ScriptUtil.is_base_data_type(_class):
				type = ScriptCommentMenu_ScriptUtil.get_type_of(_class)
				_class = ScriptCommentMenu_ScriptUtil.get_built_in_class(_class)
			else:
				type = TYPE_OBJECT
		
		var data = _ClassInfo.new()
		data._type = type
		data._class_name = _class_name
		data._script = script
		map[_class] = data
		return data


## 获取类型化数组
##[br]
##[br][code]_class[/code]  数据的类型。比如 [code]"Dictionary", Node, Sprite2D[/code] 等类名（基础数据类型需要加双引号），
##或者自定义类名 Player，或者字符串形式的类名，或者 TYPE_INT, TYPE_DICTIONARY
##[br][code]default[/code]  默认有哪些数据
static func get_type_array(_class, default : Array = []) -> Array:
	var data : _ClassInfo = get_class_info(_class)
	# 返回类型化数组
	return Array(default, data._type, data._class_name, data._script )


## 转为类型化数组
static func to_type_array(_class, array: Array) -> Array:
	return get_type_array(_class, array)


## 数组转为字典
##
##[codeblock]
##var dict_data = ScriptCommentMenu_DataUtil.array_to_dictionary( 
##    node_list, 
##    func(node): return node.name, # key 键
##    func(node): return {} 
##) 
##[/codeblock]
static func array_to_dictionary(
	list: Array, 
	get_key: Callable = func(item): return item, 
	get_value: Callable = func(item): return null 
) -> Dictionary:
	var data = {}
	var key
	var value
	for i in list:
		key = get_key.call(i)
		value = get_value.call(i)
		data[key] = value
	return data


## 引用数据
class RefData:
	var value
	
	func get_value():
		return value
	
	func _init(value) -> void:
		self.value = value
	
	func _to_string():
		return str(value)


## 获取引用数据。
##[br]
##[br][b]Note:[/b] 主要用在匿名函数里，以处理基本数据类型的值。因为匿名函数之外的基本数据类型的值
##在匿名函数修改不会发生改变。
static func get_ref_data(default) -> RefData:
	return RefData.new(default)


## 获取字典的值，如果没有，则获取并设置默认值
##[br]
##[br][code]dict[/code]  获取的字典
##[br][code]key[/code]  key 键
##[br][code]not_exists_set[/code]  没有则返回值设置这个值。这个回调方法返回要设置的数据
static func get_value_or_set(dict: Dictionary, key, not_exists_set: Callable):
	if dict.has(key) and not typeof(dict[key]) == TYPE_NIL:
		return dict[key]
	else:
		dict[key] = not_exists_set.call()
		return dict[key]


## 生成id
static func generate_id(data_list: Array) -> StringName:
	var list = []
	for i in data_list:
		list.append(hash(i))
	return ",".join(list).sha1_text()


## 如果不为空值结果值
class NotNullValueChain:
	
	func _init(value):
		set_meta("value", value)
	
	func get_value(default = null):
		return get_meta("value", default)
	
	func or_else(object, else_object: Callable) -> NotNullValueChain:
		return NotNullValueChain.new( object if object else else_object.call() )
	
	## 返回结果不为空时，这个方法需要一个参数接收值
	func if_not_null(else_object: Callable, default = null) -> NotNullValueChain:
		var value = get_value()
		return NotNullValueChain.new( else_object.call(value) if value else default )


##  如果对象不为 null 则调用。
## 可以链式调用逐步执行功能
##[codeblock]
##func get_data(object: Object):
##    return ScriptCommentMenu_DataUtil.if_not_null(object, func():
##        return object.get_script()
##    ).or_else(func():
##        print("")
##    )
##[/codeblock]
static func if_not_null(object, else_object: Callable) -> NotNullValueChain:
	return NotNullValueChain.new((
		else_object.call() if object != null else object
	))


## 获取正则
static func get_regex(pattern: String) -> RegEx:
	var re = RegEx.new()
	re.compile(pattern)
	return re


##  合并数据
##[br]
##[br][code]merge_target[/code]  合并到的目标
##[br][code]data[/code]  要追加合并的数据
##[br][return]return[/return]  返回合并后的数据
static func merge(merge_target, data):
	if merge_target is Dictionary:
		merge_target.merge(data)
		return merge_target
	elif merge_target is Array or merge_target is String:
		merge_target += merge_target
		return merge_target
	else:
		assert(false, "错误的数据类型！只能合并 [Dictionary, Array, String] 中的一种！")


## 获取一个唯一的数字 ID，从 0 始
static func get_id() -> int:
	const KEY = "DataUtil_get_id"
	if Engine.has_meta(KEY):
		var id = Engine.get_meta(KEY)
		id += 1
		Engine.set_meta(KEY, id)
		return id
	else:
		var id = 0
		Engine.set_meta(KEY, id)
		return id


## 列表转为集合hash值，这样即便列表顺序不一致他的值也是相同的
static func as_set_hash(list: Array) -> int:
	var h : int = 0
	for i in list:
		h += hash(i)
	return h

