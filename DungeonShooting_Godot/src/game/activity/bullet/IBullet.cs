
public interface IBullet : IDestroy, ICoroutine
{
    /// <summary>
    /// 攻击的层级
    /// </summary>
    uint AttackLayer { get; set; }
    
    /// <summary>
    /// 发射该子弹的武器
    /// </summary>
    Weapon Weapon { get; }
    
    /// <summary>
    /// 发射该子弹的角色
    /// </summary>
    Role TriggerRole { get; }

    /// <summary>
    /// 初始化子弹数据
    /// </summary>
    /// <param name="weapon">发射该子弹的武器</param>
    /// <param name="attackLayer">攻击的层级</param>
    void Init(Weapon weapon, uint attackLayer);
}