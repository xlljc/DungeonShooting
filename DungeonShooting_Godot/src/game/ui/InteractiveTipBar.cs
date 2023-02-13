using Godot;

/// <summary>
/// 互动提示文本
/// </summary>
public partial class InteractiveTipBar : Node2D
{

    private ActivityObject Target;
    
    private Sprite2D Icon;

    private string currImage;

    public override void _Ready()
    {
        Icon = GetNode<Sprite2D>("Icon");
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
	public void ShowBar(ActivityObject target, string icon)
    {
        Target = target;
        GlobalPosition = target.GlobalPosition;
        if (currImage != icon)
        {
            currImage = icon;
            Icon.Texture = ResourceManager.Load<Texture2D>(icon);
        }
        Visible = true;
    }
}