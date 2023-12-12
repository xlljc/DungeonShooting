using Godot;
using UI.TileSetEditor;

namespace UI.TileSetEditorSegment;

public partial class TileSetEditorSegmentPanel : TileSetEditorSegment
{

    private TileSetEditorPanel _editorPanel;
    
    public override void OnCreateUi()
    {
        _editorPanel = (TileSetEditorPanel)ParentUi;
    }

    public override void OnShowUi()
    {
        if (_editorPanel.Texture == null)
        {
            EditorWindowManager.ShowTips("警告", "请先导入纹理！", () =>
            {
                _editorPanel.TabGrid.SelectIndex = 0;
            });
        }

        S_LeftBg.Instance.Color = _editorPanel.BgColor;
    }

    public override void OnDestroyUi()
    {
        
    }

}
