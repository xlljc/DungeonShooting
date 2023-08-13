using Godot;

namespace UI.MapEditorTools;

/// <summary>
/// 标记工具
/// </summary>
public partial class MarkTool : TextureRect, IUiNodeScript
{
    private MapEditorTools.MarkTemplate _toolNode;
    private MarkInfo _markInfo;
    private bool _enter;
    private bool _isMOve;
    private Vector2 _offset;
    
    public void SetUiNode(IUiNode uiNode)
    {
        _toolNode = (MapEditorTools.MarkTemplate)uiNode;
        _toolNode.Instance.MouseEntered += OnMouseEntered;
        _toolNode.Instance.MouseExited += OnMouseExited;
    }

    public override void _Process(double delta)
    {
        if (_toolNode != null && _markInfo != null)
        {
            //鼠标在节点内
            if (_enter)
            {
                if (_isMOve)
                {
                    if (!Input.IsMouseButtonPressed(MouseButton.Left))
                    {
                        _isMOve = false;
                        if (_toolNode.UiPanel.ActiveMark == this)
                        {
                            _toolNode.UiPanel.SetActiveMark(null);
                        }
                    }
                }
                else if (!_isMOve)
                {
                    if (Input.IsMouseButtonPressed(MouseButton.Left))
                    {
                        _isMOve = true;
                        _offset = GlobalPosition - GetGlobalMousePosition();
                        if (_toolNode.UiPanel.ActiveMark == null)
                        {
                            _toolNode.UiPanel.SetActiveMark(this);
                        }
                    }
                }
            }

            //移动中
            if (_isMOve && _toolNode.UiPanel.ActiveMark == this)
            {
                GlobalPosition = _offset + GetGlobalMousePosition().Round();
                _markInfo.Position = new SerializeVector2((Position + (Size / 2).Ceil()).Round());
            }
            
            //QueueRedraw();
        }
    }
    
    public void InitData(MarkInfo markInfo)
    {
        _markInfo = markInfo;
        Position = markInfo.Position.AsVector2() - (Size / 2).Ceil();
    }
    
    private void OnMouseEntered()
    {
        _enter = true;
    }

    private void OnMouseExited()
    {
        if (!Input.IsMouseButtonPressed(MouseButton.Left))
        {
            _enter = false;
        }
    }

    // public override void _Draw()
    // {
    //     if (_markInfo != null && _markInfo.Size.X != 0 && _markInfo.Size.Y != 0)
    //     {
    //         
    //     }
    // }
}