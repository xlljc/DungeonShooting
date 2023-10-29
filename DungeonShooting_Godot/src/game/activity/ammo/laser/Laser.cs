using System.Collections;
using System.Collections.Generic;
using Godot;

/// <summary>
/// 激光子弹
/// </summary>
public partial class Laser : Area2D, IAmmo
{
    public CollisionShape2D Collision { get; private set; }
    public Sprite2D LineSprite { get; private set; }
    public RectangleShape2D Shape { get; private set; }

    public uint AttackLayer
    {
        get => CollisionMask;
        set => CollisionMask = value;
    }
    public Weapon Weapon { get; private set; }
    public Role TriggerRole { get; private set; }
    
    public bool IsDestroyed { get; private set; }
    //开启的协程
    private List<CoroutineData> _coroutineList;
    private float _pixelScale;
    private float _speed = 1200;

    public override void _Ready()
    {
        Collision = GetNodeOrNull<CollisionShape2D>("CollisionShape2D");
        Collision.Disabled = true;
        Shape = Collision.Shape as RectangleShape2D;
        LineSprite = GetNodeOrNull<Sprite2D>("LineSprite");
        _pixelScale = 1f / LineSprite.Texture.GetHeight();

        AreaEntered += OnArea2dEntered;
    }

    public void Init(Weapon weapon, uint targetLayer)
    {
        TriggerRole = weapon.TriggerRole;
        Weapon = weapon;
        AttackLayer = targetLayer;
    }

    public void Init(Weapon weapon, uint targetLayer, Vector2 position, float rotation, float width, float distance)
    {
        Init(weapon, targetLayer);
        Position = position;
        Rotation = rotation;

        //计算射线最大距离, 也就是撞到墙壁的距离
        var targetPosition = position + Vector2.FromAngle(rotation) * distance;
        var parameters = PhysicsRayQueryParameters2D.Create(position, targetPosition, PhysicsLayer.Wall);
        var result = GetWorld2D().DirectSpaceState.IntersectRay(parameters);
        if (result != null)
        {
            var point = (Vector2)result["position"];
            distance = position.DistanceTo(point);
        }

        
        Collision.SetDeferred(CollisionShape2D.PropertyName.Disabled, false);
        Collision.Position = Vector2.Zero;
        Shape.Size = Vector2.Zero;;
        LineSprite.Scale = new Vector2(0, width * _pixelScale);

        //激光飞行时间
        var time = distance / _speed;

        var tween = CreateTween();
        tween.SetParallel();
        tween.TweenProperty(LineSprite, "scale", new Vector2(distance, width * _pixelScale), time);
        tween.TweenProperty(Collision, "position", new Vector2(distance * 0.5f, 0), time);
        tween.TweenProperty(Shape, "size", new Vector2(distance, width), time);
        tween.Chain();
        tween.TweenInterval(0.2f);
        tween.Chain();
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
            //造成伤害
            role.CallDeferred(nameof(Role.Hurt), 4, Rotation);
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
