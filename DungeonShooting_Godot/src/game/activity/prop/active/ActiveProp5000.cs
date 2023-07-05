
using Godot;

[Tool]
public partial class ActiveProp5000 : ActiveProp
{
    public override void OnInit()
    {
        AutoDestroy = true;
        MaxCount = 20;
        Count = 20;
        CooldownTime = 5;
    }

    public override bool OnCheckUse()
    {
        return !Master.IsHpFull();
    }

    protected override int OnUse()
    {
        Master.Hp += 2;
        return 1;
    }
}