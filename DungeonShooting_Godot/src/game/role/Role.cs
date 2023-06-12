using System.Collections.Generic;
using Godot;

/// <summary>
/// 角色基类
/// </summary>
public abstract partial class Role : ActivityObject
{
    /// <summary>
    /// 是否是 Ai
    /// </summary>
    public bool IsAi { get; protected set; } = false;
    
    /// <summary>
    /// 默认攻击对象层级
    /// </summary>
    public const uint DefaultAttackLayer = PhysicsLayer.Player | PhysicsLayer.Enemy | PhysicsLayer.Wall | PhysicsLayer.Props;
    
    /// <summary>
    /// 伤害区域
    /// </summary>
    [Export, ExportFillNode]
    public Area2D HurtArea { get; set; }

    /// <summary>
    /// 移动速度
    /// </summary>
    public float MoveSpeed = 120f;

    /// <summary>
    /// 所属阵营
    /// </summary>
    public CampEnum Camp;

    /// <summary>
    /// 攻击目标的碰撞器所属层级, 数据源自于: PhysicsLayer
    /// </summary>
    public uint AttackLayer { get; set; } = PhysicsLayer.Wall;

    // /// <summary>
    // /// 携带的道具包裹
    // /// </summary>
    // public List<object> PropsPack { get; } = new List<object>();

    /// <summary>
    /// 角色携带的武器袋
    /// </summary>
    public Holster Holster { get; private set; }

    /// <summary>
    /// 武器挂载点
    /// </summary>
    [Export, ExportFillNode]
    public MountRotation MountPoint { get; set; }
    /// <summary>
    /// 背后武器的挂载点
    /// </summary>
    [Export, ExportFillNode]
    public Marker2D BackMountPoint { get; set; }

    /// <summary>
    /// 互动碰撞区域
    /// </summary>
    [Export, ExportFillNode]
    public Area2D InteractiveArea { get; set; }
    
    /// <summary>
    /// 脸的朝向
    /// </summary>
    public FaceDirection Face { get => _face; set => SetFace(value); }
    private FaceDirection _face;

