namespace UI.MapEditorProject;

public class GroupButtonCell : UiCell<MapEditorProject.GroupButton, MapProjectManager.MapGroupInfo>
{
    protected override void OnInit()
    {
        CellNode.Instance.Pressed += OnClick;
    }

    protected override void OnSetData(MapProjectManager.MapGroupInfo data)
    {
        CellNode.Instance.Text = data.Name;
        CellNode.Instance.TooltipText = "路径: " + data.FullPath;
    }

    protected override void OnDestroy()
    {
        CellNode.Instance.Pressed -= OnClick;
    }

    //选中工程
    private void OnClick()
    {
        ((MapEditorProjectPanel)CellNode.UiPanel).SelectGroup(Data);
    }
}