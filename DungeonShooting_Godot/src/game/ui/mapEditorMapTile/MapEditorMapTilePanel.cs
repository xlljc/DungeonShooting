using Godot;
using UI.MapEditor;

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
    
    /// <summary>
    /// 编辑器Tile对象
    /// </summary>
    public EditorTileMap EditorTileMap { get; private set; }

    public override void OnCreateUi()
    {
        var editorPanel = (MapEditorPanel)ParentUi;
        EditorTileMap = editorPanel.S_TileMap.Instance;
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
            //触发聚焦
            S_Tab1.Instance.OnFocusClick();
            //地形页签
            S_Tab2.Instance.RefreshTerrain(sourceInfo);
        }
        EditorTileMap.SetCurrSourceIndex(SourceIndex);
    }
    
    //切换笔刷类型
    private void OnChangeBrush(long index)
    {
        var v1 = false;
        var v2 = false;
        var v3 = false;
        if (index == 0) //自由绘制
        {
            v1 = true;
            EditorTileMap.SetCurrBrushType(EditorTileMap.TileMapDrawMode.Free);
        }
        else if (index == 1) //地形
        {
            v2 = true;
            EditorTileMap.SetCurrBrushType(EditorTileMap.TileMapDrawMode.Terrain);
        }
        else if (index == 2) //组合
        {
            v3 = true;
            EditorTileMap.SetCurrBrushType(EditorTileMap.TileMapDrawMode.Combination);
        }
        else
        {
            EditorTileMap.SetCurrBrushType(EditorTileMap.TileMapDrawMode.Free);
        }

        S_Tab1.Instance.Visible = v1;
        S_Tab2.Instance.Visible = v2;
        S_Tab3.Instance.Visible = v3;
    }
}
