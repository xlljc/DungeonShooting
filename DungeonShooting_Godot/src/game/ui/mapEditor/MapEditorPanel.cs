using Godot;

namespace UI.MapEditor;

public partial class MapEditorPanel : MapEditor
{
    private EditorTileMapBar _editorTileMapBar;
    
    public override void OnCreateUi()
    {
        _editorTileMapBar = new EditorTileMapBar(S_TileMap);
        S_CanvasLayer.OpenNestedUi(UiManager.UiName.MapEditorTools);
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
