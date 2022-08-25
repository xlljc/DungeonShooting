using Godot;
using System;

/// <summary>
/// 武器的基类
/// </summary>
public abstract class Weapon : ActivityObject<Weapon>
{

    /// <summary>
    /// 武器的唯一id
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// 开火回调事件
    /// </summary>
    public event Action<Weapon> FireEvent;

    /// <summary>
    /// 属性数据
    /// </summary>
    public WeaponAttribute Attribute { get; private set; }

    /// <summary>
    /// 武器的图片
    /// </summary>
    public Sprite WeaponSprite { get; private set; }

    /// <summary>
    /// 动画播放器
    /// </summary>
    /// <value></value>
    public AnimationPlayer AnimationPlayer { get; private set; }

    /// <summary>
    /// 武器攻击的目标阵营
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
    /// 武器管的开火点
    /// </summary>
    public Position2D FirePoint { get; private set; }
    /// <summary>
    /// 武器管的原点
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
    /// 武器的当前散射半径
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
    /// <summary>
    /// 换弹进度 (0 - 1)
    /// </summary>
    public float ReloadProgress
    {
        get
        {
            if (Attribute.AloneReload)
            {
                var num = 1f / Attribute.AmmoCapacity;
                return num * (Attribute.AmmoCapacity - CurrAmmo - 1) + num * (ReloadTimer / Attribute.ReloadTime);
            }
            return ReloadTimer / Attribute.ReloadTime;
        }
    }

    //是否按下
    private bool triggerFlag = false;
    //扳机计时器
    private float triggerTimer = 0;
    //开火前延时时间
    private float delayedTime = 0;
    //开火间隙时间
    private float fireInterval = 0;
    //开火武器口角度
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
    /// 根据属性创建一把武器
    /// </summary>
    /// <param name="id">武器唯一id</param>
    /// <param name="attribute">属性</param>
    public Weapon(string id, WeaponAttribute attribute)
    {
        Id = id;
        Attribute = attribute;
        //加载预制体
        var tempPrefab = ResourceManager.Load<PackedScene>(Attribute.WeaponPrefab);
        if (tempPrefab == null)
        {
            throw new Exception("WeaponAttribute中未设置'WeaponPrefab'属性!");
        }
        var tempNode = tempPrefab.Instance();
        var body = tempNode.GetChild(0);
        tempNode.RemoveChild(body);
        AddChild(body);

        WeaponSprite = GetNode<Sprite>("WeaponBody/WeaponSprite");
        FirePoint = GetNode<Position2D>("WeaponBody/FirePoint");
        OriginPoint = GetNode<Position2D>("WeaponBody/OriginPoint");
        ShellPoint = GetNode<Position2D>("WeaponBody/ShellPoint");
        AnimationPlayer = GetNode<AnimationPlayer>("WeaponBody/AnimationPlayer");
        CollisionShape2D = GetNode<CollisionShape2D>("WeaponBody/Collision");

        //更新图片
        WeaponSprite.Texture = ResourceLoader.Load<Texture>(Attribute.Sprite);
        WeaponSprite.Position = Attribute.CenterPosition;
        //开火位置
        FirePoint.Position = new Vector2(Attribute.FirePosition.x, -Attribute.FirePosition.y);
        OriginPoint.Position = new Vector2(0, -Attribute.FirePosition.y);

        //弹药量
        CurrAmmo = Attribute.AmmoCapacity;
        //剩余弹药量
        ResidueAmmo = Attribute.MaxAmmoCapacity - Attribute.AmmoCapacity;
    }

    /// <summary>
    /// 当按下扳机时调用
    /// </summary>
    protected abstract void OnDownTrigger();

    /// <summary>
    /// 当松开扳机时调用
    /// </summary>
    protected abstract void OnUpTrigger();

    /// <summary>
    /// 单次开火时调用的函数
    /// </summary>
    protected abstract void OnFire();

    /// <summary>
    /// 发射子弹时调用的函数, 每发射一枚子弹调用一次,
    /// 如果做霰弹武器效果, 一次开火发射5枚子弹, 则该函数调用5次
    /// </summary>
    protected abstract void OnShoot();

    /// <summary>
    /// 当开始换弹时调用
    /// </summary>
    protected abstract void OnReload();

    /// <summary>
    /// 当换弹完成时调用
    /// </summary>
    protected abstract void OnReloadFinish();

    /// <summary>
    /// 当武器被拾起时调用
    /// </summary>
    /// <param name="master">拾起该武器的角色</param>
    protected abstract void OnPickUp(Role master);

