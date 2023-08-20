using Godot;
using UI.MapEditor;
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
    //是否绘制角
    private bool _showCornerBlock = false;

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
    private float _startHeight;
    
    public void InitData(MapEditorTools.ToolRoot toolRoot, MarkTool markTool)
    {
        _toolRoot = toolRoot;
        _markTool = markTool;
        _markInfo = markTool.MarkInfo;
    }

    public override void _Process(double delta)
    {
        if (!Visible || _markInfo == null || _toolRoot.UiPanel.IsOpenPopUps || !_toolRoot.UiPanel.S_ToolRoot.Instance.Visible)
        {
            return;
        }

        _showCornerBlock = _markInfo.Size.X >= 4 && _markInfo.Size.Y >= 4;
        var globalMousePosition = GetGlobalMousePosition();
        if (IsDrag) //按下拖拽按钮
        {
            if (!Input.IsMouseButtonPressed(MouseButton.Left)) //松开拖拽
            {
                IsDrag = false;
            }
            else //拖拽中
            {
                var pos = globalMousePosition;
                if (pos != _prevMousePosition)
                {

                    if (_mouseInL)
                    {
                        var offset = globalMousePosition - _startMousePosition;
                        offset = offset / _toolRoot.Instance.Scale * 2f;
                        var newWidth = Mathf.Max(0, (int)(_startWidth - offset.X));
                        _markInfo.Size = new SerializeVector2(newWidth, _markInfo.Size.Y);
                    }
                    else if (_mouseInR)
                    {
                        var offset = _startMousePosition - globalMousePosition;
                        offset = offset / _toolRoot.Instance.Scale * 2f;
                        var newWidth = Mathf.Max(0, (int)(_startWidth - offset.X));
                        _markInfo.Size = new SerializeVector2(newWidth, _markInfo.Size.Y);
                    }
                    else if (_mouseInT)
                    {
                        var offset = globalMousePosition - _startMousePosition;
                        offset = offset / _toolRoot.Instance.Scale * 2f;
                        var newHeight = Mathf.Max(0, (int)(_startHeight - offset.Y));
                        _markInfo.Size = new SerializeVector2(_markInfo.Size.X, newHeight);
                    }
                    else if (_mouseInB)
                    {
                        var offset = _startMousePosition - globalMousePosition;
                        offset = offset / _toolRoot.Instance.Scale * 2f;
                        var newHeight = Mathf.Max(0, (int)(_startHeight - offset.Y));
                        _markInfo.Size = new SerializeVector2(_markInfo.Size.X, newHeight);
                    }
                    //----------------------------------------------------------------------------------
                    else if (_mouseInLT)
                    {
                        var offset = globalMousePosition - _startMousePosition;
                        offset = offset / _toolRoot.Instance.Scale * 2f;
                        var newWidth = Mathf.Max(0, (int)(_startWidth - offset.X));
                        var newHeight = Mathf.Max(0, (int)(_startHeight - offset.Y));
                        _markInfo.Size = new SerializeVector2(newWidth, newHeight);
                    }
                    else if (_mouseInLB)
                    {
                        var offsetX = (globalMousePosition.X - _startMousePosition.X) / _toolRoot.Instance.Scale.X * 2f;
                        var offsetY = (_startMousePosition.Y - globalMousePosition.Y) / _toolRoot.Instance.Scale.Y * 2f;
                        var newWidth = Mathf.Max(0, (int)(_startWidth - offsetX));
                        var newHeight = Mathf.Max(0, (int)(_startHeight - offsetY));
                        _markInfo.Size = new SerializeVector2(newWidth, newHeight);
                    }
                    else if (_mouseInRT)
                    {
                        var offsetX = (_startMousePosition.X - globalMousePosition.X) / _toolRoot.Instance.Scale.X * 2f;
                        var offsetY = (globalMousePosition.Y - _startMousePosition.Y) / _toolRoot.Instance.Scale.Y * 2f;
                        var newWidth = Mathf.Max(0, (int)(_startWidth - offsetX));
                        var newHeight = Mathf.Max(0, (int)(_startHeight - offsetY));
                        _markInfo.Size = new SerializeVector2(newWidth, newHeight);
                    }
                    else if (_mouseInRB)
                    {
                        var offset = _startMousePosition - globalMousePosition;
                        offset = offset / _toolRoot.Instance.Scale * 2f;
                        var newWidth = Mathf.Max(0, (int)(_startWidth - offset.X));
                        var newHeight = Mathf.Max(0, (int)(_startHeight - offset.Y));
                        _markInfo.Size = new SerializeVector2(newWidth, newHeight);
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
            //必须要选中
            if (_toolRoot.UiPanel.EditorMap.Instance.MouseType == EditorTileMap.MouseButtonType.Edit)
            {
                var mousePosition = GetLocalMousePosition();
                //判断鼠标是否在点上
                if (_showCornerBlock && Utils.IsPositionInRect(mousePosition, GetLeftTopRect()))
                {
                    _mouseInLT = true;
                    flag = true;
                }
                else if (_showCornerBlock && Utils.IsPositionInRect(mousePosition, GetRightTopRect()))
                {
                    _mouseInRT = true;
                    flag = true;
                }
                else if (_showCornerBlock && Utils.IsPositionInRect(mousePosition, GetLeftBottomRect()))
                {
                    _mouseInLB = true;
                    flag = true;
                }
                else if (_showCornerBlock && Utils.IsPositionInRect(mousePosition, GetRightBottomRect()))
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
            }

            if (flag)
            {
                if (Input.IsMouseButtonPressed(MouseButton.Left))
                {
                    IsDrag = true;
                    _startMousePosition = globalMousePosition;
                    _prevMousePosition = _startMousePosition;
                    _startPosition = _markTool.Position;
                    _startWidth = _markInfo.Size.X;
                    _startHeight = _markInfo.Size.Y;
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
        if (_showCornerBlock)
        {
            //绘制角
            DrawRect(GetLeftTopRect(), _mouseInLT ? _cornerHoverColor : _cornerColor);
            DrawRect(GetLeftBottomRect(), _mouseInLB ? _cornerHoverColor : _cornerColor);
            DrawRect(GetRightTopRect(), _mouseInRT ? _cornerHoverColor : _cornerColor);
            DrawRect(GetRightBottomRect(), _mouseInRB ? _cornerHoverColor : _cornerColor);
        }
    }

    private Rect2 GetTopRect()
    {
        return new Rect2(-(_markInfo.Size.X + 2) / 2f + 0.5f, -(_markInfo.Size.Y + 2) / 2f - 0.5f, (_markInfo.Size.X + 2) - 1, 1);
    }
    
    private Rect2 GetBottomRect()
    {
        return new Rect2(-(_markInfo.Size.X + 2) / 2f + 0.5f, (_markInfo.Size.Y + 2) / 2f - 0.5f, (_markInfo.Size.X + 2) - 1, 1);
    }

    private Rect2 GetLeftRect()
    {
        return new Rect2(-(_markInfo.Size.X + 2) / 2f - 0.5f, -(_markInfo.Size.Y + 2) / 2f + 0.5f, 1, (_markInfo.Size.Y + 2) - 1);
    }
    
    private Rect2 GetRightRect()
    {
        return new Rect2((_markInfo.Size.X + 2) / 2f - 0.5f, -(_markInfo.Size.Y + 2) / 2f + 0.5f, 1, (_markInfo.Size.Y + 2) - 1);
    }

    private Rect2 GetLeftTopRect()
    {
        return new Rect2(-(_markInfo.Size.X + 2) / 2f - 1.5f, -(_markInfo.Size.Y + 2) / 2f - 1.5f, 3, 3);
    }
    
    private Rect2 GetLeftBottomRect()
    {
        return new Rect2(-(_markInfo.Size.X + 2) / 2f - 1.5f, (_markInfo.Size.Y + 2) / 2f - 1.5f, 3, 3);
    }
    
    private Rect2 GetRightTopRect()
    {
        return new Rect2((_markInfo.Size.X + 2) / 2f - 1.5f, -(_markInfo.Size.Y + 2) / 2f - 1.5f, 3, 3);
    }
    
    private Rect2 GetRightBottomRect()
    {
        return new Rect2((_markInfo.Size.X + 2) / 2f - 1.5f, (_markInfo.Size.Y + 2) / 2f - 1.5f, 3, 3);
    }
}