    /// <summary>
    /// 是否死亡
    /// </summary>
    public bool IsDie { get; private set; }
    
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
    private int _hp = 20;

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
            //护盾值改变
            if (temp != _maxHp) OnChangeMaxHp(_maxHp);
        }
    }
    private int _maxHp = 20;
    
    /// <summary>
    /// 当前护盾值
    /// </summary>
    public int Shield
    {
        get => _shield;
        protected set
        {
            int temp = _shield;
            _shield = value;
            //护盾被破坏
            if (temp > 0 && _shield <= 0) OnShieldDamage();
            //护盾值改变
            if (temp != _shield) OnChangeShield(_shield);
        }
    }
    private int _shield = 0;

    /// <summary>
    /// 最大护盾值
    /// </summary>
    public int MaxShield
    {
        get => _maxShield;
        protected set
        {
            int temp = _maxShield;
            _maxShield = value;
            if (temp != _maxShield) OnChangeMaxShield(_maxShield);
        }
    }
    private int _maxShield = 0;

    /// <summary>
    /// 当前角色所看向的对象, 也就是枪口指向的对象
    /// </summary>
    public ActivityObject LookTarget { get; set; }

    //初始缩放
    private Vector2 _startScale;
    //所有角色碰撞的道具
    private readonly List<ActivityObject> _interactiveItemList = new List<ActivityObject>();

    private CheckInteractiveResult _tempResultData;

    /// <summary>
    /// 可以互动的道具
    /// </summary>
    public ActivityObject InteractiveItem { get; private set; }

    /// <summary>
    /// 当血量改变时调用
    /// </summary>
    protected virtual void OnChangeHp(int hp)
    {
    }

    /// <summary>
    /// 当最大血量改变时调用
    /// </summary>
    protected virtual void OnChangeMaxHp(int maxHp)
    {
    }
    
    /// <summary>
    /// 护盾值改变时调用
    /// </summary>
    protected virtual void OnChangeShield(int shield)
    {
    }

    /// <summary>
    /// 最大护盾值改变时调用
    /// </summary>
    protected virtual void OnChangeMaxShield(int maxShield)
    {
    }

    /// <summary>
    /// 当护盾被破坏时调用
    /// </summary>
    protected virtual void OnShieldDamage()
    {
    }

    /// <summary>
    /// 当受伤时调用
    /// </summary>
    /// <param name="damage">受到的伤害</param>
    protected virtual void OnHit(int damage)
    {
    }

    /// <summary>
    /// 当可互动的物体改变时调用, result 参数为 null 表示变为不可互动
    /// </summary>
    /// <param name="result">检测是否可互动时的返回值</param>
    protected virtual void ChangeInteractiveItem(CheckInteractiveResult result)
    {
    }

    /// <summary>
    /// 死亡时调用
    /// </summary>
    protected virtual void OnDie()
    {
    }

    public override void OnInit()
    {
        Holster = new Holster(this);
        _startScale = Scale;
        MountPoint.Master = this;
        
        HurtArea.CollisionLayer = CollisionLayer;
        HurtArea.CollisionMask = 0;
        
        Face = FaceDirection.Right;

        //连接互动物体信号
        InteractiveArea.BodyEntered += _OnPropsEnter;
        InteractiveArea.BodyExited += _OnPropsExit;
    }

    protected override void Process(float delta)
    {
        //看向目标
        if (LookTarget != null)
        {
            Vector2 pos = LookTarget.GlobalPosition;
            //脸的朝向
            var gPos = GlobalPosition;
            if (pos.X > gPos.X && Face == FaceDirection.Left)
            {
                Face = FaceDirection.Right;
            }
            else if (pos.X < gPos.X && Face == FaceDirection.Right)
            {
                Face = FaceDirection.Left;
            }
            //枪口跟随目标
            MountPoint.SetLookAt(pos);
        }
        
        //检查可互动的道具
        bool findFlag = false;
        for (int i = 0; i < _interactiveItemList.Count; i++)
        {
            var item = _interactiveItemList[i];
            if (item == null || item.IsDestroyed)
            {
                _interactiveItemList.RemoveAt(i--);
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
                        else if (result.ShowIcon != _tempResultData.ShowIcon) //切换状态
                        {
                            ChangeInteractiveItem(result);
                        }
                    }
                    _tempResultData = result;
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
    /// 当武器放到后背时调用, 用于设置武器位置和角度
    /// </summary>
    /// <param name="weapon">武器实例</param>
    /// <param name="index">放入武器袋的位置</param>
    public virtual void OnPutBackMount(Weapon weapon, int index)
    {
        if (index < 8)
        {
            if (index % 2 == 0)
            {
                weapon.Position = new Vector2(-4, 3);
                weapon.RotationDegrees = 90 - (index / 2f) * 20;
                weapon.Scale = new Vector2(-1, 1);
            }
            else
            {
                weapon.Position = new Vector2(4, 3);
                weapon.RotationDegrees = 270 + (index - 1) / 2f * 20;
                weapon.Scale = new Vector2(1, 1);
            }
        }
        else
        {
            weapon.Visible = false;
        }
    }
    
    protected override void OnAffiliationChange()
    {
        //身上的武器的所属区域也得跟着变
        Holster.ForEach((weapon, i) =>
        {
            if (AffiliationArea != null)
            {
                AffiliationArea.InsertItem(weapon);
            }
            else if (weapon.AffiliationArea != null)
            {
                weapon.AffiliationArea.RemoveItem(weapon);
            }
        });
    }

    /// <summary>
    /// 获取当前角色的中心点坐标
    /// </summary>
    public Vector2 GetCenterPosition()
    {
        return MountPoint.GlobalPosition;
    }

    /// <summary>
    /// 使角色看向指定的坐标,
    /// 注意, 调用该函数会清空 LookTarget, 因为拥有 LookTarget 时也会每帧更新玩家视野位置
    /// </summary>
    /// <param name="pos"></param>
    public void LookTargetPosition(Vector2 pos)
    {
        LookTarget = null;
        //脸的朝向
        var gPos = GlobalPosition;
        if (pos.X > gPos.X && Face == FaceDirection.Left)
        {
            Face = FaceDirection.Right;
        }
        else if (pos.X < gPos.X && Face == FaceDirection.Right)
        {
            Face = FaceDirection.Left;
        }
        //枪口跟随目标
        MountPoint.SetLookAt(pos);
    }
    
    /// <summary>
    /// 判断指定坐标是否在角色视野方向
    /// </summary>
    public bool IsPositionInForward(Vector2 pos)
    {
        var gps = GlobalPosition;
        return (Face == FaceDirection.Left && pos.X <= gps.X) ||
               (Face == FaceDirection.Right && pos.X >= gps.X);
    }

    /// <summary>
    /// 返回所有武器是否弹药都打光了
    /// </summary>
    public bool IsAllWeaponTotalAmmoEmpty()
    {
        foreach (var weapon in Holster.Weapons)
        {
            if (weapon != null && !weapon.IsTotalAmmoEmpty())
            {
                return false;
            }
        }

        return true;
    }
    
    /// <summary>
    /// 拾起一个武器, 返回是否成功拾取, 如果不想立刻切换到该武器, exchange 请传 false
    /// </summary>
    /// <param name="weapon">武器对象</param>
    /// <param name="exchange">是否立即切换到该武器, 默认 true </param>
    public virtual bool PickUpWeapon(Weapon weapon, bool exchange = true)
    {
        if (Holster.PickupWeapon(weapon, exchange) != -1)
        {
            //从可互动队列中移除
            _interactiveItemList.Remove(weapon);
            return true;
        }

        return false;
    }

    /// <summary>
    /// 切换到下一个武器
    /// </summary>
    public virtual void ExchangeNext()
    {
        Holster.ExchangeNext();
    }

    /// <summary>
    /// 切换到上一个武器
    /// </summary>
    public virtual void ExchangePrev()
    {
        Holster.ExchangePrev();
    }

    /// <summary>
    /// 扔掉当前使用的武器, 切换到上一个武器
    /// </summary>
    public virtual void ThrowWeapon()
    {
        ThrowWeapon(Holster.ActiveIndex);
    }

    /// <summary>
    /// 扔掉指定位置的武器
    /// </summary>
    /// <param name="index">武器在武器袋中的位置</param>
    public virtual void ThrowWeapon(int index)
    {
        var weapon = Holster.GetWeapon(index);
        if (weapon == null)
        {
            return;
        }

        var temp = weapon.AnimatedSprite.Position;
        if (Face == FaceDirection.Left)
        {
            temp.Y = -temp.Y;
        }
        var pos = GlobalPosition + temp.Rotated(weapon.GlobalRotation);
        Holster.RemoveWeapon(index);
        //播放抛出效果
        weapon.ThrowWeapon(this, GlobalPosition);
    }

    /// <summary>
    /// 返回是否存在可互动的物体
    /// </summary>
    public bool HasInteractive()
    {
        return InteractiveItem != null;
    }

    /// <summary>
    /// 触发与碰撞的物体互动, 并返回与其互动的物体
    /// </summary>
    public ActivityObject TriggerInteractive()
    {
        if (HasInteractive())
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
    public virtual void Reload()
    {
        if (Holster.ActiveWeapon != null)
        {
            Holster.ActiveWeapon.Reload();
        }
    }

    /// <summary>
    /// 触发攻击
    /// </summary>
    public virtual void Attack()
    {
        if (Holster.ActiveWeapon != null)
        {
            Holster.ActiveWeapon.Trigger();
        }
    }

    /// <summary>
    /// 受到伤害, 如果是在碰撞信号处理函数中调用该函数, 请使用 CallDeferred 来延时调用, 否则很有可能导致报错
    /// </summary>
    /// <param name="damage">伤害的量</param>
    /// <param name="angle">角度</param>
    public virtual void Hurt(int damage, float angle)
    {
        OnHit(damage);
        if (Shield > 0)
        {
            Shield -= damage;
        }
        else
        {
            Hp -= damage;
            //播放血液效果
            // var packedScene = ResourceManager.Load<PackedScene>(ResourcePath.prefab_effect_Blood_tscn);
            // var blood = packedScene.Instance<Blood>();
            // blood.GlobalPosition = GlobalPosition;
            // blood.Rotation = angle;
            // GameApplication.Instance.Node3D.GetRoot().AddChild(blood);
        }
        
        //受伤特效
        PlayHitAnimation();
        
        //死亡判定
        if (Hp <= 0)
        {
            //死亡
            if (!IsDie)
            {
                IsDie = true;
                OnDie();
            }
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
                Scale = _startScale;
            }
            else
            {
                RotationDegrees = 180;
                Scale = new Vector2(_startScale.X, -_startScale.Y);
            }
        }
    }
    
    /// <summary>
    /// 连接信号: InteractiveArea.BodyEntered
    /// 与物体碰撞
    /// </summary>
    private void _OnPropsEnter(Node2D other)
    {
        if (other is ActivityObject propObject && !propObject.CollisionWithMask(PhysicsLayer.InHand))
        {
            if (!_interactiveItemList.Contains(propObject))
            {
                _interactiveItemList.Add(propObject);
            }
        }
    }

    /// <summary>
    /// 连接信号: InteractiveArea.BodyExited
    /// 物体离开碰撞区域
    /// </summary>
    private void _OnPropsExit(Node2D other)
    {
        if (other is ActivityObject propObject)
        {
            if (_interactiveItemList.Contains(propObject))
            {
                _interactiveItemList.Remove(propObject);
            }
            if (InteractiveItem == propObject)
            {
                InteractiveItem = null;
                ChangeInteractiveItem(null);
            }
        }
    }
}