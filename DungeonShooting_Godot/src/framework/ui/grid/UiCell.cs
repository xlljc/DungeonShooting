
using Godot;

/// <summary>
/// 网格组件中单个格子的数据处理类
/// </summary>
/// <typeparam name="TUiCellNode">ui节点类型</typeparam>
/// <typeparam name="T">数据类型</typeparam>
public abstract class UiCell<TUiCellNode, T> : IDestroy where TUiCellNode : IUiCellNode
{
    public bool IsDestroyed { get; private set; }

    /// <summary>
    /// 当前 Cell 在 UiGrid 组件中的索引位置
    /// </summary>
    public int Index { get; private set; } = -1;
    
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
    public virtual void OnInit()
    {
    }

    /// <summary>
    /// 当前cell被分配值时调用
    /// </summary>
    public virtual void OnSetData(T data)
    {
    }

    /// <summary>
    /// 当当前Ui被点击时调用, 如果 Cell 的模板为 BaseButton 类型, 则 UiCell 会自动绑定点击事件
    /// </summary>
    public virtual void OnClick()
    {
    }

    /// <summary>
    /// 当启用当前 Cell 时调用
    /// </summary>
    public virtual void OnEnable()
    {
    }

    /// <summary>
    /// 当禁用当前 Cell 时调用
    /// </summary>
    public virtual void OnDisable()
    {
    }

    /// <summary>
    /// 当检测当前 Cell 是否可以被选中时调用
    /// </summary>
    public virtual bool CanSelect()
    {
        return true;
    }
    
    /// <summary>
    /// 当前 Cell 选中时调用, 设置 UiGrid.SelectIndex 时触发
    /// </summary>
    public virtual void OnSelect()
    {
    }

    /// <summary>
    /// 当前 Cell 取消选中时调用, 设置 UiGrid.SelectIndex 时触发
    /// </summary>
    public virtual void OnUnSelect()
    {
    }

    /// <summary>
    /// 当 Cell 索引发生改变时调用, 在 UiGrid 中调用 Insert(), Remove() 等函数时被动触发当前 Cell 索引值改变, Cell 业务逻辑需要用到索引值时, 那么就可以重写该函数<br/>
    /// 注意: 该函数第一次调用会在 OnSetData() 之前调用
    /// </summary>
    public virtual void OnRefreshIndex()
    {
    }

    /// <summary>
    /// 销毁当前cell时调用
    /// </summary>
    public virtual void OnDestroy()
    {
    }

    /// <summary>
    /// 初始化数据
    /// </summary>
    public void Init(UiGrid<TUiCellNode, T> grid, TUiCellNode cellNode, int index)
    {
        if (_init)
        {
            return;
        }

        _init = true;
        Grid = grid;
        CellNode = cellNode;
        //绑定点击事件
        if (cellNode.GetUiInstance() is BaseButton button)
        {
            button.Pressed += Click;
        }
        OnInit();
        SetIndex(index);
    }

    /// <summary>
    /// 设置当前cell的值, 这个函数由 UiGrid 调用
    /// </summary>
    public void SetData(T data)
    {
        Data = data;
        OnSetData(data);
    }

    /// <summary>
    /// 设置当前 Cell 的索引
    /// </summary>
    public void SetIndex(int index)
    {
        if (Index != index)
        {
            Index = index;
            OnRefreshIndex();
        }
    }

    /// <summary>
    /// 触发点击当前Ui, 如果 Cell 的模板为 BaseButton 类型, 则 UiCell 会自动绑定点击事件
    /// </summary>
    public void Click()
    {
        Grid.SelectIndex = Index;
        OnClick();
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