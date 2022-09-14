//该样例演示如何声明一个类对象

//导入简化的命名空间后的类, global 命名空间下的成员不需要导入
import Behavior = example.framework.Behavior;
//同样也可以直接导入简化名称的命名空间
import myName = example.framework;
//导入整个命名空间
import system;

//除导入语句外, 脚本第一句必须是这个, 定义该文件所属的命名空间, 但是可以忽略, 默认为 global 命名空间
namespace example.defineClass;
/*
这一句也必须写在文件的开头, 在声明 namespace 之后, 
在命名空间 example.defineClass 下声明一个叫 MyClass 的类, 继承自 Behavior 类
当一个文件内写过 class xxx 后, 就表名该文件是一个类, 逻辑代码和声明代码必须写在声明 class后面
*/
class MyClass extends Behavior;

//在类中声明一个 a 变量, a 如果不赋值的话就默认为 null
var a;
var b: number = 1.5;
var c = null;
private var d = true; //私有属性
readonly var e = false; //只读属性

//在类中声明一个叫 length 的 get 属性, get 属性必须有返回值
get length() {
    return b;
}
//在类中声明一个叫 length 的 set 属性, set 属性设置的数据变量叫 param
//param 关键字只能出现在 set 属性中, 并且只读
set length(val: object) {
    b = val;
}

//在类中声明一个 say 的函数, 并且支持传入一个参数
func say(str: string) {
    var message = "say: " + str;
    print(message);
}
//在类中声明一个 say 的函数重载, 该重载为 0 个参数
//注意, 因为脚本数据类型为弱类型, 无法通过数据类型判断重载, 所以函数重载是根据参数长度来进行重载
func say() {
    say("hello");
}
//声明可变参数类型重载
//因为重载是根据参数长度来的, 可变参数就意味着参数至少为 0 个至多为无限个, 因此如果声明了可变参数, 声明的其他重载参数长度必须要小于等于可变参数的位置索引
//例如: 声明了 say(a, ...b), 那么就不能再声明参数长度为2或者更长的重载了
func say(target: string, ...args: string[]) {
    var length = args.length;
    print("length: " + length);
}

//在类中声明构造函数, 其他地方调用 new MyClass(); 时就会调用该类的构造函数, 无参构造可省略
//参数重载和函数的规范一致, new() 构造函数可以被视为一个特殊函数, 其他非构造函数不能调用 new() 函数
func new() {

}
//构造继承, 构造函数继承该类的无参构造函数
func new(message: string) extends new() {
    print("创建了MyClass, message: " + message);
}

//语法展示
private func grammarExample() {
	//调用父类函数
	super.say();
	//判断实例是否是该类型
	if (i is Map) {
		print("是字典");
	} else {
		print("不是字典");
	}
	//for循环100次
	for (var i = 0; i < 100; i++) {
		continue;
	}
	//while循环
	while (true) {
		break;
	}
	//repeat循环, 执行100次
	repeat(100) {
		
	}
	//函数表达式
	var f1 = (a1) => {
		
	}
	//执行函数
	f1(1);
	//将成员函数存入变量
	var f2: func(string) = func(say, string);
	////数组
	var array = [1, 2, "str"];
	//匿名对象, 无法扩展和删除属性, 只能对已有的属性进行赋值
	var obj = {
		x: 1,
		y: -2.2,
		list: ["str", true],
	};
	//字典, 允许扩展和删除属性, 性能比匿名对象低
	var map = @{
		a: "",
		b: 1,
		c: false
	};
}

//静态部分注意, 如果声明了一个叫 a 的非静态成员, 那么就不能声明一个叫 a 的静态变量

//在类中声明一个静态的 sa 变量, 并附上初始值 1
static var sa = 2;

//静态函数, 参数重载和普通函数的规范一致
static func staticSay() {

}
