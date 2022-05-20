using Godot;
using System;

/// <summary>
/// 枪的基类
/// </summary>
public class Gun : Node2D
{
    /// <summary>
    /// 开火回调事件
    /// </summary>
    public event Action<Gun> FireEvent;

    /// <summary>
    /// 属性数据
    /// </summary>
    public GunAttribute Attribute
    {
        get
        {
            if (_attribute == null)
            {
                throw new Exception("请先调用Init来初始化枪的属性");
            }
            return _attribute;
        }
        private set => _attribute = value;
    }
    private GunAttribute _attribute;

    /// <summary>
    /// 枪的图片
    /// </summary>
    public Sprite Sprite { get; private set; }

    /// <summary>
    /// 枪攻击的目标阵营
    /// </summary>
    public CampEnum TargetCamp { get; set; }
    /// <summary>
    /// 开火点
    /// </summary>
    public Position2D FirePoint { get; private set; }
    /// <summary>
    /// 原点
    /// </summary>
    public Position2D OriginPoint { get; private set; }
    /// <summary>
    /// 枪的当前散射半径
    /// </summary>
    public float CurrScatteringRange { get; private set; } = 0;

    //是否按下
    private bool triggerFlag = false;
    //扳机计时器
    private float triggerTimer = 0;
    //开火前延时时间
    private float delayedTime = 0;
    //开火间隙时间
    private float fireInterval = 0;
    //开火枪口角度
    private float fireAngle = 0;
    //攻击冷却计时
    private float attackTimer = 0;
    //攻击状态
    private bool attackFlag = false;
    //按下的时间
    private float downTimer = 0;
    //松开的时间
    private float upTimer = 0;
    //连发次数
    private float continuousCount = 0;
    private bool continuousShootFlag = false;
    //子弹
    private PackedScene bulletPacked;

    public override void _EnterTree()
    {
        Sprite = GetNode<Sprite>("Sprite");
        FirePoint = GetNode<Position2D>("FirePoint");
        OriginPoint = GetNode<Position2D>("OriginPoint");
    }

    public override void _Process(float delta)
    {
        if (triggerFlag)
        {
            if (upTimer > 0) //第一帧按下扳机
            {
                upTimer = 0;
                DownTrigger();
            }
            downTimer += delta;
        }
        else
        {
            if (downTimer > 0) //第一帧松开扳机
            {
                downTimer = 0;
                UpTriggern();
            }
            upTimer += delta;
        }

        // 攻击的计时器
        if (attackTimer > 0)
        {
            attackTimer -= delta;
            if (attackTimer < 0)
            {
                delayedTime += attackTimer;
                attackTimer = 0;
            }
        }
        else if (delayedTime > 0)
        {
            delayedTime -= delta;
            if (attackTimer < 0)
            {
                delayedTime = 0;
            }
        }

        //连发判断
        if (continuousCount > 0 && delayedTime <= 0 && attackTimer <= 0)
        {
            TriggernFire();
        }

        if (!attackFlag && attackTimer <= 0)
        {
            CurrScatteringRange = Mathf.Max(CurrScatteringRange - Attribute.ScatteringRangeBackSpeed * delta, Attribute.StartScatteringRange);
        }
        triggerTimer = triggerTimer > 0 ? triggerTimer - delta : 0;
        triggerFlag = false;
        attackFlag = false;

        //枪身回归
        Position = Position.MoveToward(Vector2.Zero, 35 * delta);
        if (fireInterval == 0)
        {
            RotationDegrees = 0;
        }
        else
        {
            RotationDegrees = Mathf.Lerp(0, fireAngle, attackTimer / fireInterval);
        }
    }

    public void Init(GunAttribute attribute)
    {
        Attribute = attribute;
        //更新图片
        var texture = ResourceLoader.Load<Texture>(attribute.Sprite);
        Sprite.Texture = texture;
        //子弹
        bulletPacked = ResourceLoader.Load<PackedScene>(attribute.Bullet);
        //开火位置
        FirePoint.Position = new Vector2(attribute.BarrelLength, FirePoint.Position.y);
    }

