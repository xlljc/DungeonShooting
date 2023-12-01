using System.Collections.Generic;
using System.Linq;
using Godot;

namespace UI.RoomMap;

public partial class RoomMapPanel : RoomMap
{

    private EventFactory _factory = EventManager.CreateEventFactory();
    private List<RoomDoorInfo> _needRefresh = new List<RoomDoorInfo>();
    
    public override void OnCreateUi()
    {
        InitMap();
        _factory.AddEventListener(EventEnum.OnPlayerFirstEnterRoom, OnPlayerFirstEnterRoom);
        _factory.AddEventListener(EventEnum.OnPlayerFirstEnterAisle, OnPlayerFirstEnterAisle);
    }


    public override void OnDestroyUi()
    {
        _factory.RemoveAllEventListener();
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

        if (_needRefresh.Count > 0)
        {
            foreach (var roomDoorInfo in _needRefresh)
            {
                HandlerRefreshUnknownSprite(roomDoorInfo);
            }
            _needRefresh.Clear();
        }

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
    
    private void OnPlayerFirstEnterRoom(object data)
    {
        var roomInfo = (RoomInfo)data;
        roomInfo.PreviewSprite.Visible = true;
        
        if (roomInfo.Doors!= null)
        {
            foreach (var roomDoor in roomInfo.Doors)
            {
                RefreshUnknownSprite(roomDoor);
            }
        }
    }
    
    private void OnPlayerFirstEnterAisle(object data)
    {
        var roomDoorInfo = (RoomDoorInfo)data;
        roomDoorInfo.AislePreviewSprite.Visible = true;

        RefreshUnknownSprite(roomDoorInfo);
        RefreshUnknownSprite(roomDoorInfo.ConnectDoor);
    }

    private void RefreshUnknownSprite(RoomDoorInfo roomDoorInfo)
    {
        if (!_needRefresh.Contains(roomDoorInfo))
        {
            _needRefresh.Add(roomDoorInfo);
        }
    }

    private void HandlerRefreshUnknownSprite(RoomDoorInfo roomDoorInfo)
    {
        //是否探索房间
        var flag1 = roomDoorInfo.RoomInfo.RoomFogMask.IsExplored;
        //是否探索过道
        var flag2 = roomDoorInfo.AisleFogMask.IsExplored;
        if (flag1 == flag2) //不显示问号
        {
            if (roomDoorInfo.UnknownSprite != null)
            {
                roomDoorInfo.UnknownSprite.QueueFree();
                roomDoorInfo.UnknownSprite = null;
            }
        }
        else
        {
            var unknownSprite = roomDoorInfo.UnknownSprite ?? CreateUnknownSprite(roomDoorInfo);
            var pos = (roomDoorInfo.OriginPosition + roomDoorInfo.GetEndPosition()) / 2;
            if (!flag2) //偏向过道
            {
                if (roomDoorInfo.Direction == DoorDirection.N)
                    pos += new Vector2I(0, -2);
                else if (roomDoorInfo.Direction == DoorDirection.S)
                    pos += new Vector2I(0, 2);
                else if (roomDoorInfo.Direction == DoorDirection.E)
                    pos += new Vector2I(2, 0);
                else if (roomDoorInfo.Direction == DoorDirection.W)
                    pos += new Vector2I(-2, 0);
            }
            else //偏向房间
            {
                if (roomDoorInfo.Direction == DoorDirection.N)
                    pos -= new Vector2I(0, -2);
                else if (roomDoorInfo.Direction == DoorDirection.S)
                    pos -= new Vector2I(0, 2);
                else if (roomDoorInfo.Direction == DoorDirection.E)
                    pos -= new Vector2I(2, 0);
                else if (roomDoorInfo.Direction == DoorDirection.W)
                    pos -= new Vector2I(-2, 0);
            }
            unknownSprite.Position = pos;
        }
    }

    private Sprite2D CreateUnknownSprite(RoomDoorInfo roomInfoDoor)
    {
        var unknownSprite = new Sprite2D();
        unknownSprite.Texture = ResourceManager.LoadTexture2D(ResourcePath.resource_sprite_ui_commonIcon_Unknown_png);
        unknownSprite.Scale = new Vector2(0.25f, 0.25f);
        roomInfoDoor.UnknownSprite = unknownSprite;
        S_Root.AddChild(unknownSprite);
        return unknownSprite;
    }

}
