#region 基础敌人设计思路
/*
敌人有三种状态: 
状态1: 未发现玩家, 视野不可穿墙, 该状态下敌人移动比较规律, 移动速度较慢, 一旦玩家进入视野或者听到玩家枪声, 立刻切换至状态3, 该房间的敌人不能再回到状态1
状态2: 发现有玩家, 但不知道在哪, 视野不可穿墙, 该情况下敌人移动速度明显加快, 移动不规律, 一旦玩家进入视野或者听到玩家枪声, 立刻切换至状态3
状态3: 明确知道玩家的位置, 视野允许穿墙, 移动速度与状态2一致, 进入该状态时, 敌人之间会相互告知玩家所在位置, 并朝着玩家位置开火,
       如果有墙格挡, 则有一定概率继续开火, 一旦玩家立刻敌人视野超哥一段时间, 敌人自动切换为状态2

敌人状态1只存在于少数房间内, 比如特殊房间, 大部分情况下敌人应该是状态2, 或者玩家进入房间时就被敌人发现
*/
#endregion


using Godot;

/// <summary>
/// 基础敌人
/// </summary>
[Tool]
public partial class Enemy : Role
{
    /// <summary>
    /// 敌人身上的状态机控制器
    /// </summary>
    public StateController<Enemy, AiStateEnum> StateController { get; private set; }

    /// <summary>
    /// 视野半径, 单位像素, 发现玩家后改视野范围可以穿墙
    /// </summary>
    public float ViewRange { get; set; } = 250;

    /// <summary>
    /// 发现玩家后的视野半径
    /// </summary>
    public float TailAfterViewRange { get; set; } = 400;

    /// <summary>
    /// 背后的视野半径, 单位像素
    /// </summary>
    public float BackViewRange { get; set; } = 50;

    /// <summary>
    /// 视野检测射线, 朝玩家打射线, 检测是否碰到墙
    /// </summary>
    public RayCast2D ViewRay { get; private set; }

    /// <summary>
    /// 导航代理
    /// </summary>
    public NavigationAgent2D NavigationAgent2D { get; private set; }

    /// <summary>
    /// 导航代理中点
    /// </summary>
    public Marker2D NavigationPoint { get; private set; }

    //开火间隙时间
    private float _enemyAttackTimer = 0;
    //目标在视野内的时间
    private float _targetInViewTime = 0;

    public override void OnInit()
    {
        base.OnInit();
        IsAi = true;
        StateController = AddComponent<StateController<Enemy, AiStateEnum>>();

        AttackLayer = PhysicsLayer.Wall | PhysicsLayer.Prop | PhysicsLayer.Player;
        EnemyLayer = PhysicsLayer.Player;
        Camp = CampEnum.Camp2;

        RoleState.MoveSpeed = 20;

        MaxHp = 20;
        Hp = 20;

        //视野射线
        ViewRay = GetNode<RayCast2D>("ViewRay");
        NavigationPoint = GetNode<Marker2D>("NavigationPoint");
        NavigationAgent2D = NavigationPoint.GetNode<NavigationAgent2D>("NavigationAgent2D");

        //PathSign = new PathSign(this, PathSignLength, GameApplication.Instance.Node3D.Player);

        //注册Ai状态机
        StateController.Register(new AiNormalState());
        StateController.Register(new AiProbeState());
        StateController.Register(new AiTailAfterState());
        StateController.Register(new AiFollowUpState());
        StateController.Register(new AiLeaveForState());
        StateController.Register(new AiSurroundState());
        StateController.Register(new AiFindAmmoState());
        
        //默认状态
        StateController.ChangeStateInstant(AiStateEnum.AiNormal);
    }

    public override void EnterTree()
    {
        base.EnterTree();
        if (!World.Enemy_InstanceList.Contains(this))
        {
            World.Enemy_InstanceList.Add(this);
        }
    }

    public override void ExitTree()
    {
        base.ExitTree();
        World.Enemy_InstanceList.Remove(this);
    }

    protected override void OnDie()
    {
        //扔掉所有武器
        var weapons = WeaponPack.GetAndClearItem();
        for (var i = 0; i < weapons.Length; i++)
        {
            weapons[i].ThrowWeapon(this);
        }

        var effPos = Position + new Vector2(0, -Altitude);
        //血液特效
        var blood = ResourceManager.LoadAndInstantiate<AutoDestroyParticles>(ResourcePath.prefab_effect_enemy_EnemyBloodEffect_tscn);
        blood.Position = effPos - new Vector2(0, 12);
        blood.AddToActivityRoot(RoomLayerEnum.NormalLayer);
        
        //创建敌人碎片
        var count = Utils.Random.RandomRangeInt(3, 6);
        for (var i = 0; i < count; i++)
        {
            var debris = Create(Ids.Id_effect0001);
            debris.PutDown(effPos, RoomLayerEnum.NormalLayer);
            debris.InheritVelocity(this);
        }
        
        //派发敌人死亡信号
        EventManager.EmitEvent(EventEnum.OnEnemyDie, this);
        Destroy();
    }

