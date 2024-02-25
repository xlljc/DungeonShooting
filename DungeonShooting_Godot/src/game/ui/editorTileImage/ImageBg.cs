using Godot;

namespace UI.EditorTileImage;

public partial class ImageBg : EditorGridBg
{
    public new EditorTileImage.Bg UiNode => (EditorTileImage.Bg)base.UiNode;
    
    public override void SetUiNode(IUiNode uiNode)
    {
        base.SetUiNode(uiNode);
        InitNode(UiNode.L_TextureRoot.Instance);
        var arr = UiManager.Get_TileSetEditor_Instance();
        if (arr.Length > 0)
        {
            UiNode.Instance.Color = arr[0].BgColor;
        }
        //聚焦按钮点击
        UiNode.L_FocusBtn.Instance.Pressed += DoFocus;
    }
    
    /// <summary>
    /// 聚焦
    /// </summary>
    public void DoFocus()
    {
        var texture = UiNode.L_TextureRoot.L_TileSprite.Instance.Texture;
        Utils.DoFocusNode(ContainerRoot, Size, texture != null ? texture.GetSize() : Vector2.Zero);
        RefreshGridTrans();
    }
}