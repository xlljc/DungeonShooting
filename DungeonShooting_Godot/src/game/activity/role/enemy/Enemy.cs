
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

    //锁定目标时间
    private float _lockTargetTime = 0;

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
}