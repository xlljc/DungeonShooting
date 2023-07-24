
using Godot;

/// <summary>
/// 检测互动返回的数据集
/// </summary>
public class CheckInteractiveResult
{
    public enum InteractiveType
    {
        /// <summary>
        /// 拾起物体
        /// </summary>
        PickUp,
        /// <summary>
        /// 拾起弹药
        /// </summary>
        Bullet,
        /// <summary>
        /// 替换
        /// </summary>
        Replace,
    }
    
    /// <summary>
    /// 互动物体
    /// </summary>
    public ActivityObject Target;
    /// <summary>
    /// 是否可以互动
    /// </summary>
    public bool CanInteractive = false;
    /// <summary>
    /// 互动提示类型
    /// </summary>
    public InteractiveType Type = InteractiveType.PickUp;

    public CheckInteractiveResult(ActivityObject target)
    {
        Target = target;
    }
    
    public CheckInteractiveResult(ActivityObject target, bool canInteractive)
    {
        Target = target;
        CanInteractive = canInteractive;
    }
    
    public CheckInteractiveResult(ActivityObject target, bool canInteractive, InteractiveType type)
    {
        Target = target;
        CanInteractive = canInteractive;
        Type = type;
    }

    /// <summary>
    /// 互动提示显示的图标
    /// </summary>
    public Texture2D GetIcon()
    {
        if (!_init)
        {
            Init();
        }
        switch (Type)
        {
            case InteractiveType.PickUp:
                return _pickUpIcon;
            case InteractiveType.Bullet:
                return _bulletIcon;
            case InteractiveType.Replace:
                return _replaceIcon;
        }

        return null;
    }
    
    private static Texture2D _pickUpIcon;
    private static Texture2D _bulletIcon;
    private static Texture2D _replaceIcon;
    private static bool _init = false;
    private static void Init()
    {
        _init = true;
        _pickUpIcon = ResourceManager.Load<Texture2D>(ResourcePath.resource_sprite_ui_roomUI_icon_pickup_png);
        _bulletIcon = ResourceManager.Load<Texture2D>(ResourcePath.resource_sprite_ui_roomUI_icon_bullet_png);
        _replaceIcon = ResourceManager.Load<Texture2D>(ResourcePath.resource_sprite_ui_roomUI_icon_replace_png);
    }
}