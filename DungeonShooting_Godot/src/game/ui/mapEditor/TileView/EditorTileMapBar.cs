using Godot;

namespace UI.MapEditor;

public class EditorTileMapBar
{
    private MapEditorPanel _editorPanel;
    private MapEditor.TileMap _editorTileMap;
    
    public EditorTileMapBar(MapEditorPanel editorPanel, MapEditor.TileMap editorTileMap)
    {
        _editorTileMap = editorTileMap;
        _editorPanel = editorPanel;
        _editorTileMap.Instance.MapEditorPanel = editorPanel;
        _editorTileMap.Instance.MapEditorToolsPanel = editorPanel.S_MapEditorTools.Instance;
        _editorTileMap.Instance.MapEditorToolsPanel.EditorMap = _editorTileMap;
    }

    public void OnShow()
    {
        _editorTileMap.L_Brush.Instance.Draw += OnDrawGuides;
        var mapEditorToolsPanel = _editorPanel.S_MapEditorTools.Instance;
        mapEditorToolsPanel.S_HandTool.Instance.Pressed += _editorTileMap.Instance.OnSelectHandTool;
        mapEditorToolsPanel.S_PenTool.Instance.Pressed += _editorTileMap.Instance.OnSelectPenTool;
        mapEditorToolsPanel.S_RectTool.Instance.Pressed += _editorTileMap.Instance.OnSelectRectTool;
        mapEditorToolsPanel.S_DoorTool.Instance.Pressed += _editorTileMap.Instance.OnSelectDoorTool;
        mapEditorToolsPanel.S_CenterTool.Instance.Pressed += _editorTileMap.Instance.OnClickCenterTool;
    }

    public void OnHide()
    {
        _editorTileMap.L_Brush.Instance.Draw -= OnDrawGuides;
        var mapEditorToolsPanel = _editorPanel.S_MapEditorTools.Instance;
        mapEditorToolsPanel.S_HandTool.Instance.Pressed -= _editorTileMap.Instance.OnSelectHandTool;
        mapEditorToolsPanel.S_PenTool.Instance.Pressed -= _editorTileMap.Instance.OnSelectPenTool;
        mapEditorToolsPanel.S_RectTool.Instance.Pressed -= _editorTileMap.Instance.OnSelectRectTool;
        mapEditorToolsPanel.S_DoorTool.Instance.Pressed -= _editorTileMap.Instance.OnSelectDoorTool;
        mapEditorToolsPanel.S_CenterTool.Instance.Pressed -= _editorTileMap.Instance.OnClickCenterTool;
    }

    public void Process(float delta)
    {
        _editorTileMap.L_Brush.Instance.QueueRedraw();
    }

    private void OnDrawGuides()
    {
        _editorTileMap.Instance.DrawGuides(_editorTileMap.L_Brush.Instance);
    }
}