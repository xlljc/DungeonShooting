
using Config;
using Godot;

/// <summary>
/// 爆炸
/// </summary>
public partial class Explode : Area2D, IPoolItem
{
    public bool IsRecycled { get; set; }
    public string Logotype { get; set; }

    public bool IsDestroyed { get; private set; }

    /// <summary>
    /// 动画播放器
    /// </summary>
    public AnimationPlayer AnimationPlayer { get; private set; }
    /// <summary>
    /// 碰撞器
    /// </summary>
    public CollisionShape2D CollisionShape { get; private set; }
    /// <summary>
    /// 碰撞器形状对象
    /// </summary>
    public CircleShape2D CircleShape { get; private set; }

    /// <summary>
    /// 爆炸攻击的层级
    /// </summary>
    public uint AttackLayer { get; private set; }


    private bool _init = false;
    private float _hitRadius;
    private int _harm;
    private float _repelledRadius;
    private float _maxRepelled;
    
    public void Destroy()
    {
        if (IsDestroyed)
        {
            return;
        }

        IsDestroyed = true;
        QueueFree();
    }

    /// <summary>
    /// 初始化爆炸数据
    /// </summary>
    /// <param name="affiliationArea">爆炸所在区域</param>
    /// <param name="attackLayer">攻击的层级</param>
    /// <param name="hitRadius">伤害半径</param>
    /// <param name="harm">造成的伤害</param>
    /// <param name="repelledRadius">击退半径</param>
    /// <param name="maxRepelled">最大击退速度</param>
    public void Init(AffiliationArea affiliationArea, uint attackLayer, float hitRadius, int harm, float repelledRadius, float maxRepelled)
    {
        if (!_init)
        {
            _init = true;
            AnimationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
            CollisionShape = GetNode<CollisionShape2D>("CollisionShape2D");
            CircleShape = (CircleShape2D)CollisionShape.Shape;
            AnimationPlayer.AnimationFinished += OnAnimationFinish;
            BodyEntered += OnBodyEntered;
        }
        
        AttackLayer = attackLayer;
        _hitRadius = hitRadius;
        _harm = harm;
        _repelledRadius = repelledRadius;
        _maxRepelled = maxRepelled;
        CollisionMask = attackLayer | PhysicsLayer.Prop | PhysicsLayer.Debris;
        CircleShape.Radius = Mathf.Max(hitRadius, maxRepelled);

        //冲击波
        if (affiliationArea != null)
        {
            ShockWave(affiliationArea);
        }
    }
    
    /// <summary>
    /// 播放爆炸, triggerRole 为触发该爆炸的角色
    /// </summary>
    public void RunPlay(AdvancedRole triggerRole = null)
    {
        GameCamera.Main.CreateShake(new Vector2(6, 6), 0.7f, true);
        AnimationPlayer.Play(AnimatorNames.Play);
        //播放爆炸音效
        SoundManager.PlaySoundByConfig("explosion0002", Position, triggerRole);
    }

    //爆炸冲击波
    private void ShockWave(AffiliationArea affiliationArea)
    {
        var position = Position;
        var freezeSprites = affiliationArea.RoomInfo.StaticSprite.CollisionCircle(position, _repelledRadius, true);
        foreach (var freezeSprite in freezeSprites)
        {
            var temp = freezeSprite.Position - position;
            freezeSprite.ActivityObject.MoveController.AddForce(temp.Normalized() * _maxRepelled * (_repelledRadius - temp.Length()) / _repelledRadius);
        }
    }

    public void OnReclaim()
    {
        GetParent().CallDeferred(Node.MethodName.RemoveChild, this);
    }

    public void OnLeavePool()
    {
        
    }

    private void OnAnimationFinish(StringName name)
    {
        if (name == AnimatorNames.Play)
        {
            ObjectPool.Reclaim(this);
        }
    }

    private void OnBodyEntered(Node2D node)
    {
        var o = node.AsActivityObject();
        if (o != null)
        {
            var temp = o.Position - Position;
            var len = temp.Length();
            var angle = temp.Angle();

            if (len <= _hitRadius) //在伤害半径内
            {
                if (o is Role role) //是角色
                {
                    role.CallDeferred(nameof(role.Hurt), _harm, angle);
                }
                else if (o is Bullet bullet) //是子弹
                {
                    if (bullet is BoomBullet boomBullet) //如果是爆炸子弹, 则直接销毁
                    {
                        boomBullet.PlayBoom();
                    }
                    bullet.Destroy();
                    return;
                }
            }
            
            if (len <= _repelledRadius) //击退半径内
            {
                var repelled = (_repelledRadius - len) / _repelledRadius * _maxRepelled;
                //o.MoveController.SetAllVelocity(Vector2.Zero);
                o.MoveController.AddForce(Vector2.FromAngle(angle) * repelled);
            }
        }
    }
}