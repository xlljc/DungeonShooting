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
        //搜索按钮
        S_TileSearchButton.Instance.Pressed += OnSearchClick;
        //创建按钮
        S_TileAddButton.Instance.Pressed += OnAddClick;
        //删除按钮
        S_TileDeleteButton.Instance.Pressed += OnDeleteClick;
        //编辑按钮
        S_TileEditButton.Instance.Pressed += OnEditClick;
        
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
    
    //搜索
    private void OnSearchClick()
    {
        SearchData(S_TileSearchInput.Instance.Text);
    }
    
    //创建
    private void OnAddClick()
    {
        EditorWindowManager.ShowCreateTileSet((name, split) =>
        {
            //创建TileSetInfo
            var tileSetInfo = new TileSetInfo();
            tileSetInfo.InitData();
            tileSetInfo.Name = name;
            split.SetTileSetInfo(tileSetInfo);
            GameApplication.Instance.TileSetConfig.Add(name, split);
            //保存TileSetInfo 
            EditorTileSetManager.SaveTileSetInfo(tileSetInfo);
            //保存TileSetSplit数据
            EditorTileSetManager.SaveTileSetConfig();
            //刷新
            OnSearchClick();
        });
    }
    
    //删除
    private void OnDeleteClick()
    {
        if (Grid.SelectIndex < 0)
        {
            EditorWindowManager.ShowTips("提示", "请选择要删除的TileSet！");
            return;
        }

        var tileSetSplit = Grid.SelectData;
        //这里要判断是否引用, 后面再做
        
        //删除数据
        EditorWindowManager.ShowConfirm("提示", "确认删除该TileSet吗，删除后无法恢复！", (v) =>
        {
            if (v)
            {
                Directory.Delete(EditorTileSetManager.CustomTileSetPath + tileSetSplit.TileSetInfo.Name, true);
                GameApplication.Instance.TileSetConfig.Remove(tileSetSplit.TileSetInfo.Name);
                EditorTileSetManager.SaveTileSetConfig();
                tileSetSplit.Destroy();
                //刷新
                OnSearchClick();
            }
        });
    }
    
    //编辑按钮
    private void OnEditClick()
    {
        if (Grid.SelectIndex < 0)
        {
            EditorWindowManager.ShowTips("提示", "请选择要删除的TileSet！");
            return;
        }

        var tileSetSplit = Grid.SelectData;
        EditorWindowManager.ShowEditTileSet(tileSetSplit, (v) =>
        {
            EditorTileSetManager.SaveTileSetConfig();
            //刷新
            OnSearchClick();
        });
    }
}