    /// <summary>
    /// 当武器从武器袋中扔掉时调用
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

    public override ComponentControl<Weapon> CreateComponentControl()
    {
        return new ComponentControl<Weapon>(this);
    }

    public override void _Process(float delta)
    {
        if (Master == null) //这把武器被扔在地上
        {
            Reloading = false;
            ReloadTimer = 0;
            triggerTimer = triggerTimer > 0 ? triggerTimer - delta : 0;
            triggerFlag = false;
            attackFlag = false;
            attackTimer = attackTimer > 0 ? attackTimer - delta : 0;
            CurrScatteringRange = Mathf.Max(CurrScatteringRange - Attribute.ScatteringRangeBackSpeed * delta, Attribute.StartScatteringRange);
            continuousCount = 0;
            delayedTime = 0;
        }
        else if (Master.Holster.ActiveWeapon != this) //当前武器没有被使用
        {
            Reloading = false;
            ReloadTimer = 0;
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
                    UpTrigger();
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
                TriggerFire();
            }

            if (!attackFlag && attackTimer <= 0)
            {
                CurrScatteringRange = Mathf.Max(CurrScatteringRange - Attribute.ScatteringRangeBackSpeed * delta, Attribute.StartScatteringRange);
            }
            triggerTimer = triggerTimer > 0 ? triggerTimer - delta : 0;
            triggerFlag = false;
            attackFlag = false;

            //武器身回归
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
            var fireFlag = true; //是否能开火
            if (Reloading) //换弹中
            {
                if (CurrAmmo > 0 && Attribute.AloneReload && Attribute.AloneReloadCanShoot) //立即停止换弹
                {
                    Reloading = false;
                    ReloadTimer = 0;
                }
                else
                {
                    fireFlag = false;
                }
            }
            else if (CurrAmmo <= 0)
            {
                fireFlag = false;
                //子弹不够
                _Reload();
            }

            if (fireFlag)
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
                    TriggerFire();
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
        OnDownTrigger();
    }

    /// <summary>
    /// 刚松开扳机
    /// </summary>
    private void UpTrigger()
    {
        continuousShootFlag = false;
        if (delayedTime > 0)
        {
            continuousCount = 0;
        }
        OnUpTrigger();
    }

    /// <summary>
    /// 触发开火
    /// </summary>
    private void TriggerFire()
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
        //武器口角度
        var angle = new Vector2(GameConfig.ScatteringDistance, CurrScatteringRange).Angle();

        //创建子弹
        for (int i = 0; i < bulletCount; i++)
        {
            //先算武器口方向
            Rotation = (float)GD.RandRange(-angle, angle);
            //发射子弹
            OnShoot();
        }

