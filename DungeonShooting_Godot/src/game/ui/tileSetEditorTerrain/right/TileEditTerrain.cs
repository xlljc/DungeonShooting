using Godot;

namespace UI.TileSetEditorTerrain;

public partial class TileEditTerrain : EditorGridBg<TileSetEditorTerrain.LeftBottomBg>
{
    public override void SetUiNode(IUiNode uiNode)
    {
        base.SetUiNode(uiNode);
        var tileTexture = UiNode.L_TileTexture;
        InitNode(tileTexture.Instance, UiNode.L_Grid.Instance);
        tileTexture.L_Brush.Instance.TileTexture = tileTexture.Instance;
        
        //聚焦按钮点击
        UiNode.L_FocusBtn.Instance.Pressed += OnFocusClick;
    }

    public override void _Process(double delta)
    {
        var flag = UiNode.UiPanel.IsDraggingCell;
        UiNode.L_Grid.Instance.Visible = flag;
        UiNode.L_TileTexture.L_Brush.Instance.Visible = flag;
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