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
        
    }

    public void OnHide()
    {
        
    }

    public void Process(float delta)
    {
        _editorTileMap.Instance.QueueRedraw();
    }
    
    
}