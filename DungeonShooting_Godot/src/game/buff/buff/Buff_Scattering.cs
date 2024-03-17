
using Godot;

[Buff("Scattering", "提高武器精准度buff, 参数‘1’为提升的精准度百分比值(小数)")]
public class Buff_Scattering : BuffFragment
{
    private float _value;

    public override void InitParam(float arg1)
    {
        _value = arg1;
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