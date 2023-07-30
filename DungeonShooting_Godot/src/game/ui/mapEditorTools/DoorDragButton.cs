using System;
using Godot;
using UI.MapEditor;

namespace UI.MapEditorTools;

public partial class DoorDragButton : TextureButton
{
    private static Vector2 _stepValue = new Vector2(GameConfig.TileCellSize, GameConfig.TileCellSize);

    
    /// <summary>
    /// 拖拽当前物体的回调函数, 第一个参数为拖拽状态, 第二个参数为相对于初始点的拖拽偏移坐标
    /// </summary>
    public event Action<DragState, Vector2> DragEvent;

    private DoorDragArea _parent;
    private bool _down;
    private Vector2 _startPos;
    private Vector2 _prevPos;
    private MapEditorToolsPanel _mapEditorToolsPanel;
    
    public override void _Ready()
    {
        _parent = GetParent<DoorDragArea>();
        ButtonDown += OnButtonDown;
        ButtonUp += OnButtonUp;
    }

    public void SetMapEditorToolsPanel(MapEditorToolsPanel panel)
    {
        _mapEditorToolsPanel = panel;
    }
    
    public override void _Process(double delta)
    {
        if (_down)
        {
            if (DragEvent != null)
            {
                var value = (GetGlobalMousePosition() - _startPos) / _parent.Scale / _mapEditorToolsPanel.S_DoorToolRoot.Instance.Scale;
                var offset = Utils.Adsorption(value, _stepValue);
                //处理朝向问题
                if (_parent.Direction == DoorDirection.E)
                {
                    offset = new Vector2(offset.Y, offset.X);
                }
                else if (_parent.Direction == DoorDirection.S)
                {
                    offset = new Vector2(-offset.X, offset.Y);
                }
                else if (_parent.Direction == DoorDirection.W)
                {
                    offset = new Vector2(offset.Y, -offset.X);
                }
                
                if (offset != _prevPos)
                {
                    _prevPos = offset;
                    DragEvent(DragState.DragMove, offset);
                }
            }
        }
    }

    private void OnButtonDown()
    {
        if (_mapEditorToolsPanel.EditorMap.Instance.MouseType != EditorTileMap.MouseButtonType.Door)
        {
            return;
        }
        if (_down)
        {
            return;
        }
        _down = true;
        Modulate = new Color(0.7f, 0.7f, 0.7f, 1);
        _startPos = GetGlobalMousePosition();
        _prevPos = new Vector2(-99999, -99999);
        if (DragEvent != null)
        {
            DragEvent(DragState.DragStart, Vector2.Zero);
        }
    }

    private void OnButtonUp()
    {
        if (_mapEditorToolsPanel.EditorMap.Instance.MouseType != EditorTileMap.MouseButtonType.Door)
        {
            return;
        }
        if (!_down)
        {
            return;
        }
        _down = false;
        Modulate = new Color(1, 1, 1, 1);
        if (DragEvent != null)
        {
            var value = (GetGlobalMousePosition() - _startPos) / _parent.Scale / _mapEditorToolsPanel.S_DoorToolRoot.Instance.Scale;
            var offset = Utils.Adsorption(value, _stepValue);
            _prevPos = offset;
            DragEvent(DragState.DragEnd, offset);
        }
    }
}