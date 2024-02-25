
using Godot;

/// <summary>
/// 输入事件名称
/// </summary>
public static class InputAction
{
    /// <summary>
    /// 鼠标左键
    /// </summary>
    public static readonly StringName MouseLeft = "mouse_left";
    /// <summary>
    /// 鼠标右键
    /// </summary>
    public static readonly StringName MouseRight = "mouse_right";
    /// <summary>
    /// 鼠标中建
    /// </summary>
    public static readonly StringName mouseMiddle = "mouse_middle";

    public static readonly StringName MoveLeft = "move_left";
    public static readonly StringName MoveRight = "move_right";
    public static readonly StringName MoveUp = "move_up";
    public static readonly StringName MoveDown = "move_down";
    public static readonly StringName ExchangeWeapon = "exchangeWeapon";
    public static readonly StringName ThrowWeapon = "throwWeapon";
    public static readonly StringName Interactive = "interactive";
    public static readonly StringName Reload = "reload";
    public static readonly StringName Fire = "fire";
    public static readonly StringName MeleeAttack = "meleeAttack";
    public static readonly StringName Roll = "roll";
    public static readonly StringName UseActiveProp = "useActiveProp";
    public static readonly StringName ExchangeProp = "exchangeProp";
    public static readonly StringName RemoveProp = "removeProp";
    public static readonly StringName Map = "map";
    public static readonly StringName Menu = "menu";
}