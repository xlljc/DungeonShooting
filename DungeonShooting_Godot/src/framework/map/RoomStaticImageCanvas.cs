
using Godot;

public class RoomStaticImageCanvas : IDestroy
{
    public bool IsDestroyed { get; private set; }
    /// <summary>
    /// 画布节点实例
    /// </summary>
    public ImageCanvas CanvasSprite { get; }
    /// <summary>
    /// 房间坐标相对于画布坐标偏移量, 单位: 像素
    /// </summary>
    public Vector2I RoomOffset { get; set; }

    public RoomStaticImageCanvas(Node root, Vector2I position, int width, int height)
    {
        CanvasSprite = new ImageCanvas(width, height);
        //CanvasSprite.Clear(new Color(1, 1, 1, 0.2f));
        CanvasSprite.GlobalPosition = position;
        root.AddChild(CanvasSprite);
    }

    /// <summary>
    /// 将世界坐标转为画布下的坐标
    /// </summary>
    public Vector2I ToImageCanvasPosition(Vector2 pos)
    {
        pos = pos - CanvasSprite.GlobalPosition;
        return new Vector2I((int)pos.X, (int)pos.Y);
    }

    public void Destroy()
    {
        if (IsDestroyed)
        {
            return;
        }

        IsDestroyed = true;
    }
}