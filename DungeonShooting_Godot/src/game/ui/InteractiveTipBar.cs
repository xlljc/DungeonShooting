using Godot;

/// <summary>
/// 互动提示文本
/// </summary>
public class InteractiveTipBar : Node2D
{

    private ActivityObject Target;
    
    private Label Message;
    private Sprite Icon;
    private Sprite Bg;

    private string currImage;

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
    /// <param name="target">所在坐标</param>
    /// <param name="icon">显示图标</param>
    /// <param name="message">显示文本</param>
	public void ShowBar(ActivityObject target, string icon, string message)
    {
        Target = target;
        GlobalPosition = GameApplication.Instance.ViewToGlobalPosition(target.GlobalPosition);
        Message.Text = message;
        if (currImage != icon)
        {
            currImage = icon;
            Icon.Texture = ResourceManager.Load<Texture>(icon);
        }
        Visible = true;
    }

    public override void _Process(float delta)
    {
        if (Visible)
        {
            var pos = GameApplication.Instance.ViewToGlobalPosition(Target.GlobalPosition);
            GlobalPosition = pos.Round();

        }
    }

    public override void _Draw()
    {
        DrawString(GameApplication.Instance.Font, new Vector2(0, 20), GlobalPosition.ToString(), Colors.Red);
        GD.Print("draw...");
    }
}