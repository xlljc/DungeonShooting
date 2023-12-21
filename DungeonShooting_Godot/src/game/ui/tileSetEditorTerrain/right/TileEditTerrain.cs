using Godot;

namespace UI.TileSetEditorTerrain;

public partial class TileEditTerrain : EditorGridBg<TileSetEditorTerrain.LeftBottomBg>
{
    public override void SetUiNode(IUiNode uiNode)
    {
        base.SetUiNode(uiNode);
        InitNode(UiNode.L_TileTexture.Instance, UiNode.L_Grid.Instance);
        UiNode.L_TileTexture.L_Brush.Instance.TileTexture = UiNode.L_TileTexture;
        
        //聚焦按钮点击
        UiNode.L_FocusBtn.Instance.Pressed += OnFocusClick;
    }

    public override void _Process(double delta)
    {
        var isMouseInRect = UiNode.L_TileTexture.Instance.IsMouseInRect();
        UiNode.L_Grid.Instance.Visible = isMouseInRect;
        UiNode.L_TileTexture.L_Brush.Instance.Visible = isMouseInRect;
    }

    /// <summary>
    /// 改变TileSet纹理
    /// </summary>
    public void OnChangeTileSetTexture()
    {
        //UiNode.L_TileTexture.Instance.Size = UiNode.L_TileTexture.Instance.Texture.GetSize();
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