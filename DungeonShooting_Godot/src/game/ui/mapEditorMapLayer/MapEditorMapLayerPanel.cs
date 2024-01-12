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
        
        _grid.Add(new LayerButtonData("地板", false, EditorTileMap.AutoFloorLayer));
        _grid.Add(new LayerButtonData("侧方墙壁", true, EditorTileMap.AutoMiddleLayer));
        _grid.Add(new LayerButtonData("顶部墙壁", true, EditorTileMap.AutoTopLayer));
        _grid.Add(new LayerButtonData("标记数据层", false, EditorTileMap.MarkLayer));
        _grid.SelectIndex = 0;
    }

    public override void OnDestroyUi()
    {
        _grid.Destroy();
    }

}
