using Godot;

public partial class Trail : Line2D
{
    [Export]
    public int length = 300;

    private Node2D parent;
    private Vector2 offset = Vector2.Zero;

    public override void _Ready()
    {
        offset = Position;
        parent = GetParent<Node2D>();
        TopLevel = true;
    }

    public override void _Process(double delta)
    {
        GlobalPosition = Vector2.Zero;

        var point = parent.GlobalPosition + offset;
        AddPoint(point, 0);
        
        if (GetPointCount() > length)
        {
            RemovePoint(GetPointCount() - 1);
        }
    }
}