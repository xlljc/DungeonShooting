
using System.Text.Json;

[BuffFragment("ShieldRecoveryTime", "单格护盾减少的恢复时间, 参数‘1’单位: 秒")]
public class Buff_ShieldRecoveryTime : BuffFragment
{
    private float _time;
    
    public override void InitParam(JsonElement[] args)
    {
        _time = args[0].GetSingle();
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