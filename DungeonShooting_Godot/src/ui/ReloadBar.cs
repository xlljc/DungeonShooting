using Godot;

public class ReloadBar : Sprite
{
    private Sprite block;

    public override void _Ready()
    {
        block = GetNode<Sprite>("ReloadBarBlock");
    }

    public void HideBar()
    {
        Visible = false;
    }

    public void ShowBar(Vector2 position)
    {
        Visible = true;
        GlobalPosition = position;
    }

}