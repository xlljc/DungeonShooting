using Godot;

namespace UI.MapEditorCreateMark;

public abstract partial class AttributeBase : HBoxContainer, IUiNodeScript
{
    /// <summary>
    /// 属性名称
    /// </summary>
    public string AttrName { get; set; }
    
    public abstract void SetUiNode(IUiNode uiNode);
    
    /// <summary>
    /// 设置属性显示名称
    /// </summary>
    public abstract void SetAttributeLabel(string text);

    /// <summary>
    /// 获取属性值
    /// </summary>
    public abstract string GetAttributeValue();
}