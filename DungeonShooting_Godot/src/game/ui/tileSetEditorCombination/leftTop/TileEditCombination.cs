using System.Collections.Generic;
using Godot;

namespace UI.TileSetEditorCombination;

public partial class TileEditCombination : GridBg<TileSetEditorCombination.LeftTopBg>
{
    //kay: 代表原图中的坐标
    private Dictionary<Vector2I, CombinationCell> _brushData = new Dictionary<Vector2I, CombinationCell>();
    private Vector2I _brushOffset = Vector2I.Zero;
    private int _xStart = 0;
    private int _yStart = 0;
    private int _xEnd = 0;
    private int _yEnd = 0;
    
    public override void SetUiNode(IUiNode uiNode)
    {
        base.SetUiNode(uiNode);
        Grid = UiNode.L_Grid.Instance;
        ContainerRoot = UiNode.L_CombinationRoot.Instance;
        
        UiNode.UiPanel.AddEventListener(EventEnum.OnSelectContainerCell, OnSelectContainerCell);
        UiNode.UiPanel.AddEventListener(EventEnum.OnClearContainerCell, OnClearContainerCell);
    }

    public override void _Process(double delta)
    {
        if (!UiNode.UiPanel.IsOpen)
        {
            return;
        }

        UiNode.L_CombinationRoot.L_BrushRoot.Instance.Position = UiNode.L_CombinationRoot.Instance
            .GetLocalMousePosition().FloorAdsorption(GameConfig.TileCellSizeVector2I);
    }

    private void OnSelectContainerCell(object obj)
    {
        if (obj is Vector2I cell && !_brushData.ContainsKey(cell))
        {
            var cellData = new CombinationCell();
            var pos = cell * GameConfig.TileCellSize + _brushOffset;
            cellData.Position = pos;
            cellData.InitData(UiNode.UiPanel.EditorPanel.Texture, cell);
            UiNode.L_CombinationRoot.L_BrushRoot.AddChild(cellData);
            _brushData.Add(cell, cellData);
            
            //计算起始点和终点
            //if (pos.X < _xStart || pos.X > _xEnd || pos.) 
        }
    }
    
    private void OnClearContainerCell(object obj)
    {
        foreach (var keyValuePair in _brushData)
        {
            keyValuePair.Value.QueueFree();
        }
        _brushData.Clear();
        _brushOffset = Vector2I.Zero;
        _xStart = 0;
        _yStart = 0;
        _xEnd = 0;
        _yEnd = 0;
    }
}