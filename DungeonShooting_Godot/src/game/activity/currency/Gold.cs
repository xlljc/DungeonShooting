
using Godot;

[Tool]
public partial class Gold : ActivityObject
{
    public override void OnInit()
    {
        DefaultLayer = RoomLayerEnum.YSortLayer;
    }
}