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
    private Gun InteractiveItem = null;

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
        Holster.PickupGun(gun);
    }

    /// <summary>
    /// 切换到下一个武器
    /// </summary>
    public void TriggerExchangeNext()
    {
        Holster.ExchangeNext();
    }

    /// <summary>
    /// 切换到上一个武器
    /// </summary>
    public void ExchangePrev()
    {
        Holster.ExchangePrev();
    }

    /// <summary>
    /// 扔掉当前使用的武器, 切换到上一个武器
    /// </summary>
    public void TriggerThrowGun()
    {
        var gun = Holster.RmoveGun(Holster.ActiveIndex);
        //播放抛出效果
        if (gun != null)
        {
            if (Face == FaceDirection.Left)
            {
                gun.Scale *= new Vector2(1, -1);
                gun.RotationDegrees = 180;
            }
            gun.Position = Vector2.Zero;
            var temp = new ThrowGun();
            var startPos = GlobalPosition + new Vector2(0, 0);
            var startHeight = 6;
            var direction = GlobalRotationDegrees + MathUtils.RandRangeInt(-20, 20);
            var xf = 30;
            var yf = MathUtils.RandRangeInt(60, 120);
            var rotate = MathUtils.RandRangeInt(-180, 180);
            temp.InitThrow(new Vector2(20, 20), startPos, startHeight, direction, xf, yf, rotate, gun, gun.GunSprite);
            RoomManager.Current.SortRoot.AddChild(temp);
        }
    }

    /// <summary>
    /// 返回是否存在可互动的物体
    /// </summary>
    public bool HasTnteractive()
    {
        return InteractiveItem != null;
    }

    /// <summary>
    /// 触发与碰撞的物体互动
    /// </summary>
    public void TriggerTnteractive()
    {
        if (HasTnteractive())
        {
            InteractiveItem.Tnteractive(this);
        }
    }

    /// <summary>
    /// 触发换弹
    /// </summary>
    public void TriggerReload()
    {
        if (Holster.ActiveGun != null)
        {
            Holster.ActiveGun._Reload();
        }
    }

    /// <summary>
    /// 触发攻击
    /// </summary>
    public void TriggerAttack()
    {
        if (Holster.ActiveGun != null)
        {
            Holster.ActiveGun.Trigger();
        }
    }

    /// <summary>
    /// 设置脸的朝向
    /// </summary>
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

    /// <summary>
    /// 连接信号: InteractiveArea.area_entered
    /// </summary>
    private void _OnPropsEnter(Area2D other)
    {
        if (other is Gun gun)
        {
            InteractiveItem = gun;
            GD.Print("enter");
        }
    }

    /// <summary>
    /// 连接信号: InteractiveArea.area_exited
    /// </summary>
    private void _OnPropsExit(Area2D other)
    {
        if (other == InteractiveItem)
        {
            InteractiveItem = null;
            GD.Print("exit");
        }
    }

}