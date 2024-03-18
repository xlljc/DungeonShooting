
/// <summary>
/// 道具逻辑片段组件
/// </summary>
public abstract class PropFragment : Component<PropActivity>
{
    /// <summary>
    /// 所属角色对象
    /// </summary>
    public Role Role => Master?.Master;
    
    /// <summary>
    /// 当道具被拾起时调用 (在 Role 赋值之后调用)
    /// </summary>
    public abstract void OnPickUpItem();

    /// <summary>
    /// 当道具被移除时调用 (在 Role 置为 null 之前调用)
    /// </summary>
    public abstract void OnRemoveItem();

    /// <summary>
    /// 返回道具是否在背包中
    /// </summary>
    public bool IsInPackage()
    {
        return Master != null;
    }
    
    /// <summary>
    /// 初始化被动属性参数
    /// </summary>
    public virtual void InitParam(float arg1)
    {
        Debug.LogError($"'{GetType().FullName}'为实现1参数初始化函数!");
    }
    
    /// <summary>
    /// 初始化被动属性参数
    /// </summary>
    public virtual void InitParam(float arg1, float arg2)
    {
        Debug.LogError($"'{GetType().FullName}'为实现2参数初始化函数!");
    }
    
    /// <summary>
    /// 初始化被动属性参数
    /// </summary>
    public virtual void InitParam(float arg1, float arg2, float arg3)
    {
        Debug.LogError($"'{GetType().FullName}'为实现4参数初始化函数!");
    }
    
    /// <summary>
    /// 初始化被动属性参数
    /// </summary>
    public virtual void InitParam(float arg1, float arg2, float arg3, float arg4)
    {
        Debug.LogError($"'{GetType().FullName}'为实现4参数初始化函数!");
    }
}