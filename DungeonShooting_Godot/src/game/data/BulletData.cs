
using Config;
using Godot;

/// <summary>
/// 子弹数据
/// </summary>
public class BulletData : IClone<BulletData>
{
    /// <summary>
    /// 数据所在世界对象
    /// </summary>
    public World World;
    
    /// <summary>
    /// 发射该子弹的武器, 可能为null
    /// </summary>
    public Weapon Weapon;
    
    /// <summary>
    /// 使用的配置数据
    /// </summary>
    public ExcelConfig.BulletBase BulletBase;
    
    /// <summary>
    /// 发射该子弹的角色, 可能为null
    /// </summary>
    public Role TriggerRole;
    
    /// <summary>
    /// 造成的伤害
    /// </summary>
    public int Harm;

    /// <summary>
    /// 击退值
    /// </summary>
    public float Repel;

    /// <summary>
    /// 最大飞行距离
    /// </summary>
    public float MaxDistance;

    /// <summary>
    /// 子弹飞行速度
    /// </summary>
    public float FlySpeed;

    /// <summary>
    /// 初始离地高度
    /// </summary>
    public float Altitude;
    
    /// <summary>
    /// 纵轴速度
    /// </summary>
    public float VerticalSpeed;
    
    /// <summary>
    /// 反弹次数
    /// </summary>
    public int BounceCount;

    /// <summary>
    /// 子弹最大穿透次数
    /// </summary>
    public int Penetration;

    /// <summary>
    /// 子弹最大存在时间
    /// </summary>
    public float LifeTime;

    /// <summary>
    /// 坐标
    /// </summary>
    public Vector2 Position;
    
    /// <summary>
    /// 旋转角度
    /// </summary>
    public float Rotation;
    
    public BulletData(World world)
    {
        World = world;
    }
    
    public BulletData Clone()
    {
        return new BulletData(World)
        {
            Weapon = Weapon,
            BulletBase = BulletBase,
            TriggerRole = TriggerRole,
            Harm = Harm,
            Repel = Repel,
            MaxDistance = MaxDistance,
            FlySpeed = FlySpeed,
            Altitude = Altitude,
            VerticalSpeed = VerticalSpeed,
            BounceCount = BounceCount,
            Penetration = Penetration,
            LifeTime = LifeTime,
            Position = Position,
            Rotation = Rotation
        };
    }
}