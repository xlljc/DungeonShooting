
using Godot;

/// <summary>
/// 物体所受到的外力的描述对象
/// </summary>
public class ExternalForce
{
    /// <summary>
    /// 当前力的名称
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// 是否启用这个力对象, 如果禁用, 则该力对象则不会参与到运动计算当中, 而且不会调用 PhysicsProcess 方法
    /// </summary>
    public bool Enable { get; set; } = true;

    /// <summary>
    /// 当前力的速率
    /// </summary>
    public Vector2 Velocity { get; set; } = Vector2.Zero;

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