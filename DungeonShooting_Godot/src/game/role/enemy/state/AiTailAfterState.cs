
/// <summary>
/// AI 发现玩家
/// </summary>
public class AiTailAfterState : StateBase<Enemy, AIStateEnum>
{
    
    private float _navigationUpdateTimer = 0;
    
    public AiTailAfterState() : base(AIStateEnum.AITailAfter)
    {
    }

    public override void PhysicsProcess(float delta)
    {
        //跟随玩家
        var master = Master;
        if (master.NavigationAgent2D.IsNavigationFinished())
        {
            return;
        }
        var playerGlobalPosition = GameApplication.Instance.Room.Player.GlobalPosition;
        //临时处理, 让敌人跟随玩家
        if (_navigationUpdateTimer <= 0)
        {
            _navigationUpdateTimer = 0.2f;
            if (master.NavigationAgent2D.GetTargetLocation() != playerGlobalPosition)
            {
                master.NavigationAgent2D.SetTargetLocation(playerGlobalPosition);
            }
        }
        else
        {
            _navigationUpdateTimer -= delta;
        }
        
        var nextPos = master.NavigationAgent2D.GetNextLocation();
        master.LookTargetPosition(playerGlobalPosition);
        master.AnimatedSprite.Animation = AnimatorNames.Run;
        master.Velocity = (nextPos - master.GlobalPosition - master.NavigationPoint.Position).Normalized() * master.MoveSpeed;
        master.CalcMove(delta);
    }
}