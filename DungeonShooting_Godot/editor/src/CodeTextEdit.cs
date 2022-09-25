using Godot;
using File = System.IO.File;

namespace DScript.GodotEditor
{
	public class CodeTextEdit : TextEdit
	{
		/// <summary>
		/// 获取当前的文本编辑器
		/// </summary>
		public static CodeTextEdit CurrentTextEdit { get; private set; }
		
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
		public static readonly string[] KeyCodes =
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

		//记录光标位置
		private int _line;
		private int _column;
		
		/// <summary>
		/// 字体大小
		/// </summary>
		public int FontSize { get; private set; } = 15;
		
		/// <summary>
		/// 绘制对象
		/// </summary>
		public TextEditPainter EditPainter { get; private set; }
		
		public override void _Ready()
		{
			CurrentTextEdit = this;
			
			EditPainter = GetNode<TextEditPainter>("TextEditPainter");
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
				//绘制报错行
				EditPainter.DrawTextEditErrorLine(CursorGetLine());
			}
		}
		
		/// <summary>
		/// 刷新记录的字符串长度
		/// </summary>
		public void TriggerTextChanged()
		{
			EmitSignal("text_changed");
		}

		/// <summary>
		/// 获取指定范围内的文本
		/// </summary>
		/// <param name="startLine">起始行</param>
		/// <param name="startColumn">起始列</param>
		/// <param name="endLine">结束行</param>
		/// <param name="endColumn">结束列</param>
		public string GetTextInRange(int startLine, int startColumn, int endLine, int endColumn)
		{
			var line = CursorGetLine();
			var column = CursorGetColumn();
			Select(startLine, startColumn, endLine, endColumn);
			var key = GetSelectionText();
			Select(line, column, line, column);
			return key;
		}

		/// <summary>
		/// 连接信号, 当文本改变时调用
		/// </summary>
		private void _on_TextEdit_text_changed()
		{
			var newLength = Text.Length;

			// //括号补全
			// if (newLength > prevTextLength)
			// {
			// 	var line = CursorGetLine();
			// 	var column = CursorGetColumn();
			// 	//前一个字符串
			// 	var key = GetTextInRange(line, column - 1, line, column);
			// 	
			// 	for (int i = 0; i < autoCompeleteRight.Length; i++)
			// 	{
			// 		if (key == autoCompeleteRight[i])
			// 		{
			// 			InsertTextAtCursor(autoCompeleteLeft[i]);
			// 			CursorSetColumn(CursorGetColumn() - 1);
			// 		}
			// 	}
			// }

			if (newLength < prevTextLength) //删除内容
			{
				if (CodeHintManager.IsShowPanel()) //提示面板打开
				{
					//关闭面板
					CodeHintManager.OverInput();
				}
			}
			else if (newLength > prevTextLength) //添加内容
			{
				if (CodeHintManager.EnterInput) //是否输入过换行
				{
					Undo();
					CodeHintManager.EnterInput = false;
					newLength -= 1;
				}
				else //触发输入
				{
					//触发输入
					CodeHintManager.TriggerInput(this);
				}
			}

			prevTextLength = newLength;
		}

		private void _on_TextEdit_cursor_changed()
		{
			if (CodeHintManager.IsShowPanel() && (Input.IsKeyPressed((int)KeyList.Up) ||
			    Input.IsKeyPressed((int)KeyList.Down)))
			{
				CursorSetLine(_line);
				CursorSetColumn(_column);
			}
			else
			{
				//记录光标位置
				_line = CursorGetLine();
				_column = CursorGetColumn();
			}
		}
	}
}