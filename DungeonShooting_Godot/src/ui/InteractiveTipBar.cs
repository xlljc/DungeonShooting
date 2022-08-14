using Godot;

public class InteractiveTipBar : Node2D
{

    private Label Message;
    private Sprite Icon;
    private Sprite Bg;

    public override void _Ready()
    {
        Message = GetNode<Label>("Message");
        Icon = GetNode<Sprite>("Icon");
        Bg = GetNode<Sprite>("Bg");
    }

    public void HideBar()
    {
        Visible = false;
    }

	public void ShowBar(Vector2 pos, string icon, string message)
    {
        GlobalPosition = pos;
        Message.Text = message;
        Icon.Texture = ResourceManager.Load<Texture>(icon);
        Visible = true;
    }
}