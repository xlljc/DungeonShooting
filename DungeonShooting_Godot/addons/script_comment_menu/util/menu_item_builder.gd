#============================================================
#    Menu Item Builder
#============================================================
# - datetime: 2022-07-17 14:14:42
#============================================================
# 菜单项建造器

var _menu : PopupMenu
var _name : String
var _method : Callable
var _short : Dictionary = {
	key = KEY_NONE,
	ctrl = false,
	alt = false,
	shift = false,
}
var _as_separator : bool = false

var _id : int = -1


#============================================================
#  Set/Get
#============================================================
##  设置添加的菜单
##[br]
##[br][code]menu[/code]  菜单
func set_menu(menu: PopupMenu):
	self._menu = menu
	return self

##  设置菜单按钮对象
func set_menu_by_menu_button(menu_button: MenuButton):
	set_menu( menu_button.get_popup() )
	return self


##  设置菜单名
##[br]
##[br][code]name[/code]  
func set_item_name(name: String):
	self._name = name
	return self

##  设置连接的方法
##[br]
##[br][code]method[/code]  连接的方法
func set_connect(method: Callable):
	self._method = method
	return self

##  设置快捷键
##[br]
##[br][code]key[/code]  按键 Key 值
##[br][code]ctrl[/code]  Ctrl
##[br][code]alt[/code]  Alt
##[br][code]shift[/code]  Shift
func set_shortcut(
	key: int
	, ctrl: bool = false
	, alt: bool = false
	, shift: bool = false
):
	self._short.key = key
	self._short.ctrl = ctrl
	self._short.alt = alt
	self._short.shift = shift
	return self

func set_key(key : int):
	self._short.key = key
	return self

func set_ctrl(value : bool = true):
	self._short.ctrl = value
	return self

func set_shift(value : bool = true):
	self._short.shift = value
	return self

func set_alt(value : bool = true):
	self._short.alt = value
	return self


#============================================================
#  自定义
#============================================================
##  实例化一个 Builder
##[br]
##[br][code]return[/code]  返回实例化对象
static func instance():
	var builder = load("res://addons/script_comment_menu/util/menu_item_builder.gd").new()
	return builder


##  添加分隔符
func add_separator():
	_as_separator = true
	return self


## 构建添加
##[br]
##[br][code]return[/code]  返回菜单的 id 值
func build() -> int:
	_id = _menu.item_count
	
	# 引用这个 builder，不这样则会因为引用消失而造成下面的 _id_pressed 失效
	_menu.set_meta("MenuItemMenu_%d" % _id, self)
	
	if _menu.id_pressed.connect( _id_pressed ) != OK:
		printerr("  > id_pressed 信号连接方法失败")
	
	if not _as_separator:
		_menu.add_item(_name)
	else:
		_menu.add_separator(_name)
	
	if _short.key != KEY_NONE:
		var input = InputEventKey.new()
		input.keycode = _short.key
		input.ctrl_pressed = _short.ctrl
		input.alt_pressed = _short.alt
		input.shift_pressed = _short.shift
		var shortcut = Shortcut.new()
		shortcut.events.append(input)
		_menu.set_item_shortcut(_id, shortcut)
	
	return _id


#============================================================
#  连接信号
#============================================================
func _id_pressed(id):
	if id == _id:
		_method.call()
