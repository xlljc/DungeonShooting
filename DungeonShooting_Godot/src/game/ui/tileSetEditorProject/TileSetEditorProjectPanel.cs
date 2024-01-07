using System.IO;
using System.Text.Json;
using Godot;

namespace UI.TileSetEditorProject;

public partial class TileSetEditorProjectPanel : TileSetEditorProject
{
    /// <summary>
    /// TileSet列表
    /// </summary>
    public UiGrid<TileButton, TileSetSplit> Grid { get; private set; }
    
    /// <summary>
    /// TileSet Source 预览列表
    /// </summary>
    public UiGrid<Preview, TileSetSourceInfo> PreviewGrid { get; private set; }
    
    public override void OnCreateUi()
    {
        S_Back.Instance.Visible = PrevUi != null;
        S_Back.Instance.Pressed += () =>
        {
            OpenPrevUi();
        };

        S_TileSearchButton.Instance.Pressed += OnSearchClick;

        Grid = CreateUiGrid<TileButton, TileSetSplit, TileButtonCell>(S_TileButton);
        Grid.SetColumns(1);
        Grid.SetCellOffset(new Vector2I(0, 5));
        Grid.SetHorizontalExpand(true);

        PreviewGrid = CreateUiGrid<Preview, TileSetSourceInfo, PreviewCell>(S_Preview);
        PreviewGrid.SetColumns(1);
        PreviewGrid.SetCellOffset(new Vector2I(0, 15));
        PreviewGrid.SetHorizontalExpand(true);
        
        //初始化数据
        OnSearchClick();
    }

    public override void OnShowUi()
    {
        OnSearchClick();
    }

    /// <summary>
    /// 刷新数据
    /// </summary>
    public void SearchData(string text)
    {
        Grid.RemoveAll();
        PreviewGrid.RemoveAll();
        text = text.ToLower();
        foreach (var tileSetSplit in GameApplication.Instance.TileSetConfig)
        {
            if (text.Length == 0 || tileSetSplit.Value.TileSetInfo.Name.ToLower().Contains(text))
            {
                Grid.Add(tileSetSplit.Value);
            }
        }
    }
    
    private void OnSearchClick()
    {
        SearchData(S_TileSearchInput.Instance.Text);
    }
}
