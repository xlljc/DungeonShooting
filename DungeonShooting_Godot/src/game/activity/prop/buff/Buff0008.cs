
using Godot;

/// <summary>
/// 眼镜, 提高武器50%精准度
/// </summary>
[GlobalClass, Tool]
public partial class Buff0008 : Buff
{
    protected override void OnPickUp(Role master)
    {
        master.RoleState.CalcStartScatteringEvent += CalcStartScatteringEvent;
        master.RoleState.CalcFinalScatteringEvent += CalcFinalScatteringEvent;
    }

    protected override void OnRemove(Role master)
    {
        master.RoleState.CalcStartScatteringEvent -= CalcStartScatteringEvent;
        master.RoleState.CalcFinalScatteringEvent -= CalcFinalScatteringEvent;
    }

    private void CalcStartScatteringEvent(Weapon weapon, float originValue, RefValue<float> refValue)
    {
        refValue.Value *= 0.5f;
    }
    
    private void CalcFinalScatteringEvent(Weapon weapon, float originValue, RefValue<float> refValue)
    {
        refValue.Value *= 0.5f;
    }
}