using Godot;

namespace UI.MapEditorCreateMark;

public abstract partial class AttributeBase : Control, IUiNodeScript
{
    /// <summary>
    /// 属性名称
    /// </summary>
    public string AttrName { get; set; }
    
    public abstract void SetUiNode(IUiNode uiNode);
    public abstract void OnDestroy();

    /// <summary>
    /// 获取属性值
    /// </summary>
    public abstract string GetAttributeValue();
}