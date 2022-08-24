using System.Collections.Generic;
using Godot;

public abstract class GameObject<T> : IProcess where T : Node2D
{
    public Vector3 Position { get; set; }
    public Vector2 Position2D { get; set; }

    public T Node;

    private List<IComponent<T>> _components = new List<IComponent<T>>();
    private bool _isDestroy = false;

    public GameObject(T node)
    {
        Node = node;
    }

    public void AddComponent<TN>(Component<TN, T> component) where TN : Node
    {
        if (!_components.Contains(component))
        {
            component.SetGameObject(this);
            Node.AddChild(component.Node);
        }
    }

    public void RemoveComponent<TN>(Component<TN, T> component) where TN : Node
    {
        if (_components.Remove(component))
        {
            component.SetGameObject(null);
            Node.RemoveChild(component.Node);
        }
    }

    public void Process(float delta)
    {
        var arr = _components.ToArray();
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i].Process(delta);
        }
    }

    public void PhysicsProcess(float delta)
    {
        var arr = _components.ToArray();
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i].PhysicsProcess(delta);
        }
    }

    public void Destroy()
    {
        if (_isDestroy)
        {
            return;
        }
        _isDestroy = true;
    }

    public bool IsDestroy()
    {
        return _isDestroy;
    }
}