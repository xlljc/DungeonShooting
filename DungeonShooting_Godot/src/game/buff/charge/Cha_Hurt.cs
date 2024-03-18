
[ChargeFragment("Hurt", 
    "造成伤害充能, 参数1为充满能量需要造成的伤害值")]
public class Cha_Hurt : ChargeFragment
{
    private int _value = 100;

    public override void InitParam(float arg1)
    {
        _value = (int)arg1;
    }

    public override void OnUse()
    {
        Master.ChargeProgress = 0;
    }

    public override void OnPickUpItem()
    {
        Role.OnDamageEvent += OnDamageEvent;
    }

    public override void OnRemoveItem()
    {
        Role.OnDamageEvent -= OnDamageEvent;
    }

    private void OnDamageEvent(Role role, int value)
    {
        Master.ChargeProgress += 1f / _value * value;
    }
}