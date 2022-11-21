
/// <summary>
/// AI 发现玩家
/// </summary>
public class AITailAfterState : IState<Enemy, AIStateEnum>
{
    public AIStateEnum StateType { get; } = AIStateEnum.AITailAfter;
    public Enemy Master { get; set; }
    public StateController<Enemy, AIStateEnum> StateController { get; set; }
    public void Enter(AIStateEnum prev, params object[] args)
    {
        //临时处理
        //Master.PathSign.Enable = true;
    }

    public void PhysicsProcess(float delta)
    {
        var master = Master;
        if (Master.PathSign.Enable)
        {
            var targetSign = master.PathSign;
            var enemyPos = master.GlobalPosition;
            if (targetSign.Next == null)
            {
                var targetPosition = targetSign.TargetPosition;

                if (enemyPos.DistanceSquaredTo(targetPosition) <=
                    master.Velocity.LengthSquared() * delta) //移动到下一个节点了, 还是没有找到目标, 变为第二状态
                {
                    StateController.ChangeStateLate(AIStateEnum.AINormal);
                }
                else //继续移动
                {
                    master.LookTargetPosition(targetPosition);
                    master.AnimatedSprite.Animation = AnimatorNames.Run;
                    master.Velocity = (targetPosition - enemyPos).Normalized() * master.MoveSpeed;
                    master.CalcMove(delta);
                }
            }
            else
            {
                var nextPos = targetSign.Next.GlobalPosition;

                if (enemyPos.DistanceSquaredTo(nextPos) <=
                    master.Velocity.LengthSquared() * delta) //已经移动到下一个节点了, 删除下一个节点, 后面的接上
                {
                    var nextNext = targetSign.Next.Next;
                    var tempPos = targetSign.Next.TargetPosition;
                    targetSign.Next.Next = null;
                    targetSign.Next.Destroy();
                    targetSign.Next = nextNext;

                    if (nextNext != null) //下一个点继续移动
                    {
                        nextPos = nextNext.GlobalPosition;
                        master.LookTargetPosition(nextPos);
                        master.AnimatedSprite.Animation = AnimatorNames.Run;
                        master.Velocity = (nextPos - enemyPos).Normalized() * master.MoveSpeed;
                        master.CalcMove(delta);
                    }
                    else
                    {
                        targetSign.TargetPosition = tempPos;
                    }
                }
                else //继续移动
                {
                    master.LookTargetPosition(nextPos);
                    master.AnimatedSprite.Animation = AnimatorNames.Run;
                    master.Velocity = (nextPos - enemyPos).Normalized() * master.MoveSpeed;
                    master.CalcMove(delta);
                }
            }
        }
    }

    public bool CanChangeState(AIStateEnum next)
    {
        return true;
    }

    public void Exit(AIStateEnum next)
    {
        
    }
}