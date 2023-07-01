

using System;
using System.Collections.Generic;
using Godot;

/// <summary>
/// Ui网格组件
/// </summary>
/// <typeparam name="TNodeType">原生Godot类型</typeparam>
/// <typeparam name="TUiNodeType">Ui节点类型</typeparam>
/// <typeparam name="TData">传给Cell的数据类型</typeparam>
public partial class UiGrid<TNodeType, TUiNodeType, TData> : GridContainer, IDestroy where TNodeType : Node where TUiNodeType : IUiNode<TNodeType, TUiNodeType>
{
    public bool IsDestroyed { get; private set; }
    private TUiNodeType _template;
    private Node _parent;
    private Type _cellType;
    private Stack<UiCell<TNodeType, TUiNodeType, TData>> _cellPool = new Stack<UiCell<TNodeType, TUiNodeType, TData>>();
    private List<UiCell<TNodeType, TUiNodeType, TData>> _cellList = new List<UiCell<TNodeType, TUiNodeType, TData>>();

    public UiGrid(TUiNodeType template, Type cellType, int columns, int offsetX, int offsetY)
    {
        _template = template;
        _cellType = cellType;
        _parent = _template.Instance.GetParent();
        _parent.RemoveChild(_template.Instance);
        _parent.AddChild(this);
        Columns = columns;
        AddThemeConstantOverride("h_separation", offsetX);
        AddThemeConstantOverride("v_separation", offsetY);
    }

    public override void _Ready()
    {
        if (_template.Instance is Control control)
        {
            Position = control.Position;
        }
    }

    public void SetDataList(TData[] array)
    {
        if (array.Length > _cellList.Count)
        {
            do
            {
                var cell = GetCellInstance();
                _cellList.Add(cell);
                AddChild(cell.CellNode.Instance);
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
            _cellList[i].OnSetData(data);
        }
    }

    public void Add(TData data)
    {
        var cell = GetCellInstance();
        _cellList.Add(cell);
        AddChild(cell.CellNode.Instance);
        cell.OnSetData(data);
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
    }

    private UiCell<TNodeType, TUiNodeType, TData> GetCellInstance()
    {
        if (_cellPool.Count > 0)
        {
            return _cellPool.Pop();
        }

        var uiCell = Activator.CreateInstance(_cellType) as UiCell<TNodeType, TUiNodeType, TData>;
        if (uiCell is null)
        {
            throw new Exception($"cellType 无法转为'{typeof(UiCell<TNodeType, TUiNodeType, TData>).FullName}'类型!");
        }
        uiCell.CellNode = _template.Clone();
        uiCell.Grid = this;
        uiCell.OnInit();
        return uiCell;
    }

    private void ReclaimCellInstance(UiCell<TNodeType, TUiNodeType, TData> cell)
    {
        RemoveChild(cell.CellNode.Instance);
        _cellPool.Push(cell);
    }
}