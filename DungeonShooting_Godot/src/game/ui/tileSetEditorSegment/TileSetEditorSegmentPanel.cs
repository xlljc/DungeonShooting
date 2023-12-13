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
}