    /// <summary>
    /// 扳机函数, 调用即视为扣动扳机
    /// </summary>
    public void Trigger()
    {
        //是否第一帧按下
        var justDown = downTimer == 0;
        //是否能发射
        var flag = false;
        if (continuousCount <= 0) //不能处于连发状态下
        {
            if (Attribute.ContinuousShoot) //自动射击
            {
                if (triggerTimer > 0)
                {
                    if (continuousShootFlag)
                    {
                        flag = true;
                    }
                }
                else
                {
                    flag = true;
                    if (delayedTime <= 0 && attackTimer <= 0)
                    {
                        continuousShootFlag = true;
                    }
                }
            }
            else //半自动
            {
                if (justDown && triggerTimer <= 0)
                {
                    flag = true;
                }
            }
        }

        if (flag)
        {
            if (justDown)
            {
                //开火前延时
                delayedTime = Attribute.DelayedTime;
                //扳机按下间隔
                triggerTimer = Attribute.TriggerInterval;
                //连发数量
                if (!Attribute.ContinuousShoot)
                {
                    continuousCount = MathUtils.RandRangeInt(Attribute.MinContinuousCount, Attribute.MaxContinuousCount);
                }
            }
            if (delayedTime <= 0 && attackTimer <= 0)
            {
                TriggernFire();
            }
            attackFlag = true;
        }
        triggerFlag = true;
    }

    /// <summary>
    /// 刚按下扳机
    /// </summary>
    private void DownTrigger()
    {

    }

    /// <summary>
    /// 刚松开扳机
    /// </summary>
    private void UpTriggern()
    {
        continuousShootFlag = false;
    }

    private void TriggernFire()
    {
        continuousCount = continuousCount > 0 ? continuousCount - 1 : 0;
        fireInterval = 60 / Attribute.FiringSpeed;
        attackTimer += fireInterval;
        Fire();
        //当前的散射半径
        CurrScatteringRange = Mathf.Min(CurrScatteringRange + Attribute.ScatteringRangeAddValue, Attribute.FinalScatteringRange);
        //枪的旋转角度
        RotationDegrees -= Attribute.UpliftAngle;
        fireAngle = RotationDegrees;
        //枪身位置
        Position = new Vector2(Mathf.Max(-Attribute.MaxBacklash, Position.x - MathUtils.RandRange(Attribute.MinBacklash, Attribute.MaxBacklash)), Position.y);

        if (FireEvent != null)
        {
            FireEvent(this);
        }
    }

    /// <summary>
    /// 开火
    /// </summary>
    protected virtual void Fire()
    {
        //开火发射的子弹数量
        var bulletCount = MathUtils.RandRangeInt(Attribute.MaxFireBulletCount, Attribute.MinFireBulletCount);
        //枪口角度
        var angle = new Vector2(GameConfig.ScatteringDistance, CurrScatteringRange).Angle();

        //创建子弹
        for (int i = 0; i < bulletCount; i++)
        {
            //先算枪口方向
            Rotation = (float)GD.RandRange(-angle, angle);

            //创建子弹
            var bullet = CreateBullet<HighSpeedBullet>(bulletPacked);
            //位置
            bullet.GlobalPosition = FirePoint.GlobalPosition;
            //角度
            bullet.Rotation = (FirePoint.GlobalPosition - OriginPoint.GlobalPosition).Angle();
            GetTree().CurrentScene.AddChild(bullet);
            //飞行距离
            var distance = MathUtils.RandRange(Attribute.MinDistance, Attribute.MaxDistance);
            //初始化子弹数据
            bullet.InitData(distance, Colors.White);
        }
    }

    public T CreateBullet<T>(PackedScene bulletPack) where T : Bullet
    {
        T bullet = bulletPack.Instance<T>();
        bullet.Init(TargetCamp, this, null);
        return bullet;
    }
}