    protected override void Process(float delta)
    {
        base.Process(delta);
        _enemyAttackTimer -= delta;

        //目标在视野内的时间
        var currState = StateController.CurrState;
        if (currState == AiStateEnum.AiSurround || currState == AiStateEnum.AiFollowUp)
        {
            _targetInViewTime += delta;
        }
        else
        {
            _targetInViewTime = 0;
        }

        EnemyPickUpWeapon();
    }

    protected override void OnHit(int damage, bool realHarm)
    {
        //受到伤害
        var state = StateController.CurrState;
        if (state == AiStateEnum.AiNormal || state == AiStateEnum.AiProbe || state == AiStateEnum.AiLeaveFor)
        {
            StateController.ChangeState(AiStateEnum.AiTailAfter);
        }
    }

    /// <summary>
    /// 返回地上的武器是否有可以拾取的, 也包含没有被其他敌人标记的武器
    /// </summary>
    public bool CheckUsableWeaponInUnclaimed()
    {
        foreach (var unclaimedWeapon in World.Weapon_UnclaimedWeapons)
        {
            //判断是否能拾起武器, 条件: 相同的房间
            if (unclaimedWeapon.AffiliationArea == AffiliationArea)
            {
                if (!unclaimedWeapon.IsTotalAmmoEmpty())
                {
                    if (!unclaimedWeapon.HasSign(SignNames.AiFindWeaponSign))
                    {
                        return true;
                    }
                    else
                    {
                        //判断是否可以移除该标记
                        var enemy = unclaimedWeapon.GetSign<Enemy>(SignNames.AiFindWeaponSign);
                        if (enemy == null || enemy.IsDestroyed) //标记当前武器的敌人已经被销毁
                        {
                            unclaimedWeapon.RemoveSign(SignNames.AiFindWeaponSign);
                            return true;
                        }
                        else if (!enemy.IsAllWeaponTotalAmmoEmpty()) //标记当前武器的敌人已经有新的武器了
                        {
                            unclaimedWeapon.RemoveSign(SignNames.AiFindWeaponSign);
                            return true;
                        }
                    }
                }
            }
        }

        return false;
    }
    
    /// <summary>
    /// 寻找可用的武器
    /// </summary>
    public Weapon FindTargetWeapon()
    {
        Weapon target = null;
        var position = Position;
        foreach (var weapon in World.Weapon_UnclaimedWeapons)
        {
            //判断是否能拾起武器, 条件: 相同的房间, 或者当前房间目前没有战斗, 或者不在战斗房间
            if (weapon.AffiliationArea == AffiliationArea)
            {
                //还有弹药
                if (!weapon.IsTotalAmmoEmpty())
                {
                    //查询是否有其他敌人标记要拾起该武器
                    if (weapon.HasSign(SignNames.AiFindWeaponSign))
                    {
                        var enemy = weapon.GetSign<Enemy>(SignNames.AiFindWeaponSign);
                        if (enemy == this) //就是自己标记的
                        {

                        }
                        else if (enemy == null || enemy.IsDestroyed) //标记当前武器的敌人已经被销毁
                        {
                            weapon.RemoveSign(SignNames.AiFindWeaponSign);
                        }
                        else if (!enemy.IsAllWeaponTotalAmmoEmpty()) //标记当前武器的敌人已经有新的武器了
                        {
                            weapon.RemoveSign(SignNames.AiFindWeaponSign);
                        }
                        else //放弃这把武器
                        {
                            continue;
                        }
                    }

                    if (target == null) //第一把武器
                    {
                        target = weapon;
                    }
                    else if (target.Position.DistanceSquaredTo(position) >
                             weapon.Position.DistanceSquaredTo(position)) //距离更近
                    {
                        target = weapon;
                    }
                }
            }
        }

        return target;
    }

    /// <summary>
    /// 检查是否能切换到 AiStateEnum.AiLeaveFor 状态
    /// </summary>
    /// <returns></returns>
    public bool CanChangeLeaveFor()
    {
        if (!World.Enemy_IsFindTarget)
        {
            return false;
        }

        var currState = StateController.CurrState;
        if (currState == AiStateEnum.AiNormal || currState == AiStateEnum.AiProbe)
        {
            //判断是否在同一个房间内
            return World.Enemy_FindTargetAffiliationSet.Contains(AffiliationArea);
        }
        
        return false;
    }

