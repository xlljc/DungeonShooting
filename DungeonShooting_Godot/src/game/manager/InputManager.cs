
using Godot;

/// <summary>
/// 输入管理器
/// </summary>
public static class InputManager
{
    /// <summary>
    /// 获取鼠标在SubViewport节点下的坐标
    /// </summary>
    public static Vector2 GetViewportMousePosition()
    {
        var application = GameApplication.Instance;
        return application.GlobalToViewPosition(application.GetGlobalMousePosition() - new Vector2(25, 25));
    }

    /// <summary>
    /// 更新输入管理器
    /// </summary>
    public static void Update(float delta)
    {
        
    }

}