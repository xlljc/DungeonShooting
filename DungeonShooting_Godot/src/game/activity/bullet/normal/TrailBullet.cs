
using System.Collections.Generic;
using Godot;

[Tool]
public partial class TrailBullet : Bullet
{
    [Export, ExportFillNode]
    public Line2D Trail { get; set; }

    private int _pointCount = 30;
    private readonly List<Vector2> points = new List<Vector2>();

    protected override void Process(float delta)
    {
        base.Process(delta);

        if (points.Count < _pointCount)
        {
            points.Add(AnimatedSprite.GlobalPosition);
        }
        else
        {
            points.RemoveAt(0);
            points.Add(AnimatedSprite.GlobalPosition);
        }

        var tempPos = new Vector2[points.Count];
        tempPos[points.Count - 1] = Vector2.Zero;
        for (var i = points.Count - 2; i >= 0; i--)
        {
            tempPos[i] = AnimatedSprite.ToLocal(points[i]);
        }
        //更新line2d点坐标
        Trail.Points = tempPos;
    }

    public override void OnLeavePool()
    {
        base.OnLeavePool();
        points.Clear();
        Trail.Points = new Vector2[0];
    }
}