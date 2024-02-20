using Godot;

/// <summary>
/// 弓箭
/// </summary>
[Tool]
public partial class Bow : Weapon
{
    /// <summary>
    /// 弓箭挂载点
    /// </summary>
    [Export, ExportFillNode]
    public Marker2D ArrowPoint { get; set; }

    //正在使用的弓箭
    private Arrow _activeArrow;

    public override void OnInit()
    {
        base.OnInit();
        _activeArrow = (Arrow)FireManager.ShootBullet(this, 0, Attribute.Bullet);
        _activeArrow.Pickup();
        _activeArrow.MoveController.Enable = false;
        _activeArrow.CollisionArea.Monitoring = false;
        _activeArrow.Collision.Disabled = true;
        _activeArrow.Position = Vector2.Zero;
        ArrowPoint.AddChild(_activeArrow);
    }

    protected override void Process(float delta)
    {
        base.Process(delta);
        _activeArrow.ShadowOffset = ShadowOffset + new Vector2(0, Altitude);
        _activeArrow.Visible = !IsTotalAmmoEmpty();
    }

    protected override void OnPickUp(Role master)
    {
        _activeArrow.RefreshBulletColor(master.IsEnemyWithPlayer());
    }

    protected override void OnRemove(Role master)
    {
        _activeArrow.RefreshBulletColor(false);
    }

    protected override void OnBeginCharge()
    {
        //拉弓开始蓄力
        AnimationPlayer.Play(AnimatorNames.Pull);
    }

    protected override void OnChargeFinish()
    {
        //蓄力完成
        SetShake(0.7f);
    }

    protected override void OnEndCharge()
    {
        //结束蓄力
        SetShake(0);
        AnimationPlayer.Play(AnimatorNames.Reset);
    }

    protected override void OnFire()
    {
        if (Master.IsPlayer())
        {
            //创建抖动
            GameCamera.Main.DirectionalShake(Vector2.Right.Rotated(GlobalRotation) * Attribute.CameraShake);
        }
    }

    protected override void OnShoot(float fireRotation)
    {
        FireManager.ShootBullet(this, fireRotation, Attribute.Bullet);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (_activeArrow != null)
        {
            _activeArrow.Destroy();
            _activeArrow = null;
        }
    }
}