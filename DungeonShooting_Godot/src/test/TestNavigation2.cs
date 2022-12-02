using Godot;

public class TestNavigation2 : Node2D
{
	private Navigation2D _navigation2D;


	private Sprite _enemy;
	private NavigationAgent2D _navigationAgent2D;

	public override void _Ready()
	{
		_navigation2D = GetNode<Navigation2D>("Navigation2D");
		_enemy = _navigation2D.GetNode<Sprite>("Enemy");
		_navigationAgent2D = _enemy.GetNode<NavigationAgent2D>("NavigationAgent2D");

		_navigationAgent2D.SetTargetLocation(GetGlobalMousePosition());

	}

	public override void _PhysicsProcess(float delta)
	{
		if (_navigationAgent2D.IsNavigationFinished())
		{
			return;
		}
		
		var pos = _navigationAgent2D.GetNextLocation();
		_enemy.GlobalPosition = _enemy.GlobalPosition.MoveToward(pos, 100 * delta);

	}

	public override void _Process(float delta)
	{
		Update();
	}

	public override void _Draw()
	{
		var points = _navigationAgent2D.GetNavPath();
		if (points != null && points.Length >= 2)
		{
			DrawPolyline(points, Colors.Red);
			//DrawMultiline(points, Colors.Red);
		}
	}

	private void _on_Timer_timeout()
	{
		var target = GetGlobalMousePosition();
		if (_navigationAgent2D.GetTargetLocation() != target)
		{
			GD.Print("更新目标位置...");
			_navigationAgent2D.SetTargetLocation(target);
		}
	}
}