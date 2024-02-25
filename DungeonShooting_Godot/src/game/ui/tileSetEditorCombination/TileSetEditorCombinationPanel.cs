using Godot;
using UI.TileSetEditor;

namespace UI.TileSetEditorCombination;

public partial class TileSetEditorCombinationPanel : TileSetEditorCombination
{
    /// <summary>
    /// 父Ui
    /// </summary>
    public TileSetEditorPanel EditorPanel;
    
    public override void OnCreateUi()
    {
        EditorPanel = (TileSetEditorPanel)ParentUi;
        
        //改变选中资源事件
        AddEventListener(EventEnum.OnSelectTileSetSource, OnSelectTileSetSource);
        //改变纹理事件
        AddEventListener(EventEnum.OnSetTileTexture, OnSetTileTexture);
        //改变背景颜色事件
        AddEventListener(EventEnum.OnSetTileSetBgColor, OnSetTileSetBgColor);
        OnSetTileSetBgColor(EditorPanel.BgColor);
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
    }

    public override void OnDestroyUi()
    {
        
    }
    
    //改变选中资源事件
    private void OnSelectTileSetSource(object obj)
    {
        //取消选中组合
        S_LeftBottomBg.Instance.ClearSelectCell();
        //清除绘制的组合
        S_LeftTopBg.Instance.ClearAllCell();
        //清除gird中的组合
        var grid = S_RightBg.Instance.Grid;
        grid.RemoveAll();
        if (obj != null)
        {
            //grid加载组合
            var sourceInfo = (TileSetSourceInfo)obj;
            foreach (var combinationInfo in sourceInfo.Combination)
            {
                var src = EditorPanel.TextureImage;
                var previewImage = ImportCombinationData.GetPreviewTexture(src, combinationInfo.Cells, combinationInfo.Positions);
                grid.Add(new ImportCombinationData(ImageTexture.CreateFromImage(previewImage), combinationInfo));
            }
        }
    }

    //改变TileSet纹理
    private void OnSetTileTexture(object arg)
    {
        S_LeftBottomBg.Instance.OnChangeTileSetTexture();
        if (EditorPanel.InitTexture)
        {
            S_RightBg.Instance.OnChangeTileSetTexture();
        }
    }
    
    //改变TileSet背景颜色
    private void OnSetTileSetBgColor(object arg)
    {
        //背景颜色
        if (arg is Color color)
        {
            S_LeftTopBg.Instance.Color = color;
            S_LeftBottomBg.Instance.Color = color;
        }
    }
}
