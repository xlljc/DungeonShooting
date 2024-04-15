
using System.Text.Json;

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
    /// 初始化被动属性参数
    /// </summary>
    public abstract void InitParam(JsonElement[] args);
    
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
}