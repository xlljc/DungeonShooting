using System;
using Config;
using Godot;


/// <summary>
/// 玩家角色基类, 所有角色都必须继承该类
/// </summary>
[Tool]
public partial class Player : Role
{
    /// <summary>
    /// 当玩家第一次进入房间时调用
    /// </summary>
    public event Action<RoomInfo> OnFirstEnterRoomEvent;
    
    /// <summary>
    /// 玩家身上的状态机控制器
    /// </summary>
    public StateController<Player, PlayerStateEnum> StateController { get; private set; }

    public PlayerRoleState PlayerRoleState { get; private set; }
    
    /// <summary>
    /// 是否可以翻滚
    /// </summary>
    public bool CanRoll => _rollCoolingTimer <= 0;
    
    //翻滚冷却计时器
    private float _rollCoolingTimer = 0;
    
    private BrushImageData _brushData2;
    
    public override void OnInit()
    {
        base.OnInit();

        IsAi = false;
        StateController = AddComponent<StateController<Player, PlayerStateEnum>>();
        Camp = CampEnum.Camp1;

        MaxHp = 6;
        Hp = 6;
        MaxShield = 0;
        Shield = 0;

        WeaponPack.SetCapacity(4);
        ActivePropsPack.SetCapacity(1);
        
        // debug用
        //DebugSet();
        
        //注册状态机
        StateController.Register(new PlayerIdleState());
        StateController.Register(new PlayerMoveState());
        StateController.Register(new PlayerRollState());
        //默认状态
        StateController.ChangeStateInstant(PlayerStateEnum.Idle);
        
        //InitSubLine();
        
        _brushData2 = new BrushImageData(ExcelConfig.LiquidMaterial_Map["0001"]);
        
        WeaponPack.SetCapacity(10);
    }

    private void DebugSet()
    {
        if (World is Hall)
        {
            return;
        }
        RoleState.Acceleration = 3000;
        RoleState.Friction = 3000;
        RoleState.MoveSpeed = 500;
        CollisionLayer = PhysicsLayer.None;
        CollisionMask = PhysicsLayer.None;
        //GameCamera.Main.Zoom = new Vector2(0.5f, 0.5f);
        // this.CallDelay(0.5f, () =>
        // {
        //     PickUpWeapon(Create<Weapon>(Ids.Id_weapon0009));
        //     PickUpWeapon(Create<Weapon>(Ids.Id_weapon0008));
        //     PickUpWeapon(Create<Weapon>(Ids.Id_weapon0007));
        //     PickUpWeapon(Create<Weapon>(Ids.Id_weapon0006));
        // });
        World.Color = new Color(1, 1, 1, 1); //关闭迷雾
        //显示房间小地图
        this.CallDelay(1, () =>
        {
            GameApplication.Instance.DungeonManager.StartRoomInfo.EachRoom(info =>
            {
                info.PreviewSprite.Visible = true;
                foreach (var roomDoorInfo in info.Doors)
                {
                    roomDoorInfo.AislePreviewSprite.Visible = true;
                }
            });
        });

    }

    protected override RoleState OnCreateRoleState()
    {
        var roleState = new PlayerRoleState();
        PlayerRoleState = roleState;
        return roleState;
    }

