
using Godot;

/// <summary>
/// 物体所受到的外力的描述对象
/// </summary>
public class ExternalForce
{
    /// <summary>
    /// 所在的移动控制器
    /// </summary>
    public MoveController MoveController { get; set; }
    
    /// <summary>
    /// 当前力的名称
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// 是否启用这个力对象, 如果禁用, 则该力对象则不会参与到运动计算当中, 而且不会调用 PhysicsProcess 方法
    /// </summary>
    public bool Enable { get; set; } = true;

    /// <summary>
    /// 是否在空中也会受到阻力
    /// </summary>
    public bool EnableResistanceInTheAir { get; set; } = false;

    /// <summary>
    /// 当速度( Velocity 和 RotationSpeed )到达 0 后是否自动销毁, 默认 true
    /// </summary>
    public bool AutoDestroy { get; set; } = true;
    
    /// <summary>
    /// 速率的阻力大小, 也就是速度每秒衰减的量
    /// </summary>
    public float VelocityResistance { get; set; }
    
    /// <summary>
    /// 当前力的速率
    /// </summary>
    public Vector2 Velocity { get; set; } = Vector2.Zero;

    /// <summary>
    /// 当前力对物体造成的旋转速度, 弧度制
    /// </summary>
    public float RotationSpeed { get; set; }
    
    /// <summary>
    /// 旋转速率阻力大小, 也就是速度每秒衰减的量
    /// </summary>
    public float RotationResistance { get; set; }

    /// <summary>
    /// 物理帧更新
    /// </summary>
    public virtual void PhysicsProcess(float delta)
    {
    }

    public ExternalForce(string name)
    {
        Name = name;
    }
}