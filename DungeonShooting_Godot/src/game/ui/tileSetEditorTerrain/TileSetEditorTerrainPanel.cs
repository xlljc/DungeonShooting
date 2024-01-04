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

    private UiGrid<LeftCell, Rect2I> _leftGrid;
    private UiGrid<RightCell, bool> _rightGrid;
    
    public override void OnCreateUi()
    {
        EditorPanel = (TileSetEditorPanel)ParentUi;
        
        //改变纹理事件
        AddEventListener(EventEnum.OnSetTileTexture, OnSetTileTexture);
        //背景颜色改变
        AddEventListener(EventEnum.OnSetTileSetBgColor, OnChangeTileSetBgColor);
        
        _leftGrid = CreateUiGrid<LeftCell, Rect2I, MaskCell>(S_LeftCell);
        _leftGrid.SetCellOffset(Vector2I.Zero);

        var sRightCell = S_RightCell;
        var terrainSize = S_TerrainRoot.Instance.Size.AsVector2I();
        terrainSize = terrainSize / GameConfig.TileCellSize;
        Debug.Log("terrainSize: " + terrainSize);
        sRightCell.Instance.Position = Vector2.Zero;
        _rightGrid = CreateUiGrid<RightCell, bool, TerrainCell>(sRightCell);
        _rightGrid.SetCellOffset(Vector2I.Zero);
        _rightGrid.SetColumns(terrainSize.X);
        for (var y = 0; y < terrainSize.Y; y++)
        {
            for (var x = 0; x < terrainSize.X; x++)
            {
                _rightGrid.Add(false);
            }
        }

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

    //改变TileSet纹理
    private void OnSetTileTexture(object arg)
    {
        S_LeftBg.Instance.OnChangeTileSetTexture();

        _leftGrid.RemoveAll();
        var cellHorizontal = EditorPanel.CellHorizontal;
        if (cellHorizontal <= 0)
        {
            return;
        }
        var cellVertical = EditorPanel.CellVertical;
        _leftGrid.SetColumns(cellHorizontal);
        for (var y = 0; y < cellVertical; y++)
        {
            for (var x = 0; x < cellHorizontal; x++)
            {
                _leftGrid.Add(new Rect2I(x * GameConfig.TileCellSize, y * GameConfig.TileCellSize, GameConfig.TileCellSize, GameConfig.TileCellSize));
            }
        }
    }
    
    //更改背景颜色
    private void OnChangeTileSetBgColor(object obj)
    {
        S_LeftBg.Instance.Color = EditorPanel.BgColor;
        S_LeftBottomBg.Instance.Color = EditorPanel.BgColor;
    }
}
