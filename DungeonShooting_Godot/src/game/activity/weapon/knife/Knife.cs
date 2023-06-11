
using Godot;

[Tool, GlobalClass]
public partial class Knife : Weapon
{
    
    private Area2D _hitArea;
    private int _attackIndex = 0;
    
    public override void OnInit()
    {
        base.OnInit();
        
        _hitArea = GetNode<Area2D>("HitArea");
        _hitArea.Monitoring = false;
        _hitArea.Monitorable = false;
        _hitArea.BodyEntered += OnBodyEntered;
        //禁用自动播放动画
        IsAutoPlaySpriteFrames = false;
    }

    protected override void Process(float delta)
    {
        base.Process(delta);
        if (IsActive)
        {
            //让碰撞节点与武器挂载节点位置保持一致, 而不跟着武器走
            _hitArea.GlobalPosition = Master.MountPoint.GlobalPosition;
        }
    }

    protected override void PhysicsProcess(float delta)
    {
        base.PhysicsProcess(delta);
        //过去两个物理帧后就能关闭碰撞了
        if (++_attackIndex >= 2)
        {
            _hitArea.Monitoring = false;
        }
    }

    protected override void OnStartCharge()
    {
        //开始蓄力时武器角度上抬120度
        RotationDegrees = -120;
    }

    protected override void OnFire()
    {
        GD.Print("近战武器攻击! 蓄力时长: " + GetTriggerChargeTime() + ", 扳机按下时长: " + GetTriggerDownTime());
        //更新碰撞层级
        _hitArea.CollisionMask = GetAttackLayer();
        //启用碰撞
        _hitArea.Monitoring = true;
        _attackIndex = 0;

        if (IsActive) //被使用
        {
            //播放挥刀特效
            SpecialEffectManager.Play(
                ResourcePath.resource_spriteFrames_KnifeHit1_tres, "default",
                Master.MountPoint.GlobalPosition, GlobalRotation + Mathf.Pi * 0.5f, new Vector2((int)Master.Face, 1) * AnimatedSprite.Scale,
                new Vector2(17, 4), 1
            );
        }


        if (Master == Player.Current)
        {
            //创建抖动
            //GameCamera.Main.ProcessDirectionalShake(Vector2.Right.Rotated(GlobalRotation - Mathf.Pi * 0.5f) * 1.5f);
        }
    }

    protected override void OnShoot(float fireRotation)
    {
        
    }

    protected override int UseAmmoCount()
    {
        //这里要做判断, 如果没有碰到敌人, 则不消耗弹药 (耐久)
        return 0;
    }

    private void OnBodyEntered(Node2D body)
    {
        GD.Print("碰到物体: " + body.Name);
        var activityObject = body.AsActivityObject();
        if (activityObject != null)
        {
            if (activityObject is Role role)
            {
                role.CallDeferred(nameof(Role.Hurt), 10, (role.GetCenterPosition() - GlobalPosition).Angle());
            }
        }
    }
}
