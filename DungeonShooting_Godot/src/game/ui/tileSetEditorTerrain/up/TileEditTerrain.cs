using Godot;

namespace UI.TileSetEditorTerrain;

public partial class TileEditTerrain : EditorGridBg<TileSetEditorTerrain.TopBg>
{
    public override void SetUiNode(IUiNode uiNode)
    {
        base.SetUiNode(uiNode);
        var tileTexture = UiNode.L_TerrainRoot;
        InitNode(tileTexture.Instance, UiNode.L_Grid.Instance);
        var terrainBrush = tileTexture.L_Brush.Instance;
        terrainBrush.Root = tileTexture.Instance;
        terrainBrush.TerrainTextureList.Add(tileTexture.L_TerrainTexture1.Instance);
        terrainBrush.TerrainTextureList.Add(tileTexture.L_TerrainTexture2.Instance);
        terrainBrush.TerrainTextureList.Add(tileTexture.L_TerrainTexture3.Instance);
        
        //聚焦按钮点击
        UiNode.L_FocusBtn.Instance.Pressed += OnFocusClick;
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
        var root = UiNode.L_TerrainRoot.Instance;
        Utils.DoFocusNode(ContainerRoot, Size, root.Size);
        RefreshGridTrans();
    }
}