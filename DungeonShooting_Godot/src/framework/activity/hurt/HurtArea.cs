using Godot;

/// <summary>
/// 可被子弹击中的区域
/// </summary>
[Tool]
public partial class HurtArea : Area2D, IHurt
{
    /// <summary>
    /// 所属角色
    /// </summary>
    public Role Master { get; private set; }

    public void InitRole(Role role)
    {
        Master = role;
    }

    public override void _Ready()
    {
        Monitoring = false;
    }

    public bool CanHurt(ActivityObject target)
    {
        //无敌状态
        if (Master.Invincible)
        {
            return true;
        }

        if (target is Role role)
        {
            return Master.IsEnemy(role);
        }
        return true;
    }

    public void Hurt(ActivityObject target, int damage, float angle)
    {
        Master.CallDeferred(nameof(Master.HurtHandler), target, damage, angle);
    }
}