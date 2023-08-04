namespace UI.MapEditorMapMark;

public class EditorMarkWaveCell : UiCell<MapEditorMapMark.WaveTemplate, object>
{
    public override void OnInit()
    {
        CellNode.L_HBoxContainer.L_TextureButton.Instance.Pressed += OnExpandOrClose;
    }

    //展开/收起按钮点击
    private void OnExpandOrClose()
    {
        var textureButton = CellNode.L_HBoxContainer.L_TextureButton.Instance;
        var marginContainer = CellNode.L_MarginContainer.Instance;
        var flag = !marginContainer.Visible;
        marginContainer.Visible = flag;
        if (flag)
        {
            textureButton.TextureNormal = ResourceManager.LoadTexture2D(ResourcePath.resource_sprite_ui_commonIcon_Down_png);
        }
        else
        {
            textureButton.TextureNormal = ResourceManager.LoadTexture2D(ResourcePath.resource_sprite_ui_commonIcon_Left_png);
        }
    }
}