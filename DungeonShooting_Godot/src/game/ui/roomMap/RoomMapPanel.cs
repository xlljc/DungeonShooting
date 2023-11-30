using System.Linq;
using Godot;

namespace UI.RoomMap;

public partial class RoomMapPanel : RoomMap
{

    public override void OnCreateUi()
    {
        DrawRoom();
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
        
        S_Root.Instance.Position = S_DrawContainer.Instance.Size / 2 - Player.Current.Position / 12;
    }
    
    private void DrawRoom()
    {
        var startRoom = GameApplication.Instance.DungeonManager.StartRoomInfo;
        startRoom.EachRoom(roomInfo =>
        {
            //房间区域
            var navigationPolygonData = roomInfo.RoomSplit.TileInfo.NavigationList[0];
            var points = navigationPolygonData.GetPoints();
            var newPoints = new Vector2[points.Length];
            for (var i = 0; i < points.Length; i++)
            {
                newPoints[i] = roomInfo.ToGlobalPosition(points[i]);
            }

            var outline = new PolygonOutline();
            outline.SetPoints(newPoints);
            S_Root.AddChild(outline);
            
            //过道
            if (roomInfo.Doors != null)
            {
                foreach (var doorInfo in roomInfo.Doors)
                {
                    if (doorInfo.IsForward)
                    {
                        var aislePoints = doorInfo.AisleNavigation.GetPoints();
                        // var newAislePoints = new Vector2[aislePoints.Length];
                        // for (var i = 0; i < aislePoints.Length; i++)
                        // {
                        //     newAislePoints[i] = roomInfo.ToGlobalPosition(aislePoints[i]);
                        // }

                        var aisleOutline = new PolygonOutline();
                        aisleOutline.SetPoints(aislePoints);
                        S_Root.AddChild(aisleOutline);
                    }
                }
            }
            //roomInfo.Doors[0].Navigation.OpenNavigationData
        });

    }
}
