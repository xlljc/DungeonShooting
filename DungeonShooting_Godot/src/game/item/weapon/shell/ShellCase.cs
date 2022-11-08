
using Godot;

/// <summary>
/// 弹壳类
/// </summary>
public class ShellCase : ActivityObject
{
    public ShellCase() : base(ResourcePath.prefab_weapon_shell_ShellCase_tscn)
    {
        ShadowOffset = new Vector2(0, 1);
    }

    protected override void OnThrowOver()
    {
        AwaitDestroy();
    }

    private async void AwaitDestroy()
    {
        //30秒后销毁
        await ToSignal(GetTree().CreateTimer(30), "timeout");
        Destroy();
    }
}