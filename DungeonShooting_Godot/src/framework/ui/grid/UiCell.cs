
/// <summary>
/// 网格组件中单个格子的数据处理类
/// </summary>
/// <typeparam name="TUiCellNode">ui节点类型</typeparam>
/// <typeparam name="T">数据类型</typeparam>
public abstract class UiCell<TUiCellNode, T> : IDestroy where TUiCellNode : IUiCellNode
{
    public bool IsDestroyed { get; private set; }
    
    /// <summary>
    /// 所在的网格对象
    /// </summary>
    public UiGrid<TUiCellNode, T> Grid { get; private set; }
    
    /// <summary>
    /// 当前cell使用的Ui节点对象
    /// </summary>
    public TUiCellNode CellNode { get; private set; }
    
    /// <summary>
    /// 当前cell分配的数据
    /// </summary>
    public T Data { get; private set; }

    private bool _init = false;

    /// <summary>
    /// 当前cell初始化时调用
    /// </summary>
    protected virtual void OnInit()
    {
    }

    /// <summary>
    /// 当前cell被分配值时调用
    /// </summary>
    protected virtual void OnSetData(T data)
    {
    }

    /// <summary>
    /// 销毁当前cell时调用
    /// </summary>
    protected virtual void OnDestroy()
    {
    }

    /// <summary>
    /// 初始化数据
    /// </summary>
    public void Init(UiGrid<TUiCellNode, T> grid, TUiCellNode cellNode)
    {
        if (_init)
        {
            return;
        }

        _init = true;
        Grid = grid;
        CellNode = cellNode;
        OnInit();
    }

    /// <summary>
    /// 设置当前cell的值
    /// </summary>
    public void SetData(T data)
    {
        Data = data;
        OnSetData(data);
    }
    
    public void Destroy()
    {
        if (IsDestroyed)
        {
            return;
        }

        OnDestroy();
        IsDestroyed = true;
    }
}