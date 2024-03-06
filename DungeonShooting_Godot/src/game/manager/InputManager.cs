
using Godot;

/// <summary>
/// 输入管理器
/// </summary>
public static class InputManager
{
    /// <summary>
    /// 移动方向, 已经归一化, 键鼠: 键盘WASD
    /// </summary>
    public static Vector2 MoveAxis { get; private set; }
    
    /// <summary>
    /// 鼠标在SubViewport节点下的坐标, 键鼠: 鼠标移动
    /// </summary>
    public static Vector2 CursorPosition { get; private set; }
    
    /// <summary>
    /// 是否按下打开轮盘按钮, 键鼠: 键盘Tab
    /// </summary>
    public static bool Roulette { get; set; }
    
    /// <summary>
    /// 是否按下切换上一把武器, 键鼠: 键盘Q
    /// </summary>
    public static bool ExchangeWeapon { get; private set; }

    /// <summary>
    /// 是否按下投抛武器按钮, 键鼠: 键盘G
    /// </summary>
    public static bool ThrowWeapon { get; private set; }
    
    /// <summary>
    /// 是否按下使用道具按钮, 键鼠: 键盘F
    /// </summary>
    public static bool UseActiveProp { get; private set; }
    
    /// <summary>
    /// 是否按下切换道具按钮, 键鼠: 键盘Z
    /// </summary>
    public static bool ExchangeProp { get; private set; }
    
    /// <summary>
    /// 是否按下丢弃道具按钮, 键鼠: 键盘X
    /// </summary>
    public static bool RemoveProp { get; private set; }
    
    /// <summary>
    /// 是否按钮互动按钮, 键鼠: 键盘E
    /// </summary>
    public static bool Interactive { get; private set; }
    
    /// <summary>
    /// 是否按钮换弹按钮, 键鼠: 键盘R
    /// </summary>
    public static bool Reload { get; private set; }
    
    /// <summary>
    /// 是否按钮开火按钮, 键鼠: 鼠标左键
    /// </summary>
    public static bool Fire { get; private set; }
    
    /// <summary>
    /// 是否按钮近战攻击按钮 (使用远程武器发起的近战攻击), 键鼠: 键盘Space
    /// </summary>
    public static bool MeleeAttack { get; private set; }
    
    /// <summary>
    /// 是否按下翻滚按钮, 键鼠: 鼠标右键
    /// </summary>
    public static bool Roll { get; private set; }
    
    /// <summary>
    /// 是否按下打开地图按钮, 键鼠: 键盘Ctrl
    /// </summary>
    public static bool Map { get; private set; }
    
    /// <summary>
    /// 菜单键, 键鼠: esc
    /// </summary>
    public static bool Menu { get; private set; }

    /// <summary>
    /// 更新输入管理器
    /// </summary>
    public static void Update(float delta)
    {
        MoveAxis = Input.GetVector(InputAction.MoveLeft, InputAction.MoveRight, InputAction.MoveUp, InputAction.MoveDown);
        var application = GameApplication.Instance;
        if (application != null)
        {
            CursorPosition = application.GlobalToViewPosition(application.GetGlobalMousePosition());
        }

        ExchangeWeapon = Input.IsActionJustPressed(InputAction.ExchangeWeapon);
        ThrowWeapon = Input.IsActionJustPressed(InputAction.ThrowWeapon);
        Interactive = Input.IsActionJustPressed(InputAction.Interactive);
        Reload = Input.IsActionJustPressed(InputAction.Reload);
        Fire = Input.IsActionPressed(InputAction.Fire);
        MeleeAttack = Input.IsActionJustPressed(InputAction.MeleeAttack);
        Roll = Input.IsActionJustPressed(InputAction.Roll);
        UseActiveProp = Input.IsActionJustPressed(InputAction.UseActiveProp);
        RemoveProp = Input.IsActionJustPressed(InputAction.RemoveProp);
        ExchangeProp = Input.IsActionJustPressed(InputAction.ExchangeProp);
        Map = Input.IsActionPressed(InputAction.Map);
        Menu = Input.IsActionJustPressed(InputAction.Menu);
        Roulette = Input.IsActionPressed(InputAction.Roulette);
    }
}