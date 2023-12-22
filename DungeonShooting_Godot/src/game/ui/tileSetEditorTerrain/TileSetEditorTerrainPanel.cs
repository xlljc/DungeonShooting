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

    private UiGrid<Cell, Rect2I> _grid;
    
    public override void OnCreateUi()
    {
        EditorPanel = (TileSetEditorPanel)ParentUi;
        
        //改变纹理事件
        AddEventListener(EventEnum.OnSetTileTexture, OnSetTileTexture);

        _grid = CreateUiGrid<Cell, Rect2I, MaskCell>(S_Cell);
        _grid.SetCellOffset(Vector2I.Zero);
        OnSetTileTexture(EditorPanel.Texture);
    }

    public override void OnDestroyUi()
    {
        
    }
    
    //改变TileSet纹理
    private void OnSetTileTexture(object arg)
    {
        S_LeftBg.Instance.OnChangeTileSetTexture();

        _grid.RemoveAll();
        var cellHorizontal = EditorPanel.CellHorizontal;
        if (cellHorizontal <= 0)
        {
            return;
        }
        var cellVertical = EditorPanel.CellVertical;
        _grid.SetColumns(cellHorizontal);
        for (var y = 0; y < cellVertical; y++)
        {
            for (var x = 0; x < cellHorizontal; x++)
            {
                _grid.Add(new Rect2I(x * GameConfig.TileCellSize, y * GameConfig.TileCellSize, GameConfig.TileCellSize, GameConfig.TileCellSize));
            }
        }
    }
}
