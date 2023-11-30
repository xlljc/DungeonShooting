using Godot;

namespace UI.RoomMap;

public partial class PolygonOutline : Polygon2D
{
    private Vector2[] _points;

    public override void _Ready()
    {
        Color = new Color(0, 0, 0, 0.5882353F);
    }

    public void SetPoints(Vector2[] points)
    {
        _points = points;
        Polygon = points;
    }

    public override void _Draw()
    {
        for (var i = 0; i < _points.Length; i++)
        {
            DrawLine(_points[i], _points[(i + 1) % _points.Length], Colors.Red, 6f);
        }
    }
}