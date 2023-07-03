
using Godot;

/// <summary>
/// 血量上限buff, 心之容器 + 1
/// </summary>
[Tool]
public partial class BuffPropProp0002 : BuffProp
{
    protected override void OnPickUp(Role master)
    {
        master.MaxHp += 2;
        master.Hp += 2;
    }

    protected override void OnRemove(Role master)
    {
        master.MaxHp -= 2;
    }
}