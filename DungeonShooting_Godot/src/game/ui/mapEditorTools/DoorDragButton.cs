using System;
using Godot;

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
    
    public override void _Ready()
    {
        _parent = GetParent<DoorDragArea>();
        ButtonDown += OnButtonDown;
        ButtonUp += OnButtonUp;
    }

    public override void _Process(double delta)
    {
        if (_down)
        {
            if (DragEvent != null)
            {
                var offset = Utils.Adsorption((GetGlobalMousePosition() - _startPos) / _parent.Scale, _stepValue);
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
        _down = true;
        Modulate = new Color(0.7f, 0.7f, 0.7f, 1);
        _startPos = GetGlobalMousePosition();
        _prevPos = Vector2.Zero;
        if (DragEvent != null)
        {
            DragEvent(DragState.DragStart, Vector2.Zero);
        }
    }

    private void OnButtonUp()
    {
        _down = false;
        Modulate = new Color(1, 1, 1, 1);
        if (DragEvent != null)
        {
            var offset = Utils.Adsorption((GetGlobalMousePosition() - _startPos) / _parent.Scale, _stepValue);
            _prevPos = offset;
            DragEvent(DragState.DragEnd, offset);
        }
    }
}