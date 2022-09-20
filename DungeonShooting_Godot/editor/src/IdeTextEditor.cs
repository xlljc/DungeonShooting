using Godot;

namespace Editor
{
	public class IdeTextEditor : TextEdit
	{
		private readonly Color KeyCodeColor = new Color(86 / 255f,156 / 255f,214 / 255f);
		private readonly Color AnnotationColor = new Color(77 / 255f,144 / 255f,52 / 255f);
		private readonly Color StringColor = new Color(214 / 255f,157 / 255f,133 / 255f);
		private readonly string[] KeyCodes = {
			"var",
			"namespace",
			"this",
			"class",
			"extends",
			"func",
			"get",
			"set",
			"import",
			"static",
			"new",
			"return",
			"for",
			"switch",
			"case",
			"break",
			"default",
			"while",
			"do",
			"is",
			"repeat",
			"null",
			"true",
			"false",
			"readonly",
			"enum",
			"private",
			"super",
			"if",
			"else",
			"continue",
			"typeof"
		};
		
		public override void _Ready()
		{
			//添加关键字
			for (int i = 0; i < KeyCodes.Length; i++)
			{
				AddKeywordColor(KeyCodes[i], KeyCodeColor);
			}
			AddColorRegion("//", "", AnnotationColor, true);
			AddColorRegion("/*", "*/", AnnotationColor);
			AddColorRegion("\"", "\"", StringColor);
			Text = @"
//导入命名空间
import system;
//声明一个类, 继承Object
class MyClass extends Object;

//声明变量
var text = ""hello \""world\"""";

func say(message) {
	print(message);
}

static test() {
	var arr = [1, 2, 3];
	if (arr.length > 0) {
		for (var i = 0; i < arr.length; i++) {
			print(arr[i]);
		}
	}
}

";
		}
	}
}