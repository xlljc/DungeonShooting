using Godot;

/// <summary>
/// 枪上的属性
/// </summary>
public class GunAttribute
{
    /// <summary>
    /// 武器的唯一id
    /// </summary>
    public string Id = "1";
    /// <summary>
    /// 武器显示的名称
    /// </summary>
    public string Name = "Gun1";
    /// <summary>
    /// 主武器
    /// </summary>
    public GunWeightType WeightType = GunWeightType.MainWeapon;
    /// <summary>
    /// 枪的图片
    /// </summary>
    public Texture Sprite;
    /// <summary>
    /// 子弹预制体
    /// </summary>
    public PackedScene BulletPack;
    /// <summary>
    /// 弹壳预制体
    /// </summary>
    public PackedScene ShellPack;
    /// <summary>
    /// 是否连续发射, 如果为false, 则每次发射都需要扣动扳机
    /// </summary>
    public bool ContinuousShoot = true;
    /// <summary>
    /// 是否为松发开火, 也就是松开扳机才开火, 若要启用该属性, 必须将 'ContinuousShoot' 设置为 false
    /// </summary>
    public bool LooseShoot = false;
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
    /// 初始射速, 初始每秒分钟能发射多少发子弹
    /// </summary>
    public float StartFiringSpeed = 300;
    /// <summary>
    /// 最终射速, 最终每秒分钟能发射多少发子弹
    /// </summary>
    public float FinalFiringSpeed = 300;
    /// <summary>
    /// 按下扳机并开火后射速增加速率
    /// </summary>
    public float FiringSpeedAddSpeed = 2;
    /// <summary>
    /// 松开扳机后射速消散速率
    /// </summary>
    public float FiringSpeedBackSpeed = 10;
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
    /// 武器精灵的旋转中心坐标
    /// </summary>
    public Vector2 CenterPosition = new Vector2(0, 0);
    /// <summary>
    /// 开火位置
    /// </summary>
    public Vector2 FirePosition = new Vector2(11, 0);
    /// <summary>
    /// 握把位置
    /// </summary>
    public Vector2 HoldPosition = new Vector2(4, -3);
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
    /// <summary>
    /// 开火后枪口角度恢复速度倍数
    /// </summary>
    public float UpliftAngleRestore = 1;

    public GunAttribute() 
    {
        
    }
}