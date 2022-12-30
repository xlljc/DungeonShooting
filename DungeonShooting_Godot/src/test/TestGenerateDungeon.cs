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

	public override void _Ready()
	{
		GD.Randomize();
		_tileMap = GetNode<TileMap>(TileMapPath);
		_camera = GetNode<Camera2D>(Camera);

		var temp = new GenerateDungeon(_tileMap);
		var nowTicks = DateTime.Now.Ticks;
		temp.Generate();
		GD.Print("useTime: " + (DateTime.Now.Ticks - nowTicks) / 10000 + "毫秒");
	}

	public override void _Process(float delta)
	{
		//移动相机位置
		var dir = Input.GetVector("move_left", "move_right", "move_up", "move_down");
		_camera.Position += dir * 500 * delta;
		
		
	}
}