
using Godot;

namespace NnormalState;

/// <summary>
/// 目标在视野内, 跟进目标, 如果距离在子弹有效射程内, 则开火
/// </summary>
public class AiFollowUpState : StateBase<Enemy, AINormalStateEnum>
{
    //导航目标点刷新计时器
    private float _navigationUpdateTimer = 0;
    private float _navigationInterval = 0.3f;

    public AiFollowUpState() : base(AINormalStateEnum.AiFollowUp)
    {
    }

    public override void Enter(AINormalStateEnum prev, params object[] args)
    {
        _navigationUpdateTimer = 0;
        Master.TargetInView = true;
    }

    public override void Process(float delta)
    {
        var playerPos = Player.Current.GetCenterPosition();

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
        
        var distanceSquared = Master.GlobalPosition.DistanceSquaredTo(playerPos);
        //是否在攻击范围内
        var inAttackRange = distanceSquared <= Mathf.Pow(Master.GetAttackRange(), 2);

        //枪口指向玩家
        Master.LookTargetPosition(playerPos);
        
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

        //检测玩家是否在视野内
        if (Master.IsInTailAfterViewRange(playerPos))
        {
            Master.TargetInView = !Master.TestViewRayCast(playerPos);
            //关闭射线检测
            Master.TestViewRayCastOver();
        }
        else
        {
            Master.TargetInView = false;
        }

        //在视野中
        if (Master.TargetInView)
        {
            if (inAttackRange) //在攻击范围内
            {
                //距离够近, 可以切换到环绕模式
                if (distanceSquared <= Mathf.Pow(Master.GetAttackRange() * 0.7f, 2) * 0.7f)
                {
                    ChangeState(AINormalStateEnum.AiSurround);
                }
                else if (Master.GetAttackTimer() <= 0) //攻击
                {
                    ChangeState(AINormalStateEnum.AiAttack);
                }
            }
        }
        else //不在视野中
        {
            ChangeState(AINormalStateEnum.AiTailAfter);
        }
    }

    public override void DebugDraw()
    {
        var playerPos = Player.Current.GetCenterPosition();
        Master.DrawLine(new Vector2(0, -8), Master.ToLocal(playerPos), Colors.Red);
    }
}