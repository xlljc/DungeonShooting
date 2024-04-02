
using Godot;

/// <summary>
/// 商店老板
/// </summary>
[Tool]
public partial class ShopBoss : AiRole
{
    public override void OnInit()
    {
        base.OnInit();
        SetAttackDesire(false); //默认不攻击
        SetMoveDesire(false); //默认不攻击
    }

    protected override RoleState OnCreateRoleState()
    {
        var roleState = new RoleState();
        roleState.MoveSpeed = 50;
        return roleState;
    }

    public override void OnCreateWithMark(RoomPreinstall roomPreinstall, ActivityMark activityMark)
    {
        
    }
}