    protected override void Process(float delta)
    {
        base.Process(delta);
        if (IsDie)
        {
            return;
        }

        if (_rollCoolingTimer > 0)
        {
            _rollCoolingTimer -= delta;
        }
        
        if (MountLookTarget) //看向目标
        {
            //脸的朝向
            var gPos = Position;
            Vector2 mousePos = InputManager.CursorPosition;
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

        if (InputManager.ExchangeWeapon) //切换武器
        {
            ExchangeNextWeapon();
        }
        else if (InputManager.ThrowWeapon) //扔掉武器
        {
            ThrowWeapon();

            // //测试用的, 所有敌人也扔掉武器
            // if (Affiliation != null)
            // {
            //     var enemies = Affiliation.FindIncludeItems(o =>
            //     {
            //         return o.CollisionWithMask(PhysicsLayer.Enemy);
            //     });
            //     foreach (var activityObject in enemies)
            //     {
            //         if (activityObject is Enemy enemy)
            //         {
            //             enemy.ThrowWeapon();
            //         }
            //     }
            // }
        }
        else if (InputManager.Interactive) //互动物体
        {
            TriggerInteractive();
        }
        else if (InputManager.Reload) //换弹
        {
            Reload();
        }

        var meleeAttackFlag = false;
        if (InputManager.MeleeAttack) //近战攻击
        {
            if (StateController.CurrState != PlayerStateEnum.Roll) //不能是翻滚状态
            {
                if (WeaponPack.ActiveItem != null && WeaponPack.ActiveItem.Attribute.CanMeleeAttack)
                {
                    meleeAttackFlag = true;
                    MeleeAttack();
                }
            }
        }
        if (!meleeAttackFlag && InputManager.Fire) //正常开火
        {
            if (StateController.CurrState != PlayerStateEnum.Roll) //不能是翻滚状态
            {
                Attack();
                // //测试用,触发房间内地上的武器开火
                // var weaponArray = AffiliationArea.FindEnterItems(o => o is Weapon);
                // foreach (Weapon activityObject in weaponArray)
                // {
                //     activityObject.Trigger(this);
                // }
            }
        }

        if (InputManager.Roulette) //打开武器轮盘
        {
            //UiManager.Open_WeaponRoulette();
        }
        else if (InputManager.UseActiveProp) //使用道具
        {
            UseActiveProp();
        }
        else if (InputManager.ExchangeProp) //切换道具
        {
            ExchangeNextActiveProp();
        }
        else if (InputManager.RemoveProp) //扔掉道具
        {
            ThrowActiveProp();
        }

        if (Input.IsKeyPressed(Key.P)) //测试用, 自杀
        {
            //Hurt(1000, 0);
            Hp = 0;
            HurtHandler(this, 1000, 0);
        }
        else if (Input.IsKeyPressed(Key.O)) //测试用, 消灭房间内所有敌人
        {
            var enemyList = AffiliationArea.FindIncludeItems(o => o is Role role && role.IsEnemyWithPlayer());
            foreach (var enemy in enemyList)
            {
                var hurt = ((Enemy)enemy).HurtArea;
                if (hurt.CanHurt(Camp))
                {
                    hurt.Hurt(this, 1000, 0);
                }
            }
        }
        // //测试用
        // if (InputManager.Roll) //鼠标处触发互动物体
        // {
        //     var now = DateTime.Now;
        //     var mousePosition = GetGlobalMousePosition();
        //     var freezeSprites = AffiliationArea.RoomInfo.StaticSprite.CollisionCircle(mousePosition, 25, true);
        //     Debug.Log("检测数量: " + freezeSprites.Count + ", 用时: " + (DateTime.Now - now).TotalMilliseconds);
        //     foreach (var freezeSprite in freezeSprites)
        //     {
        //         var temp = freezeSprite.Position - mousePosition;
        //         freezeSprite.ActivityObject.MoveController.AddForce(temp.Normalized() * 300 * (25f - temp.Length()) / 25f);
        //     }
        // }
        
        if (Face == FaceDirection.Right)
        {
            TipRoot.Scale = Vector2.One;
        }
        else
        {
            TipRoot.Scale = new Vector2(-1, 1);
        }

        //测试刷地
        //DrawLiquid(_brushData2);
    }

    protected override void OnAffiliationChange(AffiliationArea prevArea)
    {
        BrushPrevPosition = null;
        base.OnAffiliationChange(prevArea);
    }

    protected override void OnPickUpWeapon(Weapon weapon)
    {
        EventManager.EmitEvent(EventEnum.OnPlayerPickUpWeapon, weapon);
    }

    protected override void OnThrowWeapon(Weapon weapon)
    {
        EventManager.EmitEvent(EventEnum.OnPlayerRemoveWeapon, weapon);
    }

    protected override int OnHandlerHurt(int damage)
    {
        //修改受到的伤害, 每次只受到1点伤害
        return 1;
    }

    protected override void OnHit(ActivityObject target, int damage, float angle, bool realHarm)
    {
        //进入无敌状态
        if (realHarm) //真实伤害
        {
            PlayInvincibleFlashing(RoleState.WoundedInvincibleTime);
        }
        else //护盾抵消掉的
        {
            PlayInvincibleFlashing(RoleState.ShieldInvincibleTime);
        }
    }

    protected override void OnChangeHp(int hp)
    {
        //GameApplication.Instance.Ui.SetHp(hp);
        EventManager.EmitEvent(EventEnum.OnPlayerHpChange, hp);
    }

    protected override void OnChangeMaxHp(int maxHp)
    {
        //GameApplication.Instance.Ui.SetMaxHp(maxHp);
        EventManager.EmitEvent(EventEnum.OnPlayerMaxHpChange, maxHp);
    }

    protected override void ChangeInteractiveItem(CheckInteractiveResult prev, CheckInteractiveResult result)
    {
        if (prev != null && prev.Target.ShowOutline)
        {
            prev.Target.OutlineColor = Colors.Black;
        }
        if (result != null && result.Target.ShowOutline)
        {
            result.Target.OutlineColor = Colors.White;
        }
        //派发互动对象改变事件
        EventManager.EmitEvent(EventEnum.OnPlayerChangeInteractiveItem, result);
    }

    protected override void OnChangeShield(int shield)
    {
        //GameApplication.Instance.Ui.SetShield(shield);
        EventManager.EmitEvent(EventEnum.OnPlayerShieldChange, shield);
    }

    protected override void OnChangeMaxShield(int maxShield)
    {
        //GameApplication.Instance.Ui.SetMaxShield(maxShield);
        EventManager.EmitEvent(EventEnum.OnPlayerMaxShieldChange, maxShield);
    }

    protected override void OnDie()
    {
        StateController.Enable = false;
        GameCamera.Main.SetFollowTarget(null);
        BasisVelocity = Vector2.Zero;
        MoveController.ClearForce();

        //暂停游戏
        World.Current.Pause = true;
        //弹出结算面板
        GameApplication.Instance.Cursor.SetGuiMode(true);
        UiManager.Open_Settlement();
    }

    protected override void OnPickUpActiveProp(ActiveProp activeProp)
    {
        EventManager.EmitEvent(EventEnum.OnPlayerPickUpProp, activeProp);
    }

    protected override void OnRemoveActiveProp(ActiveProp activeProp)
    {
        EventManager.EmitEvent(EventEnum.OnPlayerRemoveProp, activeProp);
    }

    protected override void OnPickUpBuffProp(BuffProp buffProp)
    {
        EventManager.EmitEvent(EventEnum.OnPlayerPickUpProp, buffProp);
    }

    protected override void OnRemoveBuffProp(BuffProp buffProp)
    {
        EventManager.EmitEvent(EventEnum.OnPlayerRemoveProp, buffProp);
    }

    /// <summary>
    /// 处理角色移动的输入
    /// </summary>
    public void HandleMoveInput(float delta)
    {
        var dir = InputManager.MoveAxis;
        // 移动. 如果移动的数值接近0(是用 摇杆可能出现 方向 可能会出现浮点)，就friction的值 插值 到 0
        // 如果 有输入 就以当前速度，用acceleration 插值到 对应方向 * 最大速度
        if (Mathf.IsZeroApprox(dir.X))
        {
            BasisVelocity = new Vector2(Mathf.MoveToward(BasisVelocity.X, 0, RoleState.Friction * delta), BasisVelocity.Y);
        }
        else
        {
            BasisVelocity = new Vector2(Mathf.MoveToward(BasisVelocity.X, dir.X * RoleState.MoveSpeed, RoleState.Acceleration * delta), BasisVelocity.Y);
        }

        if (Mathf.IsZeroApprox(dir.Y))
        {
            BasisVelocity = new Vector2(BasisVelocity.X, Mathf.MoveToward(BasisVelocity.Y, 0, RoleState.Friction * delta));
        }
        else
        {
            BasisVelocity = new Vector2(BasisVelocity.X, Mathf.MoveToward(BasisVelocity.Y, dir.Y * RoleState.MoveSpeed, RoleState.Acceleration * delta));
        }
    }

    /// <summary>
    /// 翻滚结束
    /// </summary>
    public void OverRoll()
    {
        _rollCoolingTimer = PlayerRoleState.RollCoolingTime;
    }

    // protected override void DebugDraw()
    // {
    //     base.DebugDraw();
    //     DrawArc(GetLocalMousePosition(), 25, 0, Mathf.Pi * 2f, 20, Colors.Red, 1);
    // }

    public override void AddGold(int goldCount)
    {
        base.AddGold(goldCount);
        EventManager.EmitEvent(EventEnum.OnPlayerGoldChange, RoleState.Gold);
    }

    public override void UseGold(int goldCount)
    {
        base.UseGold(goldCount);
        EventManager.EmitEvent(EventEnum.OnPlayerGoldChange, RoleState.Gold);
    }

    /// <summary>
    /// 玩家第一次进入房间时调用
    /// </summary>
    public virtual void OnFirstEnterRoom(RoomInfo roomInfo)
    {
        if (OnFirstEnterRoomEvent != null)
        {
            OnFirstEnterRoomEvent(roomInfo);
        }
    }
}