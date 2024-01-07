
using Godot;
using UI.TileSetEditor;

namespace UI.TileSetEditorProject;

public class TileButtonCell : UiCell<TileSetEditorProject.TileButton, TileSetSplit>
{
    public override void OnInit()
    {
        CellNode.L_SelectTexture.Instance.Visible = false;
    }

    public override void OnSetData(TileSetSplit data)
    {
        CellNode.L_TileName.Instance.Text = data.TileSetInfo.Name;
    }

    public override void OnDoubleClick()
    {
        //打开TileSet编辑器面板
        var tileSetEditorPanel = CellNode.UiPanel.OpenNextUi<TileSetEditorPanel>(UiManager.UiNames.TileSetEditor);
        tileSetEditorPanel.InitData(Data);
    }

    public override void OnSelect()
    {
        CellNode.L_SelectTexture.Instance.Visible = true;
        var previewGrid = CellNode.UiPanel.PreviewGrid;
        previewGrid.SetDataList(Data.TileSetInfo.Sources.ToArray());
    }

    public override void OnUnSelect()
    {
        CellNode.L_SelectTexture.Instance.Visible = false;
    }
}