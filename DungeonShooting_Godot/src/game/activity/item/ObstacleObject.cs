
using Godot;

/// <summary>
/// 障碍物, 会阻挡玩家移动, 也会被子弹击中
/// </summary>
[Tool]
public partial class ObstacleObject : ActivityObject, IHurt
{
	public virtual void Hurt(ActivityObject target, int damage, float angle)
	{
	}
}
