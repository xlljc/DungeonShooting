using System;
using Godot;

/// <summary>
/// 模拟抛出的物体, 使用时将对象挂载到该节点上即可
/// </summary>
public class ThrowNode : Node2D
{
    public float MaxHeight = 30;
    public float TargetHeight = 0;
    public float StartHeight = 0;

    public Vector2 StartPos;
    public Vector2 TargetPos = new Vector2(120, 100);
    public Vector2 RealPosition = Vector2.Zero;


    public override void _Ready()
    {
        StartPos = GlobalPosition;
        RealPosition = StartPos;
    }

    public override void _Process(float delta)
    {
        if (RealPosition.DistanceSquaredTo(TargetPos) > 1)
        {
            float v = (StartPos.DistanceTo(RealPosition)) / (StartPos.DistanceTo(TargetPos));
            float progress = Mathf.Sin(v * Mathf.Pi);

            float y = 0;
            if (v <= 0.5f)
            {
                y = Mathf.Lerp(StartHeight, MaxHeight, progress);
            }
            else
            {
                y = Mathf.Lerp(TargetHeight, MaxHeight, progress);
            }

            RealPosition = RealPosition.MoveToward(TargetPos, 100 * delta);
            GlobalPosition = RealPosition - new Vector2(0, y);
        }

    }

}