
using Godot;

public interface IHurt
{
    /// <summary>
    /// 返回是否可以造成伤害
    /// </summary>
    /// <param name="targetCamp">攻击目标所属层级</param>
    bool CanHurt(CampEnum targetCamp);
    
    /// <summary>
    /// 受到伤害
    /// </summary>
    /// <param name="target">触发伤害的对象, 为 null 表示不存在对象或者对象已经被销毁</param>
    /// <param name="damage">伤害的量</param>
    /// <param name="angle">伤害角度（弧度制）</param>
    void Hurt(ActivityObject target, int damage, float angle);
}