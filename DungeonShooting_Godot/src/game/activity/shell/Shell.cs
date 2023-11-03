
using Godot;

/// <summary>
/// 弹壳类
/// </summary>
[Tool]
public partial class Shell : ActivityObject
{
    public override void OnInit()
    {
        ShadowOffset = new Vector2(0, 1);
        ThrowCollisionSize = new Vector2(5, 5);
    }

    protected override void Process(float delta)
    {
        //落地静止后将弹壳变为静态贴图
        if (!IsThrowing && MoveController.IsMotionless())
        {
            if (AffiliationArea != null)
            {
                Freeze();
            }
            else
            {
                Debug.Log("弹壳投抛到画布外了, 强制消除...");
                Destroy();
            }
        }
    }
}