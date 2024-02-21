
using System;
using System.Collections;
using Godot;

/// <summary>
/// 地牢管理器
/// </summary>
public partial class DungeonManager : Node2D
{
    /// <summary>
    /// 起始房间
    /// </summary>
    public RoomInfo StartRoomInfo => _dungeonGenerator?.StartRoomInfo;
    
    /// <summary>
    /// 当前玩家所在的房间
    /// </summary>
    public RoomInfo ActiveRoomInfo => Player.Current?.AffiliationArea?.RoomInfo;
    
    /// <summary>
    /// 当前玩家所在的区域
    /// </summary>
    public AffiliationArea ActiveAffiliationArea => Player.Current?.AffiliationArea;

    /// <summary>
    /// 是否在地牢里
    /// </summary>
    public bool IsInDungeon { get; private set; }

    /// <summary>
    /// 是否是编辑器模式
    /// </summary>
    public bool IsEditorMode { get; private set; }
    
    /// <summary>
    /// 当前使用的配置
    /// </summary>
    public DungeonConfig CurrConfig { get; private set; }
	
    /// <summary>
    /// 当前玩家所在游戏世界对象
    /// </summary>
    public World CurrWorld { get; private set; }

    /// <summary>
    /// 自动图块配置
    /// </summary>
    public AutoTileConfig AutoTileConfig { get; private set; }

    private UiBase _prevUi;
    private DungeonTileMap _dungeonTileMap;
    private DungeonGenerator _dungeonGenerator;
    
    //用于检查房间敌人的计时器
    private float _checkEnemyTimer = 0;
    //用于记录玩家上一个所在区域
    private AffiliationArea _affiliationAreaFlag;


    public DungeonManager()
    {
        //绑定事件
        EventManager.AddEventListener(EventEnum.OnPlayerFirstEnterRoom, OnPlayerFirstEnterRoom);
        EventManager.AddEventListener(EventEnum.OnPlayerEnterRoom, OnPlayerEnterRoom);
    }
    
    /// <summary>
    /// 创建新的 World 对象, 相当于清理房间
    /// </summary>
    public World CreateNewWorld(SeedRandom random, string scenePath = ResourcePath.scene_World_tscn)
    {
        if (CurrWorld != null)
        {
            ClearWorld();
            CurrWorld.QueueFree();
        }
        CurrWorld = ResourceManager.LoadAndInstantiate<World>(scenePath);
        GameApplication.Instance.SceneRoot.AddChild(CurrWorld);
        if (CurrWorld is not Hall)
        {
            CurrWorld.InitLayer();
        }

        CurrWorld.InitRandomPool(random);
        return CurrWorld;
    }

    /// <summary>
    /// 销毁 World 对象, 相当于清理房间
    /// </summary>
    public void DestroyWorld()
    {
        //销毁所有物体
        if (CurrWorld != null)
        {
            ClearWorld();
            CurrWorld.QueueFree();
        }
		
        //销毁池中所有物体
        ObjectPool.DisposeAllItem();

        CurrWorld = null;
    }
    
    
    //清理世界
    private void ClearWorld()
    {
        var childCount = CurrWorld.NormalLayer.GetChildCount();
        for (var i = 0; i < childCount; i++)
        {
            var c = CurrWorld.NormalLayer.GetChild(i);
            if (c is IDestroy destroy)
            {
                destroy.Destroy();
            }
        }
        childCount = CurrWorld.YSortLayer.GetChildCount();
        for (var i = 0; i < childCount; i++)
        {
            var c = CurrWorld.YSortLayer.GetChild(i);
            if (c is IDestroy destroy)
            {
                destroy.Destroy();
            }
        }
    }

    /// <summary>
    /// 进入大厅
    /// </summary>
    public void LoadHall(Action finish = null)
    {
        GameApplication.Instance.StartCoroutine(RunLoadHallCoroutine(finish));
    }

    public void ExitHall(Action finish = null)
    {
        GameApplication.Instance.StartCoroutine(RunExitHallCoroutine(finish));
    }
    
    /// <summary>
    /// 加载地牢
    /// </summary>
    public void LoadDungeon(DungeonConfig config, Action finish = null)
    {
        IsEditorMode = false;
        CurrConfig = config;
        GameApplication.Instance.StartCoroutine(RunLoadDungeonCoroutine(finish));
    }
    
    /// <summary>
    /// 重启地牢
    /// </summary>
    public void RestartDungeon(DungeonConfig config)
    {
        IsEditorMode = false;
        CurrConfig = config;
        ExitDungeon(() =>
        {
            LoadDungeon(CurrConfig);
        });
    }

