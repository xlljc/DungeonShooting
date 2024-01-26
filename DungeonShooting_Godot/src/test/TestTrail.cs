using Godot;
using System;

public partial class TestTrail : Node2D
{
    private Sprite2D sprite;
    
    public override void _Ready()
    {
        sprite = GetNode<Sprite2D>("Sprite2D");
    }

    public override void _Process(double delta)
    {
        sprite.Position = GetLocalMousePosition();
    }
}
