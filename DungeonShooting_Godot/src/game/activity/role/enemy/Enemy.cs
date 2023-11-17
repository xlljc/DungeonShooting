
using System;
using Config;
using Godot;
using NnormalState;

/// <summary>
/// 基础敌人
/// </summary>
[Tool]
public partial class Enemy : Role
{
    /// <summary>
    /// 目标是否在视野内
    /// </summary>
    public bool TargetInView { get; set; } = true;
    
    /// <summary>
    /// 敌人身上的状态机控制器
    /// </summary>
    public StateController<Enemy, AINormalStateEnum> StateController { get; private set; }

    /// <summary>
    /// 视野半径, 单位像素, 发现玩家后改视野范围可以穿墙
    /// </summary>
    public float ViewRange { get; set; } = 250;

    /// <summary>
    /// 发现玩家后的视野半径
    /// </summary>
    public float TailAfterViewRange { get; set; } = 400;

    /// <summary>
    /// 背后的视野半径, 单位像素
    /// </summary>
    public float BackViewRange { get; set; } = 50;

    /// <summary>
    /// 攻击范围
    /// </summary>
    public float AttackRange { get; set; } = 200;

    /// <summary>
    /// 视野检测射线, 朝玩家打射线, 检测是否碰到墙
    /// </summary>
    [Export, ExportFillNode]
    public RayCast2D ViewRay { get; private set; }

    /// <summary>
    /// 导航代理
    /// </summary>
    [Export, ExportFillNode]
    public NavigationAgent2D NavigationAgent2D { get; private set; }

    /// <summary>
    /// 导航代理中点
    /// </summary>
    [Export, ExportFillNode]
    public Marker2D NavigationPoint { get; private set; }
    
    /// <summary>
    /// 开火位置
    /// </summary>
    [Export, ExportFillNode]
    public Marker2D FirePoint { get; private set; }

    /// <summary>
    /// 攻击时间间隔
    /// </summary>
    public float AttackInterval { get; set; } = 5;
    
    /// <summary>
    /// 当前敌人所看向的对象, 也就是枪口指向的对象
    /// </summary>
    public ActivityObject LookTarget { get; set; }
    
    /// <summary>
    /// Ai 攻击属性
    /// </summary>
    public ExcelConfig.AiAttackAttr AttackAttribute { get; private set; }
    
    //锁定目标时间
    private float _lockTargetTime = 0;
    //攻击冷却计时器
    private float _attackTimer = 0;

    public override void OnInit()
    {
        base.OnInit();
        AttackAttribute = ExcelConfig.AiAttackAttr_Map["0001"];
        IsAi = true;
        StateController = AddComponent<StateController<Enemy, AINormalStateEnum>>();

        AttackLayer = PhysicsLayer.Wall | PhysicsLayer.Player;
        EnemyLayer = PhysicsLayer.Player;
        Camp = CampEnum.Camp2;

        RoleState.MoveSpeed = 20;

        MaxHp = 20;
        Hp = 20;
        
        StateController.Register(new AiNormalState());
        StateController.Register(new AiTailAfterState());
        StateController.Register(new AiFollowUpState());
        StateController.Register(new AiSurroundState());
        StateController.Register(new AiLeaveForState());
        StateController.Register(new AiAttackState());
        StateController.ChangeState(AINormalStateEnum.AiNormal);
    }

    public override void EnterTree()
    {
        if (!World.Enemy_InstanceList.Contains(this))
        {
            World.Enemy_InstanceList.Add(this);
        }
    }

    public override void ExitTree()
    {
        World.Enemy_InstanceList.Remove(this);
    }

    public override void Attack()
    {
        _attackTimer = AttackInterval;
        OnAttack();
    }

    /// <summary>
    /// 敌人发动攻击
    /// </summary>
    protected virtual void OnAttack()
    {
        Debug.Log("触发攻击");
        var bulletData = FireManager.GetBulletData(this, ConvertRotation(Position.AngleTo(LookPosition)), ExcelConfig.BulletBase_Map["0006"]);
        FireManager.ShootBullet(bulletData, AttackLayer);
    }
    
    protected override void Process(float delta)
    {
        base.Process(delta);
        if (IsDie)
        {
            return;
        }
        
        //看向目标
        if (LookTarget != null)
        {
            var pos = LookTarget.Position;
            LookPosition = pos;
            //脸的朝向
            var gPos = Position;
            if (pos.X > gPos.X && Face == FaceDirection.Left)
            {
                Face = FaceDirection.Right;
            }
            else if (pos.X < gPos.X && Face == FaceDirection.Right)
            {
                Face = FaceDirection.Left;
            }
        }

        if (_attackTimer > 0)
        {
            _attackTimer -= delta;
        }
        //目标在视野内的时间
        var currState = StateController.CurrState;
        if (currState == AINormalStateEnum.AiAttack && _attackTimer <= 0) //必须在可以开火时记录时间
        {
            _lockTargetTime += delta;
        }
        else
        {
            _lockTargetTime = 0;
        }
    }

