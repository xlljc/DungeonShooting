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
    
    /// <summary>
    /// 是否拖拽中
    /// </summary>
    public bool IsDrag { get; private set; }
    
    private bool _enter;
    private MapEditorTools.MarkTemplate _toolNode;
    private bool _isDown;
    private Vector2 _offset;
    private MarkAreaTool _markAreaToolUp;
    
    public void SetUiNode(IUiNode uiNode)
    {
        _toolNode = (MapEditorTools.MarkTemplate)uiNode;
        _toolNode.Instance.MouseEntered += OnMouseEntered;
        _toolNode.Instance.MouseExited += OnMouseExited;
        
        _markAreaToolUp = new MarkAreaTool();
        _markAreaToolUp.Position = Size / 2;
        _markAreaToolUp.Visible = false;
        AddChild(_markAreaToolUp);
    }

    public override void _Process(double delta)
    {
        if (_toolNode != null && MarkInfo != null)
        {
            if (_enter)
            {
                if (_isDown)
                {
                    //松开鼠标或者在拖拽区域
                    if (!Input.IsMouseButtonPressed(MouseButton.Left) || _markAreaToolUp.IsDrag)
                    {
                        _isDown = false;
                        IsDrag = false;
                    }
                }
                else if (!_isDown)
                {
                    //按下鼠标
                    if (!_markAreaToolUp.IsDrag && Input.IsMouseButtonPressed(MouseButton.Left))
                    {
                        _isDown = true;
                        if (_toolNode.UiPanel.ActiveMark != this)
                        {
                            IsDrag = false;
                            _toolNode.UiPanel.SetActiveMark(this);
                        }
                        else
                        {
                            _offset = GlobalPosition - GetGlobalMousePosition();
                            IsDrag = true;
                        }
                    }
                }
            }
            
            //拖拽中
            if (IsDrag && _toolNode.UiPanel.ActiveMark == this)
            {
                GlobalPosition = _offset + GetGlobalMousePosition().Round();
                MarkInfo.Position = new SerializeVector2((Position + (Size / 2).Ceil()).Round());
            }

            //只有选中物体才会显示拖拽区域
            if (_toolNode.UiPanel.ActiveMark == this && MarkInfo.Size.X > 0 && MarkInfo.Size.Y > 0)
            {
                _markAreaToolUp.Visible = true;
            }
            else
            {
                _markAreaToolUp.Visible = false;
            }
            QueueRedraw();
        }
    }
    
    /// <summary>
    /// 初始化数据
    /// </summary>
    public void InitData(MarkInfo markInfo)
    {
        MarkInfo = markInfo;
        Position = markInfo.Position.AsVector2() - (Size / 2).Ceil();
        _markAreaToolUp.InitData(_toolNode.UiPanel.S_ToolRoot, this);
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
        if (MarkInfo != null && MarkInfo.Size.X > 0 && MarkInfo.Size.Y > 0 && !_markAreaToolUp.Visible)
        {
            var size = MarkInfo.Size.AsVector2();
            DrawRect(new Rect2(-size / 2 + Size / 2, size.X, size.Y), new Color(1, 1, 1, 0.3f), false, 1);
        }
    }
}