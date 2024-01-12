
using Godot;

public class FormItemData<T> where T : Control
{
    /// <summary>
    /// 显示文本
    /// </summary>
    public string Label;
    /// <summary>
    /// 挂载的节点
    /// </summary>
    public T UiNode;

    public FormItemData(string label, T uiNode)
    {
        Label = label;
        UiNode = uiNode;
    }
}