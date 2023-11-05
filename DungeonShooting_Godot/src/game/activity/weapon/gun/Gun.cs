using Godot;

/// <summary>
/// 普通的枪
/// </summary>
[Tool]
public partial class Gun : Weapon
{
    protected override void OnFire()
    {
        if (Master == Player.Current)
        {
            //创建抖动
            GameCamera.Main.DirectionalShake(Vector2.Right.Rotated(GlobalRotation) * Attribute.CameraShake);
        }

        //创建开火特效
        if (!string.IsNullOrEmpty(Attribute.FireEffect))
        {
            var packedScene = ResourceManager.Load<PackedScene>(Attribute.FireEffect);
            var sprite = packedScene.Instantiate<AutoDestroySprite>();
            // sprite.GlobalPosition = FirePoint.GlobalPosition;
            // sprite.GlobalRotation = FirePoint.GlobalRotation;
            // sprite.AddToActivityRoot(RoomLayerEnum.YSortLayer);
            sprite.Position = GetLocalFirePosition();
            AddChild(sprite);
        }
    }

    protected override void OnShoot(float fireRotation)
    {
        ShootBullet(fireRotation, Attribute.Bullet);
    }

    // //测试用, 敌人被消灭时触发手上武器开火
    // protected override void OnRemove(Role master)
    // {
    //     base.OnRemove(master);
    //
    //     if (master.IsDie && master.IsEnemyWithPlayer())
    //     {
    //         this.CallDelay(0, () =>
    //         {
    //             Debug.Log("敌人扔掉武器触发攻击");
    //             Trigger(master);
    //         });
    //     }
    // }
}