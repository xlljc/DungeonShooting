#============================================================
#    Inject Util
#============================================================
# - author: zhangxuetu
# - datetime: 2023-05-14 23:53:46
# - version: 4.0
#============================================================
## 注入节点工具
class_name InjectUtil


## 自动注入 unique （唯一名称）节点属性
##[br]
##[br][code]parent[/code]  目标节点，对这个节点的属性进行自动注入节点属性
##[br][code]prefix[/code]  注入的属性的前缀值
##[br]示例：
##[codeblock]
##extends Node
##
##var __init_node__ = InjectUtil.auto_inject(self, "_")
### 当前场景中有 %sprite 、%collision 节点则会自动获取并自动设置下面两个属性
##var _sprite : Sprite2D
##var _collision: Collision
##
##[/codeblock]
static func auto_inject(parent: Node, prefix: String = "", open_err: bool = false):
	var method : Callable = func():
		for data in (parent.get_script() as GDScript).get_script_property_list():
			if data['type'] == TYPE_OBJECT and parent[data['name']] == null:
				var prop = str(data['name']).trim_prefix(prefix)
				if parent.has_node("%" + prop):
					# 注入属性
					var node = parent.get_node_or_null("%" + prop)
					if node:
						parent[data['name']] = node
					else:
						if open_err:
							printerr("没有 ", prop, " 属性相关节点")
	
	if parent.is_inside_tree():
		method.call()
	else:
		parent.tree_entered.connect(method, Object.CONNECT_ONE_SHOT)
	return true

