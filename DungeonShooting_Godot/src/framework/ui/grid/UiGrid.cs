

using System;
using System.Collections.Generic;
using Godot;

/// <summary>
/// Ui网格组件
/// </summary>
/// <typeparam name="TUiCellNode">Ui节点类型</typeparam>
/// <typeparam name="TData">传给Cell的数据类型</typeparam>
public class UiGrid<TUiCellNode, TData> : IUiGrid where TUiCellNode : IUiCellNode
{
    public bool IsDestroyed { get; private set; }
    
    public int SelectIndex
    {
        get => _selectIndex;
        set
        {
            var newIndex = Mathf.Clamp(value, -1, _cellList.Count - 1);
            if (_selectIndex != newIndex)
            {
                //检测新的 Cell 是否可以被选中
                if (newIndex >= 0)
                {
                    var uiCell = _cellList[newIndex];
                    //不能被选中, 直接跳出
                    if (!uiCell.CanSelect())
                    {
                        return;
                    }
                }
                var prevIndex = _selectIndex;
                _selectIndex = newIndex;

                //取消选中上一个
                if (prevIndex >= 0 && prevIndex < _cellList.Count)
                {
                    var uiCell = _cellList[prevIndex];
                    uiCell.OnUnSelect();
                }

                //选中新的
                if (newIndex >= 0)
                {
                    var uiCell = _cellList[newIndex];
                    uiCell.OnSelect();
                }
            }
        }
    }
    
    public bool Visible
    {
        get => _gridContainer.Visible;
        set => _gridContainer.Visible = value;
    }

    //模板对象
    private TUiCellNode _template;
    //模板大小
    private Vector2 _size = Vector2.Zero;
    //cell逻辑处理类
    private Type _cellType;
    //当前活动的cell池
    private Stack<UiCell<TUiCellNode, TData>> _cellPool = new Stack<UiCell<TUiCellNode, TData>>();
    //当前已被回收的cell池
    private List<UiCell<TUiCellNode, TData>> _cellList = new List<UiCell<TUiCellNode, TData>>();
    //godot原生网格组件
    private UiGridContainer _gridContainer;
    //单个cell偏移
    private Vector2I _cellOffset;
    //列数
    private int _columns;
    //是否自动扩展列数
    private bool _autoColumns;
    //选中的cell索引
    private int _selectIndex = -1;

    public UiGrid(TUiCellNode template, Type cellType)
    {
        _gridContainer = new UiGridContainer(OnReady, OnProcess);
        _gridContainer.Ready += OnReady;
        _template = template;
        _cellType = cellType;
        var uiInstance = _template.GetUiInstance();
        uiInstance.AddSibling(_gridContainer);
        uiInstance.GetParent().RemoveChild(uiInstance);
        if (uiInstance is Control control)
        {
            _size = control.Size;
        }
    }

    /// <summary>
    /// 设置每个 Cell 之间的偏移量
    /// </summary>
    public void  SetCellOffset(Vector2I offset)
    {
        _cellOffset = offset;
        _gridContainer.AddThemeConstantOverride("h_separation", offset.X);
        _gridContainer.AddThemeConstantOverride("v_separation", offset.Y);
    }

    /// <summary>
    /// 获取每个 Cell 之间的偏移量
    /// </summary>
    public Vector2I GetCellOffset()
    {
        return _cellOffset;
    }

    /// <summary>
    /// 设置列数
    /// </summary>
    public void SetColumns(int columns)
    {
        _columns = columns;
        _gridContainer.Columns = columns;
    }

    /// <summary>
    /// 获取列数
    /// </summary>
    public int GetColumns()
    {
        return _gridContainer.Columns;
    }

    /// <summary>
    /// 设置是否开启自动扩展列, 如果开启, 则组件会根据 GridContainer 组件所占用的宽度自动设置列数
    /// </summary>
    public void SetAutoColumns(bool flag)
    {
        if (flag != _autoColumns)
        {
            _autoColumns = flag;
            if (_autoColumns)
            {
                _gridContainer.Resized += OnGridResized;
                OnGridResized();
            }
            else
            {
                _gridContainer.Columns = _columns;
                _gridContainer.Resized -= OnGridResized;
            }
        }
    }

    /// <summary>
    /// 获取是否开启自动扩展列
    /// </summary>
    public bool GetAutoColumns()
    {
        return _autoColumns;
    }

    /// <summary>
    /// 设置当前组件布局方式是否横向扩展, 如果为 true, 则 GridContainer 的宽度会撑满父物体
    /// </summary>
    public void SetHorizontalExpand(bool flag)
    {
        _gridContainer.SetHorizontalExpand(flag);
    }

    /// <summary>
    /// 获取当前组件布局方式是否横向扩展
    /// </summary>
    public bool GetHorizontalExpand()
    {
        return _gridContainer.GetHorizontalExpand();
    }

    /// <summary>
    /// 获取所有数据
    /// </summary>
    public TData[] GetAllData()
    {
        var array = new TData[_cellList.Count];
        for (var i = 0; i < _cellList.Count; i++)
        {
            array[i] = _cellList[i].Data;
        }

        return array;
    }

    /// <summary>
    /// 获取所有 Cell 对象
    /// </summary>
    public UiCell<TUiCellNode, TData>[] GetAllCell()
    {
        return _cellList.ToArray();
    }

