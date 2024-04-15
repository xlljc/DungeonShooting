using System.Text.Json;

[ConditionFragment(
    "Hp", 
    "判断血量, ",
    Arg1 = "(string)判断条件符号, 分别为: >, <, =, >=, <=",
    Arg2 = "(int)比较的值"
)]
public class Cond_Hp : ConditionFragment
{
    private string _symbol;
    private int _value;
    public override void InitParam(JsonElement[] args)
    {
        _symbol = args[0].GetString();
        _value = args[1].GetInt32();
    }

    public override bool OnCheckUse()
    {
        switch (_symbol)
        {
            case ">":
                return Role.Hp > _value;
            case "<":
                return Role.Hp < _value;
            case "=":
                return Role.Hp == _value;
            case ">=":
                return Role.Hp >= _value;
            case "<=":
                return Role.Hp <= _value;
        }

        return false;
    }
}