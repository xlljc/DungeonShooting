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
		var dir = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		_camera.Position += dir * 1000 * delta;
		
		Update();
	}

	public override void _Draw()
	{

		DrawRoomInfo(_generateDungeon.StartRoom);

	}

	private void DrawRoomInfo(RoomInfo room)
	{
		var cellSize = _tileMap.CellSize;
		var pos1 = (room.Position + room.Size / 2) * cellSize;
		foreach (var nextRoom in room.Next)
		{
			var pos2 = (nextRoom.Position + nextRoom.Size / 2) * cellSize;
			DrawLine(pos1, pos2, Colors.Red);
			DrawRoomInfo(nextRoom);
		}
		DrawString(_font, pos1, room.Id.ToString(), Colors.Yellow);
		
		foreach (var roomDoor in room.Doors)
		{
			var originPos = roomDoor.OriginPosition * cellSize;
			switch (roomDoor.Direction)
			{
				case DoorDirection.E:
					DrawLine(originPos, originPos + new Vector2(3, 0) * cellSize, Colors.Yellow);
					DrawLine(originPos + new Vector2(0, 4) * cellSize, originPos + new Vector2(3, 4) * cellSize, Colors.Yellow);
					break;
				case DoorDirection.W:
					DrawLine(originPos, originPos - new Vector2(3, 0) * cellSize, Colors.Yellow);
					DrawLine(originPos + new Vector2(0, 4) * cellSize, originPos - new Vector2(3, -4) * cellSize, Colors.Yellow);
					break;
				case DoorDirection.S:
					DrawLine(originPos, originPos + new Vector2(0, 3) * cellSize, Colors.Yellow);
					DrawLine(originPos + new Vector2(4, 0) * cellSize, originPos + new Vector2(4, 3) * cellSize, Colors.Yellow);
					break;
				case DoorDirection.N:
					DrawLine(originPos, originPos - new Vector2(0, 3) * cellSize, Colors.Yellow);
					DrawLine(originPos + new Vector2(4, 0) * cellSize, originPos - new Vector2(-4, 3) * cellSize, Colors.Yellow);
					break;
			}
		}
	}
}