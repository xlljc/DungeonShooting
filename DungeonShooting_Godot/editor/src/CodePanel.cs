using Godot;

namespace Editor
{
	public class CodePanel : Control
	{
		/// <summary>
		/// 初始缩放
		/// </summary>
		public Vector2 StartScale { get; private set; }
		
		private Control ScalePanel;
		private TextEditPainter _editPainter;
		
		private CodeTextEditor _codeTextEditor;
		
		
		public override void _Ready()
		{
			_codeTextEditor = GetNode<CodeTextEditor>("ScalePanel/TextEdit");
			_editPainter = _codeTextEditor.GetNode<TextEditPainter>("TextEditPainter");
			
			ScalePanel = GetNode<Control>("ScalePanel");
			StartScale = ScalePanel.RectScale;
			
			_editPainter.SetIdePanel(this);
			_editPainter.SetTextEdit(_codeTextEditor);
			
			_on_ScalePanel_resized();
		}

		public override void _Process(float delta)
		{
			_editPainter.Update();
		}
		
		private void _on_ScalePanel_resized()
		{
			//更新textEditor的缩放
			if (ScalePanel != null)
			{
				ScalePanel.SetSize(RectSize / StartScale);
			}
		}
	}
}
