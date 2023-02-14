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


using System.Collections.Generic;
using Godot;

/// <summary>
/// 基础敌人
/// </summary>
public partial class Enemy : Role
{

    /// <summary>
    /// 公共属性, 是否找到目标, 如果找到目标, 则所有敌人都会知道玩家的位置
    /// </summary>
    public static bool IsFindTarget { get; private set; }

    /// <summary>
    /// 找到的目标的位置, 如果目标在视野内, 则一直更新
    /// </summary>
    public static Vector2 FindTargetPosition { get; private set; }

    private static readonly List<Enemy> _enemies = new List<Enemy>();

    /// <summary>
    /// 敌人身上的状态机控制器
    /// </summary>
    public StateController<Enemy, AiStateEnum> StateController { get; }

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
    public RayCast2D ViewRay { get; }

    /// <summary>
    /// 导航代理
    /// </summary>
    public NavigationAgent2D NavigationAgent2D { get; }

    /// <summary>
    /// 导航代理中点
    /// </summary>
    public Marker2D NavigationPoint { get; }

    private float _enemyAttackTimer = 0;

    public Enemy() : base(ResourcePath.prefab_role_Enemy_tscn)
    {
        IsAi = true;
        StateController = AddComponent<StateController<Enemy, AiStateEnum>>();

        AttackLayer = PhysicsLayer.Wall | PhysicsLayer.Props | PhysicsLayer.Player;
        Camp = CampEnum.Camp2;

        MoveSpeed = 30;

        Holster.SlotList[2].Enable = true;
        Holster.SlotList[3].Enable = true;
        
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
    }

    public override void _Ready()
    {
        base._Ready();
        //默认状态
        StateController.ChangeState(AiStateEnum.AiNormal);

        NavigationAgent2D.TargetPosition = GameApplication.Instance.RoomManager.Player.GlobalPosition;
    }

    public override void _EnterTree()
    {
        if (!_enemies.Contains(this))
        {
            _enemies.Add(this);
        }
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        _enemies.Remove(this);
    }

    protected override void OnDie()
    {
        //扔掉所有武器
        var weapons = Holster.GetAndClearWeapon();
        for (var i = 0; i < weapons.Length; i++)
        {
            weapons[i].ThrowWeapon(this);
        }
        Destroy();
    }

    protected override void Process(float delta)
    {
        base.Process(delta);
        _enemyAttackTimer -= delta;

        EnemyPickUpWeapon();
    }

    protected override void OnHit(int damage)
    {
        //受到伤害
        var state = StateController.CurrState;
        if (state == AiStateEnum.AiNormal || state == AiStateEnum.AiProbe || state == AiStateEnum.AiLeaveFor)
        {
            StateController.ChangeStateLate(AiStateEnum.AiTailAfter);
        }
    }

    /// <summary>
    /// 返回地上的武器是否有可以拾取的, 也包含没有被其他敌人标记的武器
    /// </summary>
    public bool CheckUsableWeaponInUnclaimed()
    {
        //如果存在有子弹的武器
        foreach (var unclaimedWeapon in Weapon.UnclaimedWeapons)
        {
            if (!unclaimedWeapon.IsTotalAmmoEmpty() && !unclaimedWeapon.HasSign(SignNames.AiFindWeaponSign))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 更新敌人视野
    /// </summary>
    public static void UpdateEnemiesView()
    {
        IsFindTarget = false;
        for (var i = 0; i < _enemies.Count; i++)
        {
            var enemy = _enemies[i];
            var state = enemy.StateController.CurrState;
            if (state == AiStateEnum.AiFollowUp || state == AiStateEnum.AiSurround) //目标在视野内
            {
                IsFindTarget = true;
                FindTargetPosition = Player.Current.GetCenterPosition();
            }
        }
    }

    /// <summary>
    /// Ai触发的攻击
    /// </summary>
    public void EnemyAttack(float delta)
    {
        var weapon = Holster.ActiveWeapon;
        if (weapon != null)
        {
            if (weapon.IsTotalAmmoEmpty()) //当前武器弹药打空
            {
                //切换到有子弹的武器
                var index = Holster.FindWeapon((we, i) => !we.IsTotalAmmoEmpty());
                if (index != -1)
                {
                    Holster.ExchangeByIndex(index);
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
            else //正常射击
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
        if (Holster.ActiveWeapon != null)
        {
            var attribute = Holster.ActiveWeapon.Attribute;
            return Mathf.Lerp(attribute.MinDistance, attribute.MaxDistance, weight);
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
            if (Holster.ActiveWeapon == null) //手上没有武器, 无论如何也要拾起
            {
                TriggerInteractive();
                return;
            }

            //没弹药了
            if (weapon.IsTotalAmmoEmpty())
            {
                return;
            }
            
            var index = Holster.FindWeapon((we, i) => we.TypeId == weapon.TypeId);
            if (index != -1) //与武器袋中武器类型相同, 补充子弹
            {
                if (!Holster.GetWeapon(index).IsAmmoFull())
                {
                    TriggerInteractive();
                }

                return;
            }

            // var index2 = Holster.FindWeapon((we, i) =>
            //     we.Attribute.WeightType == weapon.Attribute.WeightType && we.IsTotalAmmoEmpty());
            var index2 = Holster.FindWeapon((we, i) => we.IsTotalAmmoEmpty());
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
