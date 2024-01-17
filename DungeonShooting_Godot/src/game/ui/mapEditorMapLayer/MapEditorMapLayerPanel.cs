using System.Collections.Generic;
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

        _grid = CreateUiGrid<LayerButton, TileMapLayerData, LayerButtonCell>(S_LayerButton);
        _grid.SetCellOffset(new Vector2I(0, 2));
        _grid.SetHorizontalExpand(true);

        S_CheckButton.Instance.Toggled += OnToggled;
    }

    /// <summary>
    /// 初始化层级数据
    /// </summary>
    public void InitData()
    {
        _grid.Add(new TileMapLayerData("地板", MapLayer.AutoFloorLayer, false));
        _grid.Add(new TileMapLayerData("底层1", MapLayer.CustomFloorLayer1, false));
        _grid.Add(new TileMapLayerData("底层2", MapLayer.CustomFloorLayer2, false));
        _grid.Add(new TileMapLayerData("底层3", MapLayer.CustomFloorLayer3, false));
        _grid.Add(new TileMapLayerData("侧方墙壁", MapLayer.AutoMiddleLayer, true));
        _grid.Add(new TileMapLayerData("中层1", MapLayer.CustomMiddleLayer1, false));
        _grid.Add(new TileMapLayerData("中层2", MapLayer.CustomMiddleLayer2, false));
        _grid.Add(new TileMapLayerData("顶部墙壁", MapLayer.AutoTopLayer, true));
        _grid.Add(new TileMapLayerData("顶层", MapLayer.CustomTopLayer, false));
        _grid.Add(new TileMapLayerData("标记数据层", MapLayer.MarkLayer, true));
        _grid.SelectIndex = 0;
    }

    private void OnToggled(bool toggledon)
    {
        EditorTileMap.SetDesaltOtherLayer(toggledon);
    }
}
