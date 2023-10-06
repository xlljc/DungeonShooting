
using Godot;

/// <summary>
/// 交叉过道数据
/// </summary>
public class CrossAisleRectData
{
    /// <summary>
    /// 第一道门连接的过道区域
    /// </summary>
    public Rect2I Rect1;
    /// <summary>
    /// 交叉点区域
    /// </summary>
    public Rect2I Cross;
    /// <summary>
    /// 第二道门连接的过道区域
    /// </summary>
    public Rect2I Rect2;

    /// <summary>
    /// 计算并返回过道所占矩形大小
    /// </summary>
    public Rect2I CalcAisleRect()
    {
        int x, y, w, h;
        if (Rect1.Position.X < Rect2.Position.X)
        {
            x = Rect1.Position.X;
            w = Rect2.Position.X - Rect1.Position.X + Rect2.Size.X;
        }
        else
        {
            x = Rect2.Position.X;
            w = Rect1.Position.X - Rect2.Position.X + Rect1.Size.X;
        }
        if (Rect1.Position.Y < Rect2.Position.Y)
        {
            y = Rect1.Position.Y;
            h = Rect2.Position.Y - Rect1.Position.Y + Rect2.Size.Y;
        }
        else
        {
            y = Rect2.Position.Y;
            h = Rect1.Position.Y - Rect2.Position.Y + Rect1.Size.Y;
        }

        return new Rect2I(x, y, w, h);
    }
}