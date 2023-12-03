
using Godot;

[Tool]
public partial class EnemyDead0002 : AutoFreezeObject
{
    /// <summary>
    /// 上一帧笔刷坐标
    /// </summary>
    public Vector2I? PrevPosition = null;
    private BrushImageData _brushData;

    public override void OnInit()
    {
        base.OnInit();
        _brushData = LiquidBrushManager.GetBrush("0002");
    }

    protected override void Process(float delta)
    {
        base.Process(delta);
        
        //测试笔刷
        if (FreezeCount == 0 && AffiliationArea != null)
        {
            var pos = AffiliationArea.RoomInfo.LiquidCanvas.ToLiquidCanvasPosition(Position);
            AffiliationArea.RoomInfo.LiquidCanvas.DrawBrush(_brushData, PrevPosition, pos, 0);
            PrevPosition = pos;
        }
    }
}