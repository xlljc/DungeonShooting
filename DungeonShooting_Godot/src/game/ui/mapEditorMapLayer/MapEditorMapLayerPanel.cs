using Godot;
using UI.MapEditor;

namespace UI.MapEditorMapLayer;

public partial class MapEditorMapLayerPanel : MapEditorMapLayer
{
    private UiGrid<LayerButton, TileMapLayerData> _grid;
    
    public override void OnCreateUi()
    {
        //var mapEditorMapTilePanel = ((MapEditorPanel)ParentUi).S_MapEditorMapTile.Instance;

        S_AddButton.Instance.Pressed += OnAddClick;
        S_EditButton.Instance.Pressed += OnEditClick;
        S_DeleteButton.Instance.Pressed += OnDeleteClick;
        
        _grid = new UiGrid<LayerButton, TileMapLayerData>(S_LayerButton, typeof(LayerButtonCell));
        _grid.SetCellOffset(new Vector2I(0, 2));
        _grid.SetHorizontalExpand(true);
        
        _grid.Add(new TileMapLayerData("地板", EditorTileMap.AutoFloorLayer, false));
        _grid.Add(new TileMapLayerData("侧方墙壁", EditorTileMap.AutoMiddleLayer, true));
        _grid.Add(new TileMapLayerData("顶部墙壁", EditorTileMap.AutoTopLayer, true));
        _grid.Add(new TileMapLayerData("标记数据层", EditorTileMap.MarkLayer, false));
        _grid.SelectIndex = 0;
    }

    public override void OnDestroyUi()
    {
        _grid.Destroy();
    }

    private void OnAddClick()
    {
        EditorWindowManager.ShowCreateCustomLayer(null, info =>
        {
            
        });
    }
    
    private void OnEditClick()
    {
        
    }
    
    private void OnDeleteClick()
    {
        
    }
}