    /// <summary>
    /// 退出地牢
    /// </summary>
    public void ExitDungeon(Action finish = null)
    {
        IsInDungeon = false;
        GameApplication.Instance.StartCoroutine(RunExitDungeonCoroutine(finish));
    }
    
    //-------------------------------------------------------------------------------------

    /// <summary>
    /// 在编辑器模式下进入地牢
    /// </summary>
    /// <param name="config">地牢配置</param>
    public void EditorPlayDungeon(DungeonConfig config)
    {
        IsEditorMode = true;
        CurrConfig = config;
        if (_prevUi != null)
        {
            _prevUi.HideUi();
        }
        GameApplication.Instance.StartCoroutine(RunLoadDungeonCoroutine(null));
    }
    
    /// <summary>
    /// 在编辑器模式下进入地牢
    /// </summary>
    /// <param name="prevUi">记录上一个Ui</param>
    /// <param name="config">地牢配置</param>
    public void EditorPlayDungeon(UiBase prevUi, DungeonConfig config)
    {
        IsEditorMode = true;
        CurrConfig = config;
        _prevUi = prevUi;
        if (_prevUi != null)
        {
            _prevUi.HideUi();
        }
        GameApplication.Instance.StartCoroutine(RunLoadDungeonCoroutine(null));
    }

    /// <summary>
    /// 在编辑器模式下退出地牢, 并且打开上一个Ui
    /// </summary>
    public void EditorExitDungeon()
    {
        IsInDungeon = false;
        GameApplication.Instance.StartCoroutine(RunExitDungeonCoroutine(() =>
        {
            IsEditorMode = false;
            //显示上一个Ui
            if (_prevUi != null)
            {
                _prevUi.ShowUi();
            }
        }));
    }
    
    //-------------------------------------------------------------------------------------

    public override void _Process(double delta)
    {
        if (IsInDungeon)
        {
            if (CurrWorld.Pause) //已经暂停
            {
                return;
            }
            
            //暂停游戏
            if (InputManager.Menu)
            {
                CurrWorld.Pause = true;
                //鼠标改为Ui鼠标
                GameApplication.Instance.Cursor.SetGuiMode(true);
                //打开暂停Ui
                UiManager.Open_PauseMenu();
            }
            
            //更新迷雾
            FogMaskHandler.Update();
            
            _checkEnemyTimer += (float)delta;
            if (_checkEnemyTimer >= 1)
            {
                _checkEnemyTimer %= 1;
                //检查房间内的敌人存活状况
                OnCheckEnemy();
            }
            
            if (ActivityObject.IsDebug)
            {
                QueueRedraw();
            }
        }
    }

    private IEnumerator RunLoadHallCoroutine(Action finish)
    {
        //打开 loading UI
        UiManager.Open_Loading();
        yield return 0;
        
        var hall = (Hall)CreateNewWorld(Utils.Random, ResourcePath.scene_Hall_tscn);
        yield return 0;

        //创建房间数据
        var roomInfo = new RoomInfo(0, DungeonRoomType.None, null);
        roomInfo.Size = new Vector2I(50, 50);
        roomInfo.Position = Vector2I.Zero;
        hall.RoomInfo = roomInfo;
        yield return 0;
        
        //创建归属区域
        var affiliation = new AffiliationArea();
        affiliation.Name = "AffiliationArea_Hall";
        affiliation.Init(roomInfo, new Rect2I(roomInfo.Position, roomInfo.Size * GameConfig.TileCellSize));
        roomInfo.AffiliationArea = affiliation;
        hall.AffiliationAreaRoot.AddChild(affiliation);
        yield return 0;
        
        //静态渲染精灵根节点, 用于放置sprite
        var spriteRoot = new RoomStaticSprite(roomInfo);
        spriteRoot.Name = "SpriteRoot";
        roomInfo.StaticSprite = spriteRoot;
        hall.StaticSpriteRoot.AddChild(spriteRoot);
        yield return 0;
        
        //静态精灵画布
        var canvasSprite = new ImageCanvas(roomInfo.Size.X * GameConfig.TileCellSize, roomInfo.Size.Y * GameConfig.TileCellSize);
        canvasSprite.Position = roomInfo.Position;
        roomInfo.StaticImageCanvas = canvasSprite;
        roomInfo.StaticSprite.AddChild(canvasSprite);
        yield return 0;
        
        //液体画布
        var liquidCanvas = new LiquidCanvas(roomInfo, roomInfo.Size.X * GameConfig.TileCellSize, roomInfo.Size.Y * GameConfig.TileCellSize);
        liquidCanvas.Position = roomInfo.Position;
        roomInfo.LiquidCanvas = liquidCanvas;
        roomInfo.StaticSprite.AddChild(liquidCanvas);
        yield return 0;
        
        //创建玩家
        var player = ActivityObject.Create<Player>(ActivityObject.Ids.Id_role0001);
        player.Name = "Player";
        player.Position = hall.BirthMark.Position;
        player.PutDown(RoomLayerEnum.YSortLayer);
        affiliation.InsertItem(player);
        Player.SetCurrentPlayer(player);
        player.WeaponPack.PickupItem(ActivityObject.Create<Weapon>(ActivityObject.Ids.Id_weapon0001));
        GameApplication.Instance.Cursor.SetGuiMode(false);
        yield return 0;

        //打开游戏中的ui
        UiManager.Open_RoomUI();
        UiManager.Destroy_Loading();

        if (finish != null)
        {
            finish();
        }
    }
    
