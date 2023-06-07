
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using Config;
using Godot;

public partial class GameApplication : Node2D
{
	public static GameApplication Instance { get; private set; }
	
	/// <summary>
	/// 游戏渲染视口
	/// </summary>
	[Export] public SubViewport SubViewport;

	/// <summary>
	/// SubViewportContainer 组件
	/// </summary>
	[Export] public SubViewportContainer SubViewportContainer;

	/// <summary>
	/// 场景根节点
	/// </summary>
	[Export] public Node2D SceneRoot;
	
	/// <summary>
	/// 全局根节点
	/// </summary>
	[Export] public Node2D GlobalNodeRoot;

	/// <summary>
	/// 是否开启调试
	/// </summary>
	[ExportGroup("Debug")]
	[Export] public bool Debug = false;

	/// <summary>
	/// 测试用, 指定生成的房间
	/// </summary>
	[Export]
	public PackedScene[] DesignatedRoom;

	/// <summary>
	/// 鼠标指针
	/// </summary>
	public Cursor Cursor { get; private set; }
	
	/// <summary>
	/// 游戏世界
	/// </summary>
	public World World { get; private set; }

	/// <summary>
	/// 地牢管理器
	/// </summary>
	public DungeonManager DungeonManager { get; private set; }
	
	/// <summary>
	/// 房间配置
	/// </summary>
	public Dictionary<string, DungeonRoomGroup> RoomConfig { get; private set; }
	
	/// <summary>
	/// 房间配置数据, key: 模板房间资源路径
	/// </summary>
	public Dictionary<string, DungeonRoomSplit> RoomConfigMap { get; private set; }

	/// <summary>
	/// 游戏视图大小
	/// </summary>
	public Vector2 ViewportSize { get; private set; } = new Vector2(480, 270);
	
	/// <summary>
	/// 像素缩放
	/// </summary>
	public int PixelScale { get; private set; } = 4;
	
	/// <summary>
	/// 地牢配置信息
	/// </summary>
	public DungeonConfig DungeonConfig { get; private set; }

	//开启的协程
	private List<CoroutineData> _coroutineList;
	
	public GameApplication()
	{
		Instance = this;
		
		//初始化配置表
		ExcelConfig.Init();
		//初始化房间配置数据
		InitRoomConfig();
		//初始化 ActivityObject
		ActivityObject.InitActivity();
		//初始化武器数据
		Weapon.InitWeaponAttribute();
		
		DungeonConfig = new DungeonConfig();
		DungeonConfig.GroupName = "testGroup";
		DungeonConfig.RoomCount = 0;
	}
	
	public override void _EnterTree()
	{
		//随机化种子
		//GD.Randomize();
		//固定帧率
		Engine.MaxFps = 60;
		//调试绘制开关
		ActivityObject.IsDebug = Debug;
		//Engine.TimeScale = 0.2f;

		//窗体大小改变
		GetWindow().SizeChanged += OnWindowSizeChanged;
		RefreshSubViewportSize();

#if TOOLS
		InitDesignatedRoom();
#endif
		//初始化ui
		UiManager.Init();
		// 初始化鼠标
		InitCursor();
		//地牢管理器
		DungeonManager = new DungeonManager();
		DungeonManager.Name = "DungeonManager";
		SceneRoot.AddChild(DungeonManager);
		//打开主菜单Ui
		UiManager.Open_Main();
	}

	public override void _Process(double delta)
	{
		var newDelta = (float)delta;
		InputManager.Update(newDelta);
		
		//协程更新
		if (_coroutineList != null)
		{
			ProxyCoroutineHandler.ProxyUpdateCoroutine(ref _coroutineList, newDelta);
		}
	}

	/// <summary>
	/// 创建新的 World 对象, 相当于清理房间
	/// </summary>
	public World CreateNewWorld()
	{
		if (World != null)
		{
			World.QueueFree();
		}
		World = ResourceManager.LoadAndInstantiate<World>(ResourcePath.scene_World_tscn);
		SceneRoot.AddChild(World);
		return World;
	}

	/// <summary>
	/// 销毁 World 对象, 相当于清理房间
	/// </summary>
	public void DestroyWorld()
	{
		if (World != null)
		{
			World.QueueFree();
		}

		World = null;
	}
	
