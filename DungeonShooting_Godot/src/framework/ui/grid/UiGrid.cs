

using System;
using System.Collections.Generic;
using Godot;

/// <summary>
/// Ui网格组件
/// </summary>
/// <typeparam name="TNodeType">原生Godot类型</typeparam>
/// <typeparam name="TUiNodeType">Ui节点类型</typeparam>
/// <typeparam name="TData">传给Cell的数据类型</typeparam>
public partial class UiGrid<TUiCellNode, TData> : GridContainer, IDestroy where TUiCellNode : IUiCellNode
{
    public bool IsDestroyed { get; private set; }
    private TUiCellNode _template;
    private Node _parent;
    private Type _cellType;
    private Stack<UiCell<TUiCellNode, TData>> _cellPool = new Stack<UiCell<TUiCellNode, TData>>();
    private List<UiCell<TUiCellNode, TData>> _cellList = new List<UiCell<TUiCellNode, TData>>();

    public UiGrid(TUiCellNode template, Type cellType, int columns, int offsetX, int offsetY)
    {
        _template = template;
        _cellType = cellType;
        var uiInstance = _template.GetUiInstance();
        _parent = uiInstance.GetParent();
        _parent.RemoveChild(uiInstance);
        _parent.AddChild(this);
        Columns = columns;
        AddThemeConstantOverride("h_separation", offsetX);
        AddThemeConstantOverride("v_separation", offsetY);
    }

    public override void _Ready()
    {
        if (_template.GetUiInstance() is Control control)
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
                AddChild(cell.CellNode.GetUiInstance());
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
        AddChild(cell.CellNode.GetUiInstance());
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
        uiCell.CellNode = (TUiCellNode)_template.CloneUiCell();
        uiCell.Grid = this;
        uiCell.OnInit();
        return uiCell;
    }

    private void ReclaimCellInstance(UiCell<TUiCellNode, TData> cell)
    {
        RemoveChild(cell.CellNode.GetUiInstance());
        _cellPool.Push(cell);
    }
}