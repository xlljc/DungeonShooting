
using Godot;

public static class MapLayerManager
{
    public static void InitMapLayer(TileMap tileMap)
    {
        //删除之前的层级
        var layersCount = tileMap.GetLayersCount();
        for (var i = layersCount - 1; i > 0; i--)
        {
            tileMap.RemoveLayer(layersCount);
        }
        tileMap.AddLayer(MapLayer.AutoFloorLayer);
        tileMap.SetLayerZIndex(MapLayer.AutoFloorLayer, -10);
        tileMap.SetLayerNavigationEnabled(MapLayer.AutoFloorLayer, false);
        tileMap.SetLayerName(MapLayer.AutoFloorLayer, nameof(MapLayer.AutoFloorLayer));
        
        tileMap.AddLayer(MapLayer.CustomFloorLayer1);
        tileMap.SetLayerZIndex(MapLayer.CustomFloorLayer1, -10);
        tileMap.SetLayerNavigationEnabled(MapLayer.CustomFloorLayer1, false);
        tileMap.SetLayerName(MapLayer.CustomFloorLayer1, nameof(MapLayer.CustomFloorLayer1));
        
        tileMap.AddLayer(MapLayer.CustomFloorLayer2);
        tileMap.SetLayerZIndex(MapLayer.CustomFloorLayer2, -10);
        tileMap.SetLayerNavigationEnabled(MapLayer.CustomFloorLayer2, false);
        tileMap.SetLayerName(MapLayer.CustomFloorLayer2, nameof(MapLayer.CustomFloorLayer2));
        
        tileMap.AddLayer(MapLayer.CustomFloorLayer3);
        tileMap.SetLayerZIndex(MapLayer.CustomFloorLayer3, -10);
        tileMap.SetLayerNavigationEnabled(MapLayer.CustomFloorLayer3, false);
        tileMap.SetLayerName(MapLayer.CustomFloorLayer3, nameof(MapLayer.CustomFloorLayer3));
        
        tileMap.AddLayer(MapLayer.AutoMiddleLayer);
        tileMap.SetLayerZIndex(MapLayer.AutoMiddleLayer, 2);
        tileMap.SetLayerNavigationEnabled(MapLayer.AutoMiddleLayer, false);
        tileMap.SetLayerYSortEnabled(MapLayer.AutoMiddleLayer, true);
        tileMap.SetLayerName(MapLayer.AutoMiddleLayer, nameof(MapLayer.AutoMiddleLayer));
        
        tileMap.AddLayer(MapLayer.CustomMiddleLayer1);
        tileMap.SetLayerZIndex(MapLayer.CustomMiddleLayer1, 2);
        tileMap.SetLayerNavigationEnabled(MapLayer.CustomMiddleLayer1, false);
        tileMap.SetLayerYSortEnabled(MapLayer.CustomMiddleLayer1, true);
        tileMap.SetLayerName(MapLayer.CustomMiddleLayer1, nameof(MapLayer.CustomMiddleLayer1));
        
        tileMap.AddLayer(MapLayer.CustomMiddleLayer2);
        tileMap.SetLayerZIndex(MapLayer.CustomMiddleLayer2, 2);
        tileMap.SetLayerNavigationEnabled(MapLayer.CustomMiddleLayer2, false);
        tileMap.SetLayerYSortEnabled(MapLayer.CustomMiddleLayer2, true);
        tileMap.SetLayerName(MapLayer.CustomMiddleLayer2, nameof(MapLayer.CustomMiddleLayer2));
        
        tileMap.AddLayer(MapLayer.AutoTopLayer);
        tileMap.SetLayerZIndex(MapLayer.AutoTopLayer, 10);
        tileMap.SetLayerNavigationEnabled(MapLayer.AutoTopLayer, false);
        tileMap.SetLayerName(MapLayer.AutoTopLayer, nameof(MapLayer.AutoTopLayer));
        
        tileMap.AddLayer(MapLayer.CustomTopLayer);
        tileMap.SetLayerZIndex(MapLayer.CustomTopLayer, 10);
        tileMap.SetLayerNavigationEnabled(MapLayer.CustomTopLayer, false);
        tileMap.SetLayerName(MapLayer.CustomTopLayer, nameof(MapLayer.CustomTopLayer));
        
        tileMap.AddLayer(MapLayer.AutoAisleFloorLayer);
        tileMap.SetLayerZIndex(MapLayer.AutoAisleFloorLayer, -10);
        tileMap.SetLayerNavigationEnabled(MapLayer.AutoAisleFloorLayer, false);
        tileMap.SetLayerName(MapLayer.AutoAisleFloorLayer, nameof(MapLayer.AutoAisleFloorLayer));
    }
}