
using Godot;

public class ImageRenderSprite
{
    public Sprite2D Sprite { get; }

    public ImageTexture Texture { get; }

    private static Image _emptyImage;
    private static Image EmptyImage
    {
        get
        {
            if (_emptyImage == null)
            {
                _emptyImage = Image.Create(1, 1, false, Image.Format.Rgba8);
            }
    
            return _emptyImage;
        }
    }

    public ImageRenderSprite()
    {
        var sprite = new Sprite2D();
        Sprite = sprite;
        Texture = ImageTexture.CreateFromImage(EmptyImage);
        sprite.Name = "RenderSprite";
        sprite.Texture = Texture;
        sprite.Centered = false;
    }

    public void SetImage(Image image)
    {
        Texture.SetImage(image);
    }
}