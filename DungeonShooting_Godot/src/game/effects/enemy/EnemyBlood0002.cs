using Godot;
using System;

public partial class EnemyBlood0002 : Sprite2D
{
    private RoomInfo _roomInfo;
    
    public void InitRoom(RoomInfo roomInfo)
    {
        _roomInfo = roomInfo;
    }

    private void DoDestory()
    {
        var position = _roomInfo.ToCanvasPosition(GlobalPosition);
        _roomInfo.StaticImageCanvas.DrawImageInCanvas(Texture, null, position.X, position.Y, RotationDegrees,
            (int)-Offset.X, (int)-Offset.Y, false, () =>
            {
                QueueFree();
            });
    }
}
