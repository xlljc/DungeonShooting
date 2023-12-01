using System.Linq;
using Godot;

namespace UI.RoomMap;

public partial class RoomMapPanel : RoomMap
{

    public override void OnCreateUi()
    {
        InitMap();
    }

    public override void OnDestroyUi()
    {
    }

    public override void Process(float delta)
    {
        // //按下地图按键
        // if (InputManager.Map && !S_RoomMap.Instance.IsOpen)
        // {
        //     World.Current.Pause = true;
        //     S_RoomMap.Instance.ShowUi();
        // }
        // else if (!InputManager.Map && S_RoomMap.Instance.IsOpen)
        // {
        //     S_RoomMap.Instance.HideUi();
        //     World.Current.Pause = false;
        // }
        
        S_Root.Instance.Position = S_DrawContainer.Instance.Size / 2 - Player.Current.Position / 16 * S_Root.Instance.Scale;
    }
    
    //初始化小地图
    private void InitMap()
    {
        var startRoom = GameApplication.Instance.DungeonManager.StartRoomInfo;
        startRoom.EachRoom(roomInfo =>
        {
            roomInfo.PreviewSprite.Visible = false;
            S_Root.AddChild(roomInfo.PreviewSprite);

            if (roomInfo.Doors != null)
            {
                foreach (var roomInfoDoor in roomInfo.Doors)
                {
                    if (roomInfoDoor.IsForward)
                    {
                        roomInfoDoor.AislePreviewSprite.Visible = false;
                        S_Root.AddChild(roomInfoDoor.AislePreviewSprite);
                    }
                }
            }
        });

    }
}
