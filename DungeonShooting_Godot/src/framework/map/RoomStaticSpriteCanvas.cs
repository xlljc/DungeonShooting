
using Godot;

public class RoomStaticSpriteCanvas : IDestroy
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

    public RoomStaticSpriteCanvas(Node root, Vector2I position, int width, int height)
    {
        CanvasSprite = new ImageCanvas(width, height);
        //CanvasSprite.Clear(new Color(1, 1, 1, 0.2f));
        CanvasSprite.GlobalPosition = position;
        root.AddChild(CanvasSprite);
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