
using System;
using Godot;

/// <summary>
/// 网格组件中单个格子的数据处理类
/// </summary>
/// <typeparam name="TUiCellNode">ui节点类型</typeparam>
/// <typeparam name="T">数据类型</typeparam>
public abstract class UiCell<TUiCellNode, T> : IUiCell, IData<T> where TUiCellNode : IUiCellNode
{
    public bool IsDestroyed { get; private set; }
    
    public bool Enable { get; private set; }
    
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
    //上一次点击的时间
    private long _prevClickTime = -1;

    public IUiGrid GetGrid()
    {
        return Grid;
    }

    public virtual void OnInit()
    {
    }

    /// <summary>
    /// 当前cell被分配值时调用
    /// </summary>
    public virtual void OnSetData(T data)
    {
    }


    public virtual void Process(float delta)
    {
    }
    
    public virtual void OnClick()
    {
    }

    public virtual void OnDoubleClick()
    {
    }
    
    public virtual void OnEnable()
    {
    }
    
    public virtual void OnDisable()
    {
    }

    public virtual bool CanSelect()
    {
        return true;
    }
    
    public virtual void OnSelect()
    {
    }
    
    public virtual void OnUnSelect()
    {
    }
    
    public virtual void OnRefreshIndex()
    {
    }
    
    public virtual void OnDestroy()
    {
    }

    /// <summary>
    /// 当 Cell 参与排序时调用, 参数 other 为需要对比的另一个 Cell 对象<br/>
    /// 函数返回 -1 表示当前 Cell 在 other 之前<br/>
    /// 函数返回 0 表示当前 Cell 和 other 位置相同<br/>
    /// 函数返回 1 表示当前 Cell 和 other 之后<br/>
    /// </summary>
    public virtual int OnSort(UiCell<TUiCellNode, T> other)
    {
        return 0;
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
    /// 设置当前 Cell 的值, 该函数由 UiGrid 调用
    /// </summary>
    public void SetData(T data)
    {
        Data = data;
        OnSetData(data);
    }

    /// <summary>
    /// 设置当前 Cell 的索引, 该函数由 UiGrid 对象调用
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
    /// 设置是否启用该 Cell, 该函数由 UiGrid 对象调用
    /// </summary>
    public void SetEnable(bool value)
    {
        Enable = value;
        if (value)
        {
            OnEnable();
        }
        else
        {
            OnDisable();
        }
    }

    /// <summary>
    /// 触发点击当前Ui, 如果 Cell 的模板为 BaseButton 类型, 则 UiCell 会自动绑定点击事件
    /// </summary>
    public void Click()
    {
        Grid.SelectIndex = Index;
        OnClick();

        //双击判定
        if (_prevClickTime >= 0)
        {
            var now = DateTime.Now.Ticks / 10000;
            if (now <= _prevClickTime + 500)
            {
                OnDoubleClick();
            }

            _prevClickTime = now;
        }
        else
        {
            _prevClickTime = DateTime.Now.Ticks / 10000;
        }
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