    private IEnumerator RunExitHallCoroutine(Action finish)
    {
        //打开 loading UI
        UiManager.Open_Loading();
        yield return 0;
        
        CurrWorld.Pause = true;
        yield return 0;

        var hall = (Hall)CurrWorld;
        hall.RoomInfo.Destroy();
        yield return 0;
        
        UiManager.Destroy_RoomUI();
        yield return 0;
        Player.SetCurrentPlayer(null);
        DestroyWorld();
        yield return 0;
        FogMaskHandler.ClearRecordRoom();
        LiquidBrushManager.ClearData();
        BrushImageData.ClearBrushData();
        QueueRedraw();
        
        //鼠标还原
        GameApplication.Instance.Cursor.SetGuiMode(true);
        yield return 0;
        
        //关闭 loading UI
        UiManager.Destroy_Loading();
        if (finish != null)
        {
            finish();
        }
    }
    

    //执行加载地牢协程
    private IEnumerator RunLoadDungeonCoroutine(Action finish)
    {
        //打开 loading UI
        UiManager.Open_Loading();
        yield return 0;
        //生成地牢房间
        
        //最多尝试10次
        const int maxCount = 10;
        for (var i = 0; i < maxCount; i++)
        {
            SeedRandom random;
            if (CurrConfig.RandomSeed != null)
            {
                random = new SeedRandom(CurrConfig.RandomSeed.Value);
            }
            else
            {
                random = new SeedRandom();
            }
       
            var dungeonGenerator = new DungeonGenerator(CurrConfig, random);
            var rule = new DefaultDungeonRule(dungeonGenerator);
            if (!dungeonGenerator.Generate(rule)) //生成房间失败
            {
                dungeonGenerator.EachRoom(DisposeRoomInfo);
                UiManager.Destroy_Loading();
            
                if (IsEditorMode) //在编辑器模式下打开的Ui
                {
                    EditorPlayManager.IsPlay = false;
                    IsEditorMode = false;
                    //显示上一个Ui
                    if (_prevUi != null)
                    {
                        _prevUi.ShowUi();
                    }
                }
                else //正常关闭Ui
                {
                    UiManager.Open_Main();
                }

                if (i == maxCount - 1)
                {
                    EditorWindowManager.ShowTips("错误", "生成房间尝试次数过多，生成地牢房间失败，请加大房间门连接区域，或者修改地牢生成规则！");
                    yield break;
                }
                else
                {
                    yield return 0;
                }
            }
            else //生成成功!
            {
                _dungeonGenerator = dungeonGenerator;
                break;
            }
        }
        
        yield return 0;
        //创建世界场景
        CurrWorld = CreateNewWorld(_dungeonGenerator.Random);
        yield return 0;
        var group = GameApplication.Instance.RoomConfig[CurrConfig.GroupName];
        var tileSetSplit = GameApplication.Instance.TileSetConfig[group.TileSet];
        CurrWorld.TileRoot.TileSet = tileSetSplit.GetTileSet();
        //填充地牢
        AutoTileConfig = new AutoTileConfig(0, tileSetSplit.TileSetInfo.Sources[0].Terrain[0]);
        _dungeonTileMap = new DungeonTileMap(CurrWorld.TileRoot);
        yield return _dungeonTileMap.AutoFillRoomTile(AutoTileConfig, _dungeonGenerator.StartRoomInfo, CurrWorld);
        yield return _dungeonTileMap.AutoFillAisleTile(AutoTileConfig, _dungeonGenerator.StartRoomInfo, CurrWorld);
        //yield return _dungeonTileMap.AddOutlineTile(AutoTileConfig.WALL_BLOCK);
        yield return 0;
        //生成墙壁, 生成导航网格
        _dungeonTileMap.GenerateWallAndNavigation(CurrWorld, AutoTileConfig);
        yield return 0;
        //初始化所有房间
        yield return _dungeonGenerator.EachRoomCoroutine(InitRoom);

        //播放bgm
        //SoundManager.PlayMusic(ResourcePath.resource_sound_bgm_Intro_ogg, -17f);

        //地牢加载即将完成
        yield return _dungeonGenerator.EachRoomCoroutine(info => info.OnReady());
        
        //初始房间创建玩家标记
        var playerBirthMark = StartRoomInfo.RoomPreinstall.GetPlayerBirthMark();
        //创建玩家
        var player = ActivityObject.Create<Player>(ActivityObject.Ids.Id_role0001);
        if (playerBirthMark != null)
        {
            //player.Position = new Vector2(50, 50);
            player.Position = playerBirthMark.Position;
        }
        player.Name = "Player";
        player.PutDown(RoomLayerEnum.YSortLayer);
        Player.SetCurrentPlayer(player);
        GameApplication.Instance.Cursor.SetGuiMode(false);
        //打开游戏中的ui
        UiManager.Open_RoomUI();
        //派发进入地牢事件
        EventManager.EmitEvent(EventEnum.OnEnterDungeon);
        
        IsInDungeon = true;
        QueueRedraw();
        yield return 0;
        //关闭 loading UI
        UiManager.Destroy_Loading();
        if (finish != null)
        {
            finish();
        }
    }

