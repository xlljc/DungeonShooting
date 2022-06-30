
/// <summary>
/// 道具接口
/// </summary>
public interface IProp
{
    /// <summary>
    /// 与角色互动时调用
    /// </summary>
    /// <param name="master">触发者</param>
    void Tnteractive(Role master);
}