using Godot;

public class ActivityThrowData
{
    /// <summary>
    /// 是否是第一次下坠
    /// </summary>
    public bool FirstFall = true;
    
    /// <summary>
    /// 下坠是否已经结束
    /// </summary>
    public bool IsFallOver = true;

    /// <summary>
    /// 物体大小
    /// </summary>
    public Vector2 Size = Vector2.One;

    /// <summary>
    /// 碰撞器形状
    /// </summary>
    public RectangleShape2D RectangleShape;

    /// <summary>
    /// 落地之后是否弹跳
    /// </summary>
    public bool Bounce;

    /// <summary>
    /// 回弹的强度
    /// </summary>
    public float BounceStrength = 0.5f;

    /// <summary>
    /// 回弹后的速度
    /// </summary>
    public float BounceSpeed = 0.8f;
    
    //----------- 用于记录原始信息 --------------
    public bool UseOrigin = true;
    public Shape2D OriginShape;
    public Vector2 OriginPosition;
    public float OriginRotation;
    public Vector2 OriginScale;
    public int OriginZIndex;
    public Vector2 OriginSpritePosition;
    public bool OriginCollisionEnable;
    public Vector2 OriginCollisionPosition;
    public float OriginCollisionRotation;
    public Vector2 OriginCollisionScale;
    public uint OriginCollisionMask;
    public uint OriginCollisionLayer;
}