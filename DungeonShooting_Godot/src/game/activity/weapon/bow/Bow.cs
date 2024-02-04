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
    private Bullet _activeArrow;

    private void InitActiveArrow()
    {
        if (_activeArrow == null)
        {
            _activeArrow = (Bullet)FireManager.ShootBullet(this, 0, Attribute.Bullet);
            _activeArrow.Pickup();
            _activeArrow.MoveController.Enable = false;
            _activeArrow.CollisionArea.Monitoring = false;
            _activeArrow.Collision.Disabled = true;
            _activeArrow.Position = Vector2.Zero;
            ArrowPoint.AddChild(_activeArrow);
        }
    }

    protected override void OnBeginCharge()
    {
        //拉弓开始蓄力
        AnimationPlayer.Play(AnimatorNames.Pull);
    }

    protected override void OnChargeProcess(float delta, float charge)
    {
        if (Master.IsPlayer() && IsChargeFinish())
        {
            //蓄力完成抖动屏幕
            GameCamera.Main.Shake(new Vector2(Utils.Random.RandomRangeFloat(-1, 1), Utils.Random.RandomRangeFloat(-1, 1)));
        }
    }

    protected override void OnEndCharge()
    {
        //结束蓄力
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

    protected override void OnPickUp(Role master)
    {
        base.OnPickUp(master);
        InitActiveArrow();
    }

    protected override void OnRemove(Role master)
    {
        base.OnRemove(master);
        if (_activeArrow != null)
        {
            _activeArrow.DoReclaim();
            _activeArrow = null;
        }
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