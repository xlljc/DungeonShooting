
using Godot;

/// <summary>
/// 房间连接门的站位导航数据
/// </summary>
public class DoorNavigationInfo
{
    public DoorNavigationInfo(NavigationRegion2D navigationNode, NavigationPolygonData navigationData)
    {
        NavigationNode = navigationNode;
        NavigationData = navigationData;
    }

    /// <summary>
    /// 导航区域节点
    /// </summary>
    public NavigationRegion2D NavigationNode;

    /// <summary>
    /// 导航形状数据
    /// </summary>
    public NavigationPolygonData NavigationData;
}