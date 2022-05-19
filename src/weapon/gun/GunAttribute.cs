using Godot;

/// <summary>
/// 枪上的属性
/// </summary>
public class GunAttribute
{
    /// <summary>
    /// 子弹对象
    /// </summary>
    public string Bullet = "res://prefab/Bullet.tscn";
    /// <summary>
    /// 枪的图片
    /// </summary>
    public string Sprite = "res://resource/sprite/gun/gun1.png";
    /// <summary>
    /// 是否连续发射, 如果为false, 则每次发射都需要扣动扳机
    /// </summary>
    public bool ContinuousShoot = true;
    /// <summary>
    /// 连续发射最小次数, 仅当ContinuousShoot为false时生效
    /// </summary>
    public int MinContinuousCount = 3;
    /// <summary>
    /// 连续发射最大次数, 仅当ContinuousShoot为false时生效
    /// </summary>
    public int MaxContinuousCount = 3;
    /// <summary>
    /// 按下一次扳机后需要多长时间才能再次按下
    /// </summary>
    public float TriggerInterval = 0;
    /// <summary>
    /// 射速, 每秒分钟能发射多少发子弹
    /// </summary>
    public float FiringSpeed = 300;
    /// <summary>
    /// 单次开火发射子弹最小数量
    /// </summary>
    public int MinFireBulletCount = 1;
    /// <summary>
    /// 单次开火发射子弹最大数量
    /// </summary>
    public int MaxFireBulletCount = 1;
    /// <summary>
    /// 开火前延时
    /// </summary>
    public float DelayedTime = 0f;
    /// <summary>
    /// 初始散射半径
    /// </summary>
    public float StartScatteringRange = 0;
    /// <summary>
    /// 最终散射半径
    /// </summary>
    public float FinalScatteringRange = 20;
    /// <summary>
    /// 每次发射后散射增加值
    /// </summary>
    public float ScatteringRangeAddValue = 2;
    /// <summary>
    /// 松开扳机后散射销退速率
    /// </summary>
    public float ScatteringRangeBackSpeed = 10;
    /// <summary>
    /// 子弹飞行最大距离
    /// </summary>
    public float MaxDistance = 600;
    /// <summary>
    /// 子弹飞行最小距离
    /// </summary>
    public float MinDistance = 800;
    /// <summary>
    /// 枪管长度
    /// </summary>
    public float BarrelLength = 11;
    /// <summary>
    /// 重量
    /// </summary>
    public float Weight = 11;
    /// <summary>
    /// 最大后坐力 (仅用于开火后枪身抖动)
    /// </summary>
    public float MaxBacklash = 4;
    /// <summary>
    /// 最小后坐力 (仅用于开火后枪身抖动)
    /// </summary>
    public float MinBacklash = 2;
    /// <summary>
    /// 开火后枪口上抬角度
    /// </summary>
    public float UpliftAngle = 30;

    public GunAttribute() 
    {
        
    }
}