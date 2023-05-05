
/// <summary>
/// 通过表达式创建的 ActivityObject 对象结果集
/// </summary>
public class ActivityObjectResult<T> where T : ActivityObject
{
    /// <summary>
    /// 实例
    /// </summary>
    public T ActivityObject;
    
    /// <summary>
    /// 创建该对象使用的表达式数据
    /// </summary>
    public ActivityExpressionData ExpressionData;

    public ActivityObjectResult(T activityObject, ActivityExpressionData expressionData)
    {
        ActivityObject = activityObject;
        ExpressionData = expressionData;
    }
}