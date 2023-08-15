using System.Drawing;
using Godot;
using Color = Godot.Color;

namespace UI.MapEditorTools;

public partial class MarkAreaTool : Node2D
{
    /// <summary>
    /// 宽度
    /// </summary>
    public int Width { get; private set; }
    /// <summary>
    /// 高度
    /// </summary>
    public int Height { get; private set; }

    private static Color _sideColor = new Color(0, 0, 0, 0);
    private static Color _sideHoverColor = new Color(0, 0, 0, 0.3f);
    private static Color _cornerColor = new Color(1, 1, 1, 1);
    private static Color _cornerHoverColor = new Color(0.65f, 0.65f, 0.65f, 1);
    
    private bool _mouseInL = false;
    private bool _mouseInR = false;
    private bool _mouseInT = false;
    private bool _mouseInB = false;
    private bool _mouseInLT = false;
    private bool _mouseInRT = false;
    private bool _mouseInLB = false;
    private bool _mouseInRB = false;
    
    public void SetSize(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public override void _Process(double delta)
    {
        if (!Visible)
        {
            return;
        }
        
        _mouseInL = false;
        _mouseInR = false;
        _mouseInT = false;
        _mouseInB = false;
        _mouseInLT = false;
        _mouseInRT = false;
        _mouseInLB = false;
        _mouseInRB = false;

        var mousePosition = GetLocalMousePosition();
        //判断鼠标是否在点上
        if (Utils.IsPositionInRect(mousePosition, GetLeftTopRect()))
        {
            _mouseInLT = true;
        }
        else if (Utils.IsPositionInRect(mousePosition, GetRightTopRect()))
        {
            _mouseInRT = true;
        }
        else if (Utils.IsPositionInRect(mousePosition, GetLeftBottomRect()))
        {
            _mouseInLB = true;
        }
        else if (Utils.IsPositionInRect(mousePosition, GetRightBottomRect()))
        {
            _mouseInRB = true;
        }
        else if (Utils.IsPositionInRect(mousePosition, GetLeftRect()))
        {
            _mouseInL = true;
        }
        else if (Utils.IsPositionInRect(mousePosition, GetRightRect()))
        {
            _mouseInR = true;
        }
        else if (Utils.IsPositionInRect(mousePosition, GetTopRect()))
        {
            _mouseInT = true;
        }
        else if (Utils.IsPositionInRect(mousePosition, GetBottomRect()))
        {
            _mouseInB = true;
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
        return new Rect2(-Width / 2f + 0.5f, -Height / 2f - 0.5f, Width - 1, 1);
    }
    
    private Rect2 GetBottomRect()
    {
        return new Rect2(-Width / 2f + 0.5f, Height / 2f - 0.5f, Width - 1, 1);
    }

    private Rect2 GetLeftRect()
    {
        return new Rect2(-Width / 2f - 0.5f, -Height / 2f + 0.5f, 1, Height - 1);
    }
    
    private Rect2 GetRightRect()
    {
        return new Rect2(Width / 2f - 0.5f, -Height / 2f + 0.5f, 1, Height - 1);
    }

    private Rect2 GetLeftTopRect()
    {
        return new Rect2(-Width / 2f - 1.5f, -Height / 2f - 1.5f, 3, 3);
    }
    
    private Rect2 GetLeftBottomRect()
    {
        return new Rect2(-Width / 2f - 1.5f, Height / 2f - 1.5f, 3, 3);
    }
    
    private Rect2 GetRightTopRect()
    {
        return new Rect2(Width / 2f - 1.5f, -Height / 2f - 1.5f, 3, 3);
    }
    
    private Rect2 GetRightBottomRect()
    {
        return new Rect2(Width / 2f - 1.5f, Height / 2f - 1.5f, 3, 3);
    }
}