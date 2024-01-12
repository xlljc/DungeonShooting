namespace UI.TileSetEditorTerrain;

public class TerrainTabCell : UiCell<TileSetEditorTerrain.TerrainTab, TileSetTerrainInfo>
{
    public override void OnInit()
    {
        CellNode.L_SelectTexture.Instance.Visible = false;
    }

    public override void OnSetData(TileSetTerrainInfo data)
    {
        CellNode.Instance.Text = data.Name;
    }

    public override void OnSelect()
    {
        CellNode.L_SelectTexture.Instance.Visible = true;
    }

    public override void OnUnSelect()
    {
        CellNode.L_SelectTexture.Instance.Visible = false;
    }
}