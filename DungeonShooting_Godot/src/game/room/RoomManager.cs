using System;
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

    public static RoomManager Current { get; private set; }

    public CanvasLayer UI;
    public Cursor Cursor { get; private set; }
    public Player Player { get; set; }
    public Node2D ObjectRoot { get; private set; }
    public YSort SortRoot { get; private set; }
    public Viewport Viewport { get; private set; }
    public ViewportContainer ViewportContainer { get; private set; }

    public override void _EnterTree()
    {
        Current = this;
        Input.MouseMode = Input.MouseModeEnum.Hidden;

        UI = GetNode<CanvasLayer>("UI");

        // 初始化鼠标
        Cursor = MouseCursor.Instance<Cursor>();
        AddChild(Cursor);

        SortRoot = GetNode<YSort>("ItemRoot");
        Viewport = GetParentOrNull<Viewport>();
        if (Viewport != null)
        {
            ViewportContainer = Viewport.GetParentOrNull<ViewportContainer>();
        }
            
        ObjectRoot = GetNode<Node2D>("ObjectRoot");

        //初始化地图
        var node = GetNode("MapRoot").GetChild(0).GetNode("Config");
        Color color = (Color)node.GetMeta("ClearColor");
        VisualServer.SetDefaultClearColor(color);
        
        //创建玩家
        var player = new Player();
        player.Position = new Vector2(100, 100);
        player.Name = "Player";
        //SortRoot.AddChild(player);
        player.PutDown();
    }

    public override void _Ready()
    {
        //播放bgm
        SoundManager.PlayeMusic("intro.ogg", this, -17f);
        WeaponManager.GetGun("1001").PutDown(new Vector2(100, 80));
        WeaponManager.GetGun("1001").PutDown(new Vector2(80, 100));
        WeaponManager.GetGun("1001").PutDown(new Vector2(80, 80));
        WeaponManager.GetGun("1002").PutDown(new Vector2(80, 120));
        WeaponManager.GetGun("1003").PutDown(new Vector2(120, 80));

        WeaponManager.GetGun("1003").PutDown(new Vector2(180, 80));
        WeaponManager.GetGun("1003").PutDown(new Vector2(180, 180));
        WeaponManager.GetGun("1002").PutDown(new Vector2(180, 120));

    }

    public override void _Process(float delta)
    {
        
    }
}