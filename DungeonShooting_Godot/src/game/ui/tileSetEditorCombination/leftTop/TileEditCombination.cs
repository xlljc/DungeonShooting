using System.Collections.Generic;
using Godot;

namespace UI.TileSetEditorCombination;

public partial class TileEditCombination : GridBg<TileSetEditorCombination.LeftTopBg>
{
    //kay: 代表原图中的坐标, 单位: 格
    private Dictionary<Vector2I, CombinationCell> _brushData = new Dictionary<Vector2I, CombinationCell>();
    //笔刷偏移, 单位: 像素
    private Vector2I _brushOffset = Vector2I.Zero;
    //上一帧笔刷位置
    private Vector2 _brushPrevPos = Vector2.Zero;
    private bool _initBrush = false;
    private bool _drawBrushFlag = false;
    private int _xStart = int.MaxValue;
    private int _yStart = int.MaxValue;
    private int _xEnd = int.MinValue;
    private int _yEnd = int.MinValue;

    private Dictionary<Vector2I, CombinationCell> _canvas = new Dictionary<Vector2I, CombinationCell>();
    
    public override void SetUiNode(IUiNode uiNode)
    {
        base.SetUiNode(uiNode);
        Grid = UiNode.L_Grid.Instance;
        ContainerRoot = UiNode.L_CombinationRoot.Instance;
        
        UiNode.UiPanel.AddEventListener(EventEnum.OnSelectCombinationCell, OnSelectCombinationCell);
        UiNode.UiPanel.AddEventListener(EventEnum.OnClearCombinationCell, OnClearCombinationCell);
    }

    public override void _Process(double delta)
    {
        if (!UiNode.UiPanel.IsOpen)
        {
            return;
        }

        var brushRoot = UiNode.L_CombinationRoot.L_BrushRoot.Instance;
        var combinationRoot = UiNode.L_CombinationRoot.Instance;
        Vector2 newPos = combinationRoot.GetLocalMousePosition().FloorAdsorption(GameConfig.TileCellSizeVector2I) + _brushOffset;
        brushRoot.Position = newPos;

        //左键按下开始绘制
        if (_initBrush && Input.IsMouseButtonPressed(MouseButton.Left) && this.IsMouseInRect())
        {
            if (_brushPrevPos != newPos || !_drawBrushFlag)
            {
                _drawBrushFlag = true;
                _brushPrevPos = newPos;
                DrawBrush();
            }
        }
        else
        {
            _drawBrushFlag = false;
        }
    }
    
    //绘制笔刷
    private void DrawBrush()
    {
        var brushRoot = UiNode.L_CombinationRoot.L_BrushRoot.Instance;
        foreach (var keyValuePair in _brushData)
        {
            var combinationCell = keyValuePair.Value;
            var pos = (combinationCell.Position + brushRoot.Position).AsVector2I();
            if (_canvas.TryGetValue(pos, out var canvasCell))
            {
                canvasCell.CloneFrom(combinationCell);
            }
            else
            {
                canvasCell = (CombinationCell)combinationCell.Duplicate();
                canvasCell.Position = pos;
                UiNode.L_CombinationRoot.AddChild(canvasCell);
                _canvas.Add(pos, canvasCell);
            }
        }
    }

    //选中组合的图块
    private void OnSelectCombinationCell(object obj)
    {
        if (obj is Vector2I cell && !_brushData.ContainsKey(cell))
        {
            _initBrush = true;
            var cellData = new CombinationCell();
            cellData.Position = cell * GameConfig.TileCellSize;
            cellData.InitData(UiNode.UiPanel.EditorPanel.Texture, cell);
            UiNode.L_CombinationRoot.L_BrushRoot.AddChild(cellData);
            _brushData.Add(cell, cellData);
            
            //计算起始点和终点
            _xStart = Mathf.Min(cell.X, _xStart);
            _yStart = Mathf.Min(cell.Y, _yStart);
            _xEnd = Mathf.Max(cell.X, _xEnd);
            _yEnd = Mathf.Max(cell.Y, _yEnd);
            _brushOffset = new Vector2I(-(_xStart + (_xEnd - _xStart) / 2), -(_yStart + (_yEnd - _yStart) / 2)) * GameConfig.TileCellSize;
        }
    }
    
    //移除组合图块
    private void OnClearCombinationCell(object obj)
    {
        foreach (var keyValuePair in _brushData)
        {
            keyValuePair.Value.QueueFree();
        }
        _brushData.Clear();
        _initBrush = false;
        _brushOffset = Vector2I.Zero;
        _xStart = int.MaxValue;
        _yStart = int.MaxValue;
        _xEnd = int.MinValue;
        _yEnd = int.MinValue;
    }
}