    //执行退出地牢流程
    private IEnumerator RunExitDungeonCoroutine(Action finish)
    {
        //打开 loading UI
        UiManager.Open_Loading();
        yield return 0;
        CurrWorld.Pause = true;
        yield return 0;
        _dungeonGenerator.EachRoom(DisposeRoomInfo);
        yield return 0;
        _dungeonTileMap = null;
        AutoTileConfig = null;
        _dungeonGenerator = null;
        
        UiManager.Destroy_RoomUI();
        yield return 0;
        Player.SetCurrentPlayer(null);
        DestroyWorld();
        yield return 0;
        FogMaskHandler.ClearRecordRoom();
        LiquidBrushManager.ClearData();
        BrushImageData.ClearBrushData();
        QueueRedraw();
        //鼠标还原
        GameApplication.Instance.Cursor.SetGuiMode(true);
        //派发退出地牢事件
        EventManager.EmitEvent(EventEnum.OnExitDungeon);
        yield return 0;
        //关闭 loading UI
        UiManager.Destroy_Loading();
        if (finish != null)
        {
            finish();
        }
    }

    // 初始化房间
    private void InitRoom(RoomInfo roomInfo)
    {
        roomInfo.CalcRange();
        //创建门
        CreateDoor(roomInfo);
        //创建房间归属区域
        CreateRoomAffiliation(roomInfo);
        //创建 RoomStaticSprite
        CreateRoomStaticSprite(roomInfo);
        //创建静态精灵画布
        CreateRoomStaticImageCanvas(roomInfo);
        //创建液体区域
        CreateRoomLiquidCanvas(roomInfo);
        //创建迷雾遮罩
        CreateRoomFogMask(roomInfo);
        //创建房间/过道预览sprite
        CreatePreviewSprite(roomInfo);
    }

    //创建门
    private void CreateDoor(RoomInfo roomInfo)
    {
        foreach (var doorInfo in roomInfo.Doors)
        {
            RoomDoor door;
            switch (doorInfo.Direction)
            {
                case DoorDirection.E:
                    door = ActivityObject.Create<RoomDoor>(ActivityObject.Ids.Id_other_door_e);
                    door.Position = (doorInfo.OriginPosition + new Vector2(0.5f, 2)) * GameConfig.TileCellSize;
                    door.ZIndex = MapLayer.AutoTopLayer;
                    break;
                case DoorDirection.W:
                    door = ActivityObject.Create<RoomDoor>(ActivityObject.Ids.Id_other_door_w);
                    door.Position = (doorInfo.OriginPosition + new Vector2(-0.5f, 2)) * GameConfig.TileCellSize;
                    door.ZIndex = MapLayer.AutoTopLayer;
                    break;
                case DoorDirection.S:
                    door = ActivityObject.Create<RoomDoor>(ActivityObject.Ids.Id_other_door_s);
                    door.Position = (doorInfo.OriginPosition + new Vector2(2f, 1.5f)) * GameConfig.TileCellSize;
                    door.ZIndex = MapLayer.AutoTopLayer;
                    break;
                case DoorDirection.N:
                    door = ActivityObject.Create<RoomDoor>(ActivityObject.Ids.Id_other_door_n);
                    door.Position = (doorInfo.OriginPosition + new Vector2(2f, -0.5f)) * GameConfig.TileCellSize;
                    door.ZIndex = MapLayer.AutoMiddleLayer;
                    break;
                default:
                    return;
            }
            doorInfo.Door = door;
            door.Init(doorInfo);
            door.PutDown(RoomLayerEnum.NormalLayer, false);
        }
    }

