using Godot;

namespace UI.MapEditorMapTile;

/// <summary>
/// 组合笔刷页签
/// </summary>
public partial class CombinationTileTab : Control, IUiNodeScript
{
    public UiGrid<MapEditorMapTile.CellButton, ImportCombinationData> Grid;
    private MapEditorMapTile.Tab3 _uiNode;
    
    public void SetUiNode(IUiNode uiNode)
    {
        _uiNode = (MapEditorMapTile.Tab3)uiNode;
        Grid = _uiNode.UiPanel.CreateUiGrid<MapEditorMapTile.CellButton, ImportCombinationData, CombinationCell>(_uiNode.L_ScrollContainer.L_CellButton);
        Grid.SetCellOffset(new Vector2I(5, 5));
        Grid.SetAutoColumns(true);
        Grid.SetHorizontalExpand(true);
    }

    public void OnDestroy()
    {
        
    }

    /// <summary>
    /// 刷新组合数据
    /// </summary>
    public void RefreshCombination(TileSetSourceInfo sourceInfo)
    {
        Grid.RemoveAll();
        var src = sourceInfo.GetSourceImage();
        foreach (var combinationInfo in sourceInfo.Combination)
        {
            var previewImage = ImportCombinationData.GetPreviewTexture(src, combinationInfo.Cells, combinationInfo.Positions);
            Grid.Add(new ImportCombinationData(ImageTexture.CreateFromImage(previewImage), combinationInfo));
        }
    }
}