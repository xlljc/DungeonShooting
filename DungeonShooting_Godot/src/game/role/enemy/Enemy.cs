#region 基础敌人设计思路
/*
敌人有三种状态: 
状态1: 未发现玩家, 视野不可穿墙, 该状态下敌人移动比较规律, 移动速度较慢, 一旦玩家进入视野或者听到玩家枪声, 立刻切换至状态3, 该房间的敌人不能再回到状态1
状态2: 发现有玩家, 但不知道在哪, 视野不可穿墙, 该情况下敌人移动速度明显加快, 移动不规律, 一旦玩家进入视野或者听到玩家枪声, 立刻切换至状态3
状态3: 明确知道玩家的位置, 视野允许穿墙, 移动速度与状态2一致, 进入该状态时, 敌人之间会相互告知玩家所在位置, 并朝着玩家位置开火,
       如果有墙格挡, 则有一定概率继续开火, 一旦玩家立刻敌人视野超哥一段时间, 敌人自动切换为状态2

敌人状态1只存在于少数房间内, 比如特殊房间, 大部分情况下敌人应该是状态2, 或者玩家进入房间时就被敌人发现
*/
#endregion


using Godot;

/// <summary>
/// 基础敌人
/// </summary>
public class Enemy : Role
{
    
    /// <summary>
    /// 公共属性, 是否找到玩家, 如果找到玩家, 则所有敌人都会知道玩家的位置
    /// </summary>
    public static bool IsFindPlayer { get; set; }
    
    /// <summary>
    /// 敌人身上的状态机控制器
    /// </summary>
    public StateController<Enemy, AIStateEnum> StateController { get; }

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
    public RayCast2D ViewRay { get; }
    
    /// <summary>
    /// 导航代理
    /// </summary>
    public NavigationAgent2D NavigationAgent2D { get; }

    /// <summary>
    /// 导航代理中点
    /// </summary>
    public Position2D NavigationPoint { get; }

    private float _enemyAttackTimer = 0;
    
    public Enemy() : base(ResourcePath.prefab_role_Enemy_tscn)
    {
        StateController = new StateController<Enemy, AIStateEnum>();
        AddComponent(StateController);

        AttackLayer = PhysicsLayer.Wall | PhysicsLayer.Props | PhysicsLayer.Player;
        Camp = CampEnum.Camp2;

        MoveSpeed = 30;

        Holster.SlotList[2].Enable = true;
        Holster.SlotList[3].Enable = true;

        //视野射线
        ViewRay = GetNode<RayCast2D>("ViewRay");
        NavigationPoint = GetNode<Position2D>("NavigationPoint");
        NavigationAgent2D = NavigationPoint.GetNode<NavigationAgent2D>("NavigationAgent2D");

        //PathSign = new PathSign(this, PathSignLength, GameApplication.Instance.Room.Player);

        //注册Ai状态机
        StateController.Register(new AiNormalState());
        StateController.Register(new AiProbeState());
        StateController.Register(new AiTailAfterState());
    }

    public override void _Ready()
    {
        base._Ready();
        //默认状态
        StateController.ChangeState(AIStateEnum.AINormal);

        NavigationAgent2D.SetTargetLocation(GameApplication.Instance.Room.Player.GlobalPosition);
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);

        _enemyAttackTimer -= delta;
    }

    /// <summary>
    /// Ai触发的攻击
    /// </summary>
    public void EnemyAttack()
    {
        var weapon = Holster.ActiveWeapon;
        if (weapon != null)
        {
            if (weapon.Attribute.ContinuousShoot) //连发
            {
                Attack();
            }
            else //单发
            {
                if (_enemyAttackTimer <= 0)
                {
                    _enemyAttackTimer = 60f / weapon.Attribute.StartFiringSpeed;
                    Attack();
                }
            }
        }
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
    /// 调用视野检测, 如果被墙壁和其它物体遮挡, 则返回被挡住视野的物体对象, 视野无阻则返回 null
    /// </summary>
    public bool TestViewRayCast(Vector2 target)
    {
        ViewRay.Enabled = true;
        ViewRay.CastTo = ViewRay.ToLocal(target);
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
}
