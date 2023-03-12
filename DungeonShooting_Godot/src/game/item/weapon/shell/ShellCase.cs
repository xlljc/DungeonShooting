
using Godot;

/// <summary>
/// 弹壳类
/// </summary>
[RegisterActivity(ActivityIdPrefix.Shell + "0001", ResourcePath.prefab_weapon_shell_ShellCase_tscn)]
public partial class ShellCase : ActivityObject
{
    /// <summary>
    /// 动画播放器
    /// </summary>
    public AnimationPlayer AnimationPlayer { get; private set; }
    
    public override void _Ready()
    {
        AnimationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
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