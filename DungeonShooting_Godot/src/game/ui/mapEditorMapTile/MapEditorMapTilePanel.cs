using Godot;

namespace UI.MapEditorMapTile;

public partial class MapEditorMapTilePanel : MapEditorMapTile
{
    /// <summary>
    /// 使用的TileSet数据
    /// </summary>
    public TileSetSplit TileSetSplit { get; private set; }

    /// <summary>
    /// 选中的资源 Id
    /// </summary>
    public int SourceIndex { get; private set; } = -1;
    
    /// <summary>
    /// 选中的 Source
    /// </summary>
    public TileSetSourceInfo TileSetSourceInfo
    {
        get
        {
            if (SourceIndex < 0)
            {
                return null;
            }
            return TileSetSplit.TileSetInfo.Sources[SourceIndex];
        }
    }

    public override void OnCreateUi()
    {
        //切换资源
        S_SourceOption.Instance.ItemSelected += OnChangeSource;
        //切换笔刷类型
        S_HandleOption.Instance.ItemSelected += OnChangeBrush;
        OnChangeBrush(0);
    }

    public override void OnDestroyUi()
    {
        
    }

    /// <summary>
    /// 初始化TileSet数据
    /// </summary>
    public void InitData(TileSetSplit tileSetSplit)
    {
        TileSetSplit = tileSetSplit;
        //初始化Source下拉框
        var optionButton = S_SourceOption.Instance;
        foreach (var sourceInfo in tileSetSplit.TileSetInfo.Sources)
        {
            optionButton.AddItem(sourceInfo.Name);
        }

        optionButton.Selected = 0;
        OnChangeSource(0);
    }
    
    // 切换选中的资源
    private void OnChangeSource(long index)
    {
        SourceIndex = (int)index;
        if (index >= 0)
        {
            var sourceInfo = TileSetSourceInfo;
            //单格页签纹理
            S_Tab1.Instance.SetImage(sourceInfo.GetSourceImage());
            //地形页签
            S_Tab2.Instance.RefreshTerrain(sourceInfo);
        }
    }
    
    //切换笔刷类型
    private void OnChangeBrush(long index)
    {
        var v1 = false;
        var v2 = false;
        var v3 = false;
        if (index == 0) //单格
        {
            v1 = true;
        }
        else if (index == 1) //地形
        {
            v2 = true;
        }
        else if (index == 2) //组合
        {
            v3 = true;
        }

        S_Tab1.Instance.Visible = v1;
        S_Tab2.Instance.Visible = v2;
        S_Tab3.Instance.Visible = v3;
    }
}
