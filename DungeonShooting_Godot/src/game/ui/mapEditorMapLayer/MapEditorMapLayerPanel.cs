using System.Collections.Generic;
using Godot;
using UI.MapEditor;

namespace UI.MapEditorMapLayer;

public partial class MapEditorMapLayerPanel : MapEditorMapLayer
{
    /// <summary>
    /// 所有地图层级网格
    /// </summary>
    public UiGrid<LayerButton, TileMapLayerData> LayerGrid { get; private set; }
    
    /// <summary>
    /// 编辑器Tile对象
    /// </summary>
    public EditorTileMap EditorTileMap { get; private set; }
    
    public override void OnCreateUi()
    {
        var editorPanel = (MapEditorPanel)ParentUi;
        EditorTileMap = editorPanel.S_TileMap.Instance;

        LayerGrid = CreateUiGrid<LayerButton, TileMapLayerData, LayerButtonCell>(S_LayerButton);
        LayerGrid.SetCellOffset(new Vector2I(0, 2));
        LayerGrid.SetHorizontalExpand(true);

        S_CheckButton.Instance.Toggled += OnToggled;
    }

    /// <summary>
    /// 初始化层级数据
    /// </summary>
    public void InitData()
    {
        LayerGrid.Add(new TileMapLayerData("地板", MapLayer.AutoFloorLayer, false));
        LayerGrid.Add(new TileMapLayerData("底层1", MapLayer.CustomFloorLayer1, false));
        LayerGrid.Add(new TileMapLayerData("底层2", MapLayer.CustomFloorLayer2, false));
        LayerGrid.Add(new TileMapLayerData("底层3", MapLayer.CustomFloorLayer3, false));
        LayerGrid.Add(new TileMapLayerData("侧方墙壁", MapLayer.AutoMiddleLayer, true));
        LayerGrid.Add(new TileMapLayerData("中层1", MapLayer.CustomMiddleLayer1, false));
        LayerGrid.Add(new TileMapLayerData("中层2", MapLayer.CustomMiddleLayer2, false));
        LayerGrid.Add(new TileMapLayerData("顶部墙壁", MapLayer.AutoTopLayer, true));
        LayerGrid.Add(new TileMapLayerData("顶层", MapLayer.CustomTopLayer, false));
        LayerGrid.Add(new TileMapLayerData("标记数据层", MapLayer.MarkLayer, true));
        LayerGrid.SelectIndex = 0;
    }

    /// <summary>
    /// 设置指定层级显示或者隐藏
    /// </summary>
    public void SetLayerVisible(int layer, bool visible)
    {
        LayerGrid.ForEach(cell =>
        {
            ((LayerButtonCell)cell).SetLayerVisible(visible);
        });
    }

    private void OnToggled(bool toggledon)
    {
        EditorTileMap.SetDesaltOtherLayer(toggledon);
    }
}
