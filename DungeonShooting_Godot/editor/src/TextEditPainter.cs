using System.Collections.Generic;
using Godot;

namespace DScript.GodotEditor
{
	/// <summary>
	/// 负责代码编辑器内的绘制操作
	/// </summary>
	public class TextEditPainter : Node2D
	{
		private Color ErrorLineColor = new Color(1, 0, 0, 0.2f);
		
		//code面板
		private CodePanel _codePanel;
		private TextEdit _textEdit;
		//报错行数
		private List<int> _errorLines = new List<int>();

		/// <summary>
		/// 设置文本编辑器
		/// </summary>
		public void SetTextEdit(CodeTextEdit textEdit)
		{
			_textEdit = textEdit;
		}

		/// <summary>
		/// 设置代码面板
		/// </summary>
		public void SetIdePanel(CodePanel codePanel)
		{
			_codePanel = codePanel;
		}

		/// <summary>
		/// 绘制 TextEdit 中报错的行数
		/// </summary>
		/// <param name="line">报错所在行数</param>
		public void DrawTextEditErrorLine(int line)
		{
			if (!_errorLines.Contains(line))
			{
				_errorLines.Add(line);
				_textEdit.SetLineAsSafe(line, true);
			}
		}
		
		/// <summary>
		/// 取消绘制 TextEdit 中报错的行数
		/// </summary>
		/// <param name="line">报错所在行数</param>
		public void UnDrawTextEditErrorLine(int line)
		{
			_errorLines.Remove(line);
			_textEdit.SetLineAsSafe(line, false);
		}

		public Vector2 ToPainterPosition(Vector2 v)
		{
			if (_codePanel != null)
			{
				return v * _codePanel.StartScale;
			}

			return v;
		}
		
		public override void _Draw()
		{
			if (_textEdit == null) return;

			//绘制报错的行
			if (_errorLines.Count > 0)
			{
				var lineHeight = _textEdit.GetLineHeight();
				var width = _textEdit.RectSize.x - _textEdit.MinimapWidth;
				for (int i = 0; i < _errorLines.Count; i++)
				{
					var pos = _textEdit.GetPosAtLineColumn(_errorLines[i], 0);
					if (pos.x > -1 && pos.y > -1)
					{
						DrawRect(new Rect2(0, pos.y - lineHeight, width, lineHeight), ErrorLineColor);
					}
				}
			}
		}
	}
}