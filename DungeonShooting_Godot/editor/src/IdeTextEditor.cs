using Godot;

namespace Editor
{
	public class IdeTextEditor : TextEdit
	{
		private readonly Color KeyCodeColor = new Color(86 / 255f, 156 / 255f, 214 / 255f);
		private readonly Color AnnotationColor = new Color(77 / 255f, 144 / 255f, 52 / 255f);
		private readonly Color StringColor = new Color(214 / 255f, 157 / 255f, 133 / 255f);

		private readonly string[] KeyCodes =
		{
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

		private readonly string[] auto_compelete_right = { "'", "{", "\"", "(", "[" };
		private readonly string[] auto_compelete_left = { "'", "}", "\"", ")", "]" };

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

		private void _on_TextEdit_text_changed()
		{
			GD.Print(GetWordUnderCursor());
			GD.Print(GetPosAtLineColumn(1, 1));
			Select(CursorGetLine(), CursorGetColumn() - 1, CursorGetLine(), CursorGetColumn());
			var key = GetSelectionText();
			Select(CursorGetLine(), CursorGetColumn(), CursorGetLine(), CursorGetColumn());
			for (int i = 0; i < 5; i++)
			{
				if (key == auto_compelete_right[i])
				{
					InsertTextAtCursor(auto_compelete_left[i]);
					CursorSetColumn(CursorGetColumn() - 1);
				}
			}
		}
	}
}