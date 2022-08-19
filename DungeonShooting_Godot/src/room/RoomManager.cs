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
    }

    public override void _Ready()
    {
        //播放bgm
        SoundManager.PlayeMusic("intro.ogg", this, -17f);
        var gun1 = WeaponManager.GetGun("1001");
        gun1.Position = new Vector2(80, 80);
        gun1.PutDown(gun1.WeaponSprite);
        // var gun2 = WeaponManager.GetGun2();
        // gun2.Position = new Vector2(80, 120);
        // gun2.PutDown(gun2.WeaponSprite);
        // var gun3 = WeaponManager.GetGun3();
        // gun3.Position = new Vector2(120, 80);
        // gun3.PutDown(gun3.WeaponSprite);
        // var gun4 = WeaponManager.GetGun4();
        // gun4.Position = new Vector2(120, 120);
        // gun4.PutDown(gun4.WeaponSprite);
        // var gun5 = WeaponManager.GetGun5();
        // gun5.Position = new Vector2(160, 160);
        // gun5.PutDown(gun5.WeaponSprite);
    }

    public override void _Process(float delta)
    {
        
    }
}