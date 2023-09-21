
using Godot;

public partial class RoomFogMask : PointLight2D, IDestroy
{
    public bool IsDestroyed { get; private set; }
    private bool _init = false;
    
    public void Init(RoomInfo roomInfo, Rect2I rect2)
    {
        if (_init)
        {
            return;
        }
        GlobalPosition = rect2.Position + rect2.Size / 2;
        
        //创建光纹理
        var img = Image.Create(rect2.Size.X, rect2.Size.Y, false, Image.Format.Rgba8);
        img.Fill(Colors.White);
        Texture = ImageTexture.CreateFromImage(img);
    }
    
    public void Destroy()
    {
        if (IsDestroyed)
        {
            return;
        }

        IsDestroyed = true;
        QueueFree();
    }
}