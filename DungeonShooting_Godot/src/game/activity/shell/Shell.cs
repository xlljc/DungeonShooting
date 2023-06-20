
using Godot;

/// <summary>
/// 弹壳类
/// </summary>
[Tool, GlobalClass]
public partial class Shell : ActivityObject
{
    public override void OnInit()
    {
        ShadowOffset = new Vector2(0, 1);
        ThrowCollisionSize = new Vector2(5, 5);
    }

    protected override void Process(float delta)
    {
        //落地后将弹壳变为静态贴图
        if (!IsThrowing)
        {
            if (AffiliationArea != null)
            {
                BecomesStaticImage();
            }
            else
            {
                GD.Print("弹壳投抛到画布外了, 强制消除...");
                Destroy();
            }
        }
    }
}