using Godot;

namespace UI.MapEditor;

public class EditorTileMapBar
{
    private MapEditorPanel _editorPanel;
    private MapEditor.MapEditor_TileMap _editorTileMap;
    
    public EditorTileMapBar(MapEditorPanel editorPanel, MapEditor.MapEditor_TileMap editorTileMap)
    {
        _editorTileMap = editorTileMap;
        _editorPanel = editorPanel;
        _editorTileMap.Instance.MapEditorPanel = editorPanel;
    }

    public void OnShow()
    {
        _editorTileMap.L_Draw.Instance.Draw += OnDrawGuides;
        _editorPanel.ToolsPanel.S_HandTool.Instance.Pressed += _editorTileMap.Instance.OnSelectHandTool;
        _editorPanel.ToolsPanel.S_PenTool.Instance.Pressed += _editorTileMap.Instance.OnSelectPenTool;
        _editorPanel.ToolsPanel.S_RectTool.Instance.Pressed += _editorTileMap.Instance.OnSelectRectTool;
        _editorPanel.ToolsPanel.S_CenterTool.Instance.Pressed += _editorTileMap.Instance.OnClickCenterTool;
    }

    public void OnHide()
    {
        _editorTileMap.L_Draw.Instance.Draw -= OnDrawGuides;
        _editorPanel.ToolsPanel.S_HandTool.Instance.Pressed -= _editorTileMap.Instance.OnSelectHandTool;
        _editorPanel.ToolsPanel.S_PenTool.Instance.Pressed -= _editorTileMap.Instance.OnSelectPenTool;
        _editorPanel.ToolsPanel.S_RectTool.Instance.Pressed -= _editorTileMap.Instance.OnSelectRectTool;
        _editorPanel.ToolsPanel.S_CenterTool.Instance.Pressed -= _editorTileMap.Instance.OnClickCenterTool;
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