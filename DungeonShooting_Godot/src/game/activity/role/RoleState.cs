
using System;

/// <summary>
/// 角色属性类
/// </summary>
public class RoleState
{
    /// <summary>
    /// 移动速度
    /// </summary>
    public float MoveSpeed = 120f;
        
    /// <summary>
    /// 移动加速度
    /// </summary>
    public float Acceleration = 1500f;
    
    /// <summary>
    /// 移动摩擦力
    /// </summary>
    public float Friction = 900f;
    
    /// <summary>
    /// 单格护盾恢复时间, 单位: 秒
    /// </summary>
    public float ShieldRecoveryTime = 8;

    /// <summary>
    /// 受伤后的无敌时间, 单位: 秒
    /// </summary>
    public float WoundedInvincibleTime = 1f;

    /// <summary>
    /// 护盾被攻击后的无敌时间, 单位: 秒
    /// </summary>
    public float ShieldInvincibleTime = 0.5f;

    /// <summary>
    /// 攻击/发射后计算伤害
    /// </summary>
    public event Action<RefValue<int>> CalcDamageEvent;
    public int CallCalcDamageEvent(int damage)
    {
        if (CalcDamageEvent != null)
        {
            var result = new RefValue<int>(damage);
            CalcDamageEvent(result);
            return result.Value;
        }

        return damage;
    }

    /// <summary>
    /// 受伤后计算受到的伤害
    /// </summary>
    public event Action<RefValue<int>> CalcHurtDamageEvent;
    public int CallCalcHurtDamageEvent(int damage)
    {
        if (CalcHurtDamageEvent != null)
        {
            var result = new RefValue<int>(damage);
            CalcHurtDamageEvent(result);
            return result.Value;
        }

        return damage;
    }
}