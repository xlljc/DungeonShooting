
using Godot;

/// <summary>
/// Ai 寻找弹药
/// </summary>
public class AiFindAmmoState : StateBase<Enemy, AiStateEnum>
{

    private Weapon _target;

    //导航目标点刷新计时器
    private float _navigationUpdateTimer = 0;
    private float _navigationInterval = 1f;

    private bool _isInTailAfterRange = false;
    private float _tailAfterTimer = 0;

    public AiFindAmmoState() : base(AiStateEnum.AiFindAmmo)
    {
    }

    public override void Enter(AiStateEnum prev, params object[] args)
    {
        _navigationUpdateTimer = 0;
        _isInTailAfterRange = false;
        _tailAfterTimer = 0;
        //找到能用的武器
        FindTargetWeapon();
        if (_target == null)
        {
            ChangeStateLate(prev);
            return;
        }

        //标记武器
        _target.SetSign(SignNames.AiFindWeaponSign, Master);
    }

    public override void PhysicsProcess(float delta)
    {
        if (!Master.IsAllWeaponTotalAmmoEmpty()) //已经有弹药了
        {
            ChangeStateLate(GetNextState());
            return;
        }

        //更新目标位置
        if (_navigationUpdateTimer <= 0)
        {
            //每隔一段时间秒更改目标位置
            _navigationUpdateTimer = _navigationInterval;
            var position = _target.GlobalPosition;
            Master.NavigationAgent2D.TargetPosition = position;
        }
        else
        {
            _navigationUpdateTimer -= delta;
        }

        var playerPos = Player.Current.GetCenterPosition();
        //枪口指向玩家
        Master.LookTargetPosition(playerPos);

        if (_target.IsQueuedForDeletion() || _target.IsTotalAmmoEmpty()) //已经被销毁, 或者弹药已经被其他角色捡走
        {
            //再去寻找其他武器
            FindTargetWeapon();

            if (_target == null) //也没有其他可用的武器了
            {
                ChangeStateLate(GetNextState());
            }
        }
        else if (_target.Master == Master) //已经被自己拾起
        {
            ChangeStateLate(GetNextState());
        }
        else if (_target.Master != null) //武器已经被其他角色拾起!
        {
            //再去寻找其他武器
            FindTargetWeapon();

            if (_target == null) //也没有其他可用的武器了
            {
                ChangeStateLate(GetNextState());
            }
        }
        else
        {
            //检测目标没有超出跟随视野距离
            _isInTailAfterRange = Master.IsInTailAfterViewRange(playerPos);
            if (_isInTailAfterRange)
            {
                _tailAfterTimer = 0;
            }
            else
            {
                _tailAfterTimer += delta;
            }

            //向武器移动
            if (!Master.NavigationAgent2D.IsNavigationFinished())
            {
                //计算移动
                var nextPos = Master.NavigationAgent2D.GetNextPathPosition();
                Master.AnimatedSprite.Play(AnimatorNames.Run);
                Master.BasisVelocity =
                    (nextPos - Master.GlobalPosition - Master.NavigationPoint.Position).Normalized() *
                    Master.MoveSpeed;
            }
            else
            {
                Master.BasisVelocity = Vector2.Zero;
            }
        }
    }

    private AiStateEnum GetNextState()
    {
        return _tailAfterTimer > 10 ? AiStateEnum.AiNormal : AiStateEnum.AiTailAfter;
    }
    
    public override void DebugDraw()
    {
        if (_target != null)
        {
            Master.DrawLine(Vector2.Zero, Master.ToLocal(_target.GlobalPosition), Colors.Purple);

            if (_tailAfterTimer <= 0)
            {
                Master.DrawLine(Vector2.Zero, Master.ToLocal(Player.Current.GetCenterPosition()), Colors.Orange);
            }
            else if (_tailAfterTimer <= 10)
            {
                Master.DrawLine(Vector2.Zero, Master.ToLocal(Player.Current.GetCenterPosition()), Colors.Blue);
            }
            
        }
    }

    private void FindTargetWeapon()
    {
        _target = null;
        var position = Master.Position;
        foreach (var weapon in Weapon.UnclaimedWeapons)
        {
            if (!weapon.IsTotalAmmoEmpty())
            {
                //查询是否有其他敌人标记要拾起该武器
                if (weapon.HasSign(SignNames.AiFindWeaponSign))
                {
                    var enemy = weapon.GetSign<Enemy>(SignNames.AiFindWeaponSign);
                    if (enemy == Master) //就是自己标记的
                    {

                    }
                    else if (enemy == null || enemy.IsQueuedForDeletion()) //标记当前武器的敌人已经被销毁
                    {
                        weapon.RemoveSign(SignNames.AiFindWeaponSign);
                    }
                    else //放弃这把武器
                    {
                        continue;
                    }
                }

                if (_target == null) //第一把武器
                {
                    _target = weapon;
                }
                else if (_target.Position.DistanceSquaredTo(position) >
                         weapon.Position.DistanceSquaredTo(position)) //距离更近
                {
                    _target = weapon;
                }
            }
        }

        //设置目标点
        if (_target != null)
        {
            Master.NavigationAgent2D.TargetPosition = _target.GlobalPosition;
        }
    }
}