    //创建房间归属区域
    private void CreateRoomAffiliation(RoomInfo roomInfo)
    {
        var affiliation = new AffiliationArea();
        affiliation.Name = "AffiliationArea" + roomInfo.Id;
        affiliation.Init(roomInfo, new Rect2I(
            roomInfo.GetWorldPosition() + new Vector2I(GameConfig.TileCellSize * 2, GameConfig.TileCellSize * 3),
            (roomInfo.Size - new Vector2I(4, 5)) * GameConfig.TileCellSize));
        
        roomInfo.AffiliationArea = affiliation;
        CurrWorld.AffiliationAreaRoot.AddChild(affiliation);
    }

    //创建 RoomStaticSprite
    private void CreateRoomStaticSprite(RoomInfo roomInfo)
    {
        var spriteRoot = new RoomStaticSprite(roomInfo);
        spriteRoot.Name = "SpriteRoot";
        World.Current.StaticSpriteRoot.AddChild(spriteRoot);
        roomInfo.StaticSprite = spriteRoot;
    }
    
    
    //创建液体画布
    private void CreateRoomLiquidCanvas(RoomInfo roomInfo)
    {
        var rect = roomInfo.CanvasRect;

        var liquidCanvas = new LiquidCanvas(roomInfo, rect.Size.X, rect.Size.Y);
        liquidCanvas.Position = rect.Position;
        roomInfo.LiquidCanvas = liquidCanvas;
        roomInfo.StaticSprite.AddChild(liquidCanvas);
    }

    //创建静态图像画布
    private void CreateRoomStaticImageCanvas(RoomInfo roomInfo)
    {
        var rect = roomInfo.CanvasRect;

        var canvasSprite = new ImageCanvas(rect.Size.X, rect.Size.Y);
        canvasSprite.Position = rect.Position;
        roomInfo.StaticImageCanvas = canvasSprite;
        roomInfo.StaticSprite.AddChild(canvasSprite);
    }

