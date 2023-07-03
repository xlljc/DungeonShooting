
using Godot;

[Tool]
public partial class ActiveProp5000 : ActiveProp
{
    protected override void OnPickUp(Role master)
    {
        
    }

    protected override void OnRemove(Role master)
    {
        
    }

    protected override void OnUse()
    {
        GD.Print("使用道具...");
    }
}