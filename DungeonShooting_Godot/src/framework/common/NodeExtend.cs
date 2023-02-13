using System;
using Godot;

/// <summary>
/// 该类为 node 节点通用扩展函数类
/// </summary>
public static class NodeExtend
{
    /// <summary>
    /// 尝试将一个 Node2d 节点转换成一个 ActivityObject 对象, 如果转换失败, 则返回 null
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
    
    /// <summary>
    /// 尝试将一个 Node2d 节点转换成一个 ActivityObject 对象, 如果转换失败, 则返回 null
    /// </summary>
    public static T AsActivityObject<T>(this Node2D node2d) where T : ActivityObject
    {
        if (node2d is T p)
        {
            return p;
        }
        var parent = node2d.GetParent();
        if (parent != null && parent is T p2)
        {
            return p2;
        }
        return null;
    }

    /// <summary>
    /// 将节点插入的房间物体根节点
    /// </summary>
    /// <param name="node">实例</param>
    /// <param name="layer">放入的层</param>
    public static void AddToActivityRoot(this Node2D node, RoomLayerEnum layer)
    {
        GameApplication.Instance.RoomManager.GetRoomLayer(layer).AddChild(node);
    }
}