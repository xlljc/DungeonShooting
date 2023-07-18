

using System;
using System.Collections.Generic;
using Godot;

/// <summary>
/// Ui网格组件
/// </summary>
/// <typeparam name="TUiCellNode">Ui节点类型</typeparam>
/// <typeparam name="TData">传给Cell的数据类型</typeparam>
public partial class UiGrid<TUiCellNode, TData> : IDestroy where TUiCellNode : IUiCellNode
{
    public bool IsDestroyed { get; private set; }
    
    private TUiCellNode _template;
    private Vector2 _size = Vector2.Zero;
    private Node _parent;
    private Type _cellType;
    private Stack<UiCell<TUiCellNode, TData>> _cellPool = new Stack<UiCell<TUiCellNode, TData>>();
    private List<UiCell<TUiCellNode, TData>> _cellList = new List<UiCell<TUiCellNode, TData>>();

    
    private GridContainer _gridContainer;
    private Vector2I _cellOffset;
    private int _columns;
    private bool _autoColumns;

    public UiGrid(TUiCellNode template, Type cellType)
    {
        _gridContainer = new GridContainer();
        _gridContainer.Ready += OnReady;
        _template = template;
        _cellType = cellType;
        var uiInstance = _template.GetUiInstance();
        _parent = uiInstance.GetParent();
        _parent.RemoveChild(uiInstance);
        _parent.AddChild(_gridContainer);
        if (uiInstance is Control control)
        {
            _size = control.Size;
        }
    }
    
    public void SetCellOffset(Vector2I offset)
    {
        if (_gridContainer != null)
        {
            _cellOffset = offset;
            _gridContainer.AddThemeConstantOverride("h_separation", offset.X);
            _gridContainer.AddThemeConstantOverride("v_separation", offset.Y);
        }
    }

    public Vector2I GetCellOffset()
    {
        return _cellOffset;
    }
    
    public void SetColumns(int columns)
    {
        _columns = columns;
        if (_gridContainer != null)
        {
            _gridContainer.Columns = columns;
        }
    }

    public int GetColumns()
    {
        if (_gridContainer != null)
        {
            return _gridContainer.Columns;
        }

        return _columns;
    }

    public void SetAutoColumns(bool flag)
    {
        if (flag != _autoColumns)
        {
            _autoColumns = flag;
            if (_gridContainer != null)
            {
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
    }

    public bool GetAutoColumns()
    {
        return _autoColumns;
    }
    
    public void SetHorizontalExpand(bool flag)
    {
        if (_gridContainer != null)
        {
            if (flag)
            {
                _gridContainer.SizeFlagsHorizontal |= Control.SizeFlags.Expand;
            }
            else if ((_gridContainer.SizeFlagsHorizontal & Control.SizeFlags.Expand) != 0)
            {
                _gridContainer.SizeFlagsHorizontal ^= Control.SizeFlags.Expand;
            }
        }
    }

    public bool GetHorizontalExpand()
    {
        if (_gridContainer != null)
        {
            return (_gridContainer.SizeFlagsHorizontal & Control.SizeFlags.Expand) != 0;
        }

        return false;
    }
    
    /// <summary>
    /// 设置当前网格组件中的所有数据
    /// </summary>
    public void SetDataList(TData[] array)
    {
        if (array.Length > _cellList.Count)
        {
            do
            {
                var cell = GetCellInstance();
                _cellList.Add(cell);
                _gridContainer.AddChild(cell.CellNode.GetUiInstance());
            } while (array.Length > _cellList.Count);
        }
        else if(array.Length < _cellList.Count)
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
    /// 添加单条数据
    /// </summary>
    /// <param name="data"></param>
    public void Add(TData data)
    {
        var cell = GetCellInstance();
        _cellList.Add(cell);
        _gridContainer.AddChild(cell.CellNode.GetUiInstance());
        cell.SetData(data);
    }
    
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
        if (_gridContainer != null)
        {
            _gridContainer.QueueFree();
        }
    }

    private void OnReady()
    {
        _gridContainer.Ready -= OnReady;
        if (_template.GetUiInstance() is Control control)
        {
            _gridContainer.Position = control.Position;
        }
    }
    
    private UiCell<TUiCellNode, TData> GetCellInstance()
    {
        if (_cellPool.Count > 0)
        {
            return _cellPool.Pop();
        }

        var uiCell = Activator.CreateInstance(_cellType) as UiCell<TUiCellNode, TData>;
        if (uiCell is null)
        {
            throw new Exception($"cellType 无法转为'{typeof(UiCell<TUiCellNode, TData>).FullName}'类型!");
        }
        uiCell.Init(this, (TUiCellNode)_template.CloneUiCell());
        return uiCell;
    }

    private void ReclaimCellInstance(UiCell<TUiCellNode, TData> cell)
    {
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