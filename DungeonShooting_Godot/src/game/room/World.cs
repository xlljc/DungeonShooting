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
    public static World Current => GameApplication.Instance.World;
    
    /// <summary>
    /// //对象根节点
    /// </summary>
    [Export] public Node2D NormalLayer;
    
    /// <summary>
    /// 对象根节点, 带y轴排序功能
    /// </summary>
    [Export] public Node2D YSortLayer;
    
    /// <summary>
    /// 地图根节点
    /// </summary>
    [Export] public TileMap TileRoot;

    [Export] public Node2D StaticSpriteRoot;
    [Export] public Node2D AffiliationAreaRoot;
    [Export] public Node2D FogMaskRoot;
    [Export] public Node2D NavigationRoot;
    
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
    
    private bool _pause = false;
    private List<CoroutineData> _coroutineList;

    public override void _Ready()
    {
        Color = Colors.Black;
        
        //临时处理, 加载TileSet
        var tileSet = GameApplication.Instance.TileSetConfig.First().Value.GetTileSet();
        TileRoot.TileSet = tileSet;
        //TileRoot.YSortEnabled = false;
    }

    public override void _Process(double delta)
    {
        //协程更新
        ProxyCoroutineHandler.ProxyUpdateCoroutine(ref _coroutineList, (float)delta);
    }

    /// <summary>
    /// 初始化 TileMap 中的层级
    /// </summary>
    public void InitLayer()
    {
        TileRoot.AddLayer(MapLayer.AutoFloorLayer);
        TileRoot.SetLayerZIndex(MapLayer.AutoFloorLayer, -10);
        TileRoot.SetLayerNavigationEnabled(MapLayer.AutoFloorLayer, false);
        TileRoot.AddLayer(MapLayer.CustomFloorLayer1);
        TileRoot.SetLayerZIndex(MapLayer.CustomFloorLayer1, -10);
        TileRoot.SetLayerNavigationEnabled(MapLayer.CustomFloorLayer1, false);
        TileRoot.AddLayer(MapLayer.CustomFloorLayer2);
        TileRoot.SetLayerZIndex(MapLayer.CustomFloorLayer2, -10);
        TileRoot.SetLayerNavigationEnabled(MapLayer.CustomFloorLayer2, false);
        TileRoot.AddLayer(MapLayer.CustomFloorLayer3);
        TileRoot.SetLayerZIndex(MapLayer.CustomFloorLayer3, -10);
        TileRoot.SetLayerNavigationEnabled(MapLayer.CustomFloorLayer3, false);
        TileRoot.AddLayer(MapLayer.AutoMiddleLayer);
        TileRoot.SetLayerZIndex(MapLayer.AutoMiddleLayer, 0);
        TileRoot.SetLayerNavigationEnabled(MapLayer.AutoMiddleLayer, false);
        TileRoot.SetLayerYSortEnabled(MapLayer.AutoMiddleLayer, true);
        TileRoot.AddLayer(MapLayer.CustomMiddleLayer1);
        TileRoot.SetLayerZIndex(MapLayer.CustomMiddleLayer1, 0);
        TileRoot.SetLayerNavigationEnabled(MapLayer.CustomMiddleLayer1, false);
        TileRoot.SetLayerYSortEnabled(MapLayer.CustomMiddleLayer1, true);
        TileRoot.AddLayer(MapLayer.CustomMiddleLayer2);
        TileRoot.SetLayerZIndex(MapLayer.CustomMiddleLayer2, 0);
        TileRoot.SetLayerNavigationEnabled(MapLayer.CustomMiddleLayer2, false);
        TileRoot.SetLayerYSortEnabled(MapLayer.CustomMiddleLayer2, true);
        TileRoot.AddLayer(MapLayer.AutoTopLayer);
        TileRoot.SetLayerZIndex(MapLayer.AutoTopLayer, 10);
        TileRoot.SetLayerNavigationEnabled(MapLayer.AutoTopLayer, false);
        TileRoot.AddLayer(MapLayer.CustomTopLayer);
        TileRoot.SetLayerZIndex(MapLayer.CustomTopLayer, 10);
        TileRoot.SetLayerNavigationEnabled(MapLayer.CustomTopLayer, false);
        TileRoot.AddLayer(MapLayer.AutoAisleFloorLayer);
        TileRoot.SetLayerZIndex(MapLayer.AutoAisleFloorLayer, -10);
        TileRoot.SetLayerNavigationEnabled(MapLayer.AutoAisleFloorLayer, false);
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
}