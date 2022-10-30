
using Godot;

public static class InputManager
{
    /// <summary>
    /// 获取鼠标坐标
    /// </summary>
    public static Vector2 GetMousePosition()
    {
        var viewport = RoomManager.Current.Viewport;
        return viewport.GetMousePosition() / GameConfig.WindowScale
            - (GameConfig.ViewportSize / 2) + MainCamera.Main.GlobalPosition;
    }

    /// <summary>
    /// 更新输入管理器
    /// </summary>
    public static void Update(float delta)
    {

    }

}