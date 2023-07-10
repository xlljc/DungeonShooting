using Godot;

namespace UI.MapEditor;

public partial class EditorTileMap : TileMap
{
    public override void _Draw()
    {
        var mosePos = GetLocalMousePosition();
        mosePos = new Vector2((int)(mosePos.X / 16) * 16, (int)(mosePos.Y / 16) * 16);
        DrawRect(new Rect2(mosePos, 16, 16), Colors.Wheat, false);
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseButton)
        {
            if (mouseButton.ButtonIndex == MouseButton.WheelDown)
            {
                //缩小
                var scale = Scale / 1.1f;
                if (scale.LengthSquared() >= 0.5f)
                {
                    Scale = scale;
                }
                else
                {
                    GD.Print("太小了");
                }
            }
            else if (mouseButton.ButtonIndex == MouseButton.WheelUp)
            {
                //放大
                var scale = Scale * 1.1f;
                if (scale.LengthSquared() <= 2000)
                {
                    Scale = scale;
                }
                else
                {
                    GD.Print("太大了");
                }
            }
        }
    }
}