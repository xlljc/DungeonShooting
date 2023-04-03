
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
	/// 鼠标指针
	/// </summary>
	public Cursor Cursor { get; private set; }
	
	/// <summary>
	/// 游戏房间
	/// </summary>
	public RoomManager RoomManager { get; private set; }

	/// <summary>
	/// 房间配置
	/// </summary>
	public Dictionary<string, DungeonRoomGroup> RoomConfig { get; private set; }
	
	/// <summary>
	/// 游戏视图大小
	/// </summary>
	public Vector2 ViewportSize { get; private set; } = new Vector2(480, 270);
	
	/// <summary>
	/// 像素缩放
	/// </summary>
	public int PixelScale { get; private set; } = 4;

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
		//Engine.TimeScale = 0.2f;

		//窗体大小改变
		GetWindow().SizeChanged += OnWindowSizeChanged;
		RefreshSubViewportSize();

		//初始化ui
		UiManager.Init();
		
		// 初始化鼠标
		Input.MouseMode = Input.MouseModeEnum.Hidden;
		Cursor = ResourceManager.Load<PackedScene>(ResourcePath.prefab_Cursor_tscn).Instantiate<Cursor>();
		var cursorLayer = new CanvasLayer();
		cursorLayer.Name = "CursorLayer";
		cursorLayer.Layer = UiManager.GetUiLayer(UiLayer.Pop).Layer + 10;
		AddChild(cursorLayer);
		cursorLayer.AddChild(Cursor);

		//打开ui
		UiManager.Open_RoomUI();
		
		RoomManager = ResourceManager.Load<PackedScene>(ResourcePath.scene_Room_tscn).Instantiate<RoomManager>();
		SceneRoot.AddChild(RoomManager);
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

	//初始化房间配置
	private void InitRoomConfig()
	{
		//加载房间配置信息
		var file = FileAccess.Open(ResourcePath.resource_map_RoomConfig_json, FileAccess.ModeFlags.Read);
		var asText = file.GetAsText();
		RoomConfig = JsonSerializer.Deserialize<Dictionary<string, DungeonRoomGroup>>(asText);
		file.Dispose();
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
}
