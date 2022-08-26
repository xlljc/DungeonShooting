using System;
using System.Collections.Generic;
using Godot;

public class ComponentControl<T> : Node, IDestroy where T : ActivityObject
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

    public Sprite Sprite => Node.Sprite;

    public CollisionShape2D Collision => Node.Collision;

    public bool IsDestroyed { get; private set; }

    public T Node { get; private set; }

    private List<KeyValuePair<Type, Component<T>>> _components = new List<KeyValuePair<Type, Component<T>>>();

    public ComponentControl(T node)
    {
        Name = "ComponentControl";
        Node = node;
        node.AddChild(this);
    }

    public void AddComponent(Component<T> component)
    {
        if (!ContainsComponent(component))
        {
            _components.Add(new KeyValuePair<Type, Component<T>>(component.GetType(), component));
            component.SetGameObject(this);
            component.OnMount();
        }
    }

    public void RemoveComponent(Component<T> component)
    {
        if (ContainsComponent(component))
        {
            component.SetGameObject(null);
            component.OnUnMount();
        }
    }

    public Component<T> GetComponent(Type type)
    {
        for (int i = 0; i < _components.Count; i++)
        {
            var temp = _components[i];
            if (temp.Key == type)
            {
                return temp.Value;
            }
        }

        return null;
    }

    public TC GetComponent<TC>() where TC : Component<T>
    {
        var component = GetComponent(typeof(TC));
        if (component == null) return null;
        return (TC)component;
    }

    public override void _Process(float delta)
    {
        var arr = _components.ToArray();
        for (int i = 0; i < arr.Length; i++)
        {
            if (IsDestroyed) return;
            var temp = arr[i].Value;
            if (temp != null && temp.ComponentControl == this && temp.Enable)
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
            var temp = arr[i].Value;
            if (temp != null && temp.ComponentControl == this && temp.Enable)
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
            arr[i].Value?.Destroy();
        }
    }

    private bool ContainsComponent(Component<T> component)
    {
        for (int i = 0; i < _components.Count; i++)
        {
            if (_components[i].Value == component)
            {
                return true;
            }
        }
        return false;
    }
}