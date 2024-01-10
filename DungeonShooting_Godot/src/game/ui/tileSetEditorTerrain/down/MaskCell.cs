using Godot;

namespace UI.TileSetEditorTerrain;

public class MaskCell : UiCell<TileSetEditorTerrain.BottomCell, Rect2I>
{
    private TextureRect _textureRect;
    private Texture2D _texture;
    private TileSetEditorTerrainPanel _panel;
    private bool _dragMoveFlag = false;

    public override void OnInit()
    {
        _panel = CellNode.UiPanel;
        _textureRect = _panel.S_BottomBg.L_TileTexture.Instance;
        _texture = _textureRect.Texture;
        CellNode.Instance.Draw += Draw;
        CellNode.Instance.AddDragListener(OnDrag);
    }

    public override void Process(float delta)
    {
        CellNode.Instance.QueueRedraw();
    }
    
    //拖拽操作
    private void OnDrag(DragState state, Vector2 delta)
    {
        var sprite = _panel.S_DragSprite.Instance;
        if (state == DragState.DragStart) //拖拽开始
        {
            //这里要判断一下是否在BottomBg区域内
            if (CellNode.UiPanel.S_BottomBg.Instance.IsMouseInRect())
            {
                _panel.DraggingCell = this;
                Grid.SelectIndex = Index;
                _dragMoveFlag = false;
            }
        }
        else if (state == DragState.DragEnd) //拖拽结束
        {
            if (_panel.DraggingCell == this)
            {
                _panel.DraggingCell = null;
                sprite.Visible = false;
                _dragMoveFlag = false;

                if (_panel.S_TopBg.Instance.IsMouseInRect()) //找到放置的Cell
                {
                    _panel.OnDropCell(this);
                }
            }
        }
        else if (_panel.DraggingCell == this) //拖拽移动
        {
            if (!_dragMoveFlag)
            {
                _dragMoveFlag = true;
                sprite.Texture = _texture;
                sprite.RegionRect = Data;
                sprite.Scale = _panel.S_TopBg.L_TerrainRoot.Instance.Scale;
                sprite.Visible = true;
                sprite.GlobalPosition = sprite.GetGlobalMousePosition();
            }
            sprite.GlobalPosition = sprite.GetGlobalMousePosition();
        }
    }

    private void Draw()
    {
        if (Grid.SelectIndex == Index)
        {
            //选中时绘制轮廓
            CellNode.Instance.DrawRect(
                new Rect2(Vector2.Zero, CellNode.Instance.Size),
                new Color(0, 1, 1), false, 2f / _textureRect.Scale.X
            );
        }
    }
}