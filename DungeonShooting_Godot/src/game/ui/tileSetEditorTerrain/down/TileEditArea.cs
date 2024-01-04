using Godot;

namespace UI.TileSetEditorTerrain;

public partial class TileEditArea : EditorGridBg<TileSetEditorTerrain.BottomBg>
{
    public override void SetUiNode(IUiNode uiNode)
    {
        base.SetUiNode(uiNode);
        InitNode(UiNode.L_TileTexture.Instance, UiNode.L_Grid.Instance);
        UiNode.L_TileTexture.L_MaskBrush.Instance.Init(UiNode.L_TileTexture.Instance, UiNode.UiPanel.EditorPanel);
        
        UiNode.L_TileTexture.Instance.Texture = UiNode.UiPanel.EditorPanel.Texture;
        
        //聚焦按钮点击
        UiNode.L_FocusBtn.Instance.Pressed += OnFocusClick;
    }
    
    /// <summary>
    /// 改变TileSet纹理
    /// </summary>
    public void OnChangeTileSetTexture()
    {
        UiNode.L_TileTexture.Instance.Size = UiNode.L_TileTexture.Instance.Texture.GetSize();
        OnFocusClick();
    }
    
    //聚焦按钮点击
    private void OnFocusClick()
    {
        var texture = UiNode.L_TileTexture.Instance.Texture;
        Utils.DoFocusNode(ContainerRoot, Size, texture != null ? texture.GetSize() : Vector2.Zero);
        RefreshGridTrans();
    }
}