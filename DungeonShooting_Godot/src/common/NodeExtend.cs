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
        if (parent != null)
        {
            parent.StopThrow();
            return true;
        }
        return false;
    }

    public static ThrowGun StartThrowGun(this Gun gun, Role master)
    {
        if (master.Face == FaceDirection.Left)
        {
            gun.Scale *= new Vector2(1, -1);
            gun.RotationDegrees = 180;
        }
        var startPos = master.GlobalPosition;// + new Vector2(0, 0);
        var startHeight = 6;
        var direction = master.GlobalRotationDegrees + MathUtils.RandRangeInt(-20, 20);
        var xf = 30;
        var yf = MathUtils.RandRangeInt(60, 120);
        var rotate = MathUtils.RandRangeInt(-180, 180);
        gun.Position = Vector2.Zero;
        return gun.StartThrow<ThrowGun>(new Vector2(20, 20), startPos, startHeight, direction, xf, yf, rotate, gun.GunSprite);
    }
}