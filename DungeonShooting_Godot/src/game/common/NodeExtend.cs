using Godot;
using System;

/// <summary>
/// 该类为 node 节点通用扩展函数类
/// </summary>
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

    /// <summary>
    /// 将一个节点扔到地上, 并设置显示的阴影, 函数返回根据该节点创建的 ThrowNode 节点
    /// </summary>
    /// <param name="shadowTarget">显示的阴影sprite</param>
    public static ThrowNode PutDown(this ActivityObject node, Sprite shadowTarget)
    {
        //return StartThrow(node, Vector2.Zero, node.Position, 0, 0, 0, 0, 0, shadowTarget);
        RoomManager.Current.ObjectRoot.AddChild(node);
        return null;
    }

    /// <summary>
    /// 拾起一个 node 节点, 返回是否拾起成功
    /// </summary>
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

    /// <summary>
    /// 触发扔掉武器操作
    /// </summary>
    /// <param name="master">触发扔掉该武器的的角色</param>
    public static ThrowComponent StartThrowWeapon(this Weapon weapon, Role master)
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
        var rotate = MathUtils.RandRangeInt(-360, -360);
        weapon.Position = Vector2.Zero;
        return weapon.StartThrow<ThrowComponent>(new Vector2(20, 20), startPos, startHeight, direction, xf, yf, rotate, weapon.WeaponSprite);
        //return weapon.StartThrow<ThrowWeapon>(new Vector2(20, 20), startPos, startHeight, direction, xf, yf, rotate, weapon.WeaponSprite);
    }


    /// <summary>
    /// 尝试将一个node2d节点转换成一个 ActivityObject 类
    /// </summary>
    public static ActivityObject AsActivityObject(this Node2D node2d)
    {
        if (node2d is ActivityObject p)
        {
            return p;
        }
        var parent = node2d.GetParent();
        if (parent != null && parent is ActivityObject p2)
        {
            return p2;
        }
        return null;
    }
    
    public static T StartThrow<T>(this ActivityObject node, Vector2 size, Vector2 start, float startHeight, float direction, float xSpeed, float ySpeed, float rotate, Sprite shadowSprite) where T : ThrowComponent
    {
        T throwNode = node.GetComponent<T>();
        if (throwNode == null)
        {
            throwNode = Activator.CreateInstance<T>();
            node.AddComponent(throwNode);
        }
        else
        {
            throwNode.StopThrow();
        }
        throwNode.StartThrow(size, start, startHeight, direction, xSpeed, ySpeed, rotate, shadowSprite);
        return throwNode;
    }
}