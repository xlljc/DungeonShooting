
using Config;

public interface IBullet : IDestroy, ICoroutine
{
    /// <summary>
    /// 攻击的层级
    /// </summary>
    uint AttackLayer { get; set; }
    /// <summary>
    /// 子弹数据
    /// </summary>
    BulletData BulletData { get; }
    /// <summary>
    /// 初始化子弹数据
    /// </summary>
    void InitData(BulletData data, uint attackLayer);
}