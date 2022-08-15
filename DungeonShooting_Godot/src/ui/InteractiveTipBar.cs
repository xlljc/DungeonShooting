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

    /// <summary>
    /// 隐藏互动提示ui
    /// </summary>
    public void HideBar()
    {
        Visible = false;
    }

    /// <summary>
    /// 显示互动提示ui
    /// </summary>
    /// <param name="pos">所在坐标</param>
    /// <param name="icon">显示图标</param>
    /// <param name="message">显示文本</param>
	public void ShowBar(Vector2 pos, string icon, string message)
    {
        GlobalPosition = pos;
        Message.Text = message;
        Icon.Texture = ResourceManager.Load<Texture>(icon);
        Visible = true;
    }
}