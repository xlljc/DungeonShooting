
using Godot;

/// <summary>
/// 组件基类
/// </summary>
public abstract class Component<TG> : IComponent<TG> where TG : Node2D
{
    public GameObject<TG> GameObject { get; private set; }

    public Vector2 Position
    {
        get => GameObject.Position;
        set => GameObject.Position = value;
    }

    public Vector2 GlobalPosition
    {
        get => GameObject.GlobalPosition;
        set => GameObject.GlobalPosition = value;
    }
    
    public bool Visible
    {
        get => GameObject.Visible;
        set => GameObject.Visible = value;
    }

    public bool Enable { get; set; } = true;

    public bool IsDestroyed { get; private set; }

    private bool _isReady = false;
    
    public virtual void Ready()
    {
    }
    
    public virtual void Process(float delta)
    {
    }

    public virtual void PhysicsProcess(float delta)
    {
    }

    public virtual void OnDestroy()
    {
    }

    public virtual void OnMount()
    {
    }
    
    public virtual void OnUnMount()
    {
    }

    public void Destroy()
    {
        if (IsDestroyed)
        {
            return;
        }

        IsDestroyed = true;
        if (GameObject != null)
        {
            GameObject.RemoveComponent(this);
        }

        OnDestroy();
    }

    internal void _TriggerProcess(float delta)
    {
        if (!_isReady)
        {
            _isReady = true;
            Ready();
        }
        Process(delta);
    }
    internal void _TriggerPhysicsProcess(float delta)
    {
        if (!_isReady)
        {
            _isReady = true;
            Ready();
        }
        PhysicsProcess(delta);
    }
    internal void SetGameObject(GameObject<TG> gameObject)
    {
        GameObject = gameObject;
    }
}
