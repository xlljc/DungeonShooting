
using Godot;

/// <summary>
/// 房间连接门的站位导航数据
/// </summary>
public class DoorNavigationInfo
{
    public DoorNavigationInfo(RoomDoorInfo doorInfo, NavigationPolygonData doorOpenNavigationData, NavigationPolygonData doorCloseNavigationData)
    {
        DoorInfo = doorInfo;
        DoorOpenNavigationData = doorOpenNavigationData;
        DoorCloseNavigationData = doorCloseNavigationData;
    }

    /// <summary>
    /// 绑定的门对象
    /// </summary>
    public RoomDoorInfo DoorInfo;
    
    /// <summary>
    /// 导航区域节点
    /// </summary>
    public NavigationRegion2D NavigationNode;

    /// <summary>
    /// 门开启时导航形状数据
    /// </summary>
    public NavigationPolygonData DoorOpenNavigationData;
    
    /// <summary>
    /// 门关闭时导航形状数据
    /// </summary>
    public NavigationPolygonData DoorCloseNavigationData;
}