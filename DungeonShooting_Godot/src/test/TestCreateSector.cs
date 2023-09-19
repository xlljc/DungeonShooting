using Godot;
using System;

public partial class TestCreateSector : Node2D
{

    private CollisionPolygon2D _polygon2D;
    
    public override void _Ready()
    {
        GetNode<Area2D>("Area2D").AreaEntered += area =>
        {
            GD.Print("areaEnter: " + area.Name);
        };
        
        _polygon2D = GetNode<CollisionPolygon2D>("Area2D/CollisionPolygon2D");
        _polygon2D.Polygon = Utils.CreateSectorPolygon(90, 350, 160, 10);
    }

    public override void _Process(double delta)
    {
        GetNode<Area2D>("Area2D2").GlobalPosition = GetGlobalMousePosition();
    }
}
