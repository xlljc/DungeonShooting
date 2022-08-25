
using Godot;

/// <summary>
/// 组件接口
/// </summary>
public interface IComponent<TN> : IProcess, IDestroy where TN : Node2D
{
    /// <summary>
    /// 该组件绑定的GameObject对象
    /// </summary>
    GameObject<TN> GameObject { get; }
    
    /// <summary>
    /// 该组件所绑定的GameObject的坐标
    /// </summary>
    Vector2 Position { get; set; }
    
    /// <summary>
    /// 该组件所绑定的GameObject的全局坐标
    /// </summary>
    Vector2 GlobalPosition { get; set; }
    
    /// <summary>
    /// 该组件所绑定的GameObject的显示状态
    /// </summary>
    bool Visible { get; set; }
    
    /// <summary>
    /// 是否启用该组件, 如果停用, 则不会调用 Process 和 PhysicsProcess
    /// </summary>
    bool Enable { get; set; }

    /// <summary>
    /// 第一次调用 Process 或 PhysicsProcess 之前调用
    /// </summary>
    void Ready();
    
    /// <summary>
    /// 当该组件挂载到GameObject上时调用
    /// </summary>
    void OnMount();
    
    /// <summary>
    /// 当该组件被取消挂载时调用
    /// </summary>
    void OnUnMount();
}