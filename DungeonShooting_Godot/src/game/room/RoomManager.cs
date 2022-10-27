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

    public override void _EnterTree()
    {
        Current = this;
        Input.MouseMode = Input.MouseModeEnum.Hidden;

        UI = GetNode<CanvasLayer>("UI");

        // 初始化鼠标
        Cursor = MouseCursor.Instance<Cursor>();
        AddChild(Cursor);

        SortRoot = GetNode<YSort>("ItemRoot");
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
        var gun1 = WeaponManager.GetGun("1001");
        gun1.PutDown(new Vector2(80, 80));
        var gun2 = WeaponManager.GetGun("1002");
        gun2.PutDown(new Vector2(80, 120));
        var gun3 = WeaponManager.GetGun("1003");
        gun3.PutDown(new Vector2(120, 80));
        
        var gun4 = WeaponManager.GetGun("1003");
        gun4.PutDown(new Vector2(180, 80));
        var gun5 = WeaponManager.GetGun("1003");
        gun5.PutDown(new Vector2(180, 180));
        var gun6 = WeaponManager.GetGun("1002");
        gun6.PutDown(new Vector2(180, 120));
    }

    public override void _Process(float delta)
    {
        
    }
}