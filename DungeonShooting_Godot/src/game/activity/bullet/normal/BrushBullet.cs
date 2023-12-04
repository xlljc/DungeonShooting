
using Godot;

/// <summary>
/// 带笔刷的子弹
/// </summary>
[Tool]
public partial class BrushBullet : Bullet
{
    /// <summary>
    /// 笔刷 id
    /// </summary>
    [Export]
    public string BrushId { get; set; }

    /// <summary>
    /// 生效高度, 当物体离地高度小于这个值时生效, 当值小于 0 时一直生效
    /// </summary>
    [Export]
    public float EffectiveAltitude { get; set; } = -1;

    private BrushImageData _brushData;

    public override void OnInit()
    {
        base.OnInit();
        _brushData = LiquidBrushManager.GetBrush(BrushId);
    }

    protected override void Process(float delta)
    {
        base.Process(delta);
        //测试笔刷
        if ((EffectiveAltitude < 0 || Altitude <= EffectiveAltitude))
        {
            DrawLiquid(_brushData);
        }
        else
        {
            BrushPrevPosition = null;
        }
    }

    public override void OnLeavePool()
    {
        base.OnLeavePool();
        BrushPrevPosition = null;
    }
}