
using Godot;

/// <summary>
/// 弹壳类
/// </summary>
[RegisterActivity(ActivityIdPrefix.Shell + "0001", ResourcePath.prefab_weapon_shell_ShellCase_tscn)]
public partial class ShellCase : ActivityObject
{
    public override void OnInit()
    {
        base.OnInit();
        ShadowOffset = new Vector2(0, 1);
        ThrowCollisionSize = new Vector2(5, 5);
    }

    protected override void OnThrowOver()
    {
        EnableBehavior = false;
        Collision.QueueFree();
    }
}