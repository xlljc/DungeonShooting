
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
        //检测是否有错误
        var hasError = false;
        foreach (var sourceInfo in data.TileSetInfo.Sources)
        {
            if (string.IsNullOrEmpty(sourceInfo.SourcePath))
            {
                hasError = true;
                break;
            }

            foreach (var terrainInfo in sourceInfo.Terrain)
            {
                if (!terrainInfo.Ready)
                {
                    hasError = true;
                    break;
                }
            }
        }

        if (hasError)
        {
            CellNode.L_Icon.Instance.Texture = ResourceManager.LoadTexture2D(ResourcePath.resource_sprite_ui_commonIcon_Error_mini_png);
        }
        else
        {
            CellNode.L_Icon.Instance.Texture = ResourceManager.LoadTexture2D(ResourcePath.resource_sprite_ui_commonIcon_Success_mini_png);
        }
    }

    public override void OnDoubleClick()
    {
        //打开TileSet编辑器面板
        var tileSetEditorPanel = CellNode.UiPanel.ParentUi.OpenNextUi<TileSetEditorPanel>(UiManager.UiNames.TileSetEditor);
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