    /// <summary>
    /// Ai触发的攻击
    /// </summary>
    public void EnemyAttack(float delta)
    {
        var weapon = WeaponPack.ActiveItem;
        if (weapon != null)
        {
            if (weapon.IsTotalAmmoEmpty()) //当前武器弹药打空
            {
                //切换到有子弹的武器
                var index = WeaponPack.FindIndex((we, i) => !we.IsTotalAmmoEmpty());
                if (index != -1)
                {
                    WeaponPack.ExchangeByIndex(index);
                }
                else //所有子弹打光
                {
                    
                }
            }
            else if (weapon.Reloading) //换弹中
            {

            }
            else if (weapon.IsAmmoEmpty()) //弹夹已经打空
            {
                Reload();
            }
            else if (_targetInViewTime >= weapon.Attribute.AiTargetLockingTime) //正常射击
            {
                if (weapon.GetDelayedAttackTime() > 0)
                {
                    Attack();
                }
                else
                {
                    if (weapon.Attribute.ContinuousShoot) //连发
                    {
                        Attack();
                    }
                    else //单发
                    {
                        if (_enemyAttackTimer <= 0)
                        {
                            _enemyAttackTimer = 60f / weapon.Attribute.StartFiringSpeed;
                            Attack();
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// 获取武器攻击范围 (最大距离值与最小距离的中间值)
    /// </summary>
    /// <param name="weight">从最小到最大距离的过渡量, 0 - 1, 默认 0.5</param>
    public float GetWeaponRange(float weight = 0.5f)
    {
        if (WeaponPack.ActiveItem != null)
        {
            var attribute = WeaponPack.ActiveItem.Attribute;
            return Mathf.Lerp(Utils.GetConfigRangeStart(attribute.BulletDistanceRange), Utils.GetConfigRangeEnd(attribute.BulletDistanceRange), weight);
        }

        return 0;
    }

    /// <summary>
    /// 返回目标点是否在视野范围内
    /// </summary>
    public bool IsInViewRange(Vector2 target)
    {
        var isForward = IsPositionInForward(target);
        if (isForward)
        {
            if (GlobalPosition.DistanceSquaredTo(target) <= ViewRange * ViewRange) //没有超出视野半径
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 返回目标点是否在跟随状态下的视野半径内
    /// </summary>
    public bool IsInTailAfterViewRange(Vector2 target)
    {
        var isForward = IsPositionInForward(target);
        if (isForward)
        {
            if (GlobalPosition.DistanceSquaredTo(target) <= TailAfterViewRange * TailAfterViewRange) //没有超出视野半径
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 调用视野检测, 如果被墙壁和其它物体遮挡, 则返回被挡住视野的物体对象, 视野无阻则返回 null
    /// </summary>
    public bool TestViewRayCast(Vector2 target)
    {
        ViewRay.Enabled = true;
        ViewRay.TargetPosition = ViewRay.ToLocal(target);
        ViewRay.ForceRaycastUpdate();
        return ViewRay.IsColliding();
    }

    /// <summary>
    /// 调用视野检测完毕后, 需要调用 TestViewRayCastOver() 来关闭视野检测射线
    /// </summary>
    public void TestViewRayCastOver()
    {
        ViewRay.Enabled = false;
    }

    /// <summary>
    /// AI 拾起武器操作
    /// </summary>
    private void EnemyPickUpWeapon()
    {
        //这几个状态不需要主动拾起武器操作
        var state = StateController.CurrState;
        if (state == AiStateEnum.AiNormal)
        {
            return;
        }
        
        //拾起地上的武器
        if (InteractiveItem is Weapon weapon)
        {
            if (WeaponPack.ActiveItem == null) //手上没有武器, 无论如何也要拾起
            {
                TriggerInteractive();
                return;
            }

            //没弹药了
            if (weapon.IsTotalAmmoEmpty())
            {
                return;
            }
            
            var index = WeaponPack.FindIndex((we, i) => we.ItemConfig.Id == weapon.ItemConfig.Id);
            if (index != -1) //与武器背包中武器类型相同, 补充子弹
            {
                if (!WeaponPack.GetItem(index).IsAmmoFull())
                {
                    TriggerInteractive();
                }

                return;
            }

            // var index2 = Holster.FindWeapon((we, i) =>
            //     we.Attribute.WeightType == weapon.Attribute.WeightType && we.IsTotalAmmoEmpty());
            var index2 = WeaponPack.FindIndex((we, i) => we.IsTotalAmmoEmpty());
            if (index2 != -1) //扔掉没子弹的武器
            {
                ThrowWeapon(index2);
                TriggerInteractive();
                return;
            }
            
            // if (Holster.HasVacancy()) //有空位, 拾起武器
            // {
            //     TriggerInteractive();
            //     return;
            // }
        }
    }

}