    //创建迷雾遮罩
    private void CreateRoomFogMask(RoomInfo roomInfo)
    {
        var roomFog = new FogMask();
        roomFog.Name = "FogMask" + roomFog.IsDestroyed;
        roomFog.InitFog(roomInfo.Position + new Vector2I(1, 0), roomInfo.Size - new Vector2I(2, 1));
        //roomFog.InitFog(roomInfo.Position + new Vector2I(1, 1), roomInfo.Size - new Vector2I(2, 2));

        CurrWorld.FogMaskRoot.AddChild(roomFog);
        roomInfo.RoomFogMask = roomFog;
        
        //生成通道迷雾
        foreach (var roomDoorInfo in roomInfo.Doors)
        {
            //必须是正向门
            if (roomDoorInfo.IsForward)
            {
                Rect2I calcRect;
                Rect2I fogAreaRect;
                if (!roomDoorInfo.HasCross)
                {
                    calcRect = roomDoorInfo.GetAisleRect();
                    fogAreaRect = calcRect;
                    if (roomDoorInfo.Direction == DoorDirection.E || roomDoorInfo.Direction == DoorDirection.W)
                    {
                        calcRect.Position += new Vector2I(2, 0);
                        calcRect.Size -= new Vector2I(4, 0);
                    }
                    else
                    {
                        calcRect.Position += new Vector2I(0, 2);
                        calcRect.Size -= new Vector2I(0, 5);
                    }
                }
                else
                {
                    var aisleRect = roomDoorInfo.GetCrossAisleRect();
                    calcRect = aisleRect.CalcAisleRect();
                    fogAreaRect = calcRect;

                    if (roomDoorInfo.Direction == DoorDirection.E)
                    {
                        if (roomDoorInfo.ConnectDoor.Direction == DoorDirection.N) //→↑
                        {
                            calcRect.Position += new Vector2I(2, 0);
                            calcRect.Size -= new Vector2I(2, 4);
                        }
                        else //→↓
                        {
                            calcRect.Position += new Vector2I(2, 3);
                            calcRect.Size -= new Vector2I(2, 3);
                        }
                    }
                    else if (roomDoorInfo.Direction == DoorDirection.W)
                    {
                        if (roomDoorInfo.ConnectDoor.Direction == DoorDirection.N) //←↑
                        {
                            calcRect.Size -= new Vector2I(2, 4);
                        }
                        else //←↓
                        {
                            calcRect.Position += new Vector2I(0, 3);
                            calcRect.Size -= new Vector2I(2, 3);
                        }
                    }
                    else if (roomDoorInfo.Direction == DoorDirection.N)
                    {
                        if (roomDoorInfo.ConnectDoor.Direction == DoorDirection.E) //↑→
                        {
                            calcRect.Position += new Vector2I(2, -1);
                            calcRect.Size -= new Vector2I(2, 2);
                        }
                        else //↑←
                        {
                            calcRect.Position += new Vector2I(0, -1);
                            calcRect.Size -= new Vector2I(2, 2);
                        }
                    }
                    else if (roomDoorInfo.Direction == DoorDirection.S)
                    {
                        if (roomDoorInfo.ConnectDoor.Direction == DoorDirection.E) //↓→
                        {
                            calcRect.Position += new Vector2I(2, 2);
                            calcRect.Size -= new Vector2I(2, 1);
                        }
                        else //↓←
                        {
                            calcRect.Position += new Vector2I(0, 2);
                            calcRect.Size -= new Vector2I(2, 1);
                        }
                    }
                }

                //过道迷雾遮罩
                var aisleFog = new FogMask();
                var calcRectSize = calcRect.Size;
                var calcRectPosition = calcRect.Position;
                if (roomDoorInfo.Direction == DoorDirection.N || roomDoorInfo.Direction == DoorDirection.S)
                {
                    calcRectSize.Y -= 1;
                }
                else
                {
                    calcRectPosition.Y -= 1;
                    calcRectSize.Y += 1;
                }

                aisleFog.InitFog(calcRectPosition, calcRectSize);
                CurrWorld.FogMaskRoot.AddChild(aisleFog);
                roomDoorInfo.AisleFogMask = aisleFog;
                roomDoorInfo.ConnectDoor.AisleFogMask = aisleFog;

                //过道迷雾区域
                var fogArea = new AisleFogArea();
                fogArea.Init(roomDoorInfo,
                    new Rect2I(
                        fogAreaRect.Position * GameConfig.TileCellSize,
                        fogAreaRect.Size * GameConfig.TileCellSize
                    )
                );
                roomDoorInfo.AisleFogArea = fogArea;
                roomDoorInfo.ConnectDoor.AisleFogArea = fogArea;
                CurrWorld.AffiliationAreaRoot.AddChild(fogArea);
            }

            //预览迷雾区域
            var previewRoomFog = new PreviewFogMask();
            roomDoorInfo.PreviewRoomFogMask = previewRoomFog;
            previewRoomFog.Init(roomDoorInfo, PreviewFogMask.PreviewFogType.Room);
            previewRoomFog.SetActive(false);
            CurrWorld.FogMaskRoot.AddChild(previewRoomFog);
            
            var previewAisleFog = new PreviewFogMask();
            roomDoorInfo.PreviewAisleFogMask = previewAisleFog;
            previewAisleFog.Init(roomDoorInfo, PreviewFogMask.PreviewFogType.Aisle);
            previewAisleFog.SetActive(false);
            CurrWorld.FogMaskRoot.AddChild(previewAisleFog);
        }
    }

