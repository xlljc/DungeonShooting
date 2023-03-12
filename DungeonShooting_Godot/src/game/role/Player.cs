using Godot;


/// <summary>
/// 玩家角色基类, 所有角色都必须继承该类
/// </summary>
[RegisterActivity(ActivityIdPrefix.Role + "0001", ResourcePath.prefab_role_Player_tscn)]
public partial class Player : Role
{
    /// <summary>
    /// 获取当前操作的角色
    /// </summary>
    public static Player Current => GameApplication.Instance.RoomManager.Player;
    
    /// <summary>
    /// 移动加速度
    /// </summary>
    public float Acceleration { get; set; } = 1500f;
    
    /// <summary>
    /// 移动摩擦力
    /// </summary>
    public float Friction { get; set; } = 800f;
    
    public override void OnInit()
    {
        base.OnInit();
        
        AttackLayer = PhysicsLayer.Wall | PhysicsLayer.Props | PhysicsLayer.Enemy;
        Camp = CampEnum.Camp1;
        
        Holster.SlotList[2].Enable = true;
        Holster.SlotList[3].Enable = true;
        //让相机跟随玩家
        // var remoteTransform = new RemoteTransform2D();
        // AddChild(remoteTransform);
        // MainCamera.Main.GlobalPosition = GlobalPosition;
        // MainCamera.Main.ResetSmoothing();
        // remoteTransform.RemotePath = remoteTransform.GetPathTo(MainCamera.Main);
        
        RefreshGunTexture();

        MaxHp = 50;
        Hp = 50;
        MaxShield = 30;
        Shield = 30;

        // // debug用
        // Acceleration = 3000;
        // Friction = 3000;
        // MoveSpeed = 500;
        // CollisionLayer = 0;
        // CollisionMask = 0;
        // GameCamera.Main.Zoom = new Vector2(0.5f, 0.5f);
    }

    protected override void Process(float delta)
    {
        base.Process(delta);
        //脸的朝向
        if (LookTarget == null)
        {
            var gPos = GlobalPosition;
            Vector2 mousePos = InputManager.GetViewportMousePosition();
            if (mousePos.X > gPos.X && Face == FaceDirection.Left)
            {
                Face = FaceDirection.Right;
            }
            else if (mousePos.X < gPos.X && Face == FaceDirection.Right)
            {
                Face = FaceDirection.Left;
            }
            //枪口跟随鼠标
            MountPoint.SetLookAt(mousePos);
        }

        if (Input.IsActionJustPressed("exchange")) //切换武器
        {
            ExchangeNext();
        }
        else if (Input.IsActionJustPressed("throw")) //扔掉武器
        {
            ThrowWeapon();
        }
        else if (Input.IsActionJustPressed("interactive")) //互动物体
        {
            var item = TriggerInteractive();
            if (item != null)
            {
                RefreshGunTexture();
            }
        }
        else if (Input.IsActionJustPressed("reload")) //换弹
        {
            Reload();
        }
        if (Input.IsActionPressed("fire")) //开火
        {
            Attack();
        }
        //刷新显示的弹药剩余量
        RefreshGunAmmunition();
    }

    protected override void PhysicsProcess(float delta)
    {
        base.PhysicsProcess(delta);
        HandleMoveInput(delta);
        //播放动画
        PlayAnim();
    }

    public override void ExchangeNext()
    {
        base.ExchangeNext();
        RefreshGunTexture();
    }

    public override void ExchangePrev()
    {
        base.ExchangePrev();
        RefreshGunTexture();
    }

    public override void ThrowWeapon(int index)
    {
        base.ThrowWeapon(index);
        RefreshGunTexture();
    }

    public override void ThrowWeapon()
    {
        base.ThrowWeapon();
        RefreshGunTexture();
    }

    public override bool PickUpWeapon(Weapon weapon, bool exchange = true)
    {
        var v = base.PickUpWeapon(weapon, exchange);
        if (v)
        {
            RefreshGunTexture();
        }
        return v;
    }

    protected override void OnChangeHp(int hp)
    {
        GameApplication.Instance.Ui.SetHp(hp);
    }

    protected override void OnChangeMaxHp(int maxHp)
    {
        GameApplication.Instance.Ui.SetMaxHp(maxHp);
    }

    protected override void ChangeInteractiveItem(CheckInteractiveResult result)
    {
        //派发互动对象改变事件
        EventManager.EmitEvent(EventEnum.OnPlayerChangeInteractiveItem, result);
    }

    protected override void OnChangeShield(int shield)
    {
        GameApplication.Instance.Ui.SetShield(shield);
    }

    protected override void OnChangeMaxShield(int maxShield)
    {
        GameApplication.Instance.Ui.SetMaxShield(maxShield);
    }

    /// <summary>
    /// 刷新 ui 上手持的物体
    /// </summary>
    private void RefreshGunTexture()
    {
        var gun = Holster.ActiveWeapon;
        if (gun != null)
        {
            GameApplication.Instance.Ui.SetGunTexture(gun.GetDefaultTexture());
        }
        else
        {
            GameApplication.Instance.Ui.SetGunTexture(null);
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
            GameApplication.Instance.Ui.SetAmmunition(gun.CurrAmmo, gun.ResidueAmmo);
        }
    }

    //处理角色移动的输入
    private void HandleMoveInput(float delta)
    {
        //角色移动
        // 得到输入的 vector2  getvector方法返回值已经归一化过了noemalized
        Vector2 dir = Input.GetVector("move_left", "move_right", "move_up", "move_down");
        // 移动. 如果移动的数值接近0(是用 摇杆可能出现 方向 可能会出现浮点)，就friction的值 插值 到 0
        // 如果 有输入 就以当前速度，用acceleration 插值到 对应方向 * 最大速度
        if (Mathf.IsZeroApprox(dir.X))
        {
            BasisVelocity = new Vector2(Mathf.MoveToward(BasisVelocity.X, 0, Friction * delta), BasisVelocity.Y);
        }
        else
        {
            BasisVelocity = new Vector2(Mathf.MoveToward(BasisVelocity.X, dir.X * MoveSpeed, Acceleration * delta),
                BasisVelocity.Y);
        }

        if (Mathf.IsZeroApprox(dir.Y))
        {
            BasisVelocity = new Vector2(BasisVelocity.X, Mathf.MoveToward(BasisVelocity.Y, 0, Friction * delta));
        }
        else
        {
            BasisVelocity = new Vector2(BasisVelocity.X,
                Mathf.MoveToward(BasisVelocity.Y, dir.Y * MoveSpeed, Acceleration * delta));
        }
    }

    // 播放动画
    private void PlayAnim()
    {
        if (BasisVelocity != Vector2.Zero)
        {
            if ((Face == FaceDirection.Right && BasisVelocity.X >= 0) || Face == FaceDirection.Left && BasisVelocity.X <= 0) //向前走
            {
                AnimatedSprite.Play(AnimatorNames.Run);
            }
            else if ((Face == FaceDirection.Right && BasisVelocity.X < 0) || Face == FaceDirection.Left && BasisVelocity.X > 0) //向后走
            {
                AnimatedSprite.Play(AnimatorNames.ReverseRun);
            }
        }
        else
        {
            AnimatedSprite.Play(AnimatorNames.Idle);
        }
    }
}