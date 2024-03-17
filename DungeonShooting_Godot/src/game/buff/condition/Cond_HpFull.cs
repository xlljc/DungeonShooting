
[ConditionFragment("HpFull", 
    "判断满血状态, " +
    "参数1可选值: 0:判断非满血, 1:判断满血")]
public class Cond_HpFull : ConditionFragment
{
    private int _type;
    
    public override void InitParam(float arg1)
    {
        _type = (int)arg1;
    }

    public override bool OnCheckUse()
    {
        if (_type == 0)
        {
            return !Role.IsHpFull();
        }
        else
        {
            return Role.IsHpFull();
        }
    }
}