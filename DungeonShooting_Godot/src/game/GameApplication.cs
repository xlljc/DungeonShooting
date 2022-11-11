
using Godot;

public class GameApplication : Node2D
{
    public static GameApplication Instance { get; private set; }

    [Export] public PackedScene CursorPack;

    [Export] public NodePath RoomPath;

    [Export] public NodePath ViewportPath;

    [Export] public NodePath ViewportContainerPath;

    [Export] public NodePath UiPath;

    [Export] public NodePath GlobalNodeRootPath;

    [Export] public Font Font;
    
    /// <summary>
    /// 鼠标指针
    /// </summary>
    public Cursor Cursor { get; private set; }

    /// <summary>
    /// 游戏房间
    /// </summary>
    public RoomManager Room { get; private set; }

    /// <summary>
    /// 游戏渲染视口
    /// </summary>
    public Viewport Viewport { get; private set; }

    /// <summary>
    /// ViewportContainer 组件
    /// </summary>
    public ViewportContainer ViewportContainer { get; private set; }

    /// <summary>
    /// 游戏ui对象
    /// </summary>
    public RoomUI Ui { get; private set; }

    /// <summary>
    /// 全局根节点
    /// </summary>
    public Node2D GlobalNodeRoot { get; private set; }

    public GameApplication()
    {
        Instance = this;
    }

    public override void _EnterTree()
    {
        GlobalNodeRoot = GetNode<Node2D>(GlobalNodeRootPath);
        // 初始化鼠标
        Cursor = CursorPack.Instance<Cursor>();

        Room = GetNode<RoomManager>(RoomPath);
        Viewport = GetNode<Viewport>(ViewportPath);
        ViewportContainer = GetNode<ViewportContainer>(ViewportContainerPath);
        Ui = GetNode<RoomUI>(UiPath);

        Ui.AddChild(Cursor);
    }

    public Vector2 GlobalToViewPosition(Vector2 globalPos)
    {
        return globalPos / GameConfig.WindowScale - (GameConfig.ViewportSize / 2) + GameCamera.Main.GlobalPosition;
    }

    public Vector2 ViewToGlobalPosition(Vector2 viewPos)
    {
        return (viewPos - GameCamera.Main.GlobalPosition + (GameConfig.ViewportSize / 2)) * GameConfig.WindowScale - GameCamera.Main.SubPixelPosition;
    }
}