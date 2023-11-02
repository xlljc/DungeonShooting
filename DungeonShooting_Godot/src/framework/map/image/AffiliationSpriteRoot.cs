
using Godot;

public partial class AffiliationSpriteRoot : Node2D, IDestroy
{
    public bool IsDestroyed { get; private set; }
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