using Godot;

public class Player : Role
{
    /// <summary>
    /// 移动加速度
    /// </summary>
    public float Acceleration = 1500f;

    /// <summary>
    /// 移动摩擦力
    /// </summary>
    public float Friction = 800f;
    /// <summary>
    /// 移动速度
    /// </summary>
    public Vector2 Velocity = Vector2.Zero;

    /// <summary>
    /// 当前护盾值
    /// </summary>
    public int Shield
    {
        get => _shield;
        protected set
        {
            int temp = _shield;
            _shield = value;
            if (temp != _shield) OnChangeShield(_shield);
        }
    }
    private int _shield = 0;

    /// <summary>
    /// 最大护盾值
    /// </summary>
    public int MaxShield
    {
        get => _maxShield;
        protected set
        {
            int temp = _maxShield;
            _maxShield = value;
            if (temp != _maxShield) OnChangeMaxShield(_maxShield);
        }
    }
    private int _maxShield = 0;


    [Export] public PackedScene GunPrefab;

    public override void _EnterTree()
    {
        base._EnterTree();
        RoomManager.Current.Player = this;
    }

    public override void _Ready()
    {
        base._Ready();
        Holster.SlotList[2].Enable = true;
        Holster.SlotList[3].Enable = true;
        PickUpWeapon(WeaponManager.GetGun1()); //0
        PickUpWeapon(WeaponManager.GetGun2()); //1
        PickUpWeapon(WeaponManager.GetGun3()); //2
        PickUpWeapon(WeaponManager.GetGun4()); //3
        RefreshGunTexture();

        MaxHp = 50;
        Hp = 40;
        MaxShield = 30;
        Shield = 10;
    }

    public override void _Process(float delta)
    {
        base._Process(delta);

        Vector2 mousePos = GetGlobalMousePosition();
        //枪口跟随鼠标
        MountPoint.LookAt(mousePos);
        //脸的朝向
        if (mousePos.x > GlobalPosition.x && Face == FaceDirection.Left)
        {
            Face = FaceDirection.Right;
        }
        else if (mousePos.x < GlobalPosition.x && Face == FaceDirection.Right)
        {
            Face = FaceDirection.Left;
        }

        if (Input.IsActionJustPressed("exchange")) //切换武器
        {
            TriggerExchangeNext();
            RefreshGunTexture();
        }
        else if (Input.IsActionJustPressed("throw")) //扔掉武器
        {
            TriggerThrowWeapon();
            RefreshGunTexture();
        }
        else if (Input.IsActionJustPressed("interactive")) //互动物体
        {
            var item = TriggerTnteractive();
            if (item is Weapon)
            {
                RefreshGunTexture();
            }
        }
        else if (Input.IsActionJustPressed("reload")) //换弹
        {
            TriggerReload();
        }
        if (Input.IsActionPressed("fire")) //开火
        {
            TriggerAttack();
        }
        //刷新显示的弹药剩余量
        RefreshGunAmmunition();

        if (Holster.ActiveWeapon != null && Holster.ActiveWeapon.Reloading)
        {
            RoomUI.Current.ReloadBar.ShowBar(GlobalPosition, 1 - Holster.ActiveWeapon.ReloadProgress);
        }
        else
        {
            RoomUI.Current.ReloadBar.HideBar();
        }
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);
        Move(delta);
        //播放动画
        PlayAnim();
    }

    protected override void OnChangeHp(int hp)
    {
        RoomUI.Current.SetHp(hp);
    }

    protected override void OnChangeMaxHp(int maxHp)
    {
        RoomUI.Current.SetMaxHp(maxHp);
    }

    protected override void ChangeInteractiveItem(CheckInteractiveResult result)
    {
        if (result == null)
        {
            //隐藏互动提示
            RoomUI.Current.InteractiveTipBar.HideBar();
        }
        else
        {
            if (InteractiveItem is Weapon gun)
            {
                //显示互动提示
                RoomUI.Current.InteractiveTipBar.ShowBar(result.Target.GetItemPosition(), result.ShowIcon, result.Message);
            }
        }
    }

    protected void OnChangeShield(int shield)
    {
        RoomUI.Current.SetShield(shield);
    }

    protected void OnChangeMaxShield(int maxShield)
    {
        RoomUI.Current.SetMaxShield(maxShield);
    }

    /// <summary>
    /// 刷新 ui 上手持的物体
    /// </summary>
    private void RefreshGunTexture()
    {
        var gun = Holster.ActiveWeapon;
        if (gun != null)
        {
            RoomUI.Current.SetGunTexture(gun.Attribute.Sprite);
        }
        else
        {
            RoomUI.Current.SetGunTexture(null);
        }
    }

    /// <summary>
    /// 刷新 ui 上显示的弹药量
    /// </summary>
    private void RefreshGunAmmunition()
    {
        var gun = Holster.ActiveWeapon;
        if (gun != null)
        {
            RoomUI.Current.SetAmmunition(gun.CurrAmmo, gun.ResidueAmmo);
        }
    }

    private void Move(float delta)
    {
        //角色移动
        // 得到输入的 vector2  getvector方法返回值已经归一化过了noemalized
        Vector2 dir = Input.GetVector("move_left", "move_right", "move_up", "move_down");
        // 移动. 如果移动的数值接近0(是用 摇杆可能出现 方向 可能会出现浮点)，就fricition的值 插值 到 0
        // 如果 有输入 就以当前速度，用acceleration 插值到 对应方向 * 最大速度
        if (Mathf.IsZeroApprox(dir.x)) Velocity.x = Mathf.MoveToward(Velocity.x, 0, Friction * delta);
        else Velocity.x = Mathf.MoveToward(Velocity.x, dir.x * MoveSpeed, Acceleration * delta);

        if (Mathf.IsZeroApprox(dir.y)) Velocity.y = Mathf.MoveToward(Velocity.y, 0, Friction * delta);
        else Velocity.y = Mathf.MoveToward(Velocity.y, dir.y * MoveSpeed, Acceleration * delta);

        Velocity = MoveAndSlide(Velocity);
    }

    // 播放动画
    private void PlayAnim()
    {
        if (Velocity != Vector2.Zero)
        {
            if ((Face == FaceDirection.Right && Velocity.x >= 0) || Face == FaceDirection.Left && Velocity.x <= 0) //向前走
            {
                AnimatedSprite.Animation = AnimatorNames.Run;
            }
            else if ((Face == FaceDirection.Right && Velocity.x < 0) || Face == FaceDirection.Left && Velocity.x > 0) //向后走
            {
                AnimatedSprite.Animation = AnimatorNames.ReverseRun;
            }
        }
        else
        {
            AnimatedSprite.Animation = AnimatorNames.Idle;
        }
    }
}