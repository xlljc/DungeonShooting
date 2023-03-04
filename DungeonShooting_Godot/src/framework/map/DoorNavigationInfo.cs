
using Godot;

public class DoorNavigationInfo
{
    public DoorNavigationInfo(NavigationRegion2D navigationNode, NavigationPolygonData navigationData)
    {
        NavigationNode = navigationNode;
        NavigationData = navigationData;
    }

    public NavigationRegion2D NavigationNode;

    public NavigationPolygonData NavigationData;
}