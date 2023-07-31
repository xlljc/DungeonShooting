using Godot;

namespace UI.MapEditor;

public class EditorLayerBar
{
    public class LayerButtonData
    {
        /// <summary>
        /// 显示文本
        /// </summary>
        public string Title;
        /// <summary>
        /// 是否锁定
        /// </summary>
        public bool IsLock;
        /// <summary>
        /// Map层级
        /// </summary>
        public int Layer;

        public LayerButtonData(string title, bool isLock, int layer)
        {
            Title = title;
            IsLock = isLock;
            Layer = layer;
        }
    }
    
    private readonly MapEditorPanel _mapEditorPanel;
    private readonly MapEditor.MapLayer _mapLayer;
    private readonly UiGrid<MapEditor.LayerButton, LayerButtonData> _grid;

    public EditorLayerBar(MapEditorPanel mapEditorPanel, MapEditor.MapLayer mapLayer)
    {
        _mapEditorPanel = mapEditorPanel;
        _mapLayer = mapLayer;
        _grid = new UiGrid<MapEditor.LayerButton, LayerButtonData>(mapLayer.L_VBoxContainer.L_ScrollContainer.L_LayerButton, typeof(LayerButtonCell));
        _grid.SetCellOffset(new Vector2I(0, 2));
        _grid.SetHorizontalExpand(true);
        
        _grid.Add(new LayerButtonData("地面", false, EditorTileMap.AutoFloorLayer));
        _grid.Add(new LayerButtonData("自定义底层", false, EditorTileMap.CustomFloorLayer));
        _grid.Add(new LayerButtonData("中层自动图块", true, EditorTileMap.AutoMiddleLayer));
        _grid.Add(new LayerButtonData("自定义中层", false, EditorTileMap.CustomMiddleLayer));
        _grid.Add(new LayerButtonData("高层自动图块", true, EditorTileMap.AutoTopLayer));
        _grid.Add(new LayerButtonData("自定义高层", false, EditorTileMap.CustomTopLayer));
    }

    public void OnShow()
    {
        
    }

    public void OnHide()
    {
        
    }

    public void OnDestroy()
    {
        _grid.Destroy();
    }

    public void Process(float delta)
    {
        
    }
}