using System.Collections;
using System.Collections.Generic;
using Config;
using Godot;

/// <summary>
/// 激光子弹
/// </summary>
public partial class Laser : Area2D, IBullet
{
    public CollisionShape2D Collision { get; private set; }
    public Sprite2D LineSprite { get; private set; }
    public RectangleShape2D Shape { get; private set; }

    public uint AttackLayer
    {
        get => CollisionMask;
        set => CollisionMask = value;
    }

    public BulletData BulletData { get; private set; }

    public bool IsDestroyed { get; private set; }
    
    public float Width { get; set; }
    
    //开启的协程
    private List<CoroutineData> _coroutineList;
    private float _pixelScale;
    private float _speed = 2000;

    public override void _Ready()
    {
        Collision = GetNodeOrNull<CollisionShape2D>("CollisionShape2D");
        Collision.Disabled = true;
        Shape = Collision.Shape as RectangleShape2D;
        LineSprite = GetNodeOrNull<Sprite2D>("LineSprite");
        _pixelScale = 1f / LineSprite.Texture.GetHeight();

        AreaEntered += OnArea2dEntered;
    }

    public void InitData(BulletData data, uint attackLayer)
    {
        InitData(data, attackLayer, 5);
    }
    
    public void InitData(BulletData data, uint attackLayer, float width)
    {
        AttackLayer = attackLayer;
        
        Position = data.Position;
        Rotation = data.Rotation;

        //计算射线最大距离, 也就是撞到墙壁的距离
        var targetPosition = data.Position + Vector2.FromAngle(data.Rotation) * data.MaxDistance;
        var parameters = PhysicsRayQueryParameters2D.Create(data.Position, targetPosition, PhysicsLayer.Wall);
        var result = GetWorld2D().DirectSpaceState.IntersectRay(parameters);
        float distance;
        if (result != null && result.TryGetValue("position", out var point))
        {
            distance = Position.DistanceTo((Vector2)point);
        }
        else
        {
            distance = data.MaxDistance;
        }
        
        Collision.SetDeferred(CollisionShape2D.PropertyName.Disabled, false);
        Collision.Position = Vector2.Zero;
        Shape.Size = Vector2.Zero;;
        LineSprite.Scale = new Vector2(0, width * _pixelScale);

        //如果子弹会对玩家造成伤害, 则显示成红色
        if (Player.Current.CollisionWithMask(attackLayer))
        {
            LineSprite.Modulate = new Color(2.5f, 0.5f, 0.5f);
        }
        
        //激光飞行时间
        var time = distance / _speed;

        var tween = CreateTween();
        tween.SetParallel();
        tween.TweenProperty(LineSprite, "scale", new Vector2(distance, width * _pixelScale), time);
        tween.TweenProperty(Collision, "position", new Vector2(distance * 0.5f, 0), time);
        tween.TweenProperty(Shape, "size", new Vector2(distance, width), time);
        tween.Chain();
        //持续时间
        // tween.TweenInterval(0.2f);
        // tween.Chain();
        tween.TweenCallback(Callable.From(() =>
        {
            Collision.SetDeferred(CollisionShape2D.PropertyName.Disabled, false);
        }));
        tween.Chain();
        tween.TweenProperty(LineSprite, "scale", new Vector2(distance, 0), 0.3f);
        tween.Chain();
        tween.TweenCallback(Callable.From(() =>
        {
            Destroy();
        }));
        tween.Play();
    }

    public override void _Process(double delta)
    {
        ProxyCoroutineHandler.ProxyUpdateCoroutine(ref _coroutineList, (float)delta);
    }

    public void Destroy()
    {
        if (IsDestroyed)
        {
            return;
        }
        
        QueueFree();
    }
    
    private void OnArea2dEntered(Area2D other)
    {
        var role = other.AsActivityObject<Role>();
        if (role != null)
        {
            //计算子弹造成的伤害
            var damage = Utils.Random.RandomRangeInt(BulletData.MinHarm, BulletData.MaxHarm);
            if (BulletData.TriggerRole != null)
            {
                damage = BulletData.TriggerRole.RoleState.CallCalcDamageEvent(damage);
            }
            //造成伤害
            role.CallDeferred(nameof(Role.Hurt), damage, Rotation);
        }
    }

    public long StartCoroutine(IEnumerator able)
    {
        return ProxyCoroutineHandler.ProxyStartCoroutine(ref _coroutineList, able);
    }

    public void StopCoroutine(long coroutineId)
    {
        ProxyCoroutineHandler.ProxyStopCoroutine(ref _coroutineList, coroutineId);
    }

    public bool IsCoroutineOver(long coroutineId)
    {
        return ProxyCoroutineHandler.ProxyIsCoroutineOver(ref _coroutineList, coroutineId);
    }

    public void StopAllCoroutine()
    {
        ProxyCoroutineHandler.ProxyStopAllCoroutine(ref _coroutineList);
    }
}
