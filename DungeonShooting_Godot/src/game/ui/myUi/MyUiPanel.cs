using Godot;

namespace UI.MyUi;

public partial class MyUiPanel : MyUi
{

    public override void OnShowUi()
    {
        L_Control.L_Label.Instance.Text = "文本";
        L_Button.Instance.Pressed += () =>
        {

        };
    }

    public override void OnHideUi()
    {
        
    }

}
