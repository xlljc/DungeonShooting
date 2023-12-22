using Godot;

namespace UI.TileSetEditorTerrain;

public class MaskCell : UiCell<TileSetEditorTerrain.Cell, Rect2I>
{
    public override void OnInit()
    {
        CellNode.Instance.DragOutline = false;
        CellNode.Instance.InitTexture(CellNode.UiPanel.S_LeftBg.L_TileTexture.Instance);
        CellNode.Instance.AddDragListener(OnDrag);
    }

    public override void OnSetData(Rect2I data)
    {
        CellNode.Instance.SetRect(data);
    }

    public override void Process(float delta)
    {
        CellNode.Instance.QueueRedraw();
    }

    private void OnDrag(DragState state, Vector2 delta)
    {
        if (state == DragState.DragStart)
        {
            CellNode.UiPanel.IsDraggingCell = true;
            Grid.SelectIndex = Index;
            Debug.Log($"data: {Data}");
        }
        else if (state == DragState.DragEnd)
        {
            CellNode.UiPanel.IsDraggingCell = false;
        }
    }

    public override void OnSelect()
    {
        CellNode.Instance.DragOutline = true;
    }

    public override void OnUnSelect()
    {
        CellNode.Instance.DragOutline = false;
    }
}