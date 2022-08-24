using Godot;

public abstract class NodeComponent<TG, TN> : Component<TG> where TN : Node where TG : Node2D
{
    public TN Node { get; }

    public NodeComponent(TN inst)
    {
        Node = inst;
    }

    public override void OnDestroy()
    {
        Node.QueueFree();
    }
}