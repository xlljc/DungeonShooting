
using System.Collections.Generic;

/// <summary>
/// 事件类型枚举
/// </summary>
public enum EventEnum
{
    /// <summary>
    /// 敌人死亡, 参数为死亡的敌人的实例, 参数类型为<see cref="Role"/>
    /// </summary>
    OnEnemyDie,
    /// <summary>
    /// 玩家进入一个房间，参数类型为<see cref="RoomInfo"/>
    /// </summary>
    OnPlayerEnterRoom,
    /// <summary>
    /// 玩家第一次进入某个房间，参数类型为<see cref="RoomInfo"/>
    /// </summary>
    OnPlayerFirstEnterRoom,
    /// <summary>
    /// 玩家进入过道时调用, 参数类型为进入该过道的门<see cref="RoomDoorInfo"/>
    /// </summary>
    OnPlayerEnterAisle,
    /// <summary>
    /// 玩家首次进入过道时调用, 参数类型为进入该过道的门<see cref="RoomDoorInfo"/>
    /// </summary>
    OnPlayerFirstEnterAisle,
    /// <summary>
    /// 玩家可互动对象改变, 参数类型为<see cref="CheckInteractiveResult"/>
    /// </summary>
    OnPlayerChangeInteractiveItem,
    /// <summary>
    /// 玩家血量发生改变, 参数为玩家血量
    /// </summary>
    OnPlayerHpChange,
    /// <summary>
    /// 玩家最大血量发生改变, 参数为玩家最大血量
    /// </summary>
    OnPlayerMaxHpChange,
    /// <summary>
    /// 玩家护盾值发生改变, 参数为玩家护盾值
    /// </summary>
    OnPlayerShieldChange,
    /// <summary>
    /// 玩家最大护盾值发生改变, 参数为玩家最大护盾值
    /// </summary>
    OnPlayerMaxShieldChange,
    /// <summary>
    /// 玩家金币发生改变, 参数为玩家金币值
    /// </summary>
    OnPlayerGoldChange,
    /// <summary>
    /// 玩家拾起武器, 参数为<see cref="Weapon"/>
    /// </summary>
    OnPlayerPickUpWeapon,
    /// <summary>
    /// 玩家丢弃武器, 参数为<see cref="Weapon"/>
    /// </summary>
    OnPlayerRemoveWeapon,
    /// <summary>
    /// 玩家拾起道具, 参数为<see cref="PropActivity"/>
    /// </summary>
    OnPlayerPickUpProp,
    /// <summary>
    /// 玩家丢弃道具, 参数为<see cref="PropActivity"/>
    /// </summary>
    OnPlayerRemoveProp,
    
    /// <summary>
    /// 当玩家进入地牢时调用, 没有参数
    /// </summary>
    OnEnterDungeon,
    /// <summary>
    /// 当玩家退出地牢时调用, 没有参数
    /// </summary>
    OnExitDungeon,
    
    
    //------------------------- 编辑器相关 --------------------------
    
