
using Godot;

/// <summary>
/// 房间连接门的站位导航数据
/// </summary>
public class DoorNavigationInfo
{
    public DoorNavigationInfo(RoomDoorInfo doorInfo, NavigationPolygonData openNavigationData, NavigationPolygonData closeNavigationData)
    {
        DoorInfo = doorInfo;
        OpenNavigationData = openNavigationData;
        CloseNavigationData = closeNavigationData;
    }

    /// <summary>
    /// 绑定的门对象
    /// </summary>
    public RoomDoorInfo DoorInfo;
    
    /// <summary>
    /// 门开启时导航区域节点
    /// </summary>
    public NavigationRegion2D OpenNavigationNode;
    
    /// <summary>
    /// 门关闭时导航区域节点
    /// </summary>
    public NavigationRegion2D CloseNavigationNode;

    /// <summary>
    /// 门开启时导航形状数据
    /// </summary>
    public NavigationPolygonData OpenNavigationData;
    
    /// <summary>
    /// 门关闭时导航形状数据
    /// </summary>
    public NavigationPolygonData CloseNavigationData;
}