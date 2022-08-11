using Godot;
using System;

/// <summary>
/// 枪的基类
/// </summary>
public abstract class Gun : Area2D, IProp
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
    public Sprite GunSprite { get; private set; }

    /// <summary>
    /// 动画播放器
    /// </summary>
    /// <value></value>
    public AnimationPlayer AnimationPlayer { get; private set; }

    /// <summary>
    /// 枪攻击的目标阵营
    /// </summary>
    public CampEnum TargetCamp { get; set; }

    /// <summary>
    /// 该武器的拥有者
    /// </summary>
    public Role Master { get; private set; }

    /// <summary>
    /// 当前弹夹弹药剩余量
    /// </summary>
    public int CurrAmmo { get; private set; }
    /// <summary>
    /// 剩余弹药量
    /// </summary>
    public int ResidueAmmo { get; private set; }

    /// <summary>
    /// 枪管的开火点
    /// </summary>
    public Position2D FirePoint { get; private set; }
    /// <summary>
    /// 枪管的原点
    /// </summary>
    public Position2D OriginPoint { get; private set; }
    /// <summary>
    /// 弹壳抛出的点
    /// </summary>
    public Position2D ShellPoint { get; private set; }
    /// <summary>
    /// 碰撞器节点
    /// </summary>
    /// <value></value>
    public CollisionShape2D CollisionShape2D { get; private set; }
    /// <summary>
    /// 枪的当前散射半径
    /// </summary>
    public float CurrScatteringRange { get; private set; } = 0;
    /// <summary>
    /// 是否在换弹中
    /// </summary>
    /// <value></value>
    public bool Reloading { get; private set; } = false;
    /// <summary>
    /// 换弹计时器
    /// </summary>
    public float ReloadTimer { get; private set; } = 0;

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
    //连发状态记录
    private bool continuousShootFlag = false;

    /// <summary>
    /// 初始化时调用
    /// </summary>
    protected abstract void Init();

    /// <summary>
    /// 单次开火时调用的函数
    /// </summary>
    protected abstract void OnFire();

    /// <summary>
    /// 换弹时调用
    /// </summary>
    protected abstract void OnReload();

    /// <summary>
    /// 发射子弹时调用的函数, 每发射一枚子弹调用一次,
    /// 如果做霰弹枪效果, 一次开火发射5枚子弹, 则该函数调用5次
    /// </summary>
    protected abstract void OnShootBullet();

    /// <summary>
    /// 当武器被拾起时调用
    /// </summary>
    /// <param name="master">拾起该武器的角色</param>
    protected abstract void OnPickUp(Role master);

    /// <summary>
    /// 当武器被扔掉时调用
    /// </summary>
    protected abstract void OnThrowOut();

    /// <summary>
    /// 当武器被激活时调用, 也就是使用当武器是调用
    /// </summary>
    protected abstract void OnActive();

    /// <summary>
    /// 当武器被收起时调用
    /// </summary>
    protected abstract void OnConceal();

    public override void _Process(float delta)
    {
        if (Master == null) //这把武器被扔在地上
        {
            Reloading = false;
            triggerTimer = triggerTimer > 0 ? triggerTimer - delta : 0;
            triggerFlag = false;
            attackFlag = false;
            attackTimer = attackTimer > 0 ? attackTimer - delta : 0;
            CurrScatteringRange = Mathf.Max(CurrScatteringRange - Attribute.ScatteringRangeBackSpeed * delta, Attribute.StartScatteringRange);
            continuousCount = 0;
            delayedTime = 0;
        }
        else if (Master.Holster.ActiveGun != this) //当前武器没有被使用
        {
            Reloading = false;
            triggerTimer = triggerTimer > 0 ? triggerTimer - delta : 0;
            triggerFlag = false;
            attackFlag = false;
            attackTimer = attackTimer > 0 ? attackTimer - delta : 0;
            CurrScatteringRange = Mathf.Max(CurrScatteringRange - Attribute.ScatteringRangeBackSpeed * delta, Attribute.StartScatteringRange);
            continuousCount = 0;
            delayedTime = 0;
        }
        else //正在使用中
        {

            //换弹
            if (Reloading)
            {
                ReloadTimer -= delta;
                if (ReloadTimer <= 0)
                {
                    ReloadTimer = 0;
                    ReloadSuccess();
                }
            }

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
            else if (delayedTime > 0) //攻击延时
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
                //开火
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
    }

    public void Init(GunAttribute attribute)
    {
        if (_attribute != null)
        {
            throw new Exception("当前武器已经初始化过了!");
        }

        GunSprite = GetNode<Sprite>("GunSprite");
        FirePoint = GetNode<Position2D>("FirePoint");
        OriginPoint = GetNode<Position2D>("OriginPoint");
        ShellPoint = GetNode<Position2D>("ShellPoint");
        AnimationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        CollisionShape2D = GetNode<CollisionShape2D>("Collision");

        Attribute = attribute;
        //更新图片
        GunSprite.Texture = attribute.Sprite;
        GunSprite.Position = Attribute.CenterPosition;
        //开火位置
        FirePoint.Position = new Vector2(attribute.FirePosition.x, -attribute.FirePosition.y);
        OriginPoint.Position = new Vector2(0, -attribute.FirePosition.y);

        //弹药量
        CurrAmmo = attribute.AmmoCapacity;
        //剩余弹药量
        ResidueAmmo = attribute.MaxAmmoCapacity - attribute.AmmoCapacity;

        Init();
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
            if (Reloading)
            {
                //换弹中
                GD.Print("换弹中..." + (ReloadTimer / Attribute.ReloadTime));
            }
            else if (CurrAmmo <= 0)
            {
                //子弹不够
                GD.Print("弹夹打空了, 按R换弹!");
            }
            else
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
        if (delayedTime > 0)
        {
            continuousCount = 0;
        }
    }

    /// <summary>
    /// 触发开火
    /// </summary>
    private void TriggernFire()
    {
        continuousCount = continuousCount > 0 ? continuousCount - 1 : 0;

        //减子弹数量
        CurrAmmo--;
        //开火间隙
        fireInterval = 60 / Attribute.StartFiringSpeed;
        //攻击冷却
        attackTimer += fireInterval;

        //触发开火函数
        OnFire();

        //开火发射的子弹数量
        var bulletCount = MathUtils.RandRangeInt(Attribute.MaxFireBulletCount, Attribute.MinFireBulletCount);
        //枪口角度
        var angle = new Vector2(GameConfig.ScatteringDistance, CurrScatteringRange).Angle();

        //创建子弹
        for (int i = 0; i < bulletCount; i++)
        {
            //先算枪口方向
            Rotation = (float)GD.RandRange(-angle, angle);
            //发射子弹
            OnShootBullet();
        }

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
    /// 返回弹药是否到达上限
    /// </summary>
    public bool IsFullAmmo()
    {
        return CurrAmmo + ResidueAmmo >= Attribute.MaxAmmoCapacity;
    }

    /// <summary>
    /// 返回是否弹药耗尽
    /// </summary>
    public bool IsEmptyAmmo()
    {
        return CurrAmmo + ResidueAmmo == 0;
    }

    /// <summary>
    /// 拾起的弹药数量, 如果到达容量上限, 则返回拾取完毕后剩余的弹药数量
    /// </summary>
    /// <param name="count">弹药数量</param>
    public int PickUpAmmo(int count)
    {
        var num = ResidueAmmo;
        ResidueAmmo = Mathf.Min(ResidueAmmo + count, Attribute.MaxAmmoCapacity - CurrAmmo);
        return count - ResidueAmmo + num;
    }

    /// <summary>
    /// 触发换弹
    /// </summary>
    public void _Reload()
    {
        if (CurrAmmo < Attribute.AmmoCapacity && ResidueAmmo > 0 && !Reloading)
        {
            Reloading = true;
            ReloadTimer = Attribute.ReloadTime;
            OnReload();
        }
    }

    /// <summary>
    /// 换弹计时器时间到, 执行换弹操作
    /// </summary>
    private void ReloadSuccess()
    {
        if (Attribute.AloneReload) //单独装填
        {

        }
        else //换弹结束
        {
            Reloading = false;
            if (ResidueAmmo >= Attribute.AmmoCapacity)
            {
                ResidueAmmo -= Attribute.AmmoCapacity - CurrAmmo;
                CurrAmmo = Attribute.AmmoCapacity;
            }
            else
            {
                CurrAmmo = ResidueAmmo;
                ResidueAmmo = 0;
            }
        }
    }

    public bool CanTnteractive(Role master)
    {
        var masterGun = master.Holster.ActiveGun;
        //查找是否有同类型武器
        var index = master.Holster.FindGun(Attribute.Id);
        if (index != -1) //如果有这个武器
        {
            if (CurrAmmo + ResidueAmmo == 0) //没有子弹了
            {
                return false;
            }
            else if (masterGun != null && masterGun.IsFullAmmo()) //子弹满了
            {
                return false;
            }
        }
        else //没有武器
        {
            if (masterGun != null && masterGun.Attribute.WeightType == Attribute.WeightType)
            {
                return true;
            }
            else if (!master.Holster.CanPickupGun(this))
            {
                return false;
            }
        }
        return true;
    }

    public void Tnteractive(Role master)
    {
        //查找是否有同类型武器
        var index = master.Holster.FindGun(Attribute.Id);
        if (index != -1) //如果有这个武器
        {
            if (CurrAmmo + ResidueAmmo == 0) //没有子弹了
            {
                return;
            }
            var gun = master.Holster.GetGun(index);
            //子弹上限
            var maxCount = Attribute.MaxAmmoCapacity;
            //是否捡到子弹
            var flag = false;
            if (ResidueAmmo > 0 && gun.CurrAmmo + gun.ResidueAmmo < maxCount)
            {
                var count = gun.PickUpAmmo(ResidueAmmo);
                if (count != ResidueAmmo)
                {
                    ResidueAmmo = count;
                    flag = true;
                }
            }
            if (CurrAmmo > 0 && gun.CurrAmmo + gun.ResidueAmmo < maxCount)
            {
                var count = gun.PickUpAmmo(CurrAmmo);
                if (count != CurrAmmo)
                {
                    CurrAmmo = count;
                    flag = true;
                }
            }
            //播放互动效果
            if (flag)
            {
                this.StartThrow<ThrowGun>(new Vector2(20, 20), GlobalPosition, 0, 0,
                    MathUtils.RandRangeInt(-20, 20), MathUtils.RandRangeInt(20, 50),
                    MathUtils.RandRangeInt(-180, 180), GunSprite);
            }
        }
        else//没有武器
        {
            if (master.Holster.PickupGun(this) == -1)
            {
                var slot = master.Holster.SlotList[master.Holster.ActiveIndex];
                if (slot.Type == Attribute.WeightType)
                {
                    var gun = master.Holster.RmoveGun(master.Holster.ActiveIndex);
                    gun.StartThrowGun(master);
                    master.Holster.PickupGun(this);
                }
            }
        }
    }

    /// <summary>
    /// 触发落到地面
    /// </summary>
    public void _FallToGround()
    {
        //启用碰撞
        CollisionShape2D.Disabled = false;
    }

    /// <summary>
    /// 触发拾起
    /// </summary>
    public void _PickUpGun(Role master)
    {
        Master = master;
        //握把位置
        GunSprite.Position = Attribute.HoldPosition;
        //清除泛白效果
        ShaderMaterial sm = GunSprite.Material as ShaderMaterial;
        sm.SetShaderParam("schedule", 0);
        //停止动画
        AnimationPlayer.Stop();
        ZIndex = 0;
        //禁用碰撞
        CollisionShape2D.Disabled = true;
        OnPickUp(master);
    }

    /// <summary>
    /// 触发抛出
    /// </summary>
    public void _ThrowOutGun()
    {
        Master = null;
        GunSprite.Position = Attribute.CenterPosition;
        AnimationPlayer.Play("Floodlight");
        OnThrowOut();
    }

    /// <summary>
    /// 触发启用武器
    /// </summary>
    public void _Active()
    {
        OnActive();
    }

    /// <summary>
    /// 触发收起武器
    /// </summary>
    public void _Conceal()
    {
        OnConceal();
    }

    /// <summary>
    /// 实例化并返回子弹对象
    /// </summary>
    /// <param name="bulletPack">子弹的预制体</param>
    protected T CreateBullet<T>(PackedScene bulletPack, Vector2 globalPostion, float globalRotation, Node parent = null) where T : Node2D, IBullet
    {
        return (T)CreateBullet(bulletPack, globalPostion, globalRotation, parent);
    }

    /// <summary>
    /// 实例化并返回子弹对象
    /// </summary>
    /// <param name="bulletPack">子弹的预制体</param>
    protected IBullet CreateBullet(PackedScene bulletPack, Vector2 globalPostion, float globalRotation, Node parent = null)
    {
        // 实例化子弹
        Node2D bullet = bulletPack.Instance<Node2D>();
        // 设置坐标
        bullet.GlobalPosition = globalPostion;
        // 旋转角度
        bullet.GlobalRotation = globalRotation;
        if (parent == null)
        {
            RoomManager.Current.SortRoot.AddChild(bullet);
        }
        else
        {
            parent.AddChild(bullet);
        }
        // 调用初始化
        IBullet result = (IBullet)bullet;
        result.Init(TargetCamp, this, null);
        return result;
    }
}