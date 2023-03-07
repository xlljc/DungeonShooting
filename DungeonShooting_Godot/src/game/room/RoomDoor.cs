
[RegisterActivity(ActivityIdPrefix.Other + "0001", ResourcePath.prefab_map_RoomDoor_tscn)]
public partial class RoomDoor : ActivityObject
{
    private RoomDoorInfo _door;
    
    public void Init(RoomDoorInfo doorInfo)
    {
        _door = doorInfo;
    }

    public void OpenDoor()
    {
        Visible = false;
        Collision.Disabled = true;
        _door.Navigation.NavigationNode.Enabled = true;
    }

    public void CloseDoor()
    {
        Visible = true;
        Collision.Disabled = false;
        _door.Navigation.NavigationNode.Enabled = false;
    }
}