    protected override void OnHit(ActivityObject target, int damage, float angle, bool realHarm)
    {
        //受到伤害
        var state = StateController.CurrState;
        // if (state == AINormalStateEnum.AiNormal)
        // {
        //     StateController.ChangeState(AINormalStateEnum.AiLeaveFor, target);
        // }
        // else if (state == AINormalStateEnum.AiLeaveFor)
        // {
        //
        // }
    }

    protected override void OnDie()
    {
        var effPos = Position + new Vector2(0, -Altitude);
        //血液特效
        var blood = ObjectManager.GetPoolItem<AutoDestroyParticles>(ResourcePath.prefab_effect_enemy_EnemyBloodEffect_tscn);
        blood.Position = effPos - new Vector2(0, 12);
        blood.AddToActivityRoot(RoomLayerEnum.NormalLayer);
        blood.PlayEffect();

        //创建敌人碎片
        var count = Utils.Random.RandomRangeInt(3, 6);
        for (var i = 0; i < count; i++)
        {
            var debris = Create(Ids.Id_effect0001);
            debris.PutDown(effPos, RoomLayerEnum.NormalLayer);
            debris.InheritVelocity(this);
        }
        
        //派发敌人死亡信号
        EventManager.EmitEvent(EventEnum.OnEnemyDie, this);
        Destroy();
    }
    
    /// <summary>
    /// 返回目标点是否在视野范围内
    /// </summary>
    public bool IsInViewRange(Vector2 target)
    {
        var isForward = IsPositionInForward(target);
        if (isForward)
        {
            if (GlobalPosition.DistanceSquaredTo(target) <= ViewRange * ViewRange) //没有超出视野半径
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 返回目标点是否在跟随状态下的视野半径内
    /// </summary>
    public bool IsInTailAfterViewRange(Vector2 target)
    {
        var isForward = IsPositionInForward(target);
        if (isForward)
        {
            if (GlobalPosition.DistanceSquaredTo(target) <= TailAfterViewRange * TailAfterViewRange) //没有超出视野半径
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 调用视野检测, 如果被墙壁和其它物体遮挡, 则返回被挡住视野的物体对象, 视野无阻则返回 null
    /// </summary>
    public bool TestViewRayCast(Vector2 target)
    {
        ViewRay.Enabled = true;
        ViewRay.TargetPosition = ViewRay.ToLocal(target);
        ViewRay.ForceRaycastUpdate();
        return ViewRay.IsColliding();
    }

    /// <summary>
    /// 调用视野检测完毕后, 需要调用 TestViewRayCastOver() 来关闭视野检测射线
    /// </summary>
    public void TestViewRayCastOver()
    {
        ViewRay.Enabled = false;
    }
    
    /// <summary>
    /// 获取锁定目标的时间
    /// </summary>
    public float GetLockTime()
    {
        return _lockTargetTime;
    }

    /// <summary>
    /// 获取锁定目标的剩余时间
    /// </summary>
    public float GetLockRemainderTime()
    {
        return AttackAttribute.LockingTime - _lockTargetTime;
    }
    
    /// <summary>
    /// 强制设置锁定目标时间
    /// </summary>
    public void SetLockTargetTime(float time)
    {
        _lockTargetTime = time;
    }
    
    /// <summary>
    /// 获取攻击范围
    /// </summary>
    public float GetAttackRange()
    {
        return AttackRange;
    }

    public override float GetFirePointAltitude()
    {
        return -AnimatedSprite.Position.Y - FirePoint.Position.Y;
    }

    public override void LookTargetPosition(Vector2 pos)
    {
        LookTarget = null;
        base.LookTargetPosition(pos);
    }
    
    /// <summary>
    /// 获取攻击冷却计时时间, 小于等于 0 表示冷却完成
    /// </summary>
    public float GetAttackTimer()
    {
        return _attackTimer;
    }
    
    /// <summary>
    /// 执行移动操作
    /// </summary>
    public void DoMove()
    {
        AnimatedSprite.Play(AnimatorNames.Run);
        //计算移动
        var nextPos = NavigationAgent2D.GetNextPathPosition();
        BasisVelocity = (nextPos - Position - NavigationPoint.Position).Normalized() * RoleState.MoveSpeed;
    }

    /// <summary>
    /// 执行站立操作
    /// </summary>
    public void DoIdle()
    {
        AnimatedSprite.Play(AnimatorNames.Idle);
        BasisVelocity = Vector2.Zero;
    }
}