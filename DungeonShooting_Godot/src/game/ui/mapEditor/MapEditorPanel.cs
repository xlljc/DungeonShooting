using Godot;
using UI.MapEditorTools;

namespace UI.MapEditor;

public partial class MapEditorPanel : MapEditor
{
    /// <summary>
    /// 左上角工具面板
    /// </summary>
    public MapEditorToolsPanel ToolsPanel { get; private set; }
    
    private EditorTileMapBar _editorTileMapBar;

    public override void OnCreateUi()
    {
        _editorTileMapBar = new EditorTileMapBar(this, S_TileMap);
        ToolsPanel = S_CanvasLayer.OpenNestedUi<MapEditorToolsPanel>(UiManager.UiName.MapEditorTools);
    }

    public override void OnShowUi()
    {
        S_Left.Instance.Resized += OnMapViewResized;
        OnMapViewResized();
        
        _editorTileMapBar.OnShow();
    }

    public override void OnHideUi()
    {
        S_Left.Instance.Resized -= OnMapViewResized;
        _editorTileMapBar.OnHide();
    }

    public override void Process(float delta)
    {
        _editorTileMapBar.Process(delta);
    }

    //调整地图显示区域大小
    private void OnMapViewResized()
    {
        S_SubViewport.Instance.Size = S_Left.Instance.Size.AsVector2I();
    }
}
