using Godot;
using Color = Godot.Color;

namespace UI.MapEditorTools;

public partial class MarkAreaTool : Node2D
{
    //鼠标悬停颜色
    private static Color _sideColor = new Color(1, 1, 1, 1);
    private static Color _sideHoverColor = new Color(0.65f, 0.65f, 0.65f, 1);
    private static Color _cornerColor = new Color(1, 1, 1, 1);
    private static Color _cornerHoverColor = new Color(0.65f, 0.65f, 0.65f, 1);
    
    //鼠标悬停状态
    private bool _mouseInL = false;
    private bool _mouseInR = false;
    private bool _mouseInT = false;
    private bool _mouseInB = false;
    private bool _mouseInLT = false;
    private bool _mouseInRT = false;
    private bool _mouseInLB = false;
    private bool _mouseInRB = false;

    private MarkInfo _markInfo;
    private MarkTool _markTool;
    private MapEditorTools.ToolRoot _toolRoot;
    
    /// <summary>
    /// 是否正在拖拽中
    /// </summary>
    public bool IsDrag { get; private set; } = false;
    private Vector2 _startMousePosition;
    private Vector2 _prevMousePosition;
    private Vector2 _startPosition;
    private float _startWidth;
    
    public void InitData(MapEditorTools.ToolRoot toolRoot, MarkTool markTool)
    {
        _toolRoot = toolRoot;
        _markTool = markTool;
        _markInfo = markTool.MarkInfo;
    }

    public override void _Process(double delta)
    {
        if (!Visible || _markInfo == null)
        {
            return;
        }

        if (IsDrag) //按下拖拽按钮
        {
            if (!Input.IsMouseButtonPressed(MouseButton.Left)) //松开拖拽
            {
                IsDrag = false;
            }
            else //拖拽中
            {
                var pos = GetGlobalMousePosition();
                if (pos != _prevMousePosition)
                {
                    if (_mouseInL)
                    {
                        var offset = GetGlobalMousePosition() - _startMousePosition;
                        offset = offset / _toolRoot.Instance.Scale;
                        var newWidth = Mathf.Max(1, (int)(_startWidth - offset.X));
                        _markInfo.Size = new SerializeVector2(newWidth, _markInfo.Size.Y);
                        var end = (int)(_startPosition.X + _startWidth / 2f);
                        var newX = (int)(end - newWidth / 2f);
                        _markTool.Position = new Vector2(newX, _markTool.Position.Y);
                        GD.Print("newWidth: " + newWidth);
                    }
                    _prevMousePosition = pos;
                }
            }
        }
        else //未被拖拽
        {
            _mouseInL = false;
            _mouseInR = false;
            _mouseInT = false;
            _mouseInB = false;
            _mouseInLT = false;
            _mouseInRT = false;
            _mouseInLB = false;
            _mouseInRB = false;

            var flag = false;
            var mousePosition = GetLocalMousePosition();
            //判断鼠标是否在点上
            if (Utils.IsPositionInRect(mousePosition, GetLeftTopRect()))
            {
                _mouseInLT = true;
                flag = true;
            }
            else if (Utils.IsPositionInRect(mousePosition, GetRightTopRect()))
            {
                _mouseInRT = true;
                flag = true;
            }
            else if (Utils.IsPositionInRect(mousePosition, GetLeftBottomRect()))
            {
                _mouseInLB = true;
                flag = true;
            }
            else if (Utils.IsPositionInRect(mousePosition, GetRightBottomRect()))
            {
                _mouseInRB = true;
                flag = true;
            }
            else if (Utils.IsPositionInRect(mousePosition, GetLeftRect()))
            {
                _mouseInL = true;
                flag = true;
            }
            else if (Utils.IsPositionInRect(mousePosition, GetRightRect()))
            {
                _mouseInR = true;
                flag = true;
            }
            else if (Utils.IsPositionInRect(mousePosition, GetTopRect()))
            {
                _mouseInT = true;
                flag = true;
            }
            else if (Utils.IsPositionInRect(mousePosition, GetBottomRect()))
            {
                _mouseInB = true;
                flag = true;
            }

            if (flag)
            {
                if (Input.IsMouseButtonPressed(MouseButton.Left))
                {
                    IsDrag = true;
                    _startMousePosition = GetGlobalMousePosition();
                    _prevMousePosition = _startMousePosition;
                    _startPosition = _markTool.Position;
                    _startWidth = _markInfo.Size.X;
                }
            }
        }

        if (Visible)
        {
            QueueRedraw();
        }
    }

    public override void _Draw()
    {
        //绘制边框
        DrawRect(GetTopRect(), _mouseInT ? _sideHoverColor : _sideColor);
        DrawRect(GetBottomRect(), _mouseInB ? _sideHoverColor : _sideColor);
        DrawRect(GetLeftRect(), _mouseInL ? _sideHoverColor : _sideColor);
        DrawRect(GetRightRect(), _mouseInR ? _sideHoverColor : _sideColor);
        //绘制角
        DrawRect(GetLeftTopRect(), _mouseInLT ? _cornerHoverColor : _cornerColor);
        DrawRect(GetLeftBottomRect(), _mouseInLB ? _cornerHoverColor : _cornerColor);
        DrawRect(GetRightTopRect(), _mouseInRT ? _cornerHoverColor : _cornerColor);
        DrawRect(GetRightBottomRect(), _mouseInRB ? _cornerHoverColor : _cornerColor);
    }

    private Rect2 GetTopRect()
    {
        return new Rect2(-_markInfo.Size.X / 2f + 0.5f, -_markInfo.Size.Y / 2f - 0.5f, _markInfo.Size.X - 1, 1);
    }
    
    private Rect2 GetBottomRect()
    {
        return new Rect2(-_markInfo.Size.X / 2f + 0.5f, _markInfo.Size.Y / 2f - 0.5f, _markInfo.Size.X - 1, 1);
    }

    private Rect2 GetLeftRect()
    {
        return new Rect2(-_markInfo.Size.X / 2f - 0.5f, -_markInfo.Size.Y / 2f + 0.5f, 1, _markInfo.Size.Y - 1);
    }
    
    private Rect2 GetRightRect()
    {
        return new Rect2(_markInfo.Size.X / 2f - 0.5f, -_markInfo.Size.Y / 2f + 0.5f, 1, _markInfo.Size.Y - 1);
    }

    private Rect2 GetLeftTopRect()
    {
        return new Rect2(-_markInfo.Size.X / 2f - 1.5f, -_markInfo.Size.Y / 2f - 1.5f, 3, 3);
    }
    
    private Rect2 GetLeftBottomRect()
    {
        return new Rect2(-_markInfo.Size.X / 2f - 1.5f, _markInfo.Size.Y / 2f - 1.5f, 3, 3);
    }
    
    private Rect2 GetRightTopRect()
    {
        return new Rect2(_markInfo.Size.X / 2f - 1.5f, -_markInfo.Size.Y / 2f - 1.5f, 3, 3);
    }
    
    private Rect2 GetRightBottomRect()
    {
        return new Rect2(_markInfo.Size.X / 2f - 1.5f, _markInfo.Size.Y / 2f - 1.5f, 3, 3);
    }
}