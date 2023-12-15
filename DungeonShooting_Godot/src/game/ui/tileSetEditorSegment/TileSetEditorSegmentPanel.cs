using Godot;
using UI.TileSetEditor;

namespace UI.TileSetEditorSegment;

public partial class TileSetEditorSegmentPanel : TileSetEditorSegment
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
        //改变背景颜色事件
        AddEventListener(EventEnum.OnSetTileSetBgColor, OnSetTileSetBgColor);
    }

    public override void OnShowUi()
    {
        if (EditorPanel.Texture == null)
        {
            EditorWindowManager.ShowTips("警告", "请先导入纹理！", () =>
            {
                EditorPanel.TabGrid.SelectIndex = 0;
            });
        }

        S_LeftBg.Instance.OnShow();
    }

    public override void OnDestroyUi()
    {
        
    }

    //改变TileSet纹理
    private void OnSetTileTexture(object arg)
    {
        if (arg is Texture2D texture)
        {
            S_LeftBg.Instance.OnChangeTileSetTexture(texture);
            S_RightBg.Instance.OnChangeTileSetTexture(texture);
        }
    }
    
    //改变TileSet背景颜色
    private void OnSetTileSetBgColor(object arg)
    {
        //背景颜色
        if (arg is Color color)
        {
            S_LeftBg.Instance.Color = color;
        }
    }
}
