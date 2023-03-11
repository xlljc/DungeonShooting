using Godot;

/// <summary>
/// 换弹进度组件
/// </summary>
public partial class ReloadBar : Node2D
{
    private Sprite2D slot;
    private Sprite2D block;

    private int width;
    private float startX;

    public override void _Ready()
    {
        slot = GetNode<Sprite2D>("Slot");
        block = GetNode<Sprite2D>("Slot/Block");
        width = slot.Texture.GetWidth();
        startX = -(width - 3) / 2f;
    }

    /// <summary>
    /// 隐藏换弹进度组件
    /// </summary>
    public void HideBar()
    {
        Visible = false;
    }

    /// <summary>
    /// 显示换弹进度组件
    /// </summary>
    /// <param name="position">坐标</param>
    /// <param name="progress">进度, 0 - 1</param>
    public void ShowBar(Vector2 position, float progress)
    {
        Visible = true;
        GlobalPosition = position.Round();
        progress = Mathf.Clamp(progress, 0, 1);
        block.Position = new Vector2(startX + (width - 3) * progress, 0);
    }
}