    private void CreatePreviewSprite(RoomInfo roomInfo)
    {
        //房间区域
        var sprite = new TextureRect();
        //sprite.Centered = false;
        sprite.Texture = roomInfo.PreviewTexture;
        sprite.Position = roomInfo.Position + new Vector2I(1, 3);
        var material = ResourceManager.Load<ShaderMaterial>(ResourcePath.resource_material_Outline2_tres, false);
        material.SetShaderParameter("outline_color", new Color(1, 1, 1, 0.9f));
        material.SetShaderParameter("scale", 0.5f);
        sprite.Material = material;
        roomInfo.PreviewSprite = sprite;
        
        //过道
        if (roomInfo.Doors != null)
        {
            foreach (var doorInfo in roomInfo.Doors)
            {
                if (doorInfo.IsForward)
                {
                    var aisleSprite = new TextureRect();
                    //aisleSprite.Centered = false;
                    aisleSprite.Texture = doorInfo.AislePreviewTexture;
                    //调整过道预览位置
                    if (doorInfo.Direction == DoorDirection.N || doorInfo.Direction == DoorDirection.S ||
                        doorInfo.ConnectDoor.Direction == DoorDirection.N || doorInfo.ConnectDoor.Direction == DoorDirection.S)
                    {
                        aisleSprite.Position = doorInfo.AisleFloorRect.Position + new Vector2I(0, 1);
                    }
                    else
                    {
                        aisleSprite.Position = doorInfo.AisleFloorRect.Position;
                    }

                    // var aisleSpriteMaterial = ResourceManager.Load<ShaderMaterial>(ResourcePath.resource_material_Outline2_tres, false);
                    // aisleSpriteMaterial.SetShaderParameter("outline_color", new Color(1, 1, 1, 0.9f));
                    // aisleSpriteMaterial.SetShaderParameter("scale", 0.5f);
                    // aisleSprite.Material = aisleSpriteMaterial;
                    doorInfo.AislePreviewSprite = aisleSprite;
                    doorInfo.ConnectDoor.AislePreviewSprite = aisleSprite;
                }
            }
        }
    }
    
    /// <summary>
    /// 玩家第一次进入某个房间回调
    /// </summary>
    private void OnPlayerFirstEnterRoom(object o)
    {
        var room = (RoomInfo)o;
        room.OnFirstEnter();
        //如果关门了, 那么房间外的敌人就会丢失目标
        if (room.IsSeclusion)
        {
            var playerAffiliationArea = Player.Current.AffiliationArea;
            foreach (var enemy in CurrWorld.Enemy_InstanceList)
            {
                //不与玩家处于同一个房间
                if (!enemy.IsDestroyed && enemy.AffiliationArea != playerAffiliationArea)
                {
                    if (enemy.StateController.CurrState != AIStateEnum.AiNormal)
                    {
                        enemy.StateController.ChangeState(AIStateEnum.AiNormal);
                    }
                }
            }
        }
    }

    /// <summary>
    /// 玩家进入某个房间回调
    /// </summary>
    private void OnPlayerEnterRoom(object o)
    {
        var roomInfo = (RoomInfo)o;
        if (_affiliationAreaFlag != roomInfo.AffiliationArea)
        {
            if (!roomInfo.AffiliationArea.IsDestroyed)
            {
                //刷新迷雾
                FogMaskHandler.RefreshRoomFog(roomInfo);
            }

            _affiliationAreaFlag = roomInfo.AffiliationArea;
        }
    }
    
    /// <summary>
    /// 检测当前房间敌人是否已经消灭干净, 应当每秒执行一次
    /// </summary>
    private void OnCheckEnemy()
    {
        var activeRoom = ActiveRoomInfo;
        if (activeRoom != null)
        {
            if (activeRoom.RoomPreinstall.IsRunWave) //正在生成标记
            {
                if (activeRoom.RoomPreinstall.IsCurrWaveOver()) //所有标记执行完成
                {
                    //房间内是否有存活的敌人
                    var flag = ActiveAffiliationArea.ExistEnterItem(
                        activityObject => activityObject.CollisionWithMask(PhysicsLayer.Enemy)
                    );
                    //Debug.Log("当前房间存活数量: " + count);
                    if (!flag)
                    {
                        activeRoom.OnClearRoom();
                    }
                }
            }
        }
    }

    private void DisposeRoomInfo(RoomInfo roomInfo)
    {
        roomInfo.Destroy();
    }
    
    public override void _Draw()
    {
        if (ActivityObject.IsDebug)
        {
            StartRoomInfo?.EachRoom(info =>
            {
                DrawRect(new Rect2(info.Waypoints * GameConfig.TileCellSize, new Vector2(16, 16)), Colors.Red);
            });
            //绘制房间区域
            if (_dungeonGenerator != null)
            {
                DrawRoomInfo(StartRoomInfo);
            }
            //绘制边缘线

        }
    }
    
