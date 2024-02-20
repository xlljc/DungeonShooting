
using Godot;

/// <summary>
/// 游戏大厅
/// </summary>
public partial class Hall : World
{
    public RoomInfo RoomInfo { get; set; }
    
    public override void _Ready()
    {
        base._Ready();
        Color = Colors.White;
    }
}