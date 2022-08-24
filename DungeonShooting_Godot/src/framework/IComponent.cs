
using Godot;

public interface IComponent<TN> : IProcess where TN : Node2D
{
    GameObject<TN> GameObject { get; }

    void SetGameObject(GameObject<TN> gameObject);
}