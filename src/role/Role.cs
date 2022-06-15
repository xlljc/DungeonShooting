using System.Collections.Generic;
using Godot;

/// <summary>
/// 脸的朝向
/// </summary>
public enum FaceDirection
{
    Left,
    Right,
}

/// <summary>
/// 角色基类
/// </summary>
public class Role : KinematicBody2D
{
    /// <summary>
    /// 重写的纹理
    /// </summary>
    [Export] public Texture Texture;

    /// <summary>
    /// 移动速度
    /// </summary>
    [Export] public float MoveSpeed = 150f;

    /// <summary>
    /// 所属阵营
    /// </summary>
    [Export] public CampEnum Camp;

    /// <summary>
    /// 携带的道具包裹
    /// </summary>
    public List<object> PropsPack { get; } = new List<object>();

    /// <summary>
    /// 角色携带的枪套
    /// </summary>
    public Holster Holster { get; private set; }

    /// <summary>
    /// 动画播放器
    /// </summary>
    public AnimatedSprite AnimatedSprite { get; private set; }
    /// <summary>
    /// 武器挂载点
    /// </summary>
    public Position2D MountPoint { get; private set; }
    /// <summary>
    /// 背后武器的挂载点
    /// </summary>
    public Position2D BackMountPoint { get; private set; }

    /// <summary>
    /// 脸的朝向
    /// </summary>
    public FaceDirection Face { get => _face; set => SetFace(value); }
    private FaceDirection _face;

    private Vector2 StartScele;

    public override void _Ready()
    {
        StartScele = Scale;
        AnimatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
        MountPoint = GetNode<Position2D>("MountPoint");
        BackMountPoint = GetNode<Position2D>("BackMountPoint");
        // 更改纹理
        ChangeFrameTexture(AnimatorNames.Idle, AnimatedSprite, Texture);
        ChangeFrameTexture(AnimatorNames.Run, AnimatedSprite, Texture);
        ChangeFrameTexture(AnimatorNames.ReverseRun, AnimatedSprite, Texture);

        Holster = new Holster(this);
        Face = FaceDirection.Right;
    }

    /// <summary>
    /// 拾起一个武器, 并且切换到这个武器
    /// </summary>
    /// <param name="gun">武器对象</param>
    public void PickUpGun(Gun gun)
    {
        var index = Holster.PickupGun(gun);
        SetActiveGun(index);
    }

    public void ExchangeNext()
    {
        Holster.ExchangeNext();
    }

    private void SetActiveGun(int index)
    {
        Holster.ExchangeByIndex(index);
    }

    private void SetFace(FaceDirection face)
    {
        if (_face != face)
        {
            _face = face;
            if (face == FaceDirection.Right)
            {
                RotationDegrees = 0;
                Scale = StartScele;
            }
            else
            {
                RotationDegrees = 180;
                Scale = new Vector2(StartScele.x, -StartScele.y);
            }
        }
    }

    /// <summary>
    /// 更改指定动画的纹理
    /// </summary>
    private void ChangeFrameTexture(string anim, AnimatedSprite animatedSprite, Texture texture)
    {
        SpriteFrames spriteFrames = animatedSprite.Frames as SpriteFrames;
        if (spriteFrames != null)
        {
            int count = spriteFrames.GetFrameCount(anim);
            for (int i = 0; i < count; i++)
            {
                AtlasTexture temp = spriteFrames.GetFrame(anim, i) as AtlasTexture;
                temp.Atlas = Texture;
            }
        }
    }

}