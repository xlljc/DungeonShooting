using Godot;

namespace UI.MapEditorTools;

/// <summary>
/// 标记工具
/// </summary>
public partial class MarkTool : TextureRect, IUiNodeScript
{
    /// <summary>
    /// 绑定的数据
    /// </summary>
    public MarkInfo MarkInfo { get; private set; }
    private bool _enter;
    private bool _isMOve;
    private MapEditorTools.MarkTemplate _toolNode;
    private bool _isDown;
    private Vector2 _offset;
    
    public void SetUiNode(IUiNode uiNode)
    {
        _toolNode = (MapEditorTools.MarkTemplate)uiNode;
        _toolNode.Instance.MouseEntered += OnMouseEntered;
        _toolNode.Instance.MouseExited += OnMouseExited;
    }

    public override void _Process(double delta)
    {
        if (_toolNode != null && MarkInfo != null)
        {
            if (_enter)
            {
                if (_isDown)
                {
                    //松开鼠标
                    if (!Input.IsMouseButtonPressed(MouseButton.Left))
                    {
                        _isDown = false;
                        _isMOve = false;
                    }
                }
                else if (!_isDown)
                {
                    //按下鼠标
                    if (Input.IsMouseButtonPressed(MouseButton.Left))
                    {
                        _isDown = true;
                        if (_toolNode.UiPanel.ActiveMark != this)
                        {
                            _isMOve = false;
                            _toolNode.UiPanel.SetActiveMark(this);
                        }
                        else
                        {
                            _offset = GlobalPosition - GetGlobalMousePosition();
                            _isMOve = true;
                        }
                    }
                }
            }
            
            //移动中
            if (_isMOve && _toolNode.UiPanel.ActiveMark == this)
            {
                GlobalPosition = _offset + GetGlobalMousePosition().Round();
                MarkInfo.Position = new SerializeVector2((Position + (Size / 2).Ceil()).Round());
            }

            QueueRedraw();
        }
    }
    
    public void InitData(MarkInfo markInfo)
    {
        MarkInfo = markInfo;
        Position = markInfo.Position.AsVector2() - (Size / 2).Ceil();
    }
    
    private void OnMouseEntered()
    {
        _enter = true;
    }

    private void OnMouseExited()
    {
        _enter = false;
    }

    public override void _Draw()
    {
        if (MarkInfo != null && MarkInfo.Size.X != 0 && MarkInfo.Size.Y != 0)
        {
            var size = MarkInfo.Size.AsVector2();
            DrawRect(new Rect2(-size / 2 + Size / 2, size.X, size.Y), new Color(1, 1, 1, 0.3f), false, 1);
        }
    }
}