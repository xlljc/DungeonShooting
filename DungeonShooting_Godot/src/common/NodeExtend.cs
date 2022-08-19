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
        return StartThrow<T>(node, size, start, startHeight, direction, xSpeed, ySpeed, rotate, null);
    }

    public static ThrowNode StartThrow(this Node2D node, Vector2 size, Vector2 start, float startHeight, float direction, float xSpeed, float ySpeed, float rotate, Sprite shadowTarget)
    {
        return StartThrow<ThrowNode>(node, size, start, startHeight, direction, xSpeed, ySpeed, rotate, shadowTarget);
    }

    public static T StartThrow<T>(this Node2D node, Vector2 size, Vector2 start, float startHeight, float direction, float xSpeed, float ySpeed, float rotate, Sprite shadowTarget) where T : ThrowNode
    {
        ThrowNode throwNode = node.GetParentOrNull<ThrowNode>();
        T inst;
        if (throwNode == null)
        {
            inst = Activator.CreateInstance<T>();
        }
        else if (throwNode is T)
        {
            inst = throwNode as T;
        }
        else
        {
            throwNode.StopThrow();
            inst = Activator.CreateInstance<T>();
        }
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

    public static ThrowWeapon StartThrowWeapon(this Weapon weapon, Role master)
    {
        if (master.Face == FaceDirection.Left)
        {
            weapon.Scale *= new Vector2(1, -1);
            weapon.RotationDegrees = 180;
        }
        var startPos = master.GlobalPosition;// + new Vector2(0, 0);
        var startHeight = 6;
        var direction = master.GlobalRotationDegrees + MathUtils.RandRangeInt(-20, 20);
        var xf = 30;
        var yf = MathUtils.RandRangeInt(60, 120);
        var rotate = MathUtils.RandRangeInt(-180, 180);
        weapon.Position = Vector2.Zero;
        return weapon.StartThrow<ThrowWeapon>(new Vector2(20, 20), startPos, startHeight, direction, xf, yf, rotate, weapon.WeaponSprite);
    }

    public static IProp AsProp(this Node2D node2d)
    {
        if (node2d is IProp p)
        {
            return p;
        }
        var parent = node2d.GetParent();
        if (parent != null && parent is IProp p2)
        {
            return p2;
        }
        return null;
    }
}