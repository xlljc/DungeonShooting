using Godot;

public class ObjectThrowData
{
    /// <summary>
    /// 是否是第一次结束
    /// </summary>
    public bool FirstOver = true;
    
    /// <summary>
    /// 是否已经结束
    /// </summary>
    public bool IsOver = true;

    /// <summary>
    /// 物体大小
    /// </summary>
    public Vector2 Size = Vector2.One;

    /// <summary>
    /// 起始坐标
    /// </summary>
    public Vector2 StartPosition;

    /// <summary>
    /// 移动方向, 0 - 360
    /// </summary>
    public float Direction;

    /// <summary>
    /// x速度, 也就是水平速度
    /// </summary>
    public float XSpeed;

    /// <summary>
    /// y轴速度, 也就是竖直速度
    /// </summary>
    public float YSpeed;

    /// <summary>
    /// 初始x轴组队
    /// </summary>
    public float StartXSpeed;

    /// <summary>
    /// 初始y轴速度
    /// </summary>
    public float StartYSpeed;

    /// <summary>
    /// 旋转速度
    /// </summary>
    public float RotateSpeed;
    
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

    public Vector2 CurrPosition;
    public float Y;
    public Vector2 LinearVelocity;
    
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