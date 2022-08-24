using System.Collections.Generic;
using Godot;

public class GameObject<T> : Node, IDestroy where T : Node2D
{
    public float Altitude { get; set; }

    public Vector2 Position
    {
        get => Node.Position;
        set => Node.Position = value;
    }

    public Vector2 GlobalPosition
    {
        get => Node.GlobalPosition;
        set => Node.GlobalPosition = value;
    }
    
    public bool Visible
    {
        get => Node.Visible;
        set => Node.Visible = value;
    }

    public bool IsDestroyed { get; private set; }

    public T Node { get; private set; }

    private List<Component<T>> _components = new List<Component<T>>();

    public GameObject(T node)
    {
        Name = "ComponentControl";
        Node = node;
        node.AddChild(this);
    }

    public void AddComponent<TN>(NodeComponent<T, TN> nodeComponent) where TN : Node
    {
        if (!_components.Contains(nodeComponent))
        {
            _components.Add(nodeComponent);
            nodeComponent.SetGameObject(this);
            Node.AddChild(nodeComponent.Node);
            nodeComponent.OnMount();
        }
    }

    public void RemoveComponent<TN>(NodeComponent<T, TN> nodeComponent) where TN : Node
    {
        if (_components.Remove(nodeComponent))
        {
            nodeComponent.SetGameObject(null);
            Node.RemoveChild(nodeComponent.Node);
            nodeComponent.OnUnMount();
        }
    }
    
    public void AddComponent(Component<T> component)
    {
        if (!_components.Contains(component))
        {
            _components.Add(component);
            component.SetGameObject(this);
            component.OnMount();
        }
    }

    public void RemoveComponent(Component<T> component)
    {
        if (_components.Remove(component))
        {
            component.SetGameObject(null);
            component.OnUnMount();
        }
    }

    public override void _Process(float delta)
    {
        var arr = _components.ToArray();
        for (int i = 0; i < arr.Length; i++)
        {
            if (IsDestroyed) return;
            var temp = arr[i];
            if (temp != null && temp.GameObject == this && temp.Enable)
            {
                temp._TriggerProcess(delta);
            }
        }
    }

    public override void _PhysicsProcess(float delta)
    {
        var arr = _components.ToArray();
        for (int i = 0; i < arr.Length; i++)
        {
            if (IsDestroyed) return;
            var temp = arr[i];
            if (temp != null && temp.GameObject == this && temp.Enable)
            {
                temp._TriggerPhysicsProcess(delta);
            }
        }
    }

    public void Destroy()
    {
        if (IsDestroyed)
        {
            return;
        }

        IsDestroyed = true;
        Node.QueueFree();
        var arr = _components.ToArray();
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i].Destroy();
        }
    }
}