using Godot;
using System;

public static class NodeExtend
{

    public static ThrowNode StartThrow(this Node2D node, Vector2 size, Vector2 start, float startHeight, float direction, float xSpeed, float ySpeed, float rotate)
    {
        return StartThrow<ThrowNode>(node, size, start, startHeight, direction, xSpeed, ySpeed, rotate);
    }

    public static T StartThrow<T>(this Node2D node, Vector2 size, Vector2 start, float startHeight, float direction, float xSpeed, float ySpeed, float rotate) where T : ThrowNode
    {
        StartThrow<T>(node, size, start, startHeight, direction, xSpeed, ySpeed, rotate, null);
        T inst = Activator.CreateInstance<T>();
        inst.StartThrow(size, start, startHeight, direction, xSpeed, ySpeed, rotate, node);
        return inst;
    }

    public static ThrowNode StartThrow(this Node2D node, Vector2 size, Vector2 start, float startHeight, float direction, float xSpeed, float ySpeed, float rotate, Sprite shadowTarget)
    {
        return StartThrow<ThrowNode>(node, size, start, startHeight, direction, xSpeed, ySpeed, rotate, shadowTarget);
    }

    public static T StartThrow<T>(this Node2D node, Vector2 size, Vector2 start, float startHeight, float direction, float xSpeed, float ySpeed, float rotate, Sprite shadowTarget) where T : ThrowNode
    {
        T inst = Activator.CreateInstance<T>();
        inst.StartThrow(size, start, startHeight, direction, xSpeed, ySpeed, rotate, node, shadowTarget);
        return inst;
    }

    public static ThrowNode PutDown(this Node2D node, Sprite shadowTarget)
    {
        return StartThrow(node, Vector2.Zero, node.Position, 0, 0, 0, 0, 0, shadowTarget);
    }

    public static bool Pickup(this Node2D node)
    {
        ThrowNode parent = node.GetParentOrNull<ThrowNode>();
        if (parent != null) {
            parent.StopThrow();
            return true;
        }
        return false;
    }
}