using Godot;

public partial class TestNavigation2 : Node2D
{
	private Node2D _navigation2D;


	private Sprite2D _enemy;
	private NavigationAgent2D _navigationAgent2D;

	public override void _Ready()
	{
		_navigation2D = GetNode<Node2D>("Node2D");
		_enemy = _navigation2D.GetNode<Sprite2D>("Enemy");
		_navigationAgent2D = _enemy.GetNode<NavigationAgent2D>("NavigationAgent2D");

		_navigationAgent2D.TargetPosition = GetGlobalMousePosition();

	}

	public override void _PhysicsProcess(double delta)
	{
		if (_navigationAgent2D.IsNavigationFinished())
		{
			return;
		}
		
		var pos = _navigationAgent2D.GetNextPathPosition();
		_enemy.GlobalPosition = _enemy.GlobalPosition.MoveToward(pos, 100 * (float)delta);

	}

	public override void _Process(double delta)
	{
		QueueRedraw();
	}

	public override void _Draw()
	{
		var points = _navigationAgent2D.GetCurrentNavigationPath();
		if (points != null && points.Length >= 2)
		{
			DrawPolyline(points, Colors.Red);
			//DrawMultiline(points, Colors.Red);
		}
	}

	private void _on_Timer_timeout()
	{
		var target = GetGlobalMousePosition();
		_navigationAgent2D.TargetPosition = target;
	}
}