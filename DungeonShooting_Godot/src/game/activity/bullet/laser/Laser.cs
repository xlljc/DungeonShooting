using System.Collections;
using System.Collections.Generic;
using Godot;

/// <summary>
/// 激光子弹
/// </summary>
public partial class Laser : Area2D, IBullet
{
    public CollisionShape2D Collision { get; private set; }
    public Line2D Line { get; private set; }
    public RectangleShape2D Shape { get; private set; }
    
    public uint AttackLayer { get; set; }
    public Weapon Weapon { get; private set; }
    public Role TriggerRole { get; private set; }
    
    public bool IsDestroyed { get; private set; }
    //开启的协程
    private List<CoroutineData> _coroutineList;

    public override void _Ready()
    {
        Collision = GetNodeOrNull<CollisionShape2D>("CollisionShape2D");
        Shape = Collision.Shape as RectangleShape2D;
        Line = GetNodeOrNull<Line2D>("Line2D");
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
        
        Collision.Position = new Vector2(distance * 0.5f, 0);
        Shape.Size = new Vector2(distance, width);


        var tween = CreateTween();
        tween.SetParallel();
        tween.TweenProperty(Line, "width", width, 0.3);
        tween.TweenMethod(Callable.From((float v) =>
        {
            Line.SetPointPosition(1, new Vector2(v, 0));
        }), 0, distance, 0.1);
        tween.Chain();
        tween.TweenInterval(1.5);
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
