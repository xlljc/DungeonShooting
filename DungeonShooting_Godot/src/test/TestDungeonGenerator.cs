
using Godot;

/// <summary>
/// 测试地牢生成
/// </summary>
public partial class TestDungeonGenerator : Node2D
{
	[Export] public NodePath TileMapPath;
	[Export] public NodePath Camera3D;

	private TileMap _tileMap;
	private Camera2D _camera;

	private DungeonGenerator _dungeonGenerator;
	private Font _font;

	// public override void _Ready()
	// {
	// 	_tileMap = GetNode<TileMap>(TileMapPath);
	// 	_camera = GetNode<Camera2D>(Camera3D);
	//
	// 	_font = ResourceManager.Load<Font>(ResourcePath.resource_font_cn_font_36_tres);
	//
	// 	_generateDungeon = new GenerateDungeon();
	// 	_generateDungeon.Generate();
	//
	// 	foreach (var info in _generateDungeon.RoomInfos)
	// 	{
	// 		//临时铺上地砖
	// 		var id = (int)_tileMap.TileSet.GetTilesIds()[0];
	// 		for (int i = 0; i < info.Size.X; i++)
	// 		{
	// 			for (int j = 0; j < info.Size.Y; j++)
	// 			{
	// 				_tileMap.SetCell(i + (int)info.Position.X, j + (int)info.Position.Y, id);
	// 			}
	// 		}
	// 	}
	// }
	//
	// public override void _Process(float delta)
	// {
	// 	//移动相机位置
	// 	var dir = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
	// 	_camera.Position += dir * 1000 * delta;
	//
	// 	Update();
	// }
	//
	// public override void _Draw()
	// {
	// 	DrawRoomInfo(_generateDungeon.StartRoom);
	//
	// }
	//
	// private void DrawRoomInfo(RoomInfo room)
	// {
	// 	var cellSize = _tileMap.CellSize;
	// 	var pos1 = (room.Position + room.Size / 2) * cellSize;
	// 	foreach (var nextRoom in room.Next)
	// 	{
	// 		var pos2 = (nextRoom.Position + nextRoom.Size / 2) * cellSize;
	// 		DrawLine(pos1, pos2, Colors.Red);
	// 		DrawRoomInfo(nextRoom);
	// 	}
	//
	// 	DrawString(_font, pos1, room.Id.ToString(), Colors.Yellow);
	//
	// 	foreach (var roomDoor in room.Doors)
	// 	{
	// 		var originPos = roomDoor.OriginPosition * cellSize;
	// 		switch (roomDoor.Direction)
	// 		{
	// 			case DoorDirection.E:
	// 				DrawLine(originPos, originPos + new Vector2(3, 0) * cellSize, Colors.Yellow);
	// 				DrawLine(originPos + new Vector2(0, 4) * cellSize, originPos + new Vector2(3, 4) * cellSize,
	// 					Colors.Yellow);
	// 				break;
	// 			case DoorDirection.W:
	// 				DrawLine(originPos, originPos - new Vector2(3, 0) * cellSize, Colors.Yellow);
	// 				DrawLine(originPos + new Vector2(0, 4) * cellSize, originPos - new Vector2(3, -4) * cellSize,
	// 					Colors.Yellow);
	// 				break;
	// 			case DoorDirection.S:
	// 				DrawLine(originPos, originPos + new Vector2(0, 3) * cellSize, Colors.Yellow);
	// 				DrawLine(originPos + new Vector2(4, 0) * cellSize, originPos + new Vector2(4, 3) * cellSize,
	// 					Colors.Yellow);
	// 				break;
	// 			case DoorDirection.N:
	// 				DrawLine(originPos, originPos - new Vector2(0, 3) * cellSize, Colors.Yellow);
	// 				DrawLine(originPos + new Vector2(4, 0) * cellSize, originPos - new Vector2(-4, 3) * cellSize,
	// 					Colors.Yellow);
	// 				break;
	// 		}
	//
	// 		if (roomDoor.HasCross && roomDoor.RoomInfo.Id < roomDoor.ConnectRoom.Id)
	// 		{
	// 			DrawRect(new Rect2(roomDoor.Cross * cellSize, cellSize * 4), Colors.Yellow);
	// 		}
	// 	}
	//
	// 	//绘制地图上被占用的网格
	// 	// _generateDungeon.RoomGrid.ForEach((x, y, value) =>
	// 	// {
	// 	// 	DrawRect(new Rect2(new Vector2(x, y) * cellSize + new Vector2(6, 6), new Vector2(4, 4)), Colors.Green);
	// 	// });
	// }
}