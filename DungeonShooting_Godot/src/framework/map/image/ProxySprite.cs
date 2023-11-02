
using Godot;

public class ProxySprite : IDestroy
{
    public bool IsDestroyed { get; private set; }
    public bool IsRecycled { get; set; }
    public string Logotype => nameof(ProxySprite);
    
    public Vector2 Position { get; private set; }
    public Sprite2D Sprite { get; }
    public Sprite2D ShadowSprite { get; }

    private Node _root;

    public ProxySprite()
    {
        Sprite = new Sprite2D();
        ShadowSprite = new Sprite2D();
        ShadowSprite.Modulate = new Color(0, 0, 0, 0.471f);
    }

    public void SetTexture(Node root, ActivityObject activityObject)
    {
        var animatedSprite = activityObject.AnimatedSprite;
        SetTexture(
            root,
            activityObject.GetCurrentTexture(),
            animatedSprite.GlobalPosition,
            animatedSprite.GlobalRotation,
            animatedSprite.GlobalScale,
            activityObject.ShadowOffset
        );
    }

    public void SetTexture(Node root, Texture2D texture, Vector2 position, float rotation, Vector2 scale, Vector2 shadowOffset)
    {
        Position = position;
        Sprite.Position = position;
        Sprite.Rotation = rotation;
        Sprite.Scale = scale;
        Sprite.Texture = texture;

        ShadowSprite.Position = position + shadowOffset;
        ShadowSprite.Rotation = rotation;
        ShadowSprite.Scale = scale;
        ShadowSprite.Texture = texture;

        root.AddChild(ShadowSprite);
        root.AddChild(Sprite);
    }

    public void Destroy()
    {
        if (IsDestroyed)
        {
            return;
        }

        IsDestroyed = true;
        if (Sprite != null)
        {
            Sprite.QueueFree();
        }

        if (ShadowSprite != null)
        {
            ShadowSprite.QueueFree();
        }
    }
}