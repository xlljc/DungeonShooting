using Godot;

/// <summary>
/// 换弹进度组件
/// </summary>
public class ReloadBar : Node2D
{
    private Sprite slot;
    private Sprite block;

    private int width;
    private float startX;

    public override void _Ready()
    {
        slot = GetNode<Sprite>("Slot");
        block = GetNode<Sprite>("Slot/Block");
        width = slot.Texture.GetWidth();
        startX = -(width - 3) / 2;
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
        GlobalPosition = position;
        progress = Mathf.Clamp(progress, 0, 1);
        block.Position = new Vector2(startX + (width - 3) * progress, 0);
    }
}