namespace UI.MapEditor;

public class LayerButtonCell : UiCell<MapEditor.LayerButton, EditorLayerBar.LayerButtonData>
{
    private bool _visible;
    
    public override void OnInit()
    {
        CellNode.L_VisibleButton.Instance.Pressed += OnVisibleButtonClick;
    }

    public override void OnSetData(EditorLayerBar.LayerButtonData data)
    {
        if (data.IsLock)
        {
            CellNode.Instance.Icon = ResourceManager.LoadTexture2D(ResourcePath.resource_sprite_ui_mapEditorTools_Lock_png);
        }
        else
        {
            CellNode.Instance.Icon = ResourceManager.LoadTexture2D(ResourcePath.resource_sprite_ui_mapEditorTools_Unlock_png);
        }

        CellNode.Instance.Text = data.Title;
        var panel = (MapEditorPanel)CellNode.UiPanel;
        _visible = panel.S_TileMap.Instance.IsLayerEnabled(data.Layer);
        SetVisibleIcon(_visible);
    }

    private void OnVisibleButtonClick()
    {
        var panel = (MapEditorPanel)CellNode.UiPanel;
        _visible = !_visible;
        panel.S_TileMap.Instance.SetLayerEnabled(Data.Layer, _visible);
        SetVisibleIcon(_visible);
    }

    private void SetVisibleIcon(bool visible)
    {
        if (visible)
        {
            CellNode.L_VisibleButton.Instance.TextureNormal = ResourceManager.LoadTexture2D(ResourcePath.resource_sprite_ui_mapEditorTools_Visible_png);
        }
        else
        {
            CellNode.L_VisibleButton.Instance.TextureNormal = ResourceManager.LoadTexture2D(ResourcePath.resource_sprite_ui_mapEditorTools_Hide_png);
        }
    }
}