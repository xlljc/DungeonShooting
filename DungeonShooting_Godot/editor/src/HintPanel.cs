using Godot;

public class HintPanel : Popup
{
	public override void _Ready()
	{
		CallDeferred("popup");
		//Popup_();
	}
}