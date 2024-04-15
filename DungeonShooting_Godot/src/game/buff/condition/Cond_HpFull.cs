
using System.Text.Json;

[ConditionFragment(
    "HpFull", 
    "判断满血状态, ",
    Arg1 = "(boolean)判断非满血"
)]
public class Cond_HpFull : ConditionFragment
{
    private bool _type;
    
    public override void InitParam(JsonElement[] arg)
    {
        _type = arg[0].GetBoolean();
    }

    public override bool OnCheckUse()
    {
        if (_type)
        {
            return !Role.IsHpFull();
        }
        else
        {
            return Role.IsHpFull();
        }
    }
}