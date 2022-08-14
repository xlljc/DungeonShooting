using Godot;

/// <summary>
/// 道具接口
/// </summary>
public interface IProp
{
    /// <summary>
    /// 获取道具所在的坐标
    /// </summary>
    Vector2 GlobalPosition { get; }

    /// <summary>
    /// 返回是否能互动
    /// </summary>
    /// <param name="master">触发者</param>
    CheckInteractiveResult CheckInteractive(Role master);

    /// <summary>
    /// 与角色互动时调用
    /// </summary>
    /// <param name="master">触发者</param>
    void Interactive(Role master);
}