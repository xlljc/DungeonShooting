namespace UI.EditorWindow;

public class CustomButtonCell : UiCell<EditorWindow.CustomButton, EditorWindowPanel.ButtonData>
{
    protected override void OnInit()
    {
        CellNode.L_Button.Instance.Pressed += OnClick;
    }

    protected override void OnSetData(EditorWindowPanel.ButtonData data)
    {
        CellNode.L_Button.Instance.Text = data.Text;
    }

    protected override void OnDestroy()
    {
        CellNode.L_Button.Instance.Pressed -= OnClick;
    }

    private void OnClick()
    {
        if (Data.Callback != null)
        {
            Data.Callback();
        }
    }
}