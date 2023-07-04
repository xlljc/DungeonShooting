
using Godot;

[Tool]
public partial class ActiveProp5000 : ActiveProp
{
    public override bool CanUse()
    {
        return !Master.IsHpFull();
    }

    protected override void OnUse()
    {
        Master.Hp += 2;
    }
}