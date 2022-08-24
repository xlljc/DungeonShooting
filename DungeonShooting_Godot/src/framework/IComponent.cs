
using Godot;

public interface IComponent<TN> : IProcess, IDestroy where TN : Node2D
{
    GameObject<TN> GameObject { get; }
    
    Vector2 Position { get; set; }
    Vector2 GlobalPosition { get; set; }
    bool Visible { get; set; }
    bool Enable { get; set; }

    void Ready();
    void OnMount();
    void OnUnMount();
    
    void SetGameObject(GameObject<TN> gameObject);
}