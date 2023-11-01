
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
    /// <summary>
    /// 最小伤害
    /// </summary>
    public int MinHarm { get; private set; }
    /// <summary>
    /// 最大伤害
    /// </summary>
    public int MaxHarm { get; private set; }

    private bool _init = false;
    
    public void Destroy()
    {
        if (IsDestroyed)
        {
            return;
        }

        IsDestroyed = true;
        QueueFree();
    }

    public void Init(uint attackLayer, float radius, int minHarm, int maxHarm)
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
        MinHarm = minHarm;
        MaxHarm = maxHarm;
        CollisionMask = attackLayer;
        CircleShape.Radius = radius;
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
        var role = node.AsActivityObject<Role>();
        if (role != null)
        {
            var angle = (role.Position - Position).Angle();
            role.CallDeferred(nameof(role.Hurt), Utils.Random.RandomRangeInt(MinHarm, MaxHarm), angle);
            role.MoveController.AddForce(Vector2.FromAngle(angle) * 150, 300);
        }
    }
}