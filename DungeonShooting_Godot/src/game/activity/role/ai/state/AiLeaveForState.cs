
using System;
using Godot;

namespace AiState;

/// <summary>
/// 收到其他敌人通知, 前往发现目标的位置
/// </summary>
public class AiLeaveForState : StateBase<AiRole, AIStateEnum>
{
    //导航目标点刷新计时器
    private float _navigationUpdateTimer = 0;
    private float _navigationInterval = 0.3f;
    
    //目标
    private ActivityObject _target;
    //目标点
    private Vector2 _targetPosition;

    private float _idleTimer = 0;
    private bool _playAnimFlag = false;

    public AiLeaveForState() : base(AIStateEnum.AiLeaveFor)
    {
    }

    public override void Enter(AIStateEnum prev, params object[] args)
    {
        if (args.Length == 0)
        {
            throw new Exception("进入 AINormalStateEnum.AiLeaveFor 状态必须带上目标对象");
        }
        
        _target = (ActivityObject)args[0];
        
        //先检查弹药是否打光
        if (Master.IsAllWeaponTotalAmmoEmpty())
        {
            //再寻找是否有可用的武器
            var targetWeapon = Master.FindTargetWeapon();
            if (targetWeapon != null)
            {
                ChangeState(AIStateEnum.AiFindAmmo, targetWeapon, _target);
                return;
            }
        }

        _idleTimer = 1;
        _targetPosition = _target.GetCenterPosition();
        Master.LookTargetPosition(_targetPosition);
        
        _playAnimFlag = prev != AIStateEnum.AiFindAmmo;
        if (_playAnimFlag)
        {
            Master.AnimationPlayer.Play(AnimatorNames.Query);
        }
        
        //看向目标位置
        Master.LookTargetPosition(_target.GetCenterPosition());
    }

    public override void Exit(AIStateEnum next)
    {
        Master.AnimationPlayer.Play(AnimatorNames.Reset);
    }

    public override void Process(float delta)
    {
        if (_playAnimFlag && _idleTimer > 0)
        {
            _idleTimer -= delta;
            return;
        }
        //这个状态下不会有攻击事件, 所以没必要每一帧检查是否弹药耗尽
        
        //更新玩家位置
        if (_navigationUpdateTimer <= 0)
        {
            //每隔一段时间秒更改目标位置
            _navigationUpdateTimer = _navigationInterval;
            if (Master.AffiliationArea.RoomInfo.MarkTargetPosition.TryGetValue(_target.Id, out var pos))
            {
                _targetPosition = pos;
            }
            Master.NavigationAgent2D.TargetPosition = _targetPosition;
        }
        else
        {
            _navigationUpdateTimer -= delta;
        }

        if (!Master.NavigationAgent2D.IsNavigationFinished())
        {
            Master.LookTargetPosition(_targetPosition);
            //移动
            Master.DoMove();
        }
        else
        {
            //站立
            Master.DoIdle();
        }

        var attackTarget = Master.GetAttackTarget();
        var targetPos = attackTarget.GetCenterPosition();
        //检测玩家是否在视野内, 如果在, 则切换到 AiTargetInView 状态
        if (Master.IsInTailAfterViewRange(targetPos))
        {
            if (!Master.TestViewRayCast(targetPos)) //看到玩家
            {
                //关闭射线检测
                Master.TestViewRayCastOver();
                //切换成发现目标状态
                Master.LookTarget = attackTarget;
                ChangeState(AIStateEnum.AiAstonished, AIStateEnum.AiFollowUp);
                return;
            }
            else
            {
                //关闭射线检测
                Master.TestViewRayCastOver();
            }
        }

        //移动到目标掉了, 还没发现目标
        if (Master.NavigationAgent2D.IsNavigationFinished())
        {
            ChangeState(AIStateEnum.AiNormal);
        }
    }

    public override void DebugDraw()
    {
        Master.DrawLine(Vector2.Zero, Master.ToLocal(Master.NavigationAgent2D.TargetPosition), Colors.Yellow);
    }
}