    //绘制房间区域, debug 用
    private void DrawRoomInfo(RoomInfo roomInfo)
    {
        var cellSize = GameConfig.TileCellSize;
        var pos1 = (roomInfo.Position + roomInfo.Size / 2) * cellSize;
        
        //绘制下一个房间
        foreach (var nextRoom in roomInfo.Next)
        {
            var pos2 = (nextRoom.Position + nextRoom.Size / 2) * cellSize;
            DrawLine(pos1, pos2, Colors.Red);
            DrawRoomInfo(nextRoom);
        }

        DrawString(ResourceManager.DefaultFont16Px, pos1 - new Vector2I(0, 10), "Id: " + roomInfo.Id.ToString());
        DrawString(ResourceManager.DefaultFont16Px, pos1 + new Vector2I(0, 10), "Layer: " + roomInfo.Layer.ToString());

        //绘制门
        foreach (var roomDoor in roomInfo.Doors)
        {
            var originPos = roomDoor.OriginPosition * cellSize;
            switch (roomDoor.Direction)
            {
                case DoorDirection.E:
                    DrawLine(originPos, originPos + new Vector2(3, 0) * cellSize, Colors.Yellow);
                    DrawLine(originPos + new Vector2(0, 4) * cellSize, originPos + new Vector2(3, 4) * cellSize,
                        Colors.Yellow);
                    break;
                case DoorDirection.W:
                    DrawLine(originPos, originPos - new Vector2(3, 0) * cellSize, Colors.Yellow);
                    DrawLine(originPos + new Vector2(0, 4) * cellSize, originPos - new Vector2(3, -4) * cellSize,
                        Colors.Yellow);
                    break;
                case DoorDirection.S:
                    DrawLine(originPos, originPos + new Vector2(0, 3) * cellSize, Colors.Yellow);
                    DrawLine(originPos + new Vector2(4, 0) * cellSize, originPos + new Vector2(4, 3) * cellSize,
                        Colors.Yellow);
                    break;
                case DoorDirection.N:
                    DrawLine(originPos, originPos - new Vector2(0, 3) * cellSize, Colors.Yellow);
                    DrawLine(originPos + new Vector2(4, 0) * cellSize, originPos - new Vector2(-4, 3) * cellSize,
                        Colors.Yellow);
                    break;
            }
            
            //绘制房间区域
            DrawRect(new Rect2(roomInfo.Position * cellSize, roomInfo.Size * cellSize), Colors.Blue, false);

            if (roomDoor.HasCross && roomDoor.RoomInfo.Id < roomDoor.ConnectRoom.Id)
            {
                DrawRect(new Rect2(roomDoor.Cross * cellSize, new Vector2(cellSize * 4, cellSize * 4)), Colors.Yellow, false);
            }
        }
    }
    
    /// <summary>
    /// 将房间类型枚举转为字符串
    /// </summary>
    public static string DungeonRoomTypeToString(DungeonRoomType roomType)
    {
        switch (roomType)
        {
            case DungeonRoomType.Battle: return "battle";
            case DungeonRoomType.Inlet: return "inlet";
            case DungeonRoomType.Outlet: return "outlet";
            case DungeonRoomType.Boss: return "boss";
            case DungeonRoomType.Reward: return "reward";
            case DungeonRoomType.Shop: return "shop";
            case DungeonRoomType.Event: return "event";
        }

        return "battle";
    }
    
        
    /// <summary>
    /// 将房间类型枚举转为描述字符串
    /// </summary>
    public static string DungeonRoomTypeToDescribeString(DungeonRoomType roomType)
    {
        switch (roomType)
        {
            case DungeonRoomType.Battle: return "战斗房间";
            case DungeonRoomType.Inlet: return "起始房间";
            case DungeonRoomType.Outlet: return "结束房间";
            case DungeonRoomType.Boss: return "Boss房间";
            case DungeonRoomType.Reward: return "奖励房间";
            case DungeonRoomType.Shop: return "商店房间";
            case DungeonRoomType.Event: return "事件房间";
        }

        return "战斗房间";
    }

    /// <summary>
    /// 检测地牢是否可以执行生成
    /// </summary>
    /// <param name="groupName">组名称</param>
    public static DungeonCheckState CheckDungeon(string groupName)
    {
        if (GameApplication.Instance.RoomConfig.TryGetValue(groupName, out var group))
        {
            //验证该组是否满足生成地牢的条件
            if (group.InletList.Count == 0)
            {
                return new DungeonCheckState(true, "当没有可用的起始房间!");
            }
            else if (group.OutletList.Count == 0)
            {
                return new DungeonCheckState(true, "没有可用的结束房间!");
            }
            else if (group.BattleList.Count == 0)
            {
                return new DungeonCheckState(true, "没有可用的战斗房间!");
            }

            return new DungeonCheckState(false, null);
        }

        return new DungeonCheckState(true, "未找到地牢组");
    }
}