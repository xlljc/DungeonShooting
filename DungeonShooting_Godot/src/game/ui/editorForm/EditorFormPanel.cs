using Godot;

namespace UI.EditorForm;

/// <summary>
/// 最基础的表单显示Ui
/// </summary>
public partial class EditorFormPanel : EditorForm
{
    public override void OnCreateUi()
    {
        S_Item.Instance.Visible = false;
    }

    /// <summary>
    /// 添加表单元素
    /// </summary>
    public void AddItem<T>(FormItemData<T> item) where T : Control
    {
        var itemNode = S_Item.CloneAndPut();
        itemNode.Instance.Visible = true;
        itemNode.L_NameLabel.Instance.Text = item.Label + "：";
        itemNode.AddChild(item.UiNode);
        item.UiNode.SetHorizontalExpand(true);
        item.UiNode.SizeFlagsStretchRatio = 80;
    }
}
