
using System.Collections.Generic;
using System.Text.Json;
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
	
	/// <summary>
	/// 房间配置
	/// </summary>
	public List<DungeonRoomSplit> RoomConfig { get; private set; }

	public GameApplication()
	{
		Instance = this;
		
		InitRoomConfig();

		//初始化 ActivityObject
		ActivityObject.InitActivity();
	}
	
	public override void _EnterTree()
	{
		//随机化种子
		//GD.Randomize();
		//固定帧率
		Engine.MaxFps = 60;
		//调试绘制开关
		ActivityObject.IsDebug = Debug;
		//Engine.TimeScale = 0.3f;

		GlobalNodeRoot = GetNode<Node2D>(GlobalNodeRootPath);
		// 初始化鼠标
		Input.MouseMode = Input.MouseModeEnum.Hidden;
		Cursor = CursorPack.Instantiate<Cursor>();

		RoomManager = GetNode<RoomManager>(RoomPath);
		SubViewport = GetNode<SubViewport>(ViewportPath);
		SubViewportContainer = GetNode<SubViewportContainer>(ViewportContainerPath);
		Ui = GetNode<RoomUI>(UiPath);

		Ui.AddChild(Cursor);
	}

	public override void _Process(double delta)
	{
		InputManager.Update((float)delta);
	}

	/// <summary>
	/// 将 viewport 以外的全局坐标 转换成 viewport 内的全局坐标
	/// </summary>
	public Vector2 GlobalToViewPosition(Vector2 globalPos)
	{
		//return globalPos;
		return globalPos / GameConfig.WindowScale - (GameConfig.ViewportSize / 2) + GameCamera.Main.GlobalPosition;
	}

	/// <summary>
	/// 将 viewport 以内的全局坐标 转换成 viewport 外的全局坐标
	/// </summary>
	public Vector2 ViewToGlobalPosition(Vector2 viewPos)
	{
		// 3.5写法
		//return (viewPos - GameCamera.Main.GlobalPosition + (GameConfig.ViewportSize / 2)) * GameConfig.WindowScale - GameCamera.Main.SubPixelPosition;
		return (viewPos - GameCamera.Main.GlobalPosition + (GameConfig.ViewportSize / 2)) * GameConfig.WindowScale;
	}

	//初始化房间配置
	private void InitRoomConfig()
	{
		//加载房间配置信息
		var file = FileAccess.Open(ResourcePath.resource_map_RoomConfig_json, FileAccess.ModeFlags.Read);
		var asText = file.GetAsText();
		RoomConfig = JsonSerializer.Deserialize<List<DungeonRoomSplit>>(asText);
		file.Dispose();
		
		//需要处理 DoorAreaInfos 长度为 0 的房间, 并为其配置默认值
		foreach (var roomSplit in RoomConfig)
		{
			var areaInfos = roomSplit.RoomInfo.DoorAreaInfos;
			if (areaInfos.Count == 0)
			{
				areaInfos.Add(new DoorAreaInfo(DoorDirection.N, GameConfig.TileCellSize, (roomSplit.RoomInfo.Size.X - 2) * GameConfig.TileCellSize));
				areaInfos.Add(new DoorAreaInfo(DoorDirection.S, GameConfig.TileCellSize, (roomSplit.RoomInfo.Size.X - 2) * GameConfig.TileCellSize));
				areaInfos.Add(new DoorAreaInfo(DoorDirection.W, GameConfig.TileCellSize, (roomSplit.RoomInfo.Size.Y - 2) * GameConfig.TileCellSize));
				areaInfos.Add(new DoorAreaInfo(DoorDirection.E, GameConfig.TileCellSize, (roomSplit.RoomInfo.Size.Y - 2) * GameConfig.TileCellSize));
			}
		}
	}
}