    /// <summary>
    /// 创建地牢组完成时调用, 参数为<see cref="DungeonRoomGroup"/>
    /// </summary>
    OnCreateGroupFinish,
    /// <summary>
    /// 删除地牢组完成时调用, 参数为<see cref="DungeonRoomGroup"/>
    /// </summary>
    OnDeleteGroupFinish,
    /// <summary>
    /// 创建地牢房间完成时调用, 参数为<see cref="DungeonRoomSplit"/>
    /// </summary>
    OnCreateRoomFinish,
    /// <summary>
    /// 标记房间数据脏了, 也就是数据有修改
    /// </summary>
    OnTileMapDirty,
    /// <summary>
    /// 编辑器触发保存, 参数为<see cref="DungeonRoomSplit"/>>
    /// </summary>
    OnTileMapSave,
    /// <summary>
    /// 选中拖拽工具
    /// </summary>
    OnSelectDragTool,
    /// <summary>
    /// 选中绘制工具
    /// </summary>
    OnSelectPenTool,
    /// <summary>
    /// 选中绘制区域工具
    /// </summary>
    OnSelectRectTool,
    /// <summary>
    /// 选中编辑门区域工具
    /// </summary>
    OnSelectEditTool,
    /// <summary>
    /// 点击跳转到地图中心点
    /// </summary>
    OnClickCenterTool,
    /// <summary>
    /// 选中地牢组, 参数<see cref="DungeonRoomGroup"/>
    /// </summary>
    OnSelectGroup,
    /// <summary>
    /// 选中房间, 参数<see cref="DungeonRoomSplit"/>
    /// </summary>
    OnSelectRoom,
    /// <summary>
    /// 选中预设, 参数<see cref="RoomPreinstallInfo"/>
    /// </summary>
    OnSelectPreinstall,
    /// <summary>
    /// 选中波数, 参数 <see cref="List{T}"/>, T 为 <see cref="MarkInfo"/>
    /// </summary>
    OnSelectWave,
    /// <summary>
    /// 创建标记, 参数<see cref="MarkInfo"/>
    /// </summary>
    OnCreateMark,
    /// <summary>
    /// 修改标记, 参数<see cref="MarkInfo"/>
    /// </summary>
    OnEditMark,
    /// <summary>
    /// 选中标记, 参数<see cref="MarkInfo"/>或者null
    /// </summary>
    OnSelectMark,
    /// <summary>
    /// 删除标记, 参数<see cref="MarkInfo"/>
    /// </summary>
    OnDeleteMark,
    /// <summary>
    /// 设置标记显示状态, 参数<see cref="MarkInfoVisibleData"/>
    /// </summary>
    OnSetMarkVisible,
    /// <summary>
    /// 选中TileMap编辑器中的层级, 参数为选择的层级索引, 参数<see cref="int"/>
    /// </summary>
    OnSelectTileLayer,
    
    /// <summary>
    /// 保存TileSet, 参数为<see cref="TileSetSplit"/>
    /// </summary>
    OnTileSetSave,
    /// <summary>
    /// 标记TileSet脏了, 无参数
    /// </summary>
    OnTileSetDirty,
    /// <summary>
    /// 创建TileSet资源, 参数为<see cref="TileSetSourceInfo"/>
    /// </summary>
    OnCreateTileSetSource,
    /// <summary>
    /// 选中TileSet资源, 参数为<see cref="TileSetSourceInfo"/>, 参数为 null 表示取消选中
    /// </summary>
    OnSelectTileSetSource,
    /// <summary>
    /// 删除TileSet资源, 参数为<see cref="TileSetSourceInfo"/>
    /// </summary>
    OnDeleteTileSetSource,
    /// <summary>
    /// 设置TileSet纹理, 参数<see cref="Godot.Texture2D"/>
    /// </summary>
    OnSetTileTexture,
    /// <summary>
    /// 设置TileSet编辑器的背景颜色, 参数为<see cref="Godot.Color"/>
    /// </summary>
    OnSetTileSetBgColor,
    /// <summary>
    /// 选中组合模式下的Cell图块, 参数为<see cref="Godot.Vector2I"/>
    /// </summary>
    OnSelectCombinationCell,
    /// <summary>
    /// 移除组合模式下的Cell图块, 参数为<see cref="Godot.Vector2I"/>
    /// </summary>
    OnRemoveCombinationCell,
    /// <summary>
    /// 清除组合模式下的Cell图块
    /// </summary>
    OnClearCombinationCell,
    /// <summary>
    /// 导入组合, 参数为<see cref="ImportCombinationData"/>
    /// </summary>
    OnImportCombination,
    /// <summary>
    /// 移除组合, 参数为<see cref="ImportCombinationData"/>
    /// </summary>
    OnRemoveCombination,
    /// <summary>
    /// 修改组合名称, 参数为<see cref="ImportCombinationData"/>
    /// </summary>
    OnUpdateCombination,
}
