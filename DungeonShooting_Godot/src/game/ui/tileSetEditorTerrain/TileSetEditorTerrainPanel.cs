using Godot;
using UI.TileSetEditor;

namespace UI.TileSetEditorTerrain;

public partial class TileSetEditorTerrainPanel : TileSetEditorTerrain
{
    /// <summary>
    /// 父Ui
    /// </summary>
    public TileSetEditorPanel EditorPanel;

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
        for (var i = 0; i < cellVertical; i++)
        {
            for (var j = 0; j < cellHorizontal; j++)
            {
                _grid.Add(default);
            }
        }
    }
}
