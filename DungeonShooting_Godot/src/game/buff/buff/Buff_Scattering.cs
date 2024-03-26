
using System.Text.Json;
using Godot;

[BuffFragment(
    "Scattering",
    "提高武器精准度 buff",
    Arg1 = "(float)精准度提升百分比值"
)]
public class Buff_Scattering : BuffFragment
{
    private float _value;

    public override void InitParam(JsonElement[] args)
    {
        _value = args[0].GetSingle();
    }

    public override void OnPickUpItem()
    {
        Role.RoleState.CalcStartScatteringEvent += CalcStartScatteringEvent;
        Role.RoleState.CalcFinalScatteringEvent += CalcFinalScatteringEvent;
    }

    public override void OnRemoveItem()
    {
        Role.RoleState.CalcStartScatteringEvent -= CalcStartScatteringEvent;
        Role.RoleState.CalcFinalScatteringEvent -= CalcFinalScatteringEvent;
    }

    private void CalcStartScatteringEvent(float originValue, RefValue<float> refValue)
    {
        refValue.Value = Mathf.Max(0, refValue.Value - refValue.Value * _value);
    }
    
    private void CalcFinalScatteringEvent(float originValue, RefValue<float> refValue)
    {
        refValue.Value = Mathf.Max(0, refValue.Value - refValue.Value * _value);
    }
}