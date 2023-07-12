
using Godot;

public abstract class UiCell<TUiCellNode, T> : IDestroy where TUiCellNode : IUiCellNode
{
    public bool IsDestroyed { get; private set; }
    
    public UiGrid<TUiCellNode, T> Grid { get; set; }
    public TUiCellNode CellNode { get; set; }
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