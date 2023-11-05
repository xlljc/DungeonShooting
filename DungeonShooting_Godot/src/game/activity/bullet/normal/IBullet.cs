
using Config;

public interface IBullet : IDestroy, ICoroutine
{
    /// <summary>
    /// 攻击的层级
    /// </summary>
    uint AttackLayer { get; set; }
    
    /// <summary>
    /// 发射该子弹的武器
    /// </summary>
    Weapon Weapon { get; set; }
    
    /// <summary>
    /// 使用的配置数据
    /// </summary>
    ExcelConfig.BulletBase BulletBase { get; set; }
    
    /// <summary>
    /// 发射该子弹的角色
    /// </summary>
    Role TriggerRole { get; set; }
    
    /// <summary>
    /// 最小伤害
    /// </summary>
    int MinHarm { get; set; }
    
    /// <summary>
    /// 最大伤害
    /// </summary>
    int MaxHarm { get; set; }
}