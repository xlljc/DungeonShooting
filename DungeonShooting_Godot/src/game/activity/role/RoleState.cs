
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
    /// 移动摩擦力, 仅用于人物基础移动
    /// </summary>
    public float Friction = 900f;
    
    /// <summary>
    /// 翻滚速度
    /// </summary>
    public float RollSpeed = 180f;

    /// <summary>
    /// 翻滚冷却时间
    /// </summary>
    public float RollTime = 0.5f;
    
    /// <summary>
    /// 单格护盾恢复时间, 单位: 秒
    /// </summary>
    public float ShieldRecoveryTime = 18;

    /// <summary>
    /// 受伤后的无敌时间, 单位: 秒
    /// </summary>
    public float WoundedInvincibleTime = 1f;

    /// <summary>
    /// 护盾被攻击后的无敌时间, 单位: 秒
    /// </summary>
    public float ShieldInvincibleTime = 0.4f;

    /// <summary>
    /// 近战攻击间隔时间
    /// </summary>
    public float MeleeAttackTime = 0.6f;

    /// <summary>
    /// 攻击/发射后计算伤害
    /// </summary>
    public event Action<int, RefValue<int>> CalcDamageEvent;
    public int CallCalcDamageEvent(int damage)
    {
        if (CalcDamageEvent != null)
        {
            var result = new RefValue<int>(damage);
            CalcDamageEvent(damage, result);
            return result.Value;
        }

        return damage;
    }

    /// <summary>
    /// 受伤后计算受到的伤害
    /// </summary>
    public event Action<int, RefValue<int>> CalcHurtDamageEvent;
    public int CallCalcHurtDamageEvent(int damage)
    {
        if (CalcHurtDamageEvent != null)
        {
            var result = new RefValue<int>(damage);
            CalcHurtDamageEvent(damage, result);
            return result.Value;
        }

        return damage;
    }

    /// <summary>
    /// 武器初始散射值增量
    /// </summary>
    public event Action<Weapon, float, RefValue<float>> CalcStartScatteringEvent;

    public float CallCalcStartScatteringEvent(Weapon weapon, float value)
    {
        if (CalcStartScatteringEvent != null)
        {
            var result = new RefValue<float>(value);
            CalcStartScatteringEvent(weapon, value, result);
            return result.Value;
        }

        return value;
    }

    /// <summary>
    /// 武器最终散射值增量
    /// </summary>
    public event Action<Weapon, float, RefValue<float>> CalcFinalScatteringEvent;
    public float CallCalcFinalScatteringEvent(Weapon weapon, float value)
    {
        if (CalcFinalScatteringEvent != null)
        {
            var result = new RefValue<float>(value);
            CalcFinalScatteringEvent(weapon, value, result);
            return result.Value;
        }

        return value;
    }

    /// <summary>
    /// 武器开火发射子弹数量
    /// </summary>
    public event Action<Weapon, int, RefValue<int>> CalcBulletCountEvent;
    public int CallCalcBulletCountEvent(Weapon weapon, int count)
    {
        if (CalcBulletCountEvent != null)
        {
            var result = new RefValue<int>(count);
            CalcBulletCountEvent(weapon, count, result);
            return result.Value;
        }

        return count;
    }

    /// <summary>
    /// 子弹偏移角度, 角度制
    /// </summary>
    public event Action<Weapon, float, RefValue<float>> CalcBulletDeviationAngleEvent;
    public float CallCalcBulletDeviationAngleEvent(Weapon weapon, float angle)
    {
        if (CalcBulletDeviationAngleEvent != null)
        {
            var result = new RefValue<float>(angle);
            CalcBulletDeviationAngleEvent(weapon, angle, result);
            return result.Value;
        }

        return angle;
    }

    /// <summary>
    /// 子弹速度
    /// </summary>
    public event Action<Weapon, float, RefValue<float>> CalcBulletSpeedEvent;
    public float CallCalcBulletSpeedEvent(Weapon weapon, float speed)
    {
        if (CalcBulletSpeedEvent != null)
        {
            var result = new RefValue<float>(speed);
            CalcBulletSpeedEvent(weapon, speed, result);
            return result.Value;
        }

        return speed;
    }
    
    /// <summary>
    /// 子弹射程
    /// </summary>
    public event Action<Weapon, float, RefValue<float>> CalcBulletDistanceEvent;
    public float CallCalcBulletDistanceEvent(Weapon weapon, float distance)
    {
        if (CalcBulletDistanceEvent != null)
        {
            var result = new RefValue<float>(distance);
            CalcBulletDistanceEvent(weapon, distance, result);
            return result.Value;
        }

        return distance;
    }
}