
using Godot;

public static class InputManager
{
    /// <summary>
    /// 获取鼠标坐标
    /// </summary>
    public static Vector2 GetMousePosition()
    {
        var application = GameApplication.Instance;
        return application.GlobalToViewPosition(application.GetGlobalMousePosition());
    }

    /// <summary>
    /// 更新输入管理器
    /// </summary>
    public static void Update(float delta)
    {
        
    }

}