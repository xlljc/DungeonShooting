
/// <summary>
/// 调用 Enemy.EnemyAttack() 函数返回的结果
/// </summary>
public enum AiAttackState
{
    /// <summary>
    /// 未触发 EnemyAttack()
    /// </summary>
    None,
    /// <summary>
    /// 触发切换武器
    /// </summary>
    ExchangeWeapon,
    /// <summary>
    /// 没有弹药了
    /// </summary>
    NoAmmo,
    /// <summary>
    /// 换弹中
    /// </summary>
    Reloading,
    /// <summary>
    /// 触发换弹
    /// </summary>
    TriggerReload,
    /// <summary>
    /// 没有武器
    /// </summary>
    NoWeapon,
    /// <summary>
    /// 正在锁定目标中
    /// </summary>
    LockingTime,
    /// <summary>
    /// 攻击间隙时间
    /// </summary>
    AttackInterval,
    /// <summary>
    /// 成功触发攻击
    /// </summary>
    Attack,
}