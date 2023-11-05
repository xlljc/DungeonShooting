
using Config;
using Godot;

/// <summary>
/// 子弹数据
/// </summary>
public class BulletData
{
    /// <summary>
    /// 发射该子弹的武器
    /// </summary>
    public Weapon Weapon;
    
    /// <summary>
    /// 使用的配置数据
    /// </summary>
    public ExcelConfig.BulletBase BulletBase;
    
    /// <summary>
    /// 发射该子弹的角色
    /// </summary>
    public Role TriggerRole;
    
    /// <summary>
    /// 最小伤害
    /// </summary>
    public int MinHarm;
    
    /// <summary>
    /// 最大伤害
    /// </summary>
    public  int MaxHarm;

    /// <summary>
    /// 最大飞行距离
    /// </summary>
    public float MaxDistance;

    /// <summary>
    /// 子弹飞行速度
    /// </summary>
    public float FlySpeed;

    /// <summary>
    /// 坐标
    /// </summary>
    public Vector2 Position;
    
    /// <summary>
    /// 旋转角度
    /// </summary>
    public float Rotation;
}