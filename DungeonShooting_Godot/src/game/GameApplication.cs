
using Godot;

public partial class GameApplication : Node2D
{
    public static GameApplication Instance { get; private set; }

    /// <summary>
    /// 是否开启调试
    /// </summary>
    [Export] public bool Debug = false;

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
    public RoomManager RoomManager { get; private set; }

    /// <summary>
    /// 游戏渲染视口
    /// </summary>
    public SubViewport SubViewport { get; private set; }

    /// <summary>
    /// SubViewportContainer 组件
    /// </summary>
    public SubViewportContainer SubViewportContainer { get; private set; }

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
        //随机化种子
        GD.Randomize();
        //调试绘制开关
        ActivityObject.IsDebug = Debug;

        GlobalNodeRoot = GetNode<Node2D>(GlobalNodeRootPath);
        // 初始化鼠标
        Cursor = CursorPack.Instantiate<Cursor>();

        RoomManager = GetNode<RoomManager>(RoomPath);
        SubViewport = GetNode<SubViewport>(ViewportPath);
        SubViewportContainer = GetNode<SubViewportContainer>(ViewportContainerPath);
        Ui = GetNode<RoomUI>(UiPath);

        Ui.AddChild(Cursor);
    }

    /// <summary>
    /// 将 viewport 以外的全局坐标 转换成 viewport 内的全局坐标
    /// </summary>
    public Vector2 GlobalToViewPosition(Vector2 globalPos)
    {
        return globalPos / GameConfig.WindowScale - (GameConfig.ViewportSize / 2) + GameCamera.Main.GlobalPosition;
    }

    /// <summary>
    /// 将 viewport 以内的全局坐标 转换成 viewport 外的全局坐标
    /// </summary>
    public Vector2 ViewToGlobalPosition(Vector2 viewPos)
    {
        return (viewPos - GameCamera.Main.GlobalPosition + (GameConfig.ViewportSize / 2)) * GameConfig.WindowScale - GameCamera.Main.SubPixelPosition;
    }
}