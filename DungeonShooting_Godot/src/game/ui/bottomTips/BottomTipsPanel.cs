using Godot;

namespace UI.BottomTips;

public partial class BottomTipsPanel : BottomTips
{
    public override void OnCreateUi()
    {
        L_Panel.Instance.Resized += OnPanelResized;
    }

    public override void OnShowUi()
    {
        L_Panel.L_MarginContainer.L_CenterContainer.L_HBoxContainer.L_Label.Instance.Text = "提示内容";
    }

    public override void OnHideUi()
    {
        
    }

    public override void OnDisposeUi()
    {
        L_Panel.Instance.Resized += OnPanelResized;
    }

    private void OnPanelResized()
    {
        var pivotOffset = L_Panel.Instance.PivotOffset;
        pivotOffset.X = L_Panel.Instance.Size.X / 2;
        L_Panel.Instance.PivotOffset = pivotOffset;
    }

}
