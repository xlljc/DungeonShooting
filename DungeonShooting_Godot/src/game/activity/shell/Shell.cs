
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
    
    protected override void OnThrowOver()
    {
        if (AffiliationArea != null)
        {
            Freeze();
            // var proxySprite = ObjectManager.GetProxySprite();
            // proxySprite.SetTexture(AffiliationArea.SpriteRoot, this);
            // this.CallDelay(0.1f, Destroy);
        }
    }

    // protected override void Process(float delta)
    // {
    //     //落地后将弹壳变为静态贴图
    //     if (!IsThrowing)
    //     {
    //         if (AffiliationArea != null)
    //         {
    //             BecomesStaticImage();
    //         }
    //         else
    //         {
    //             Debug.Log("弹壳投抛到画布外了, 强制消除...");
    //             Destroy();
    //         }
    //     }
    // }
}