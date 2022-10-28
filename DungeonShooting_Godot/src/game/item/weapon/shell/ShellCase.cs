
using Godot;

public class ShellCase : ActivityObject
{
    public ShellCase() : base("res://prefab/weapon/shell/ShellCase.tscn")
    {
        ShadowOffset = new Vector2(0, 1);
    }

    public override void OnThrowOver()
    {
        AwaitDestroy();
    }

    private async void AwaitDestroy()
    {
        //30秒后销毁
        await ToSignal(GetTree().CreateTimer(30), "timeout");
        QueueFree();
    }
}