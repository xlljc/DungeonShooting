using System;
using Godot;

/// <summary>
/// 模拟抛出的物体, 使用时将对象挂载到该节点上即可
/// </summary>
public class ThrowNode : Node2D
{
    public bool IsOver { get; protected set; } = true;

    public Vector2 StartPosition { get; protected set; }
    public float Direction { get; protected set; }
    public float XForce { get; protected set; }
    public float YForce { get; protected set; }
    public float RotateSpeed { get; protected set; }
    public Node2D Mount { get; protected set; }

    public void InitThrow(Vector2 start, float startHeight, float direction, float xForce, float yForce, float rotate, Node2D mount)
    {
        IsOver = false;
        GlobalPosition = StartPosition = start;
        Direction = direction;
        XForce = xForce;
        YForce = -yForce;
        RotateSpeed = rotate;

        Mount = mount;
        AddChild(mount);
        mount.Position = new Vector2(0, -startHeight);
    }

    protected virtual void OnOver()
    {

    }

    public override void _Process(float delta)
    {
        if (!IsOver)
        {
            Position += new Vector2(XForce * delta, 0).Rotated(Direction * Mathf.Pi / 180);
            Mount.Position = new Vector2(0, Mount.Position.y + YForce * delta);
            Mount.GlobalRotationDegrees += RotateSpeed * delta;
            YForce += GameConfig.G * delta;

            if (Mount.Position.y >= 0)
            {
                Mount.Position = new Vector2(0, 0);
                IsOver = true;
            }
        }
    }

}