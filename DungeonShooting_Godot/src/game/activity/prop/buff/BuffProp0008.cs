
using Godot;

/// <summary>
/// 眼镜, 提高武器50%精准度
/// </summary>
[Tool]
public partial class BuffProp0008 : BuffProp
{
    public override void OnPickUpItem()
    {
        Master.RoleState.CalcStartScatteringEvent += CalcStartScatteringEvent;
        Master.RoleState.CalcFinalScatteringEvent += CalcFinalScatteringEvent;
    }

    public override void OnRemoveItem()
    {
        Master.RoleState.CalcStartScatteringEvent -= CalcStartScatteringEvent;
        Master.RoleState.CalcFinalScatteringEvent -= CalcFinalScatteringEvent;
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