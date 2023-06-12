
using Godot;

/// <summary>
/// 输入管理器
/// </summary>
public static class InputManager
{
    /// <summary>
    /// 移动方向
    /// </summary>
    public static Vector2 MoveAxis { get; private set; }
    
    /// <summary>
    /// 鼠标在SubViewport节点下的坐标
    /// </summary>
    public static Vector2 CursorPosition { get; private set; }
    
    /// <summary>
    /// 是否按下切换武器
    /// </summary>
    public static bool Exchange { get; private set; }

    /// <summary>
    /// 是否按钮投抛武器按钮
    /// </summary>
    public static bool Throw { get; private set; }
    
    /// <summary>
    /// 是否按钮互动按钮
    /// </summary>
    public static bool Interactive { get; private set; }
    
    /// <summary>
    /// 是否按钮换弹按钮
    /// </summary>
    public static bool Reload { get; private set; }
    
    /// <summary>
    /// 是否按钮开火按钮
    /// </summary>
    public static bool Fire { get; private set; }
    
    /// <summary>
    /// 是否按钮近战攻击按钮 (使用远程武器发起的近战攻击)
    /// </summary>
    public static bool MeleeAttack { get; private set; }
    
    /// <summary>
    /// 是否按下翻滚按钮
    /// </summary>
    public static bool Roll { get; private set; }
    
    /// <summary>
    /// 更新输入管理器
    /// </summary>
    public static void Update(float delta)
    {
        var application = GameApplication.Instance;
        MoveAxis = Input.GetVector("move_left", "move_right", "move_up", "move_down");
        CursorPosition = application.GlobalToViewPosition(application.GetGlobalMousePosition());
        Exchange = Input.IsActionJustPressed("exchange");
        Throw = Input.IsActionJustPressed("throw");
        Interactive = Input.IsActionJustPressed("interactive");
        Reload = Input.IsActionJustPressed("reload");
        Fire = Input.IsActionPressed("fire");
        MeleeAttack = Input.IsActionPressed("meleeAttack");
        Roll = Input.IsActionPressed("roll");
    }

}