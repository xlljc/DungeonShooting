using Generator;
using Godot;

namespace UI.EditorTools;

[Tool]
public partial class EditorToolsPanel : EditorTools
{
    public override void OnShowUi(params object[] args)
    {
        L_ScrollContainer.L_MarginContainer.L_VBoxContainer.L_HBoxContainer3.L_Button.Instance.Pressed += OnCreateUI;
    }

    public override void OnHideUi()
    {
        L_ScrollContainer.L_MarginContainer.L_VBoxContainer.L_HBoxContainer3.L_Button.Instance.Pressed -= OnCreateUI;
    }

    private void OnCreateUI()
    {
        var text = L_ScrollContainer.L_MarginContainer.L_VBoxContainer.L_HBoxContainer3.L_LineEdit.Instance.Text;
        GD.PrintErr("ui名称: " + text);
    }
    
    private void OnCloseCreateUiConfirm()
    {
        //L_CreateUiConfirm.Instance.Hide();
    }
    
    /// <summary>
    /// 更新 ResourcePath
    /// </summary>
    private void _on_Button_pressed()
    {
        ResourcePathGenerator.Generate();
    }

    /// <summary>
    /// 重新打包房间配置
    /// </summary>
    private void _on_Button2_pressed()
    {
        RoomPackGenerator.Generate();
    }
}
