using Godot;

namespace UI.TileSetEditorSegment;

public partial class TileSelectedCell : VBoxContainer, IUiNodeScript
{
    private TileSetEditorSegment.RightBg _rightBg;
    private UiGrid<TileSetEditorSegment.CellButton, Vector2I> _grid;
    
    public void SetUiNode(IUiNode uiNode)
    {
        _rightBg = (TileSetEditorSegment.RightBg)uiNode;

        _grid = new UiGrid<TileSetEditorSegment.CellButton, Vector2I>(_rightBg.L_ScrollContainer.L_CellButton, typeof(TileCell));
        _grid.SetCellOffset(new Vector2I(5, 5));
        _grid.SetAutoColumns(true);
        _grid.SetHorizontalExpand(true);
    }

    public void OnDestroy()
    {
        _grid.Destroy();
    }
    
    public void ImportCell(Vector2I cell)
    {
        _grid.Add(cell);
        _grid.Sort();
    }
    
    public void RemoveCell(Vector2I cell)
    {
        var uiCell = _grid.Find(c => c.Data == cell);
        if (uiCell != null)
        {
            _grid.RemoveByIndex(uiCell.Index);
        }
    }
}