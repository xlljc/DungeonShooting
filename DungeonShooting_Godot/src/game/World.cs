using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Godot;

/// <summary>
/// 游戏世界
/// </summary>
public partial class World : CanvasModulate, ICoroutine
{
    /// <summary>
    /// 当前的游戏世界对象
    /// </summary>
    public static World Current => GameApplication.Instance.DungeonManager.CurrWorld;
    
    /// <summary>
    /// //对象根节点
    /// </summary>
    public Node2D NormalLayer;
    
    /// <summary>
    /// 对象根节点, 带y轴排序功能
    /// </summary>
    public Node2D YSortLayer;
    
    /// <summary>
    /// 地图根节点
    /// </summary>
    public TileMap TileRoot;

    public Node2D StaticSpriteRoot;
    public Node2D AffiliationAreaRoot;
    public Node2D FogMaskRoot;
    public Node2D NavigationRoot;
    
    /// <summary>
    /// 是否暂停
    /// </summary>
    public bool Pause
    {
        get => _pause;
        set
        {
            if (_pause != value)
            {
                _pause = value;
                if (value)
                {
                    ProcessMode = ProcessModeEnum.WhenPaused;
                }
                else
                {
                    ProcessMode = ProcessModeEnum.Inherit;
                }
            }
        }
    }
    
    /// <summary>
    /// 所有被扔在地上的武器
    /// </summary>
    public HashSet<Weapon> Weapon_UnclaimedWeapons { get; } = new HashSet<Weapon>();
    
    /// <summary>
    /// 记录所有存活的敌人
    /// </summary>
    public List<Enemy> Enemy_InstanceList  { get; } = new List<Enemy>();
    
    /// <summary>
    /// 随机数对象
    /// </summary>
    public SeedRandom Random { get; private set; }
    
    /// <summary>
    /// 随机对象池
    /// </summary>
    public RandomPool RandomPool { get; private set; }
    
    private bool _pause = false;
    private List<CoroutineData> _coroutineList;

    public override void _Ready()
    {
        //TileRoot.YSortEnabled = false;
        NormalLayer = GetNode<Node2D>("TileRoot/NormalLayer");
        YSortLayer = GetNode<Node2D>("TileRoot/YSortLayer");
        TileRoot = GetNode<TileMap>("TileRoot");
        StaticSpriteRoot = GetNode<Node2D>("TileRoot/StaticSpriteRoot");
        FogMaskRoot = GetNode<Node2D>("TileRoot/FogMaskRoot");
        NavigationRoot = GetNode<Node2D>("TileRoot/NavigationRoot");
        AffiliationAreaRoot = GetNode<Node2D>("TileRoot/AffiliationAreaRoot");
    }

    public override void _Process(double delta)
    {
        //协程更新
        ProxyCoroutineHandler.ProxyUpdateCoroutine(ref _coroutineList, (float)delta);
    }

    /// <summary>
    /// 获取指定层级根节点
    /// </summary>
    public Node2D GetRoomLayer(RoomLayerEnum layerEnum)
    {
        switch (layerEnum)
        {
            case RoomLayerEnum.NormalLayer:
                return NormalLayer;
            case RoomLayerEnum.YSortLayer:
                return YSortLayer;
        }

        return null;
    }

    /// <summary>
    /// 通知其他敌人发现目标了
    /// </summary>
    /// <param name="self">发送通知的角色</param>
    /// <param name="target">目标</param>
    public void NotifyEnemyTarget(Role self, ActivityObject target)
    {
        foreach (var role in Enemy_InstanceList)
        {
            if (role != self && !role.IsDestroyed && role.AffiliationArea == self.AffiliationArea)
            {
                //将未发现目标的敌人状态置为惊讶状态
                var controller = role.StateController;
                //延时通知效果
                role.CallDelay(Utils.Random.RandomRangeFloat(0.2f, 1f), () =>
                {
                    if (controller.CurrState == AIStateEnum.AiNormal)
                    {
                        controller.ChangeState(AIStateEnum.AiLeaveFor, target);
                    }
                });
            }
        }
    }
    
    public long StartCoroutine(IEnumerator able)
    {
        return ProxyCoroutineHandler.ProxyStartCoroutine(ref _coroutineList, able);
    }
	
    public void StopCoroutine(long coroutineId)
    {
        ProxyCoroutineHandler.ProxyStopCoroutine(ref _coroutineList, coroutineId);
    }

    public bool IsCoroutineOver(long coroutineId)
    {
        return ProxyCoroutineHandler.ProxyIsCoroutineOver(ref _coroutineList, coroutineId);
    }

    public void StopAllCoroutine()
    {
        ProxyCoroutineHandler.ProxyStopAllCoroutine(ref _coroutineList);
    }

    /// <summary>
    /// 初始化随机池
    /// </summary>
    public void InitRandomPool(SeedRandom random)
    {
        Random = random;
        RandomPool = new  RandomPool(this);
    }
}