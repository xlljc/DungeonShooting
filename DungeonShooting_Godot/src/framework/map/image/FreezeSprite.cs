
using Godot;

/// <summary>
/// 用于绘制静止不动的 ActivityObject，从而优化性能
/// </summary>
public class FreezeSprite : IDestroy
{
    public bool IsDestroyed { get; private set; }
    
    /// <summary>
    /// 所在位置
    /// </summary>
    public Vector2 Position { get; private set; }
    
    /// <summary>
    /// 所属对象
    /// </summary>
    public ActivityObject ActivityObject { get; }
    
    /// <summary>
    /// 是否已经被冻结
    /// </summary>
    public bool IsFrozen { get; private set; }

    private Node _spriteParent;
    private int _spriteIndex;
    private Node _shadowParent;
    private int _shadowIndex;
    private Node _parent;
    
    public FreezeSprite(ActivityObject ao)
    {
        ActivityObject = ao;
        _spriteParent = ao.AnimatedSprite.GetParent();
        _shadowParent = ao.ShadowSprite.GetParent();
    }

    public void Freeze()
    {
        if (IsFrozen)
        {
            return;
        }

        var affiliationArea = ActivityObject.AffiliationArea;
        if (affiliationArea == null)
        {
            Debug.LogError("物体的 AffiliationArea 属性为空，不能调用 Freeze() 函数！");
            return;
        }
        
        IsFrozen = true;


        if (affiliationArea.SpriteRoot.AddFreezeSprite(this))
        {
            _spriteIndex = ActivityObject.AnimatedSprite.GetIndex();
            _shadowIndex = ActivityObject.ShadowSprite.GetIndex();
            ActivityObject.ShadowSprite.Reparent(affiliationArea.SpriteRoot);
            ActivityObject.AnimatedSprite.Reparent(affiliationArea.SpriteRoot);
            _parent = ActivityObject.GetParent();
            _parent.RemoveChild(ActivityObject);
        }
    }

    public void Unfreeze()
    {
        if (!IsFrozen)
        {
            return;
        }

        IsFrozen = false;
        
        if (ActivityObject.AffiliationArea.SpriteRoot.AddFreezeSprite(this))
        {
            _parent.AddChild(ActivityObject);
            ActivityObject.ShadowSprite.Reparent(_shadowParent);
            ActivityObject.AnimatedSprite.Reparent(_spriteParent);

            if (_spriteIndex > _shadowIndex)
            {
                _shadowParent.MoveChild(ActivityObject.ShadowSprite, _shadowIndex);
                _spriteParent.MoveChild(ActivityObject.AnimatedSprite, _spriteIndex);
            }
            else
            {
                _spriteParent.MoveChild(ActivityObject.AnimatedSprite, _spriteIndex);
                _shadowParent.MoveChild(ActivityObject.ShadowSprite, _shadowIndex);
            }
        }
    }
    

    public void Destroy()
    {
        if (IsDestroyed)
        {
            return;
        }

        IsDestroyed = true;
        if (IsFrozen)
        {
            ActivityObject.Destroy();
        }
    }
}