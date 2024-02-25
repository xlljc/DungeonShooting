using Godot;
using System;

public partial class TestTrail : Node2D
{
    private Sprite2D sprite;
    private Trail trail;
    
    public override void _Ready()
    {
        sprite = GetNode<Sprite2D>("Sprite2D");
        trail = GetNode<Trail>("Trail");
        trail.SetTarget(sprite);
    }

    public override void _Process(double delta)
    {
        sprite.Position = GetLocalMousePosition();
    }
}
