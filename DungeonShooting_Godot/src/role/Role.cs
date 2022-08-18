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
public abstract class Role : KinematicBody2D
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

    /// <summary>
    /// 血量
    /// </summary>
    public int Hp
    {
        get => _hp;
        protected set
        {
            int temp = _hp;
            _hp = value;
            if (temp != _hp) OnChangeHp(_hp);
        }
    }
    private int _hp = 0;

    /// <summary>
    /// 最大血量
    /// </summary>
    public int MaxHp
    {
        get => _maxHp;
        protected set
        {
            int temp = _maxHp;
            _maxHp = value;
            if (temp != _maxHp) OnChangeMaxHp(_maxHp);
        }
    }
    private int _maxHp = 0;

    private Vector2 StartScele;
    //所有角色碰撞的道具
    private readonly List<IProp> InteractiveItemList = new List<IProp>();

    private CheckInteractiveResult TempResultData;

    /// <summary>
    /// 可以互动的道具
    /// </summary>
    protected IProp InteractiveItem { get; private set; }

    /// <summary>
    /// 当血量改变时调用
    /// </summary>
    protected abstract void OnChangeHp(int hp);
    /// <summary>
    /// 当最大血量改变时调用
    /// </summary>
    protected abstract void OnChangeMaxHp(int maxHp);

    /// <summary>
    /// 当可互动的物体改变时调用, result 参数为 null 表示变为不可互动
    /// </summary>
    /// <param name="result">检测是否可互动时的返回值</param>
    protected abstract void ChangeInteractiveItem(CheckInteractiveResult result);

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

    public override void _Process(float delta)
    {
        //检查可互动的道具
        bool findFlag = false;
        for (int i = 0; i < InteractiveItemList.Count; i++)
        {
            var item = InteractiveItemList[i];
            if (item == null)
            {
                InteractiveItemList.RemoveAt(i--);
            }
            else
            {
                //找到可互动的道具了
                if (!findFlag)
                {
                    var result = item.CheckInteractive(this);
                    if (result.CanInteractive) //可以互动
                    {
                        findFlag = true;
                        if (InteractiveItem != item) //更改互动物体
                        {
                            InteractiveItem = item;
                            ChangeInteractiveItem(result);
                        }
                        else if (result.ShowIcon != TempResultData.ShowIcon) //切换状态
                        {
                            ChangeInteractiveItem(result);
                        }
                    }
                    TempResultData = result;
                }
            }
        }
        //没有可互动的道具
        if (!findFlag && InteractiveItem != null)
        {
            InteractiveItem = null;
            ChangeInteractiveItem(null);
        }
    }


    /// <summary>
    /// 拾起一个武器, 并且切换到这个武器
    /// </summary>
    /// <param name="weapon">武器对象</param>
    public void PickUpWeapon(Weapon weapon)
    {
        if (Holster.PickupWeapon(weapon) != -1)
        {
            //从可互动队列中移除
            InteractiveItemList.Remove(weapon);
        }
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
    public void TriggerThrowWeapon()
    {
        var weapon = Holster.RmoveWeapon(Holster.ActiveIndex);
        //播放抛出效果
        if (weapon != null)
        {
            weapon.StartThrowWeapon(this);
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
    /// 触发与碰撞的物体互动, 并返回与其互动的物体
    /// </summary>
    public IProp TriggerTnteractive()
    {
        if (HasTnteractive())
        {
            var item = InteractiveItem;
            item.Interactive(this);
            return item;
        }
        return null;
    }

    /// <summary>
    /// 触发换弹
    /// </summary>
    public void TriggerReload()
    {
        if (Holster.ActiveWeapon != null)
        {
            Holster.ActiveWeapon._Reload();
        }
    }

    /// <summary>
    /// 触发攻击
    /// </summary>
    public void TriggerAttack()
    {
        if (Holster.ActiveWeapon != null)
        {
            Holster.ActiveWeapon.Trigger();
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
    /// 与物体碰撞
    /// </summary>
    private void _OnPropsEnter(Area2D other)
    {
        if (other is IProp prop) //道具类型
        {
            if (!InteractiveItemList.Contains(prop))
            {
                InteractiveItemList.Add(prop);
            }
        }
    }

    /// <summary>
    /// 连接信号: InteractiveArea.area_exited
    /// 物体离开碰撞区域
    /// </summary>
    private void _OnPropsExit(Area2D other)
    {
        if (other is IProp prop) //道具类型
        {
            if (InteractiveItemList.Contains(prop))
            {
                InteractiveItemList.Remove(prop);
            }
            if (InteractiveItem == prop)
            {
                InteractiveItem = null;
                ChangeInteractiveItem(null);
            }
        }
    }

}