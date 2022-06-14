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

    [Export] public PackedScene GunPrefab;

    public override void _EnterTree()
    {
        base._EnterTree();
        RoomManager.Current.Player = this;
    }

    public override void _Ready()
    {
        base._Ready();


        var gun = GunManager.GetGun1();
        MountPoint.AddChild(gun);
        gun.FireEvent += FireEvent_Func;
        Holster.PickupGun(gun);
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
        //攻击
        Attack();
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);
        Move(delta);
        //播放动画
        PlayAnim();
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

    private void Attack()
    {
        if (Input.IsActionPressed("fire"))
        {
            if (Holster.ActiveGun != null)
            {
                Holster.ActiveGun.Trigger();
            }
        }
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

    //开火后
    private void FireEvent_Func(Gun gun)
    {

    }
}