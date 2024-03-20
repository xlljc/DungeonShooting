
using System.Text.Json;

[ChargeFragment("Hurt", 
    "造成伤害充能, 参数1为充满能量需要造成的伤害值")]
public class Cha_Hurt : ChargeFragment
{
    private int _value = 100;

    public override void InitParam(JsonElement[] arg)
    {
        _value = arg[0].GetInt32();
    }

    public override void OnUse()
    {
        
    }

    public override void Process(float delta)
    {
        if (Master.IsUsing)
        {
            Master.ChargeProgress = 1 - Master.UsingProgress;
        }
    }

    public override void OnUsingFinish()
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
        if (Master.IsUsing)
        {
            return;
        }
        Master.ChargeProgress += 1f / _value * value;
    }
}