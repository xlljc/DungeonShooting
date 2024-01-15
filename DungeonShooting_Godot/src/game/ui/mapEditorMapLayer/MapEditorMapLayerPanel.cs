using Godot;
using UI.MapEditor;

namespace UI.MapEditorMapLayer;

public partial class MapEditorMapLayerPanel : MapEditorMapLayer
{
    private UiGrid<LayerButton, TileMapLayerData> _grid;
    
    /// <summary>
    /// 编辑器Tile对象
    /// </summary>
    public EditorTileMap EditorTileMap { get; private set; }
    
    public override void OnCreateUi()
    {
        var editorPanel = (MapEditorPanel)ParentUi;
        EditorTileMap = editorPanel.S_TileMap.Instance;

        S_AddButton.Instance.Pressed += OnAddClick;
        S_EditButton.Instance.Pressed += OnEditClick;
        S_DeleteButton.Instance.Pressed += OnDeleteClick;

        _grid = CreateUiGrid<LayerButton, TileMapLayerData, LayerButtonCell>(S_LayerButton);
        _grid.SetCellOffset(new Vector2I(0, 2));
        _grid.SetHorizontalExpand(true);
        
        _grid.Add(new TileMapLayerData("地板", GameConfig.FloorMapLayer, GameConfig.FloorMapZIndex, false));
        _grid.Add(new TileMapLayerData("侧方墙壁", GameConfig.MiddleMapLayer, GameConfig.MiddleMapZIndex, true));
        _grid.Add(new TileMapLayerData("顶部墙壁", GameConfig.TopMapLayer, GameConfig.TopMapZindex, true));
        _grid.Add(new TileMapLayerData("标记数据层", GameConfig.MarkLayer, 0, false));
        _grid.SelectIndex = 0;
    }

    private void OnAddClick()
    {
        EditorWindowManager.ShowCreateCustomLayer(EditorTileMap.CurrRoomSplit.TileInfo.CustomLayers, info =>
        {
            _grid.Add(new TileMapLayerData(0, info), true);
            _grid.Sort();
        });
    }
    
    private void OnEditClick()
    {
        
    }
    
    private void OnDeleteClick()
    {
        
    }
}
