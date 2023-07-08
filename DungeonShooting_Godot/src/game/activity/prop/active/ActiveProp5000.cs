
using Godot;

/// <summary>
/// 医药箱, 使用后恢复一颗红心
/// </summary>
[Tool]
public partial class ActiveProp5000 : ActiveProp
{
    public override void OnInit()
    {
        AutoDestroy = true;
        MaxCount = 10;
        Superposition = true;
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