using Godot;

/// <summary>
/// 道具背包 道具容量+1
/// </summary>
[Tool]
public partial class BuffProp0014 : BuffProp
{
    public override void OnPickUpItem()
    {
        Master.ActivePropsPack.SetCapacity(Master.ActivePropsPack.Capacity + 1);
    }

    public override void OnRemoveItem()
    {
        Master.ActivePropsPack.SetCapacity(Master.ActivePropsPack.Capacity - 1);
    }
}