
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
    private int _minHarm;
    private int _maxHarm;
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
    /// <param name="attackLayer">攻击的层级</param>
    /// <param name="hitRadius">伤害半径</param>
    /// <param name="minHarm">最小伤害</param>
    /// <param name="maxHarm">最大伤害</param>
    /// <param name="repelledRadius">击退半径</param>
    /// <param name="maxRepelled">最大击退速度</param>
    public void Init(uint attackLayer, float hitRadius, int minHarm, int maxHarm, float repelledRadius, float maxRepelled)
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
        _minHarm = minHarm;
        _maxHarm = maxHarm;
        _repelledRadius = repelledRadius;
        _maxRepelled = maxRepelled;
        CollisionMask = attackLayer | PhysicsLayer.Prop | PhysicsLayer.Throwing | PhysicsLayer.Debris;
        CircleShape.Radius = Mathf.Max(hitRadius, maxRepelled);
    }
    
    public void RunPlay()
    {
        GameCamera.Main.CreateShake(new Vector2(6, 6), 0.7f, true);
        AnimationPlayer.Play(AnimatorNames.Play);
    }

    public void OnReclaim()
    {
        GetParent().RemoveChild(this);
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
            
            if (len <= _repelledRadius) //击退半径内
            {
                var repelled = (_repelledRadius - len) / _repelledRadius * _maxRepelled;
                o.MoveController.SetAllVelocity(Vector2.Zero);
                o.MoveController.AddForce(Vector2.FromAngle(angle) * repelled);
            }

            if (o is Role role)
            {
                if (len <= _hitRadius) //在伤害半径内
                {
                    role.CallDeferred(nameof(role.Hurt), Utils.Random.RandomRangeInt(_minHarm, _maxHarm), angle);
                }
            }
        }
    }
}