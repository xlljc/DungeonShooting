
/// <summary>
/// 护盾恢复时间buff
/// </summary>
[Buff("ShieldRecoveryTime", "单格护盾恢复时间, 参数‘1’单位: 秒")]
public class Buff_ShieldRecoveryTime : BuffFragment
{
    private float _time;
    
    public override void InitParam(float arg1)
    {
        _time = arg1;
    }
    
    public override void OnPickUpItem()
    {
        Role.RoleState.ShieldRecoveryTime -= _time;
    }

    public override void OnRemoveItem()
    {
        Role.RoleState.ShieldRecoveryTime += _time;
    }
}