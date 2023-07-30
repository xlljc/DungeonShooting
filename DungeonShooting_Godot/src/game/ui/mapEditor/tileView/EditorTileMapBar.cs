using Godot;

namespace UI.MapEditor;

public class EditorTileMapBar
{
    private MapEditor.TileMap _editorTileMap;
    private EventFactory _eventFactory;
    
    public EditorTileMapBar(MapEditorPanel editorPanel, MapEditor.TileMap editorTileMap)
    {
        _editorTileMap = editorTileMap;
        _editorTileMap.Instance.MapEditorPanel = editorPanel;
        _editorTileMap.Instance.MapEditorToolsPanel = editorPanel.S_MapEditorTools.Instance;
        _editorTileMap.Instance.MapEditorToolsPanel.EditorMap = _editorTileMap;

        _eventFactory = EventManager.CreateEventFactory();
    }

    public void OnShow()
    {
        _editorTileMap.L_Brush.Instance.Draw += OnDrawGuides;
        _eventFactory.AddEventListener(EventEnum.OnSelectDragTool, _editorTileMap.Instance.OnSelectHandTool);
        _eventFactory.AddEventListener(EventEnum.OnSelectPenTool, _editorTileMap.Instance.OnSelectPenTool);
        _eventFactory.AddEventListener(EventEnum.OnSelectRectTool, _editorTileMap.Instance.OnSelectRectTool);
        _eventFactory.AddEventListener(EventEnum.OnSelectDoorTool, _editorTileMap.Instance.OnSelectDoorTool);
        _eventFactory.AddEventListener(EventEnum.OnClickCenterTool, _editorTileMap.Instance.OnClickCenterTool);
    }

    public void OnHide()
    {
        _editorTileMap.L_Brush.Instance.Draw -= OnDrawGuides;
        _eventFactory.RemoveAllEventListener();
    }

    public void Process(float delta)
    {
        _editorTileMap.L_Brush.Instance.QueueRedraw();
    }

    private void OnDrawGuides()
    {
        _editorTileMap.Instance.DrawGuides(_editorTileMap.L_Brush.Instance);
    }

    public void OnDestroy()
    {
        
    }
}