namespace UI.MapEditor;

public class EditorTileMapBar
{
    private MapEditor.MapEditor_TileMap _editorTileMap;
    
    public EditorTileMapBar(MapEditor.MapEditor_TileMap editorTileMap)
    {
        _editorTileMap = editorTileMap;
    }

    public void OnShow()
    {
        _editorTileMap.L_Draw.Instance.Draw += OnDrawGuides;
    }

    public void OnHide()
    {
        _editorTileMap.L_Draw.Instance.Draw -= OnDrawGuides;
    }

    public void Process(float delta)
    {
        _editorTileMap.L_Draw.Instance.QueueRedraw();
    }

    private void OnDrawGuides()
    {
        _editorTileMap.Instance.DrawGuides(_editorTileMap.L_Draw.Instance);
    }
}