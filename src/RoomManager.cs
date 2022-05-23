using Godot;

/// <summary>
/// 房间管理器
/// </summary>
public class RoomManager : Node2D
{
    /// <summary>
    /// 鼠标指针
    /// </summary>
    [Export] public PackedScene MouseCursor;

    [Export] public NodePath UIPath;

    public static RoomManager Current { get; private set; }

    public CanvasLayer UI;
    public Cursor Cursor { get; private set; }

    public override void _EnterTree()
    {
        // Current = this;
        // Input.SetMouseMode(Input.MouseMode.Hidden);
        // // 初始化鼠标
        // Cursor = MouseCursor.Instance<Cursor>();
        // AddChild(Cursor);
    }

    public override void _Ready()
    {
        // Navigation2D navigation2D;
        // navigation2D.GetSimplePath()
        // TileMap tm = GetNode<TileMap>("图块层 1");
        // tm.TileSet.gettile;
    }
}