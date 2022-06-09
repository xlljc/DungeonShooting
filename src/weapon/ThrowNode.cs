using System;
using Godot;

/// <summary>
/// 模拟抛出的物体, 使用时将对象挂载到该节点上即可
/// </summary>
public class ThrowNode : Node2D
{
    public bool IsOver { get; protected set; } = true;

    private Vector2 StartPosition;
    private float Direction;
    private Vector2 Velocity;
    private Node2D Mount;

    public void InitThrow(Vector2 start, float direction, Vector2 velocity, Node2D mount)
    {
        IsOver = false;
        GlobalPosition = StartPosition = start;
        Direction = direction;
        Velocity = velocity;

        Mount = mount;
        AddChild(mount);
        mount.Position = Vector2.Zero;
    }

    protected virtual void OnOver()
    {

    }

    public override void _Process(float delta)
    {
        if (!IsOver)
        {
            Position += new Vector2(Velocity.x * delta, 0).Rotated(Direction * Mathf.Pi / 180);
            Mount.Position = new Vector2(0, Mount.Position.y + Velocity.y * delta);
            Velocity.y += GameConfig.G * delta;

            if (Mount.Position.y >= 0)
            {
                Mount.Position = new Vector2(0, 0);
                IsOver = true;
            }
        }
    }

}