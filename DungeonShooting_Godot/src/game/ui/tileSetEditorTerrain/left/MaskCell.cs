using Godot;

namespace UI.TileSetEditorTerrain;

public class MaskCell : UiCell<TileSetEditorTerrain.Cell, Rect2I>
{
    public override void OnInit()
    {
        CellNode.Instance.Draw += OnDraw;
        CellNode.Instance.AddDragListener(OnDrag);
    }

    public override void Process(float delta)
    {
        CellNode.Instance.QueueRedraw();
    }

    private void OnDrag(DragState state, Vector2 delta)
    {
        if (state == DragState.DragStart)
        {
            Grid.SelectIndex = Index;
        }
        Debug.Log($"state: {state}, delta: {delta}");
    }
    
    private void OnDraw()
    {
        if (Grid.SelectIndex == Index)
        {
            //选中时绘制轮廓
            CellNode.Instance.DrawRect(
                new Rect2(Vector2.Zero, CellNode.Instance.Size),
                new Color(0, 1, 1), false, 2f / CellNode.UiPanel.S_LeftBg.L_TileTexture.Instance.Scale.X
            );
        }
    }
}