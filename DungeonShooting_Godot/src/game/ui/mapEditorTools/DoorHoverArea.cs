using Godot;
using UI.MapEditorTools;

public partial class DoorHoverArea : ColorRect
{
    /// <summary>
    /// 是否拖拽中
    /// </summary>
    public static bool IsDrag { get; set; }
    
    /// <summary>
    /// 所属 Ui 对象
    /// </summary>
    public MapEditorToolsPanel MapEditorToolsPanel { get; private set; }

    /// <summary>
    /// 房间门的朝向
    /// </summary>
    public DoorDirection Direction { get; private set; }
    
    private bool _mouseHover;
    private Control _parent;
    //当前正在拖拽创建的 DoorToolTemplate
    private MapEditorTools.DoorToolTemplate _currToolTemplate;
    
    public override void _Ready()
    {
        _parent = GetParent<Control>();
        MouseEntered += OnMouseEnter;
        MouseExited += OnMouseExit;
    }

    public void Init(MapEditorToolsPanel panel, DoorDirection direction)
    {
        IsDrag = false;
        MapEditorToolsPanel = panel;
        Direction = direction;
    }

    public override void _Process(double delta)
    {
        if (_mouseHover && MapEditorToolsPanel.ActiveHoverArea == this)
        {
            if (!IsDrag)
            {
                var start = Utils.Adsorption(_parent.GetLocalMousePosition().X, GameConfig.TileCellSize);
                var previewRoot = MapEditorToolsPanel.S_HoverPreviewRoot.Instance;

                //检测是否会与其他门发生碰撞
                var canCreateDragArea = MapEditorToolsPanel.EditorMap.Instance.CheckDoorArea(Direction, start, start);
                if (canCreateDragArea)
                {
                    previewRoot.Visible = true;
                    previewRoot.Position = new Vector2(start, 0);

                    if (Input.IsMouseButtonPressed(MouseButton.Left))
                    {
                        IsDrag = true;
                        _currToolTemplate = MapEditorToolsPanel.CreateDragDoorTool(_parent.Position, Direction, start,
                            OnSubmitDoorArea, OnCancelDoorArea);
                    }
                }
                else //发生碰撞, 不能提交
                {
                    previewRoot.Visible = false;
                }
            }
        }
    }

    //提交门区域
    private void OnSubmitDoorArea(DoorDirection direction, int start, int end)
    {
        IsDrag = false;
        GD.Print("提交区域: " + direction + ", " + start + ", " + end);
        _currToolTemplate.Instance.DoorAreaInfo = MapEditorToolsPanel.EditorMap.Instance.CreateDoorArea(direction, start, end);
    }

    //取消提交
    private void OnCancelDoorArea()
    {
        IsDrag = false;
        _currToolTemplate = null;
    }

    private void OnMouseEnter()
    {
        if (MapEditorToolsPanel.ActiveHoverArea == null || !IsDrag)
        {
            _mouseHover = true;
            MapEditorToolsPanel.SetActiveHoverArea(this);
        }
    }
    
    private void OnMouseExit()
    {
        if (MapEditorToolsPanel.ActiveHoverArea == null || !IsDrag)
        {
            _mouseHover = false;
            if (MapEditorToolsPanel.ActiveHoverArea == this)
            {
                MapEditorToolsPanel.SetActiveHoverArea(null);
            }
        }
    }
}
