using System;
using Godot;

public class TestGenerateDungeon : Node2D
{
	[Export]
	public NodePath TileMapPath;

	[Export]
	public NodePath Camera;
	
	private TileMap _tileMap;
	private Camera2D _camera;

	private GenerateDungeon _generateDungeon;
	private Font _font;

	public override void _Ready()
	{
		GD.Randomize();
		_tileMap = GetNode<TileMap>(TileMapPath);
		_camera = GetNode<Camera2D>(Camera);

		_font = ResourceManager.Load<Font>(ResourcePath.resource_font_cn_font_36_tres);
		
		_generateDungeon = new GenerateDungeon(_tileMap);
		var nowTicks = DateTime.Now.Ticks;
		_generateDungeon.Generate();
		GD.Print("useTime: " + (DateTime.Now.Ticks - nowTicks) / 10000 + "毫秒");
		
	}

	public override void _Process(float delta)
	{
		//移动相机位置
		var dir = Input.GetVector("move_left", "move_right", "move_up", "move_down");
		_camera.Position += dir * 500 * delta;
		
		Update();
	}

	public override void _Draw()
	{

		DrawRoomInfo(_generateDungeon.StartRoom);

	}

	private void DrawRoomInfo(RoomInfo roomInfo)
	{
		var pos1 = (roomInfo.Position + roomInfo.Size / 2) * _tileMap.CellSize;
		foreach (var info in roomInfo.Next)
		{
			var pos2 = (info.Position + info.Size / 2) * _tileMap.CellSize;
			DrawLine(pos1, pos2, Colors.Red);
			DrawRoomInfo(info);
		}
		DrawString(_font, pos1, roomInfo.Id.ToString(), Colors.Yellow);
	}
}