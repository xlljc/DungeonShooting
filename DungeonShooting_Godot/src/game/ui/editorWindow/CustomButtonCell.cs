namespace UI.EditorWindow;

public class CustomButtonCell : UiCell<EditorWindow.CustomButton, EditorWindowPanel.ButtonData>
{
    public override void OnInit()
    {
        CellNode.L_Button.Instance.Pressed += Click;
    }

    public override void OnSetData(EditorWindowPanel.ButtonData data)
    {
        CellNode.L_Button.Instance.Text = data.Text;
    }

    public override void OnDestroy()
    {
        CellNode.L_Button.Instance.Pressed -= OnClick;
    }

    public override void OnClick()
    {
        if (Data.Callback != null)
        {
            Data.Callback();
        }
    }
}