
/// <summary>
/// 道具接口
/// </summary>
public interface IProp
{
    /// <summary>
    /// 返回是否能互动
    /// </summary>
    /// <param name="master">触发者</param>
    bool CanTnteractive(Role master);

    /// <summary>
    /// 与角色互动时调用
    /// </summary>
    /// <param name="master">触发者</param>
    void Tnteractive(Role master);
}