using Godot;

public class HintPanel : Control
{
	public override void _Ready()
	{
		var pop = GetNode<Popup>("Popup");
		pop.Popup_();
	}
}