using Godot;

/// <summary>
/// 房间管理器
/// </summary>
public class RoomManager : Node2D
{
    public Player Player { get; private set; }
    private Node2D ObjectRoot;
    private YSort SortRoot;


    private Enemy _enemy;

    public override void _EnterTree()
    {
        Input.MouseMode = Input.MouseModeEnum.Hidden;

        SortRoot = GetNode<YSort>("SortRoot");
        ObjectRoot = GetNode<Node2D>("ObjectRoot");

        //初始化地图
        var node = GetNode("MapRoot").GetChild(0).GetNode("Config");
        Color color = (Color)node.GetMeta("ClearColor");
        VisualServer.SetDefaultClearColor(color);
        
        //创建玩家
        Player = new Player();
        Player.Position = new Vector2(100, 100);
        Player.Name = "Player";
        Player.PutDown();
        
        _enemy = new Enemy();
        _enemy.Name = "Enemy";
        _enemy.PutDown(new Vector2(150, 150));
    }

    public override void _Ready()
    {
        //播放bgm
        SoundManager.PlayMusic(ResourcePath.resource_sound_bgm_Intro_ogg, -17f);
        _enemy.LookTarget = Player;
        _enemy.PickUpWeapon(WeaponManager.GetGun("1001"));

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

    /// <summary>
    /// 获取房间根节点
    /// </summary>
    /// <param name="useYSort"></param>
    /// <returns></returns>
    public Node2D GetRoot(bool useYSort)
    {
        return useYSort ? SortRoot : ObjectRoot;
    }
}