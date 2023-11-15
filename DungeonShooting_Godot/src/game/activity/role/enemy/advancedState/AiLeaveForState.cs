
using Godot;

namespace AdvancedState;

/// <summary>
/// 收到其他敌人通知, 前往发现目标的位置
/// </summary>
public class AiLeaveForState : StateBase<AdvancedEnemy, AIAdvancedStateEnum>
{
    //导航目标点刷新计时器
    private float _navigationUpdateTimer = 0;
    private float _navigationInterval = 0.3f;

    public AiLeaveForState() : base(AIAdvancedStateEnum.AiLeaveFor)
    {
    }

    public override void Enter(AIAdvancedStateEnum prev, params object[] args)
    {
        if (Master.World.Enemy_IsFindTarget)
        {
            Master.NavigationAgent2D.TargetPosition = Master.World.Enemy_FindTargetPosition;
        }
        else
        {
            ChangeState(prev);
            return;
        }

        //先检查弹药是否打光
        if (Master.IsAllWeaponTotalAmmoEmpty())
        {
            //再寻找是否有可用的武器
            var targetWeapon = Master.FindTargetWeapon();
            if (targetWeapon != null)
            {
                ChangeState(AIAdvancedStateEnum.AiFindAmmo, targetWeapon);
            }
        }
    }

    public override void Process(float delta)
    {
        //这个状态下不会有攻击事件, 所以没必要每一帧检查是否弹药耗尽
        
        //更新玩家位置
        if (_navigationUpdateTimer <= 0)
        {
            //每隔一段时间秒更改目标位置
            _navigationUpdateTimer = _navigationInterval;
            Master.NavigationAgent2D.TargetPosition = Master.World.Enemy_FindTargetPosition;
        }
        else
        {
            _navigationUpdateTimer -= delta;
        }

        if (!Master.NavigationAgent2D.IsNavigationFinished())
        {
            Master.LookTargetPosition(Master.World.Enemy_FindTargetPosition);
            //移动
            Master.DoMove();
        }
        else
        {
            //站立
            Master.DoIdle();
        }

        var playerPos = Player.Current.GetCenterPosition();
        //检测玩家是否在视野内, 如果在, 则切换到 AiTargetInView 状态
        if (Master.IsInTailAfterViewRange(playerPos))
        {
            if (!Master.TestViewRayCast(playerPos)) //看到玩家
            {
                //关闭射线检测
                Master.TestViewRayCastOver();
                //切换成发现目标状态
                ChangeState(AIAdvancedStateEnum.AiFollowUp);
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
            ChangeState(AIAdvancedStateEnum.AiNormal);
        }
    }

    public override void DebugDraw()
    {
        Master.DrawLine(Vector2.Zero, Master.ToLocal(Master.NavigationAgent2D.TargetPosition), Colors.Yellow);
    }
}
