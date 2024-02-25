using Godot;

namespace UI.MapEditorMapTile;

public class CombinationCell : UiCell<MapEditorMapTile.CellButton, ImportCombinationData>
{
    public override void OnInit()
    {
        CellNode.L_SelectTexture.Instance.Visible = false;
    }
    
    public override void OnSetData(ImportCombinationData data)
    {
        CellNode.L_CellName.Instance.Text = data.CombinationInfo.Name;
        CellNode.L_PreviewImage.Instance.Texture = data.PreviewTexture;
    }
    
    public override void OnDisable()
    {
        if (Data.PreviewTexture != null)
        {
            Data.PreviewTexture.Dispose();
            Data.PreviewTexture = null;
        }
    }

    public override void OnDestroy()
    {
        if (Data.PreviewTexture != null)
        {
            Data.PreviewTexture.Dispose();
            Data.PreviewTexture = null;
        }
    }
    
    public override void OnSelect()
    {
        CellNode.L_SelectTexture.Instance.Visible = true;
        
        //选中组合, 将组合数据设置到笔刷上
        var editorTileMap = CellNode.UiPanel.EditorTileMap;
        editorTileMap.ClearCurrBrushAtlasCoords();
        var positions = Data.CombinationInfo.Positions;
        var cells = Data.CombinationInfo.Cells;
        for (var i = 0; i < cells.Length; i++)
        {
            editorTileMap.AddCurrBrushAtlasCoords(
                positions[i].AsVector2I() / GameConfig.TileCellSize,
                cells[i].AsVector2I() / GameConfig.TileCellSize
            );
        }
    }

    public override void OnUnSelect()
    {
        CellNode.L_SelectTexture.Instance.Visible = false;
    }
}