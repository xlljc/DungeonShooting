
using Godot;

public interface IHurt
{
    /// <summary>
    /// 受到伤害
    /// </summary>
    /// <param name="target">触发伤害的对象, 为 null 表示不存在对象或者对象已经被销毁</param>
    /// <param name="damage">伤害的量</param>
    /// <param name="angle">伤害角度（弧度制）</param>
    void Hurt(ActivityObject target, int damage, float angle);
}