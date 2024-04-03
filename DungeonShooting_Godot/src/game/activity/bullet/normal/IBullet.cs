
using System;
using Config;

public interface IBullet : ICoroutine, IPoolItem
{
    /// <summary>
    /// 当物体被回收时的事件
    /// </summary>
    event Action OnReclaimEvent;
    /// <summary>
    /// 离开对象池时的事件
    /// </summary>
    event Action OnLeavePoolEvent;

    /// <summary>
    /// 子弹所在阵营
    /// </summary>
    CampEnum Camp { get; set; }
    
    /// <summary>
    /// 子弹数据
    /// </summary>
    BulletData BulletData { get; }
    
    /// <summary>
    /// 子弹状态
    /// </summary>
    BulletStateEnum State { get; }

    /// <summary>
    /// 初始化子弹数据
    /// </summary>
    void InitData(BulletData data, CampEnum camp);
    
    /// <summary>
    /// 子弹运行逻辑执行完成
    /// </summary>
    void LogicalFinish();
}