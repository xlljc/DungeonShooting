using Godot;
using UI.TileSetEditor;

namespace UI.TileSetEditorTerrain;

public partial class TileSetEditorTerrainPanel : TileSetEditorTerrain
{
    /// <summary>
    /// 父Ui
    /// </summary>
    public TileSetEditorPanel EditorPanel;
    
    public override void OnCreateUi()
    {
        EditorPanel = (TileSetEditorPanel)ParentUi;
        
        //改变纹理事件
        AddEventListener(EventEnum.OnSetTileTexture, OnSetTileTexture);
    }

    public override void OnDestroyUi()
    {
        
    }

    public override void OnShowUi()
    {
        S_LeftBg.Instance.OnShow();
    }
    
    //改变TileSet纹理
    private void OnSetTileTexture(object arg)
    {
        S_LeftBg.Instance.OnChangeTileSetTexture();
    }
}
