using Godot;

namespace UI.TileSetEditorSegment;

public class MaskRectCell : UiCell<TileSetEditorSegment.MaskRect, bool>
{
    public override void OnInit()
    {
        CellNode.Instance.Draw += OnDraw;
    }

    public override void OnSetData(bool data)
    {
        //选择当前cell时显示透明颜色
        CellNode.Instance.Color = data ? new Color(0, 0, 0, 0) : new Color(0, 0, 0, 0.7f);
    }

    public override void Process(float delta)
    {
        CellNode.Instance.QueueRedraw();
    }

    private void OnDraw()
    {
        if (Data)
        {
            //选中时绘制轮廓
            CellNode.Instance.DrawRect(
                new Rect2(Vector2.Zero, CellNode.Instance.Size),
                new Color(0, 1, 1), false, 2 / CellNode.UiPanel.S_TileTexture.Instance.Scale.X
            );
        }
    }
}