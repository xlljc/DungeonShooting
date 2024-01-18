using Godot;

namespace UI.MapEditorMapTile;

/// <summary>
/// 地形笔刷页签
/// </summary>
public partial class TerrainTileTab : Control, IUiNodeScript
{
    private MapEditorMapTile.Tab2 _uiNode;

    private UiGrid<MapEditorMapTile.TerrainItem, TerrainData> _uiGrid;
    
    public void SetUiNode(IUiNode uiNode)
    {
        _uiNode = (MapEditorMapTile.Tab2)uiNode;
        _uiGrid = _uiNode.UiPanel.CreateUiGrid<MapEditorMapTile.TerrainItem, TerrainData, TerrainCell>(_uiNode.L_ScrollContainer.L_TerrainItem);
        _uiGrid.SetColumns(1);
        _uiGrid.SetCellOffset(new Vector2I(0, 8));
        _uiGrid.SetHorizontalExpand(true);
    }

    public void OnDestroy()
    {
        
    }

    public void RefreshTerrain(TileSetSourceInfo sourceInfo)
    {
        _uiGrid.RemoveAll();

        var startSetIndex = 0;
        var panel = _uiNode.UiPanel;
        for (var i = 0; i < panel.SourceIndex; i++)
        {
            var source = panel.TileSetSplit.TileSetInfo.Sources[i];
            startSetIndex += source.Terrain.Count;
        }
        
        var start = panel.SourceIndex == 0 ? 1 : 0; //跳过 Main Source 中的 Main Terrain
        for (var i = start; i < sourceInfo.Terrain.Count; i++)
        {
            _uiGrid.Add(new TerrainData(i, sourceInfo.Terrain[i], startSetIndex + i));
        }
    }
}