    /// <summary>
    /// 根据指定索引获取数据
    /// </summary>
    public TData GetData(int index)
    {
        if (index < 0 || index >= _cellList.Count)
        {
            return default;
        }

        return _cellList[index].Data;
    }

    /// <summary>
    /// 根据指定索引获取 Cell 对象
    /// </summary>
    public UiCell<TUiCellNode, TData> GetCell(int index)
    {
        if (index < 0 || index >= _cellList.Count)
        {
            return default;
        }

        return _cellList[index];
    }

    /// <summary>
    /// 设置当前网格组件中的所有 Cell 数据, 性能较低
    /// </summary>
    public void SetDataList(TData[] array)
    {
        //取消选中
        SelectIndex = -1;
        if (array.Length > _cellList.Count)
        {
            do
            {
                var cell = GetCellInstance();
                _gridContainer.AddChild(cell.CellNode.GetUiInstance());
            } while (array.Length > _cellList.Count);
        }
        else if (array.Length < _cellList.Count)
        {
            do
            {
                var cell = _cellList[_cellList.Count - 1];
                _cellList.RemoveAt(_cellList.Count - 1);
                ReclaimCellInstance(cell);
            } while (array.Length < _cellList.Count);
        }

        for (var i = 0; i < _cellList.Count; i++)
        {
            var data = array[i];
            _cellList[i].SetData(data);
        }
    }

    /// <summary>
    /// 添加单条 Cell 数据
    /// </summary>
    public void Add(TData data)
    {
        //取消选中
        SelectIndex = -1;
        var cell = GetCellInstance();
        _gridContainer.AddChild(cell.CellNode.GetUiInstance());
        cell.SetData(data);
    }

    /// <summary>
    /// 修改指定索引的位置的 Cell 数据
    /// </summary>
    public void UpdateByIndex(int index, TData data)
    {
        var uiCell = GetCell(index);
        if (uiCell != null)
        {
            uiCell.SetData(data);
        }
    }

    /// <summary>
    /// 移除指定索引的 Cell
    /// </summary>
    /// <param name="index"></param>
    public void RemoveByIndex(int index)
    {
        if (index < 0 || index >= _cellList.Count)
        {
            return;
        }

        if (index >= _selectIndex)
        {
            //取消选中
            SelectIndex = -1;
        }
        var uiCell = _cellList[index];
        _cellList.RemoveAt(index);
        ReclaimCellInstance(uiCell);
        //更新后面的索引
        for (var i = index; i < _cellList.Count; i++)
        {
            var tempCell = _cellList[i];
            tempCell.SetIndex(i);
        }
    }

    /// <summary>
    /// 移除所有 Cell
    /// </summary>
    public void RemoveAll()
    {
        //取消选中
        SelectIndex = -1;
        var uiCells = _cellList.ToArray();
        foreach (var uiCell in uiCells)
        {
            ReclaimCellInstance(uiCell);
        }
    }

    /// <summary>
    /// 销毁当前网格组件
    /// </summary>
    public void Destroy()
    {
        if (IsDestroyed)
        {
            return;
        }

        IsDestroyed = true;

        for (var i = 0; i < _cellList.Count; i++)
        {
            _cellList[i].Destroy();
        }

        foreach (var uiCell in _cellPool)
        {
            uiCell.Destroy();
        }

        _cellList = null;
        _cellPool = null;
        _gridContainer.QueueFree();
    }
    
    private void OnReady()
    {
        if (_template.GetUiInstance() is Control control)
        {
            _gridContainer.Position = control.Position;
        }
    }
    
    private void OnProcess(float delta)
    {
        if (IsDestroyed || !_template.GetUiPanel().IsOpen)
        {
            return;
        }
        //调用 cell 更新
        var uiCells = _cellPool.ToArray();
        for (var i = 0; i < uiCells.Length; i++)
        {
            var item = uiCells[i];
            if (item.Enable)
            {
                item.Process(delta);
            }
        }
    }

    //获取 cell 实例
    private UiCell<TUiCellNode, TData> GetCellInstance()
    {
        if (_cellPool.Count > 0)
        {
            var cell = _cellPool.Pop();
            cell.SetIndex(_cellList.Count);
            cell.SetEnable(true);
            _cellList.Add(cell);
            return cell;
        }

        var uiCell = Activator.CreateInstance(_cellType) as UiCell<TUiCellNode, TData>;
        if (uiCell is null)
        {
            throw new Exception($"cellType 无法转为'{typeof(UiCell<TUiCellNode, TData>).FullName}'类型!");
        }

        _cellList.Add(uiCell);
        uiCell.Init(this, (TUiCellNode)_template.CloneUiCell(), _cellList.Count - 1);
        uiCell.SetEnable(true);
        return uiCell;
    }

    //回收 cell
    private void ReclaimCellInstance(UiCell<TUiCellNode, TData> cell)
    {
        cell.SetEnable(false);
        _gridContainer.RemoveChild(cell.CellNode.GetUiInstance());
        _cellPool.Push(cell);
    }

    private void OnGridResized()
    {
        if (_autoColumns && _gridContainer != null)
        {
            var width = _gridContainer.Size.X;
            if (width <= _size.X + _cellOffset.X)
            {
                _gridContainer.Columns = 1;
            }
            else
            {
                _gridContainer.Columns = Mathf.FloorToInt(width / (_size.X + _cellOffset.X));
            }
        }
    }
}