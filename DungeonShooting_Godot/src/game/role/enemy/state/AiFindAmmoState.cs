
using System.Linq;
using Godot;

/// <summary>
/// Ai 寻找弹药
/// </summary>
public class AiFindAmmoState : StateBase<Enemy, AiStateEnum>
{
    /// <summary>
    /// Ai 对武器的标记名称, 一旦有该标记, Ai AiFindAmmoState 状态下寻找可用武器将忽略该武器
    /// </summary>
    public const string AiFindWeaponSign = "AiFindWeaponSign";


    private Weapon _target;

    //导航目标点刷新计时器
    private float _navigationUpdateTimer = 0;
    private float _navigationInterval = 1f;

    public AiFindAmmoState() : base(AiStateEnum.AiFindAmmo)
    {
    }

    public override void Enter(AiStateEnum prev, params object[] args)
    {
        _navigationUpdateTimer = 0;
        FindTargetWeapon();
        if (_target == null)
        {
            ChangeStateLate(prev);
            return;
        }

        //标记武器
        _target.SetSign(AiFindWeaponSign, Master);
    }

    public override void PhysicsProcess(float delta)
    {
        var activeWeapon = Master.Holster.ActiveWeapon;
        if (activeWeapon != null && !activeWeapon.IsTotalAmmoEmpty()) //已经有弹药了
        {
            ChangeStateLate(AiStateEnum.AiNormal);
            return;
        }
        
        if (_target != null)
        {
            //更新目标位置
            if (_navigationUpdateTimer <= 0)
            {
                //每隔一段时间秒更改目标位置
                _navigationUpdateTimer = _navigationInterval;
                var position = _target.GlobalPosition;
                if (Master.NavigationAgent2D.GetTargetLocation() != position)
                {
                    Master.NavigationAgent2D.SetTargetLocation(position);
                }
            }
            else
            {
                _navigationUpdateTimer -= delta;
            }

            //枪口指向玩家
            Master.LookTargetPosition(Player.Current.GlobalPosition);

            if (_target.IsQueuedForDeletion() || _target.IsTotalAmmoEmpty()) //已经被销毁, 或者弹药已经被其他角色捡走
            {
                //再去寻找其他武器
                FindTargetWeapon();

                if (_target == null) //也没有其他可用的武器了
                {
                    ChangeStateLate(AiStateEnum.AiNormal);
                }
            }
            else if (_target.Master == Master) //已经被自己拾起
            {
                ChangeStateLate(AiStateEnum.AiNormal);
            }
            else if (_target.Master != null) //武器已经被其他角色拾起!
            {
                //再去寻找其他武器
                FindTargetWeapon();

                if (_target == null) //也没有其他可用的武器了
                {
                    ChangeStateLate(AiStateEnum.AiNormal);
                }
            }
            else
            {
                //向武器移动
                if (!Master.NavigationAgent2D.IsNavigationFinished())
                {
                    //计算移动
                    var nextPos = Master.NavigationAgent2D.GetNextLocation();
                    Master.AnimatedSprite.Animation = AnimatorNames.Run;
                    Master.Velocity =
                        (nextPos - Master.GlobalPosition - Master.NavigationPoint.Position).Normalized() *
                        Master.MoveSpeed;
                    Master.CalcMove(delta);
                }
            }
        }
        else
        {

        }
    }

    public override void DebugDraw()
    {
        if (_target != null)
        {
            Master.DrawLine(Vector2.Zero, Master.ToLocal(_target.GlobalPosition), Colors.Purple);
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
                if (weapon.HasSign(AiFindWeaponSign))
                {
                    var enemy = weapon.GetSign<Enemy>(AiFindWeaponSign);
                    if (enemy == Master) //就是自己标记的
                    {
                        
                    }
                    else if (enemy == null || enemy.IsQueuedForDeletion()) //标记当前武器的敌人已经被销毁
                    {
                        weapon.RemoveSign(AiFindWeaponSign);
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
            Master.NavigationAgent2D.SetTargetLocation(_target.GlobalPosition);
        }
    }
}