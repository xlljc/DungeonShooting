using Godot;
using File = System.IO.File;

namespace Editor
{
	public class CodeTextEditor : TextEdit
	{
		/// <summary>
		/// 关键字颜色
		/// </summary>
		private readonly Color KeyCodeColor = new Color(86 / 255f, 156 / 255f, 214 / 255f);
		/// <summary>
		/// 注释颜色
		/// </summary>
		private readonly Color AnnotationColor = new Color(77 / 255f, 144 / 255f, 52 / 255f);
		/// <summary>
		/// 字符串颜色
		/// </summary>
		private readonly Color StringColor = new Color(214 / 255f, 157 / 255f, 133 / 255f);
		//------- 其他着色是在godot编辑器中设置的

		/// <summary>
		/// 关键字列表
		/// </summary>
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

		
		private int prevTextLength = 0;
		
		private readonly string[] autoCompeleteRight = { "{", "\"", "(", "[" };
		private readonly string[] autoCompeleteLeft = { "}", "\"", ")", "]" };

		/// <summary>
		/// 字体大小
		/// </summary>
		public int FontSize { get; private set; } = 15;
		
		private TextEditPainter _editPainter;
		
		public override void _Ready()
		{
			_editPainter = GetNode<TextEditPainter>("TextEditPainter");
			//添加关键字
			for (int i = 0; i < KeyCodes.Length; i++)
			{
				AddKeywordColor(KeyCodes[i], KeyCodeColor);
			}

			AddColorRegion("//", "", AnnotationColor, true);
			AddColorRegion("/*", "*/", AnnotationColor);
			AddColorRegion("\"", "\"", StringColor);
			
			Text = File.ReadAllText("editor/example.ds");
			ClearUndoHistory();
		}

		public override void _Process(float delta)
		{
			if (Input.IsMouseButtonPressed((int)ButtonList.Right))
			{
				GD.Print(GetTotalVisibleRows());
				_editPainter.DrawTextEditErrorLine(CursorGetLine());
			}
		}

		private void _on_TextEdit_text_changed()
		{
			var newLength = Text.Length;
			if (newLength != prevTextLength)
			{
				//括号补全

				if (newLength > prevTextLength)
				{
					var line = CursorGetLine();
					var column = CursorGetColumn();
					Select(line, column - 1, line, column);
					var key = GetSelectionText();
					Select(line, column, line, column);
			
					for (int i = 0; i < autoCompeleteRight.Length; i++)
					{
						if (key == autoCompeleteRight[i])
						{
							InsertTextAtCursor(autoCompeleteLeft[i]);
							CursorSetColumn(CursorGetColumn() - 1);
						}
					}
				}
			}

			prevTextLength = newLength;
		}
	}
}