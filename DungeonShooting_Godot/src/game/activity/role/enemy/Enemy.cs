
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
    public StateController<Enemy, AiStateEnum> StateController { get; private set; }

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
    /// Ai攻击状态, 调用 Attack() 函数后会刷新
    /// </summary>
    public AiAttackState AttackState { get; private set; }

    /// <summary>
    /// 攻击时间间隔
    /// </summary>
    public float AttackInterval { get; set; } = 3;

    /// <summary>
    /// 锁定目标时间
    /// </summary>
    public float LockingTime { get; set; } = 2;
    
    //锁定目标时间
    private float _lockTargetTime = 0;
    //攻击冷却计时器
    private float _attackTimer = 0;

    public override void OnInit()
    {
        base.OnInit();
        IsAi = true;
        StateController = AddComponent<StateController<Enemy, AiStateEnum>>();

        AttackLayer = PhysicsLayer.Wall | PhysicsLayer.Player;
        EnemyLayer = PhysicsLayer.Player;
        Camp = CampEnum.Camp2;

        RoleState.MoveSpeed = 20;

        MaxHp = 20;
        Hp = 20;
        
        StateController.Register(new AiNormalState());
        StateController.Register(new AiTailAfterState());
        StateController.Register(new AiFollowUpState());
        StateController.ChangeState(AiStateEnum.AiNormal);
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
        if (_attackTimer > 0) //开火间隙
        {
            AttackState = AiAttackState.AttackInterval;
        }
        else if (GetLockRemainderTime() > 0) //锁定目标时间
        {
            AttackState = AiAttackState.LockingTime;
        }
        else //正常攻击
        {
            AttackState = AiAttackState.Attack;
            _attackTimer = AttackInterval;
            EnemyAttack();
        }
    }

    /// <summary>
    /// 敌人发动攻击
    /// </summary>
    public virtual void EnemyAttack()
    {
        Debug.Log("触发攻击");
        FireManager.ShootBullet(this, ConvertRotation(0), ExcelConfig.BulletBase_List[0]);
    }

    protected override void Process(float delta)
    {
        base.Process(delta);
        if (IsDie)
        {
            return;
        }

        if (_attackTimer > 0)
        {
            _attackTimer -= delta;
        }
        //目标在视野内的时间
        var currState = StateController.CurrState;
        if (currState == AiStateEnum.AiSurround || currState == AiStateEnum.AiFollowUp)
        {
            if (_attackTimer <= 0) //必须在可以开火时记录时间
            {
                _lockTargetTime += delta;
            }
            else
            {
                _lockTargetTime = 0;
            }
        }
        else
        {
            _lockTargetTime = 0;
        }
    }

    protected override void OnHit(int damage, bool realHarm)
    {
        //受到伤害
        var state = StateController.CurrState;
        if (state == AiStateEnum.AiNormal || state == AiStateEnum.AiLeaveFor) //|| state == AiStateEnum.AiProbe
        {
            StateController.ChangeState(AiStateEnum.AiTailAfter);
        }
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
    /// 检查是否能切换到 AiStateEnum.AiLeaveFor 状态
    /// </summary>
    public bool CanChangeLeaveFor()
    {
        if (!World.Enemy_IsFindTarget)
        {
            return false;
        }

        var currState = StateController.CurrState;
        if (currState == AiStateEnum.AiNormal)// || currState == AiStateEnum.AiProbe)
        {
            //判断是否在同一个房间内
            return World.Enemy_FindTargetAffiliationSet.Contains(AffiliationArea);
        }
        
        return false;
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
        return LockingTime - _lockTargetTime;
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
    /// <param name="weight">从最小到最大距离的过渡量, 0 - 1, 默认 0.5</param>
    public float GetAttackRange(float weight = 0.5f)
    {
        return 200;
    }

    public override float GetFirePointAltitude()
    {
        return -AnimatedSprite.Position.Y - FirePoint.Position.Y;
    }
}