using Godot;

namespace UI.MapEditorProject;

public class GroupButtonCell : UiCell<MapEditorProject.GroupButton, DungeonRoomGroup>
{
    public override void OnInit()
    {
        CellNode.L_SelectTexture.Instance.Visible = false;
    }

    public override void OnSetData(DungeonRoomGroup info)
    {
        CellNode.Instance.Text = info.GroupName;
        CellNode.Instance.TooltipText = "路径: " + MapProjectManager.CustomMapPath + "/" + info.GroupName;
    }

    public override void OnRefreshIndex()
    {
        GD.Print("刷新索引: " + Index);
    }

    //选中工程
    public override void OnSelect()
    {
        CellNode.UiPanel.SelectGroup(Data);
        CellNode.L_SelectTexture.Instance.Visible = true;
    }

    public override void OnUnSelect()
    {
        CellNode.L_SelectTexture.Instance.Visible = false;
    }
}