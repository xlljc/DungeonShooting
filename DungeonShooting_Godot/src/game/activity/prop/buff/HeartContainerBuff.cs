
using Godot;

/// <summary>
/// 血量上限buff, 心之容器 + 1
/// </summary>
[GlobalClass, Tool]
public partial class HeartContainerBuff : Buff
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