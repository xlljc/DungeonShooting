
using Godot;

public abstract class UiCell<TNodeType, TUiNodeType, T> : IDestroy where TNodeType : Node where TUiNodeType : IUiNode<TNodeType, TUiNodeType>
{
    public bool IsDestroyed { get; private set; }
    
    public UiGrid<TNodeType, TUiNodeType, T> Grid { get; set; }
    public TUiNodeType CellNode { get; set; }
    public T Data { get; set; }

    public virtual void OnInit()
    {
    }

    public virtual void OnSetData(T data)
    {
    }

    public void Destroy()
    {
        if (IsDestroyed)
        {
            return;
        }

        IsDestroyed = true;
    }
}