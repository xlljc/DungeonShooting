
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
    /// 产生爆炸的子弹数据
    /// </summary>
    public BulletData BulletData { get; private set; }
    
    /// <summary>
    /// 所属阵营
    /// </summary>
    public CampEnum Camp { get; private set; }

    private bool _init = false;
    private float _hitRadius;
    private int _harm;
    private float _repelledRadius;
    private float _maxRepelled;

    public override void _Ready()
    {
        CollisionMask = Role.AttackLayer;
    }

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
    /// <param name="bulletData">产生爆炸的子弹数据</param>
    /// <param name="camp">所属阵营</param>
    /// <param name="hitRadius">伤害半径</param>
    /// <param name="harm">造成的伤害</param>
    /// <param name="repelledRadius">击退半径</param>
    /// <param name="maxRepelled">最大击退速度</param>
    public void Init(BulletData bulletData, CampEnum camp, float hitRadius, int harm, float repelledRadius, float maxRepelled)
    {
        if (!_init)
        {
            _init = true;
            AnimationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
            CollisionShape = GetNode<CollisionShape2D>("CollisionShape2D");
            CircleShape = (CircleShape2D)CollisionShape.Shape;
            AnimationPlayer.AnimationFinished += OnAnimationFinish;
            AreaEntered += OnArea2dEntered;
            BodyEntered += OnBodyEntered;
        }

        Camp = camp;
        BulletData = bulletData;
        _hitRadius = hitRadius;
        _harm = harm;
        _repelledRadius = repelledRadius;
        _maxRepelled = maxRepelled;
        CircleShape.Radius = Mathf.Max(hitRadius, maxRepelled);

        //冲击波
        var affiliationArea = bulletData.TriggerRole?.AffiliationArea;
        if (affiliationArea != null)
        {
            ShockWave(affiliationArea);
        }
    }
    
    /// <summary>
    /// 播放爆炸, triggerRole 为触发该爆炸的角色
    /// </summary>
    public void RunPlay(Role triggerRole = null)
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
        if (node is IHurt hurt)
        {
            HandlerCollision(hurt);
        }
        else if (node is Bullet bullet) //是子弹
        {
            if (bullet is BoomBullet boomBullet) //如果是爆炸子弹, 则直接销毁
            {
                boomBullet.PlayBoom();
            }
            bullet.Destroy();
        }
    }
    
    private void OnArea2dEntered(Area2D other)
    {
        if (other is IHurt hurt)
        {
            HandlerCollision(hurt);
        }
    }

    private void HandlerCollision(IHurt hurt)
    {
        var temp = hurt.GetPosition() - Position;
        var len = temp.Length();
        var angle = temp.Angle();

        if (hurt.CanHurt(Camp))
        {
            var target = BulletData.TriggerRole.IsDestroyed ? null : BulletData.TriggerRole;
            if (len <= _hitRadius) //在伤害半径内
            {
                hurt.Hurt(target, _harm, angle);
            }
        
            if (len <= _repelledRadius) //击退半径内
            {
                var o = hurt.GetActivityObject();
                if (o != null)
                {
                    var repelled = (_repelledRadius - len) / _repelledRadius * _maxRepelled;
                    o.AddRepelForce(Vector2.FromAngle(angle) * repelled);
                }
            }
        }
    }
}