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

    private bool _mouseHover;
    private bool _isDrag;
    private Control _parent;

    private MapEditorTools.DoorToolTemplate _dragArea;
    private MapEditorTools.HoverPrevRoot _cloneRoot;
    
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
            var globalPosition = _parent.GlobalPosition;
            var start = Utils.Adsorption(_parent.GetLocalMousePosition().X, GameConfig.TileCellSize);
            //MapEditorToolsPanel.S_HoverPrevRoot.Instance.Visible = true;
            //MapEditorToolsPanel.S_HoverPrevRoot.Instance.GlobalPosition = new Vector2();
            
            if (!_isDrag)
            {
                if (Input.IsMouseButtonPressed(MouseButton.Left))
                {
                    _isDrag = true;
                    _dragArea = MapEditorToolsPanel.CreateDoorTool(globalPosition, Direction, start, 4 * GameConfig.TileCellSize);
                }
            }
            else
            {
                if (!Input.IsMouseButtonPressed(MouseButton.Left))
                {
                    _isDrag = false;
                    MapEditorToolsPanel.RemoveDoorTool(_dragArea);
                    _dragArea = null;
                }
            }
        }
    }

    private void OnMouseEnter()
    {
        if (MapEditorToolsPanel.ActiveHoverArea == null || !MapEditorToolsPanel.ActiveHoverArea._isDrag)
        {
            _mouseHover = true;
            MapEditorToolsPanel.SetActiveHoverArea(this);
        }
    }
    
    private void OnMouseExit()
    {
        if (MapEditorToolsPanel.ActiveHoverArea == null || !MapEditorToolsPanel.ActiveHoverArea._isDrag)
        {
            _mouseHover = false;
            MapEditorToolsPanel.S_HoverPrevRoot.Instance.Visible = false;
        }
    }
}
