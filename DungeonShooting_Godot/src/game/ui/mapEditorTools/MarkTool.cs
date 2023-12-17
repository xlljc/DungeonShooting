using Godot;
using UI.MapEditor;

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
    
    /// <summary>
    /// 标记区域
    /// </summary>
    public MarkAreaTool MarkAreaTool { get; private set; }

    private bool _enter;
    private MapEditorTools.MarkTemplate _toolNode;
    private bool _isDown;
    private Vector2 _offset;
    private Vector2 _startPos;
    
    public void SetUiNode(IUiNode uiNode)
    {
        _toolNode = (MapEditorTools.MarkTemplate)uiNode;
        _toolNode.Instance.MouseEntered += OnMouseEntered;
        _toolNode.Instance.MouseExited += OnMouseExited;
        
        MarkAreaTool = new MarkAreaTool();
        MarkAreaTool.Position = Size / 2;
        MarkAreaTool.Visible = false;
        AddChild(MarkAreaTool);
    }

    public void OnDestroy()
    {
    }

    public override void _Process(double delta)
    {
        if (_toolNode != null && MarkInfo != null && _toolNode.UiPanel.S_ToolRoot.Instance.Visible && !DoorHoverArea.IsDrag)
        {
            if (_isDown)
            {
                //松开鼠标或者在拖拽区域
                if (!Input.IsMouseButtonPressed(MouseButton.Left) || MarkAreaTool.IsDrag)
                {
                    _isDown = false;
                    IsDrag = false;
                    //移动过, 就派发修改事件
                    var pos = GlobalPosition;
                    if (_startPos != pos)
                    {
                        _startPos = pos;
                        EventManager.EmitEvent(EventEnum.OnEditorDirty);
                    }
                }
            }
            else if (_enter && !_isDown)
            {
                //判断是否可以选中
                var activeMark = _toolNode.UiPanel.ActiveMark;
                if ((activeMark == null || (!activeMark.IsDrag && !activeMark.MarkAreaTool.IsDrag)) &&
                    !MarkAreaTool.IsDrag && Input.IsMouseButtonPressed(MouseButton.Left) &&
                    _toolNode.UiPanel.EditorMap.Instance.MouseType == EditorTileMap.MouseButtonType.Edit)
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
                        _startPos = GlobalPosition;
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
            if (_toolNode.UiPanel.ActiveMark == this)
            {
                MarkAreaTool.Visible = true;
            }
            else
            {
                MarkAreaTool.Visible = false;
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
        _startPos = GlobalPosition;
        MarkAreaTool.InitData(_toolNode.UiPanel.S_ToolRoot, this);
        //显示图标
        Texture = ResourceManager.GetMarkIcon(markInfo);
    }

    /// <summary>
    /// 刷新标记数据
    /// </summary>
    public void RefreshData()
    {
        //更新坐标
        Position = MarkInfo.Position.AsVector2();
        //更新icon
        Texture = ResourceManager.GetMarkIcon(MarkInfo);
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
        if (MarkInfo != null && !MarkAreaTool.Visible)
        {
            var size = new Vector2(MarkInfo.Size.X + 2, MarkInfo.Size.Y + 2);
            DrawRect(new Rect2(-size / 2 + Size / 2, size.X, size.Y), new Color(1, 1, 1, 0.7f), false, 1);
        }
    }

    /// <summary>
    /// 选中标记
    /// </summary>
    public void OnSelect()
    {
        var a = Modulate.A;
        Modulate = new Color(0, 1, 0, a);
    }

    /// <summary>
    /// 取消选中标记
    /// </summary>
    public void OnUnSelect()
    {
        var a = Modulate.A;
        Modulate = new Color(1, 1, 1, a);
    }

    /// <summary>
    /// 设置透明度值
    /// </summary>
    public void SetModulateAlpha(float a)
    {
        var m = Modulate;
        m.A = a;
        Modulate = m;
    }
}