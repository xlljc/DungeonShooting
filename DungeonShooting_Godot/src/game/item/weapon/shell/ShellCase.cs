
using Godot;

/// <summary>
/// 弹壳类
/// </summary>
public partial class ShellCase : ActivityObject
{
    public ShellCase() : base(ResourcePath.prefab_weapon_shell_ShellCase_tscn)
    {
        ShadowOffset = new Vector2(0, 1);
    }

    protected override void OnThrowOver()
    {
        //AwaitDestroy();
        AnimationPlayer.Play("flicker");
    }

    private async void AwaitDestroy()
    {
        //2秒后销毁
        await ToSignal(GetTree().CreateTimer(2), "timeout");
        Destroy();
    }
}