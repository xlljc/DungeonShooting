
/// <summary>
/// 生成导航返回的结果
/// </summary>
public class GenerateNavigationResult
{
    public GenerateNavigationResult(bool success, NavigationPointInterleavingException exception = null)
    {
        Success = success;
        Exception = exception;
    }

    /// <summary>
    /// 是否成功
    /// </summary>
    public bool Success;
    
    /// <summary>
    /// 异常信息
    /// </summary>
    public NavigationPointInterleavingException Exception;
}