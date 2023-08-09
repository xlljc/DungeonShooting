using Config;

namespace UI.MapEditorCreateMark;

public class MarkObjectCell : UiCell<MapEditorCreateMark.MarkObject, ExcelConfig.ActivityObject>
{
    public override void OnInit()
    {
        CellNode.L_CenterContainer.L_DeleteButton.Instance.Pressed += OnDeleteClick;
    }

    public override void OnSetData(ExcelConfig.ActivityObject data)
    {
        if (string.IsNullOrEmpty(data.Icon))
        {
            CellNode.L_Icon.Instance.Visible = false;
        }
        else
        {
            CellNode.L_Icon.Instance.Visible = true;
            CellNode.L_Icon.Instance.Texture = ResourceManager.LoadTexture2D(data.Icon);
        }

        CellNode.L_IdLabel.Instance.Text = data.Id;
        CellNode.L_NameLabel.Instance.Text = data.Name;
    }

    //点击删除按钮
    private void OnDeleteClick()
    {
        Grid.RemoveByIndex(Index);
    }
}