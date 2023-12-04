
using Godot;

[Tool]
public partial class EnemyDead0002 : AutoFreezeObject
{
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
        if (FreezeCount == 0)
        {
            DrawLiquid(_brushData);
        }
        else
        {
            BrushPrevPosition = null;
        }
    }
}