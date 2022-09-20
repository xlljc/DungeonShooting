using Godot;

namespace Editor
{
	public class IdePanel : Control
	{
		private Vector2 StartScale;
		private Control ScalePanel;
		
		public override void _Ready()
		{
			ScalePanel = GetNode<Control>("ScalePanel");
			StartScale = ScalePanel.RectScale;
		}

		private void _on_ScalePanel_resized()
		{
			if (ScalePanel != null)
			{
				ScalePanel.SetSize(RectSize / StartScale);
			}
		}
	}
}
