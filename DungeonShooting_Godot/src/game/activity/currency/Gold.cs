
using Godot;

/// <summary>
/// 金币类
/// </summary>
[Tool]
public partial class Gold : ActivityObject, IPoolItem
{
	/// <summary>
	/// 金币数量
	/// </summary>
	[Export]
	public int GoldCount { get; set; } = 1;
	
	public bool IsRecycled { get; set; }
	public string Logotype { get; set; }
	
	private float _maxSpeed = 250;
	private float _speed = 0;
	private Role _moveTarget;
	
	public override void OnInit()
	{
		DefaultLayer = RoomLayerEnum.YSortLayer;
	}

	protected override void OnThrowOver()
	{
		var current = Player.Current;
		if (current != null)
		{
			this.CallDelay(0.3f, () =>
			{
				_moveTarget = current;
				MoveController.Enable = false;
			});
		}
	}

	protected override void Process(float delta)
	{
		if (_moveTarget != null && !_moveTarget.IsDestroyed)
		{
			var position = Position;
			var targetPosition = _moveTarget.Position;
			if (position.DistanceSquaredTo(targetPosition) < 3 * 3)
			{
				_moveTarget.AddGold(GoldCount);
				ObjectPool.Reclaim(this);
			}
			else
			{
				_speed = Mathf.MoveToward(_speed, _maxSpeed, _maxSpeed * delta);
				Position = position.MoveToward(targetPosition, _speed * delta);
			}
		}
	}
	
	public void OnReclaim()
	{
		GetParent().RemoveChild(this);
		_moveTarget = null;
	}

	public void OnLeavePool()
	{
		_speed = 0;
		MoveController.Enable = true;
		MoveController.ClearForce();
		MoveController.SetAllVelocity(Vector2.Zero);
	}
}
