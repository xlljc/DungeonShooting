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
    /// 敌人身上的状态机控制器
    /// </summary>
    public StateController<Enemy, AIStateEnum> StateController { get; }
    
    /// <summary>
    /// 视野半径, 单位像素
    /// </summary>
    public float ViewRange { get; set; } = 200;

    /// <summary>
    /// 背后的视野半径, 单位像素
    /// </summary>
    public float BackViewRange { get; set; } = 50;
    
    /// <summary>
    /// 视野检测射线, 朝玩家打射线, 检测是否碰到墙
    /// </summary>
    public RayCast2D ViewRay { get; }
    
    //------------------- 寻路相关 ---------------------------

    /// <summary>
    /// 移动目标标记
    /// </summary>
    public PathSign PathSign { get; }

    /// <summary>
    /// 寻路标记线段总长度
    /// </summary>
    public float PathSignLength { get; set; } = 500;

    //-------------------------------------------------------

    private Area2D _marginArea;
    
    public Enemy() : base(ResourcePath.prefab_role_Enemy_tscn)
    {
        StateController = new StateController<Enemy, AIStateEnum>();
        AddComponent(StateController);
        
        AttackLayer = PhysicsLayer.Wall | PhysicsLayer.Props | PhysicsLayer.Player;
        Camp = CampEnum.Camp2;

        MoveSpeed = 20;
        
        //视野射线
        ViewRay = GetNode<RayCast2D>("ViewRay");
        _marginArea = GetNode<Area2D>("MarginArea");
        
        PathSign = new PathSign(this, PathSignLength, GameApplication.Instance.Room.Player);
        
        //注册Ai状态机
        StateController.Register(new AINormalState());
        StateController.Register(new AIProbeState());
        StateController.Register(new AITailAfterState());
    }

    public override void _Ready()
    {
        base._Ready();
        //默认状态
        StateController.ChangeState(AIStateEnum.AINormal);
        
        _marginArea.Connect("body_shape_entered", this, nameof(OnObjectEnter));
    }

    public void OnObjectEnter(RID id, Node node, int shapeIndex, int localShapeIndex)
    {
        if (node is TileMap tileMap)
        {
            // var tileGetShape = tileMap.TileSet.TileGetShapeTransform(shapeIndex, localShapeIndex).;
            // GD.Print("enter: ", tileGetShape.GetType().FullName);
        }
    }
    
    public override void _Process(float delta)
    {
        base._Process(delta);
        if (GameApplication.Instance.Debug)
        {
            PathSign.Scale = new Vector2((int)Face, 1);
            Update();
        }
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);
        
        // var player = GameApplication.Instance.Room.Player;
        // //玩家中心点坐标
        // var playerPos = player.MountPoint.GlobalPosition;
        //
        // //玩家是否在前方
        // var isForward = IsPositionInForward(playerPos);
        //
        // if (isForward) //脸朝向玩家
        // {
        //     // if (GlobalPosition.DistanceSquaredTo(playerPos) <= ViewRange * ViewRange) //没有超出视野半径
        //     // {
        //     //     //射线检测墙体
        //     //     ViewRay.Enabled = true;
        //     //     var localPos = ViewRay.ToLocal(playerPos);
        //     //     ViewRay.CastTo = localPos;
        //     //     ViewRay.ForceRaycastUpdate();
        //     //
        //     //     if (ViewRay.IsColliding()) //在视野范围内, 但是碰到墙壁
        //     //     {
        //     //         LookTarget = null;
        //     //         StateController.ChangeState(StateEnum.Idle);
        //     //     }
        //     //     else //视野无阻
        //     //     {
        //     //         LookTarget = player;
        //     //         StateController.ChangeState(StateEnum.Run);
        //     //     }
        //     //     
        //     //     ViewRay.Enabled = false;
        //     // }
        //     // else //超出视野半径
        //     // {
        //     //     LookTarget = null;
        //     //     StateController.ChangeState(StateEnum.Idle);
        //     // }
        //     // //_prevPlayerPos = playerPos;
        // }
    }

    public override void _Draw()
    {
        if (GameApplication.Instance.Debug)
        {
            if (PathSign != null)
            {
                DrawLine(Vector2.Zero,ToLocal(PathSign.GlobalPosition), Colors.Red);
            }
        }
    }
}
