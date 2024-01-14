using Godot;

namespace UI.MapEditorMapTile;

/// <summary>
/// 单格笔刷页签
/// </summary>
public partial class SingleTileTab : EditorGridBg<MapEditorMapTile.Tab1>
{
    private ImageTexture _texture;
    
    public override void SetUiNode(IUiNode uiNode)
    {
        base.SetUiNode(uiNode);
        InitNode(UiNode.L_TabRoot.Instance, UiNode.L_Grid.Instance);
        _texture = new ImageTexture();
        UiNode.L_TabRoot.L_TileSprite.Instance.Texture = _texture;
        
        //聚焦按钮
        UiNode.L_FocusBtn.Instance.Pressed += OnFocusClick;
    }

    protected override void Dispose(bool disposing)
    {
        _texture.Dispose();
    }

    public void SetImage(Image image)
    {
        _texture.SetImage(image);
    }

    //聚焦按钮点击
    private void OnFocusClick()
    {
        var texture = UiNode.L_TabRoot.L_TileSprite.Instance.Texture;
        Utils.DoFocusNode(ContainerRoot, Size, texture != null ? texture.GetSize() : Vector2.Zero);
        RefreshGridTrans();
    }
}