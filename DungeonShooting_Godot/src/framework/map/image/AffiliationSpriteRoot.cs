
using System.Collections.Generic;
using Godot;

public partial class AffiliationSpriteRoot : Node2D, IDestroy
{
    public bool IsDestroyed { get; private set; }

    private HashSet<FreezeSprite> _freezeSprites = new HashSet<FreezeSprite>();
    
    public void Destroy()
    {
        if (IsDestroyed)
        {
            return;
        }

        IsDestroyed = true;

        foreach (var freezeSprite in _freezeSprites)
        {
            freezeSprite.Destroy();
        }
        _freezeSprites.Clear();
        QueueFree();
    }

    public bool AddFreezeSprite(FreezeSprite freezeSprite)
    {
        return _freezeSprites.Add(freezeSprite);
    }

    public bool RemoveFreezeSprite(FreezeSprite freezeSprite)
    {
        return _freezeSprites.Remove(freezeSprite);
    }
}