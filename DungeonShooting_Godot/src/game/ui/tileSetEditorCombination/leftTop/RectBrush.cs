using Godot;

namespace UI.TileSetEditorCombination;

public partial class RectBrush : Node2D
{
    /// <summary>
    /// 所在的根节点
    /// </summary>
    public Control Root { get; set; }

    private bool _drawFlag = false;
    private int _x;
    private int _y;
    private int _w;
    private int _h;
    
    public override void _Process(double delta)
    {
        QueueRedraw();
    }

    /// <summary>
    /// 停止绘制
    /// </summary>
    public void ClearDrawRect()
    {
        _drawFlag = false;
    }

    /// <summary>
    /// 设置绘制的矩形区域
    /// </summary>
    public void SetDrawRect(int x, int y, int w, int h)
    {
        _drawFlag = true;
        _x = x;
        _y = y;
        _w = w;
        _h = h;
    }

    /// <summary>
    /// 获取原点坐标, 单位: 像素
    /// </summary>
    public Vector2I GetOriginPosition()
    {
        return new Vector2I(_x, _y);
    }

    /// <summary>
    /// 获取中心点坐标, 单位: 像素
    /// </summary>
    public Vector2I GetCenterPosition()
    {
        if (!_drawFlag)
        {
            return Vector2I.Zero;
        }
        return new Vector2I(_x + _w / 2, _y + _h / 2);
    }

    /// <summary>
    /// 获取绘制的矩形大小, 单位: 像素
    /// </summary>
    public Vector2I GetRectSize()
    {
        if (!_drawFlag)
        {
            return Vector2I.Zero;
        }

        return new Vector2I(_w, _h);
    }

    public override void _Draw()
    {
        if (Root != null && _drawFlag)
        {
            DrawRect(new Rect2(_x, _y, _w, _h), new Color(1, 1, 0, 0.5f), false, 2f / Root.Scale.X);
        }
    }
}