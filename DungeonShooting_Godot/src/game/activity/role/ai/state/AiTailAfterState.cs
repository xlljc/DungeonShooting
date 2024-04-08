
using System;
using Godot;

namespace AiState;

/// <summary>
/// AI 发现玩家, 跟随玩家, 但是不在视野范围内
/// </summary>
public class AiTailAfterState : StateBase<AiRole, AIStateEnum>
{
    /// <summary>
    /// 目标是否在视野半径内
    /// </summary>
    private bool _isInViewRange;

    //导航目标点刷新计时器
    private float _navigationUpdateTimer = 0;
    private float _navigationInterval = 0.3f;

    //目标从视野消失时已经过去的时间
    private float _viewTimer;

    public AiTailAfterState() : base(AIStateEnum.AiTailAfter)
    {
    }

    public override void Enter(AIStateEnum prev, params object[] args)
    {
        if (Master.LookTarget == null)
        {
            ChangeState(AIStateEnum.AiNormal);
            return;
            //throw new Exception("进入 AIAdvancedStateEnum.AiTailAfter 状态时角色没有攻击目标!");
        }
        
        _isInViewRange = true;
        _navigationUpdateTimer = 0;
        _viewTimer = 0;
        
        //先检查弹药是否打光
        if (Master.IsAllWeaponTotalAmmoEmpty())
        {
            //再寻找是否有可用的武器
            var targetWeapon = Master.FindTargetWeapon();
            if (targetWeapon != null)
            {
                ChangeState(AIStateEnum.AiFindAmmo, targetWeapon);
            }
        }
    }

    public override void Process(float delta)
    {
        //这个状态下不会有攻击事件, 所以没必要每一帧检查是否弹药耗尽
        
        var playerPos = Master.LookTarget.GetCenterPosition();
        
        //更新玩家位置
        if (_navigationUpdateTimer <= 0)
        {
            //每隔一段时间秒更改目标位置
            _navigationUpdateTimer = _navigationInterval;
            Master.NavigationAgent2D.TargetPosition = playerPos;
        }
        else
        {
            _navigationUpdateTimer -= delta;
        }
        
        if (!Master.NavigationAgent2D.IsNavigationFinished())
        {
            //移动
            Master.DoMove();
        }
        else
        {
            //站立
            Master.DoIdle();
        }
        //检测玩家是否在视野内, 如果在, 则切换到 AiTargetInView 状态
        if (Master.IsInTailAfterViewRange(playerPos))
        {
            if (!Master.TestViewRayCast(playerPos)) //看到玩家
            {
                //关闭射线检测
                Master.TestViewRayCastOver();
                //切换成发现目标状态
                ChangeState(AIStateEnum.AiFollowUp);
                return;
            }
            else
            {
                //关闭射线检测
                Master.TestViewRayCastOver();
            }
        }
        
        //检测玩家是否在穿墙视野范围内, 直接检测距离即可
        _isInViewRange = Master.IsInViewRange(playerPos);
        if (_isInViewRange)
        {
            _viewTimer = 0;
        }
        else //超出视野
        {
            if (_viewTimer > 10) //10秒
            {
                ChangeState(AIStateEnum.AiNormal);
            }
            else
            {
                _viewTimer += delta;
            }
        }
    }

    public override void DebugDraw()
    {
        var playerPos = Master.LookTarget.GetCenterPosition();
        if (_isInViewRange)
        {
            Master.DrawLine(new Vector2(0, -8), Master.ToLocal(playerPos), Colors.Orange);
        }
        else
        {
            Master.DrawLine(new Vector2(0, -8), Master.ToLocal(playerPos), Colors.Blue);
        }
    }
}