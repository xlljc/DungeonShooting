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

    [Export] public NodePath UIPath;

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
        var gun1 = GunManager.GetGun1();
        gun1.Position = new Vector2(80, 80);
        gun1.PutDown(gun1.GunSprite);
        var gun2 = GunManager.GetGun2();
        gun2.Position = new Vector2(80, 120);
        gun2.PutDown(gun2.GunSprite);
        var gun3 = GunManager.GetGun3();
        gun3.Position = new Vector2(120, 80);
        gun3.PutDown(gun3.GunSprite);
        var gun4 = GunManager.GetGun4();
        gun4.Position = new Vector2(120, 120);
        gun4.PutDown(gun4.GunSprite);
        var gun5 = GunManager.GetGun5();
        gun5.Position = new Vector2(160, 160);
        gun5.PutDown(gun5.GunSprite);
    }

    public override void _Process(float delta)
    {
        if (Input.IsActionJustPressed("fire"))
        {
            //CommonNodeManager.CreateThrowNode();
        }
    }
}