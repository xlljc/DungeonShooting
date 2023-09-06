
/// <summary>
/// 地牢房间检查结果
/// </summary>
public class DungeonCheckState
{
    /// <summary>
    /// 是否存在异常
    /// </summary>
    public bool HasError;
    /// <summary>
    /// 异常消息
    /// </summary>
    public string ErrorMessage;

    public DungeonCheckState(bool hasError, string errorMessage)
    {
        HasError = hasError;
        ErrorMessage = errorMessage;
    }
}