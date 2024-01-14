namespace UI.MapEditorMapTile;

/// <summary>
/// 地形选项, Data 为 TileSetTerrainInfo 的 index
/// </summary>
public class TerrainCell : UiCell<MapEditorMapTile.TerrainItem, int>
{
    public TileSetTerrainInfo TileSetTerrainInfo;
    
    public override void OnInit()
    {
        CellNode.L_Select.Instance.Visible = false;
    }

    public override void OnSetData(int data)
    {
        TileSetTerrainInfo = CellNode.UiPanel.TileSetSourceInfo.Terrain[data];
        CellNode.L_TerrainName.Instance.Text = TileSetTerrainInfo.Name;
    }

    public override void OnSelect()
    {
        CellNode.L_Select.Instance.Visible = true;
    }

    public override void OnUnSelect()
    {
        CellNode.L_Select.Instance.Visible = false;
    }
}