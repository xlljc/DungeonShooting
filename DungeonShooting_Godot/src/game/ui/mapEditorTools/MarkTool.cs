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
                    }
                }
                else if (!_isMOve)
                {
                    if (Input.IsMouseButtonPressed(MouseButton.Left))
                    {
                        _isMOve = true;
                        _offset = GlobalPosition - GetGlobalMousePosition();
                    }
                }
            }

            //移动中
            if (_isMOve)
            {
                GlobalPosition = _offset + GetGlobalMousePosition().Round();
                _markInfo.Position = new SerializeVector2(Position.Round());
            }
            
            //QueueRedraw();
        }
    }
    
    public void InitData(MarkInfo markInfo)
    {
        _markInfo = markInfo;
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