using Godot;

namespace DScript.GodotEditor
{
	public class CodePanel : Control
	{
		/// <summary>
		/// 初始缩放
		/// </summary>
		public Vector2 StartScale { get; private set; }

		//缩放面板
		private Control _scalePanel;
		//绘制组件
		private TextEditPainter _editPainter;
		//文本编辑节点
		private CodeTextEditor _codeTextEditor;


		public override void _Ready()
		{
			_codeTextEditor = GetNode<CodeTextEditor>("ScalePanel/TextEdit");
			_editPainter = _codeTextEditor.GetNode<TextEditPainter>("TextEditPainter");

			_scalePanel = GetNode<Control>("ScalePanel");
			StartScale = _scalePanel.RectScale;

			_editPainter.SetIdePanel(this);
			_editPainter.SetTextEdit(_codeTextEditor);

			//刷新大小
			_on_ScalePanel_resized();
			
			//缩放代码提示面板
			CodeHintPanel.Instance.RectScale = StartScale;
		}

		public override void _Process(float delta)
		{
			_editPainter.Update();
		}

		/// <summary>
		/// 连接信号, 当面板调整大小时调用
		/// </summary>
		private void _on_ScalePanel_resized()
		{
			//更新textEditor的缩放
			if (_scalePanel != null)
			{
				_scalePanel.SetSize(RectSize / StartScale);
			}
		}
	}
}
