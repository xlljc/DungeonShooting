using Godot;
using UI.TileSetEditor;

namespace UI.TileSetEditorTerrain;

public partial class TileSetEditorTerrainPanel : TileSetEditorTerrain
{
    /// <summary>
    /// 父Ui
    /// </summary>
    public TileSetEditorPanel EditorPanel;
    
    /// <summary>
    /// 是否正在拖拽图块
    /// </summary>
    public bool IsDraggingCell { get; set; }

    private UiGrid<RightCell, bool> _topGrid1;
    private UiGrid<RightCell, bool> _topGrid2;
    private UiGrid<RightCell, bool> _topGrid3;
    private UiGrid<BottomCell, Rect2I> _bottomGrid;
    
    public override void OnCreateUi()
    {
        EditorPanel = (TileSetEditorPanel)ParentUi;
        
        //改变纹理事件
        AddEventListener(EventEnum.OnSetTileTexture, OnSetTileTexture);
        //背景颜色改变
        AddEventListener(EventEnum.OnSetTileSetBgColor, OnChangeTileSetBgColor);
        
        _bottomGrid = CreateUiGrid<BottomCell, Rect2I, MaskCell>(S_BottomCell);
        _bottomGrid.SetCellOffset(Vector2I.Zero);

        _topGrid1 = InitTopGrid(S_TerrainRoot.L_TerrainTexture1.Instance);
        _topGrid2 = InitTopGrid(S_TerrainRoot.L_TerrainTexture2.Instance);
        _topGrid3 = InitTopGrid(S_TerrainRoot.L_TerrainTexture3.Instance);

        OnSetTileTexture(EditorPanel.Texture);
        OnChangeTileSetBgColor(EditorPanel.BgColor);
    }

    public override void OnDestroyUi()
    {
        
    }

    public override void Process(float delta)
    {
        S_MaskBrush.Instance.Visible = !IsDraggingCell;
    }

    private UiGrid<RightCell, bool> InitTopGrid(Control texture)
    {
        var cellRoot = S_TopBg.L_TerrainRoot.L_CellRoot;
        var sRightCell = cellRoot.L_RightCell;
        var terrainSize = texture.Size.AsVector2I();
        terrainSize = terrainSize / GameConfig.TileCellSize;
        sRightCell.Instance.Position = texture.Position;
        var grid = CreateUiGrid<RightCell, bool, TerrainCell>(sRightCell, cellRoot.Instance);
        grid.SetCellOffset(Vector2I.Zero);
        grid.SetColumns(terrainSize.X);
        for (var y = 0; y < terrainSize.Y; y++)
        {
            for (var x = 0; x < terrainSize.X; x++)
            {
                grid.Add(false);
            }
        }
        
        return grid;
    }

    //改变TileSet纹理
    private void OnSetTileTexture(object arg)
    {
        S_BottomBg.Instance.OnChangeTileSetTexture();

        _bottomGrid.RemoveAll();
        var cellHorizontal = EditorPanel.CellHorizontal;
        if (cellHorizontal <= 0)
        {
            return;
        }
        var cellVertical = EditorPanel.CellVertical;
        _bottomGrid.SetColumns(cellHorizontal);
        for (var y = 0; y < cellVertical; y++)
        {
            for (var x = 0; x < cellHorizontal; x++)
            {
                _bottomGrid.Add(new Rect2I(x * GameConfig.TileCellSize, y * GameConfig.TileCellSize, GameConfig.TileCellSize, GameConfig.TileCellSize));
            }
        }
    }
    
    //更改背景颜色
    private void OnChangeTileSetBgColor(object obj)
    {
        S_BottomBg.Instance.Color = EditorPanel.BgColor;
        S_TopBg.Instance.Color = EditorPanel.BgColor;
    }
}
