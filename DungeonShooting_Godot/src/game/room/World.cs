using System.Collections;
using System.Collections.Generic;
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
    /// 公共属性, 敌人是否找到目标, 如果找到目标, 则与目标同房间的所有敌人都会知道目标的位置
    /// </summary>
    public bool Enemy_IsFindTarget { get; set; }

    /// <summary>
    /// 公共属性, 敌人在哪个区域找到的目标, 所有该区域下的敌人都会知道目标的位置
    /// </summary>
    public HashSet<AffiliationArea> Enemy_FindTargetAffiliationSet { get; } = new HashSet<AffiliationArea>();
    
    /// <summary>
    /// 公共属性, 敌人找到的目标的位置, 如果目标在视野内, 则一直更新
    /// </summary>
    public Vector2 Enemy_FindTargetPosition { get; set; }
    
    private bool _pause = false;
    private List<CoroutineData> _coroutineList;

    public override void _Ready()
    {
        Color = Colors.Black;
        
        //临时处理, 加载TileSet
        var tileSet = ResourceManager.Load<TileSet>(ResourcePath.resource_map_tileSet_map1_TileSet1_tres);
        var tileSetAtlasSource = (TileSetAtlasSource)tileSet.GetSource(0);
        tileSetAtlasSource.Texture = ImageTexture.CreateFromImage(Image.LoadFromFile("resource/map/tileSprite/map1/16x16 dungeon ii wall reconfig v04 spritesheet.png"));
        TileRoot.TileSet = tileSet;
        TileRoot.YSortEnabled = false;
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