	/// <summary>
	/// 将 viewport 以外的全局坐标 转换成 viewport 内的全局坐标
	/// </summary>
	public Vector2 GlobalToViewPosition(Vector2 globalPos)
	{
		//return globalPos;
		return globalPos / PixelScale - (ViewportSize / 2) + GameCamera.Main.GlobalPosition;
	}

	/// <summary>
	/// 将 viewport 以内的全局坐标 转换成 viewport 外的全局坐标
	/// </summary>
	public Vector2 ViewToGlobalPosition(Vector2 viewPos)
	{
		// 3.5写法
		//return (viewPos - GameCamera.Main.GlobalPosition + (GameConfig.ViewportSize / 2)) * GameConfig.WindowScale - GameCamera.Main.SubPixelPosition;
		return (viewPos - (GameCamera.Main.GlobalPosition + GameCamera.Main.Offset) + (ViewportSize / 2)) * PixelScale;
	}
	
	/// <summary>
	/// 开启一个协程, 返回协程 id, 协程是在普通帧执行的, 支持: 协程嵌套, WaitForSeconds, WaitForFixedProcess, Task, SignalAwaiter
	/// </summary>
	public long StartCoroutine(IEnumerator able)
	{
		return ProxyCoroutineHandler.ProxyStartCoroutine(ref _coroutineList, able);
	}

	/// <summary>
	/// 根据协程 id 停止协程
	/// </summary>
	public void StopCoroutine(long coroutineId)
	{
		ProxyCoroutineHandler.ProxyStopCoroutine(ref _coroutineList, coroutineId);
	}
    
	/// <summary>
	/// 停止所有协程
	/// </summary>
	public void StopAllCoroutine()
	{
		ProxyCoroutineHandler.ProxyStopAllCoroutine(ref _coroutineList);
	}

	//初始化房间配置
	private void InitRoomConfig()
	{
		//加载房间配置信息
		var file = FileAccess.Open(ResourcePath.resource_map_RoomConfig_json, FileAccess.ModeFlags.Read);
		var asText = file.GetAsText();
		RoomConfig = JsonSerializer.Deserialize<Dictionary<string, DungeonRoomGroup>>(asText);
		file.Dispose();

		//初始化RoomConfigMap
		RoomConfigMap = new Dictionary<string, DungeonRoomSplit>();
		foreach (var dungeonRoomGroup in RoomConfig)
		{
			foreach (var dungeonRoomType in Enum.GetValues<DungeonRoomType>())
			{
				foreach (var dungeonRoomSplit in dungeonRoomGroup.Value.GetRoomList(dungeonRoomType))
				{
					RoomConfigMap.Add(dungeonRoomSplit.ScenePath, dungeonRoomSplit);
				}
			}
		}
	}

	//窗体大小改变
	private void OnWindowSizeChanged()
	{
		var size = GetWindow().Size;
		ViewportSize = size / PixelScale;
		RefreshSubViewportSize();
	}
	
	//刷新视窗大小
	private void RefreshSubViewportSize()
	{
		var s = new Vector2I((int)ViewportSize.X, (int)ViewportSize.Y);
		s.X = s.X / 2 * 2;
		s.Y = s.Y / 2 * 2;
		SubViewport.Size = s;
		SubViewportContainer.Scale = new Vector2(PixelScale, PixelScale);
		SubViewportContainer.Size = s;
	}

	//初始化鼠标
	private void InitCursor()
	{
		Input.MouseMode = Input.MouseModeEnum.Hidden;
		Cursor = ResourceManager.LoadAndInstantiate<Cursor>(ResourcePath.prefab_Cursor_tscn);
		var cursorLayer = new CanvasLayer();
		cursorLayer.Name = "CursorLayer";
		cursorLayer.Layer = UiManager.GetUiLayer(UiLayer.Pop).Layer + 10;
		AddChild(cursorLayer);
		cursorLayer.AddChild(Cursor);
	}

#if TOOLS
	//调试模式下, 指定生成哪些房间
	private void InitDesignatedRoom()
	{
		if (DesignatedRoom != null && DesignatedRoom.Length > 0)
		{
			var list = new List<DungeonRoomSplit>();
			foreach (var packedScene in DesignatedRoom)
			{
				if (RoomConfigMap.TryGetValue(packedScene.ResourcePath, out var dungeonRoomSplit))
				{
					list.Add(dungeonRoomSplit);
				}
			}
			DungeonGenerator.SetDesignatedRoom(list);
		}
	}
#endif

}
