using Godot;
using System;

public class CollisionComponent
{
    public IObject Target { get; }

    public Area2D AreaNode { get; }

    public bool Disabled
    {
        get
        {
            return CollisionShape2DNode.Disabled;
        }
        set
        {
            CollisionShape2DNode.Disabled = value;
        }
    }

    public CollisionShape2D CollisionShape2DNode { get; }

    public CollisionComponent(IObject inst, Area2D area, CollisionShape2D collisionShape)
    {
        Target = inst;
        AreaNode = area;
        CollisionShape2DNode = collisionShape;
    }
}