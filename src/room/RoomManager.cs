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
    public Player Player { get; set; }
    public YSort ItemRoot { get; private set; }

    public override void _EnterTree()
    {
        Current = this;
        Input.SetMouseMode(Input.MouseMode.Hidden);
        // 初始化鼠标
        Cursor = MouseCursor.Instance<Cursor>();
        AddChild(Cursor);

        ItemRoot = GetNode<YSort>("YSort");

        //初始化地图
        var node = GetNode("MapRoot").GetChild(0).GetNode("Config");
        Color color = (Color) node.GetMeta("ClearColor");
        VisualServer.SetDefaultClearColor(color);

    }

    public override void _Process(float delta)
    {
        if (Input.IsActionJustPressed("fire"))
        {
            //CommonNodeManager.CreateThrowNode();
        }
    }
}