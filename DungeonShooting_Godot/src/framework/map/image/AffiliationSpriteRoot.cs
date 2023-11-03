
using System.Collections.Generic;
using Godot;

/// <summary>
/// AffiliationArea 中用于存放静态Sprite的功能类
/// </summary>
public partial class AffiliationSpriteRoot : Node2D, IDestroy
{
    private class SpriteData
    {
        public Vector2 Position;
        public FreezeSprite FreezeSprite;
    }
    
    public bool IsDestroyed { get; private set; }
    
    private readonly Grid<List<SpriteData>> _grid = new Grid<List<SpriteData>>();
    private readonly AffiliationArea _affiliationArea;

    public AffiliationSpriteRoot(AffiliationArea affiliationArea)
    {
        _affiliationArea = affiliationArea;
    }
    
    public void Destroy()
    {
        if (IsDestroyed)
        {
            return;
        }

        IsDestroyed = true;

        _grid.ForEach((x, y, data) =>
        {
            foreach (var spriteData in data)
            {
                spriteData.FreezeSprite.Destroy();
            }
            data.Clear();
        });
        _grid.Clear();
        QueueFree();
    }

    /// <summary>
    /// 添加静态精灵
    /// </summary>
    public void AddFreezeSprite(FreezeSprite freezeSprite)
    {
        
        // var result = _freezeSprites.Add(freezeSprite);
        // if (result)
        // {
        //     
        // }
        // return result;
    }

    /// <summary>
    /// 移除静态精灵
    /// </summary>
    public void RemoveFreezeSprite(FreezeSprite freezeSprite)
    {
        //return _freezeSprites.Remove(freezeSprite);
    }
}