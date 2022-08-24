using Godot;

public abstract class Component<TN, TG> : IComponent<TG> where TN : Node where TG : Node2D
{
    public GameObject<TG> GameObject { get; private set; }
    public TN Node { get; }

    public Component(TN inst)
    {
        Node = inst;
    }

    public abstract void Process(float delta);

    public abstract void PhysicsProcess(float delta);

    public void SetGameObject(GameObject<TG> gameObject)
    {
        GameObject = gameObject;
    }
}