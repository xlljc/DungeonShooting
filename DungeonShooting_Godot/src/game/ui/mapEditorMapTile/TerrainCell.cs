namespace UI.MapEditorMapTile;

/// <summary>
/// 地形选项, Data 为 TileSetTerrainInfo 的 index
/// </summary>
public class TerrainCell : UiCell<MapEditorMapTile.TerrainItem, int>
{
    /// <summary>
    /// 选中的地形配置数据
    /// </summary>
    public TileSetTerrainInfo TileSetTerrainInfo;
    
    public override void OnInit()
    {
        CellNode.L_Select.Instance.Visible = false;
        CellNode.L_ErrorIcon.Instance.Visible = false;
    }

    public override void OnSetData(int data)
    {
        TileSetTerrainInfo = CellNode.UiPanel.TileSetSourceInfo.Terrain[data];
        CellNode.L_TerrainName.Instance.Text = TileSetTerrainInfo.Name;
        
        //是否可以使用
        if (TileSetTerrainInfo.Ready)
        {
            CellNode.L_ErrorIcon.Instance.Visible = false;
            
        }
        else
        {
            CellNode.L_ErrorIcon.Instance.Visible = true;
        }
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