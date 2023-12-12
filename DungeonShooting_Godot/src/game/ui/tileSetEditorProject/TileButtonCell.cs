
using UI.TileSetEditor;

namespace UI.TileSetEditorProject;

public class TileButtonCell : UiCell<TileSetEditorProject.TileButton, TileSetInfo>
{
    public override void OnSetData(TileSetInfo data)
    {
        CellNode.L_TileName.Instance.Text = data.Name;
    }

    public override void OnDoubleClick()
    {
        var tileSetEditorPanel = CellNode.UiPanel.OpenNextUi<TileSetEditorPanel>(UiManager.UiNames.TileSetEditor);
        tileSetEditorPanel.InitData(Data);
    }
}