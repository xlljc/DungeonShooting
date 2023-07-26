using Godot;
using UI.MapEditorTools;

public partial class DoorHoverArea : ColorRect
{
    /// <summary>
    /// 所属 Ui 对象
    /// </summary>
    public MapEditorToolsPanel MapEditorToolsPanel { get; private set; }

    /// <summary>
    /// 房间门的朝向
    /// </summary>
    public DoorDirection Direction { get; private set; }
    /// <summary>
    /// 是否拖拽中
    /// </summary>
    public bool IsDrag { get; private set; }

    private bool _mouseHover;
    private Control _parent;
    
    public override void _Ready()
    {
        _parent = GetParent<Control>();
        MouseEntered += OnMouseEnter;
        MouseExited += OnMouseExit;
    }

    public void Init(MapEditorToolsPanel panel, DoorDirection direction)
    {
        MapEditorToolsPanel = panel;
        Direction = direction;
    }

    public override void _Process(double delta)
    {
        if (_mouseHover && MapEditorToolsPanel.ActiveHoverArea == this)
        {
            var start = Utils.Adsorption(_parent.GetLocalMousePosition().X, GameConfig.TileCellSize);
            MapEditorToolsPanel.S_HoverPreviewRoot.Instance.Position = new Vector2(start, 0);
            
            if (!IsDrag)
            {
                if (Input.IsMouseButtonPressed(MouseButton.Left))
                {
                    GD.Print("开始...");
                    IsDrag = true;
                    MapEditorToolsPanel.CreateDragDoorTool(_parent.Position, Direction, start, OnSubmitDoorArea);
                }
            }
            else
            {
                if (!Input.IsMouseButtonPressed(MouseButton.Left))
                {
                    GD.Print("结束...");
                    IsDrag = false;
                }
            }
        }
    }

    //提交门区域
    private void OnSubmitDoorArea(int start, int end)
    {
        
    }
    
    private void OnMouseEnter()
    {
        if (MapEditorToolsPanel.ActiveHoverArea == null || !MapEditorToolsPanel.ActiveHoverArea.IsDrag)
        {
            _mouseHover = true;
            MapEditorToolsPanel.SetActiveHoverArea(this);
        }
    }
    
    private void OnMouseExit()
    {
        if (MapEditorToolsPanel.ActiveHoverArea == null || !MapEditorToolsPanel.ActiveHoverArea.IsDrag)
        {
            _mouseHover = false;
            if (MapEditorToolsPanel.ActiveHoverArea == this)
            {
                MapEditorToolsPanel.SetActiveHoverArea(null);
            }
        }
    }
}
