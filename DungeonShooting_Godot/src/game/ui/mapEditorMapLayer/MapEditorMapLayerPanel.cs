using Godot;
using UI.MapEditor;

namespace UI.MapEditorMapLayer;

public partial class MapEditorMapLayerPanel : MapEditorMapLayer
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
    
    private UiGrid<LayerButton, LayerButtonData> _grid;
    
    public override void OnCreateUi()
    {
        _grid = new UiGrid<LayerButton, LayerButtonData>(S_LayerButton, typeof(LayerButtonCell));
        _grid.SetCellOffset(new Vector2I(0, 2));
        _grid.SetHorizontalExpand(true);
        
        _grid.Add(new LayerButtonData("地面", false, EditorTileMap.AutoFloorLayer));
        _grid.Add(new LayerButtonData("自定义底层", false, EditorTileMap.CustomFloorLayer));
        _grid.Add(new LayerButtonData("中层自动图块", true, EditorTileMap.AutoMiddleLayer));
        _grid.Add(new LayerButtonData("自定义中层", false, EditorTileMap.CustomMiddleLayer));
        _grid.Add(new LayerButtonData("高层自动图块", true, EditorTileMap.AutoTopLayer));
        _grid.Add(new LayerButtonData("自定义高层", false, EditorTileMap.CustomTopLayer));
        _grid.Add(new LayerButtonData("标记数据层", false, EditorTileMap.MarkLayer));
    }

    public override void OnDestroyUi()
    {
        _grid.Destroy();
    }

}