        //当前的散射半径
        CurrScatteringRange = Mathf.Min(CurrScatteringRange + Attribute.ScatteringRangeAddValue, Attribute.FinalScatteringRange);
        //武器的旋转角度
        RotationDegrees -= Attribute.UpliftAngle;
        fireAngle = RotationDegrees;
        //武器身位置
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
            if (ResidueAmmo >= Attribute.AloneReloadCount) //剩余子弹充足
            {
                if (CurrAmmo + Attribute.AloneReloadCount <= Attribute.AmmoCapacity)
                {
                    ResidueAmmo -= Attribute.AloneReloadCount;
                    CurrAmmo += Attribute.AloneReloadCount;
                }
                else //子弹满了
                {
                    var num = Attribute.AmmoCapacity - CurrAmmo;
                    CurrAmmo = Attribute.AmmoCapacity;
                    ResidueAmmo -= num;
                }
            }
            else if (ResidueAmmo != 0) //剩余子弹不足
            {
                if (ResidueAmmo + CurrAmmo <= Attribute.AmmoCapacity)
                {
                    CurrAmmo += ResidueAmmo;
                    ResidueAmmo = 0;
                }
                else //子弹满了
                {
                    var num = Attribute.AmmoCapacity - CurrAmmo;
                    CurrAmmo = Attribute.AmmoCapacity;
                    ResidueAmmo -= num;
                }
            }
            if (ResidueAmmo == 0 || CurrAmmo == Attribute.AmmoCapacity) //换弹结束
            {
                Reloading = false;
                ReloadTimer = 0;
                OnReloadFinish();
            }
            else
            {
                ReloadTimer = Attribute.ReloadTime;
                OnReload();
            }
        }
        else //换弹结束
        {
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
            Reloading = false;
            ReloadTimer = 0;
            OnReloadFinish();
        }
    }

    public override CheckInteractiveResult CheckInteractive<TU>(ActivityObject<TU> master)
    {
        var result = new CheckInteractiveResult(this);
        
        if (master is Role roleMaster) //碰到角色
        {
            if (Master == null)
            {
                var masterWeapon = roleMaster.Holster.ActiveWeapon;
                //查找是否有同类型武器
                var index = roleMaster.Holster.FindWeapon(Id);
                if (index != -1) //如果有这个武器
                {
                    if (CurrAmmo + ResidueAmmo != 0) //子弹不为空
                    {
                        var targetWeapon = roleMaster.Holster.GetWeapon(index);
                        if (!targetWeapon.IsFullAmmo()) //背包里面的武器子弹未满
                        {
                            //可以互动拾起弹药
                            result.CanInteractive = true;
                            result.Message = Attribute.Name;
                            result.ShowIcon = "res://resource/sprite/ui/icon/icon_bullet.png";
                            return result;
                        }
                    }
                }
                else //没有武器
                {
                    if (roleMaster.Holster.CanPickupWeapon(this)) //能拾起武器
                    {
                        //可以互动, 拾起武器
                        result.CanInteractive = true;
                        result.Message = Attribute.Name;
                        result.ShowIcon = "res://resource/sprite/ui/icon/icon_pickup.png";
                        return result;
                    }
                    else if (masterWeapon != null && masterWeapon.Attribute.WeightType == Attribute.WeightType) //替换武器
                    {
                        //可以互动, 切换武器
                        result.CanInteractive = true;
                        result.Message = Attribute.Name;
                        result.ShowIcon = "res://resource/sprite/ui/icon/icon_replace.png";
                        return result;
                    }
                }
            }
        }
        
        return result;
    }

    public override void Interactive<TU>(ActivityObject<TU> master)
    {
        if (master is Role roleMaster) //与role碰撞
        {
            //查找是否有同类型武器
            var index = roleMaster.Holster.FindWeapon(Id);
            if (index != -1) //如果有这个武器
            {
                if (CurrAmmo + ResidueAmmo == 0) //没有子弹了
                {
                    return;
                }

                var weapon = roleMaster.Holster.GetWeapon(index);
                //子弹上限
                var maxCount = Attribute.MaxAmmoCapacity;
                //是否捡到子弹
                var flag = false;
                if (ResidueAmmo > 0 && weapon.CurrAmmo + weapon.ResidueAmmo < maxCount)
                {
                    var count = weapon.PickUpAmmo(ResidueAmmo);
                    if (count != ResidueAmmo)
                    {
                        ResidueAmmo = count;
                        flag = true;
                    }
                }

                if (CurrAmmo > 0 && weapon.CurrAmmo + weapon.ResidueAmmo < maxCount)
                {
                    var count = weapon.PickUpAmmo(CurrAmmo);
                    if (count != CurrAmmo)
                    {
                        CurrAmmo = count;
                        flag = true;
                    }
                }

                //播放互动效果
                if (flag)
                {
                    this.StartThrow<ThrowWeapon>(new Vector2(20, 20), GlobalPosition, 0, 0,
                        MathUtils.RandRangeInt(-20, 20), MathUtils.RandRangeInt(20, 50),
                        MathUtils.RandRangeInt(-180, 180), WeaponSprite);
                }
            }
            else //没有武器
            {
                if (roleMaster.Holster.PickupWeapon(this) == -1)
                {
                    var slot = roleMaster.Holster.SlotList[roleMaster.Holster.ActiveIndex];
                    if (slot.Type == Attribute.WeightType)
                    {
                        var weapon = roleMaster.Holster.RmoveWeapon(roleMaster.Holster.ActiveIndex);
                        weapon.StartThrowWeapon(roleMaster);
                        roleMaster.PickUpWeapon(this);
                    }
                }
            }
        }
    }

    public Vector2 GetItemPosition()
    {
        return GlobalPosition;
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
    public void _PickUpWeapon(Role master)
    {
        Master = master;
        //握把位置
        WeaponSprite.Position = Attribute.HoldPosition;
        //清除泛白效果
        ShaderMaterial sm = WeaponSprite.Material as ShaderMaterial;
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
    /// </summary>a
    public void _ThrowOutWeapon()
    {
        Master = null;
        WeaponSprite.Position = Attribute.CenterPosition;
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