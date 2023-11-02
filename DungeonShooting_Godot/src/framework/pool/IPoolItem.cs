
/// <summary>
/// 可被对象池池回收的实例对象接口
/// </summary>
public interface IPoolItem : IDestroy
{
    /// <summary>
    /// 是否已经回收
    /// </summary>
    bool IsRecycled { get; set; }
    /// <summary>
    /// 对象唯一标识，用于在对象池中区分对象类型，可以是资源路径，也可以是配置表id
    /// </summary>
    string Logotype { get; }
    /// <summary>
    /// 当物体被回收时调用，也就是进入对象池
    /// </summary>
    void OnReclaim();
    /// <summary>
    /// 离开对象池时调用
    /// </summary>
    void OnLeavePool();
}