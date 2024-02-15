

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
    /// <summary>
    /// 选中Cell的时的回调, 参数为 Cell 索引
    /// </summary>
    public event Action<int> SelectEvent;
    
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
                
                if (SelectEvent != null)
                {
                    SelectEvent(newIndex);
                }
            }
        }
    }

    /// <summary>
    /// 选中的 Cell 包含的数据
    /// </summary>
    public TData SelectData => _selectIndex >= 0 ? _cellList[_selectIndex].Data : default;

    public bool Visible
    {
        get => GridContainer.Visible;
        set => GridContainer.Visible = value;
    }

    public int Count => _cellList.Count;

    /// <summary>
    /// Godot 原生网格容器
    /// </summary>
    public GridContainer GridContainer { get; private set; }

    //模板对象
    private TUiCellNode _template;

    //模板大小
    private Vector2 _size = Vector2.Zero;

    //cell逻辑处理类
    private Type _cellType;

    //当前活动的cell池
    private List<UiCell<TUiCellNode, TData>> _cellList = new List<UiCell<TUiCellNode, TData>>();

    //当前已被回收的cell池
    private Stack<UiCell<TUiCellNode, TData>> _cellPool = new Stack<UiCell<TUiCellNode, TData>>();

    //单个cell偏移
    private Vector2I _cellOffset;

    //列数
    private int _columns;

    //是否自动扩展列数
    private bool _autoColumns;

    //选中的cell索引
    private int _selectIndex = -1;

    public UiGrid(TUiCellNode template, Node parent, Type cellType)
    {
        GridContainer = new UiGridContainer(OnReady, OnProcess);
        GridContainer.Ready += OnReady;
        _template = template;
        _cellType = cellType;
        parent.AddChild(GridContainer);
        var uiInstance = _template.GetUiInstance();
        uiInstance.GetParent()?.RemoveChild(uiInstance);
        if (uiInstance is Control control)
        {
            _size = control.Size;
            if (control.CustomMinimumSize == Vector2.Zero)
            {
                control.CustomMinimumSize = _size;
            }
        }
    }
    
    public UiGrid(TUiCellNode template, Type cellType)
    {
        GridContainer = new UiGridContainer(OnReady, OnProcess);
        GridContainer.Ready += OnReady;
        _template = template;
        _cellType = cellType;
        var uiInstance = _template.GetUiInstance();
        uiInstance.AddSibling(GridContainer);
        uiInstance.GetParent().RemoveChild(uiInstance);
        if (uiInstance is Control control)
        {
            _size = control.Size;
            if (control.CustomMinimumSize == Vector2.Zero)
            {
                control.CustomMinimumSize = _size;
            }
        }
    }

    /// <summary>
    /// 设置每个 Cell 之间的偏移量
    /// </summary>
    public void SetCellOffset(Vector2I offset)
    {
        _cellOffset = offset;
        GridContainer.AddThemeConstantOverride("h_separation", offset.X);
        GridContainer.AddThemeConstantOverride("v_separation", offset.Y);
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
        GridContainer.Columns = columns;
    }

    /// <summary>
    /// 获取列数
    /// </summary>
    public int GetColumns()
    {
        return GridContainer.Columns;
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
                GridContainer.Resized += OnGridResized;
                OnGridResized();
            }
            else
            {
                GridContainer.Columns = _columns;
                GridContainer.Resized -= OnGridResized;
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
        SetHorizontalExpand(GridContainer, flag);
    }

    /// <summary>
    /// 获取当前组件布局方式是否横向扩展
    /// </summary>
    public bool GetHorizontalExpand()
    {
        return GetHorizontalExpand(GridContainer);
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
    /// 根据自定义回调查询数据
    /// </summary>
    public UiCell<TUiCellNode, TData> Find(Func<UiCell<TUiCellNode, TData>, bool> func)
    {
        foreach (var uiCell in _cellList)
        {
            if (func(uiCell))
            {
                return uiCell;
            }
        }

        return null;
    }
    
    /// <summary>
    /// 遍历所有 Cell
    /// </summary>
    public void ForEach(Action<UiCell<TUiCellNode, TData>> callback)
    {
        foreach (var uiCell in _cellList)
        {
            callback(uiCell);
        }
    }
    
    /// <summary>
    /// 遍历所有 Cell, 回调函数返回 false 跳出循环
    /// </summary>
    public void ForEach(Func<UiCell<TUiCellNode, TData>, bool> callback)
    {
        foreach (var uiCell in _cellList)
        {
            if (!callback(uiCell))
            {
                return;
            }
        }
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
                GridContainer.AddChild(cell.CellNode.GetUiInstance());
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
    /// 添加单条 Cell 数据, select 为是否立即选中
    /// </summary>
    public void Add(TData data, bool select = false)
    {
        var cell = GetCellInstance();
        GridContainer.AddChild(cell.CellNode.GetUiInstance());
        cell.SetData(data);
        if (select)
        {
            SelectIndex = Count - 1;
        }
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
        _cellList.Clear();
        foreach (var uiCell in uiCells)
        {
            ReclaimCellInstance(uiCell);
        }
    }

    public void Click(int index)
    {
        if (index < 0 || index >= _cellList.Count)
        {
            return;
        }

        _cellList[index].Click();
    }

    /// <summary>
    /// 对所有已经启用的 Cell 进行排序操作, 排序时会调用 Cell 的 OnSort() 函数用于处理排序逻辑<br/>
    /// 注意: 排序会影响 Cell 的 Index
    /// </summary>
    public void Sort()
    {
        if (_cellList.Count <= 0)
        {
            return;
        }

        //这里记录 SelectIndex 是让排序后 SelectIndex 指向的 Cell 不变
        var selectIndex = SelectIndex;
        var selectCell = GetCell(selectIndex);
        //执行排序操作
        _cellList.Sort((a, b) => a.OnSort(b));
        if (selectIndex >= 0)
        {
            selectIndex = _cellList.FindIndex(cell => cell == selectCell);
        }

        //先移除所有节点
        for (var i = 0; i < _cellList.Count; i++)
        {
            GridContainer.RemoveChild(_cellList[i].CellNode.GetUiInstance());
        }

        if (selectIndex >= 0)
        {
            _selectIndex = selectIndex;
        }

        //以新的顺序加入GridContainer
        for (var i = 0; i < _cellList.Count; i++)
        {
            GridContainer.AddChild(_cellList[i].CellNode.GetUiInstance());
        }

        //刷新Index
        for (var i = 0; i < _cellList.Count; i++)
        {
            var cell = _cellList[i];
            cell.SetIndex(i);
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
        _template.GetUiInstance().QueueFree();
        GridContainer.QueueFree();
    }

    private void OnReady()
    {
        if (_template.GetUiInstance() is Control control)
        {
            GridContainer.Position = control.Position;
        }
    }

    private void OnProcess(float delta)
    {
        if (IsDestroyed || !_template.GetUiPanel().IsOpen)
        {
            return;
        }

        //调用 cell 更新
        var uiCells = _cellList.ToArray();
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
        GridContainer.RemoveChild(cell.CellNode.GetUiInstance());
        _cellPool.Push(cell);
    }

    private void OnGridResized()
    {
        if (_autoColumns && GridContainer != null)
        {
            var width = GridContainer.Size.X;
            if (width <= _size.X + _cellOffset.X)
            {
                GridContainer.Columns = 1;
            }
            else
            {
                GridContainer.Columns = Mathf.FloorToInt(width / (_size.X + _cellOffset.X));
            }
        }
    }

    /// <summary>
    /// 设置Ui布局方式是否横向扩展, 如果为 true, 则 GridContainer 的宽度会撑满父物体
    /// </summary>
    private static void SetHorizontalExpand(Control control, bool flag)
    {
        if (flag)
        {
            control.LayoutMode = 1;
            control.AnchorsPreset = (int)Control.LayoutPreset.TopWide;
            control.SizeFlagsHorizontal |= Control.SizeFlags.Expand;
        }
        else if ((control.SizeFlagsHorizontal & Control.SizeFlags.Expand) != 0)
        {
            control.LayoutMode = 1;
            control.AnchorsPreset = (int)Control.LayoutPreset.TopLeft;
            control.SizeFlagsHorizontal ^= Control.SizeFlags.Expand;
        }
    }

    /// <summary>
    /// 获取Ui布局方式是否横向扩展
    /// </summary>
    private static bool GetHorizontalExpand(Control control)
    {
        return (control.SizeFlagsHorizontal & Control.SizeFlags.Expand) != 0;
    }
}