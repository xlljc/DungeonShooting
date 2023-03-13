using Godot;

public class ActivityFallData
{
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