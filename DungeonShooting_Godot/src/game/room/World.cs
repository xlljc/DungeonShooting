using System.Collections.Generic;
using Godot;

/// <summary>
/// 游戏世界
/// </summary>
public partial class World : Node2D
{
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
    public Vector2 EnemyFindTargetPosition { get; set; }
    
    private bool _pause = false;
    
    public override void _Ready()
    {
        TileRoot.YSortEnabled = false;
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
    
}