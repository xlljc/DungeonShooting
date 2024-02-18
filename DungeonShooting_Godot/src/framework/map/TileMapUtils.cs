
using System.Collections.Generic;
using System.Linq;
using Godot;
using Godot.Collections;

public static class TileMapUtils
{
    public const int MainSource = 0;
    public const int MainTerrainSet = 0;
    public const int MainTerrain = 0;
    
    /// <summary>
    /// 生成自动图块和地形, 并烘焙好导航网格
    /// </summary>
    public static Rect2I GenerateTerrain(TileMap tileMap, NavigationRegion2D navigationRegion, AutoTileConfig autoTileConfig)
    {
        var list = new List<Vector2I>();
        var xStart = int.MaxValue;
        var yStart = int.MaxValue;
        var xEnd = int.MinValue;
        var yEnd = int.MinValue;

        var temp = tileMap.GetUsedCells(MapLayer.AutoFloorLayer);
        var autoCellLayerGrid = temp.ToHashSet();
        foreach (var (x, y) in autoCellLayerGrid)
        {
            //计算范围
                if (x < xStart)
                    xStart = x;
                else if (x > xEnd)
                    xEnd = x;

                if (y < yStart)
                    yStart = y;
                else if (y > yEnd)
                    yEnd = y;
                
                //填充墙壁
                if (!autoCellLayerGrid.Contains(new Vector2I(x, y - 1)))
                {
                    var left = autoCellLayerGrid.Contains(new Vector2I(x - 1, y - 1));
                    var right = autoCellLayerGrid.Contains(new Vector2I(x + 1, y - 1));
                    if (left && right)
                    {
                        var tileCellData1 = autoTileConfig.Wall_Vertical_SingleTop;
                        tileMap.SetCell(MapLayer.AutoFloorLayer, new Vector2I(x, y - 2), tileCellData1.SourceId, tileCellData1.AutoTileCoords);
                        var tileCellData2 = autoTileConfig.Wall_Vertical_SingleBottom;
                        tileMap.SetCell(MapLayer.AutoFloorLayer, new Vector2I(x, y - 1), tileCellData2.SourceId, tileCellData2.AutoTileCoords);
                    }
                    else if (left)
                    {
                        var tileCellData1 = autoTileConfig.Wall_Vertical_LeftTop;
                        tileMap.SetCell(MapLayer.AutoFloorLayer, new Vector2I(x, y - 2), tileCellData1.SourceId, tileCellData1.AutoTileCoords);
                        var tileCellData2 = autoTileConfig.Wall_Vertical_LeftBottom;
                        tileMap.SetCell(MapLayer.AutoFloorLayer, new Vector2I(x, y - 1), tileCellData2.SourceId, tileCellData2.AutoTileCoords);
                    }
                    else if (right)
                    {
                        var tileCellData1 = autoTileConfig.Wall_Vertical_RightTop;
                        tileMap.SetCell(MapLayer.AutoFloorLayer, new Vector2I(x, y - 2), tileCellData1.SourceId, tileCellData1.AutoTileCoords);
                        var tileCellData2 = autoTileConfig.Wall_Vertical_RightBottom;
                        tileMap.SetCell(MapLayer.AutoFloorLayer, new Vector2I(x, y - 1), tileCellData2.SourceId, tileCellData2.AutoTileCoords);
                    }
                    else
                    {
                        var tileCellData1 = autoTileConfig.Wall_Vertical_CenterTop;
                        tileMap.SetCell(MapLayer.AutoFloorLayer, new Vector2I(x, y - 2), tileCellData1.SourceId, tileCellData1.AutoTileCoords);
                        var tileCellData2 = autoTileConfig.Wall_Vertical_CenterBottom;
                        tileMap.SetCell(MapLayer.AutoFloorLayer, new Vector2I(x, y - 1), tileCellData2.SourceId, tileCellData2.AutoTileCoords);
                    }
                }
        }
        
        //绘制临时边界
        var temp1 = new List<Vector2I>();
        for (var x = xStart - 3; x <= xEnd + 3; x++)
        {
            var p1 = new Vector2I(x, yStart - 5);
            var p2 = new Vector2I(x, yEnd + 3);
            temp1.Add(p1);
            temp1.Add(p2);
            //上横
            tileMap.SetCell(MapLayer.AutoFloorLayer, p1, autoTileConfig.TopMask.SourceId, autoTileConfig.TopMask.AutoTileCoords);
            //下横
            tileMap.SetCell(MapLayer.AutoFloorLayer, p2, autoTileConfig.TopMask.SourceId, autoTileConfig.TopMask.AutoTileCoords);
        }
        for (var y = yStart - 5; y <= yEnd + 3; y++)
        {
            var p1 = new Vector2I(xStart - 3, y);
            var p2 = new Vector2I(xEnd + 3, y);
            temp1.Add(p1);
            temp1.Add(p2);
            //左竖
            tileMap.SetCell(MapLayer.AutoFloorLayer, p1, autoTileConfig.TopMask.SourceId, autoTileConfig.TopMask.AutoTileCoords);
            //右竖
            tileMap.SetCell(MapLayer.AutoFloorLayer, p2, autoTileConfig.TopMask.SourceId, autoTileConfig.TopMask.AutoTileCoords);
        }
        
        //计算需要绘制的图块
        var temp2 = new List<Vector2I>();
        for (var x = xStart - 2; x <= xEnd + 2; x++)
        {
            for (var y = yStart - 4; y <= yEnd + 2; y++)
            {
                if (!autoCellLayerGrid.Contains(new Vector2I(x, y)) && !autoCellLayerGrid.Contains(new Vector2I(x, y + 1)) && !autoCellLayerGrid.Contains(new Vector2I(x, y + 2)))
                {
                    list.Add(new Vector2I(x, y));
                    if (!IsMaskCollisionGround(autoCellLayerGrid, x, y))
                    {
                        temp2.Add(new Vector2I(x, y));
                    }
                }
            }
        }
        var arr = new Array<Vector2I>(list);
        //绘制自动图块
        tileMap.SetCellsTerrainConnect(MapLayer.AutoFloorLayer, arr, MainTerrainSet, MainTerrain, false);
        
        //擦除临时边界
        for (var i = 0; i < temp1.Count; i++)
        {
            tileMap.EraseCell(MapLayer.AutoFloorLayer, temp1[i]);
        }

        //计算区域
        var rect = Utils.CalcRect(autoCellLayerGrid);
        rect.Position -= new Vector2I(2, 3);
        rect.Size += new Vector2I(4, 5);
        
        //开始绘制导航网格
        GenerateNavigation(navigationRegion, rect.Position, rect.Size);

        //擦除临时边界2
        for (var i = 0; i < temp2.Count; i++)
        {
            tileMap.EraseCell(MapLayer.AutoFloorLayer, temp2[i]);
        }
        
        //将墙壁移动到指定层
        MoveTerrainCell(tileMap, autoTileConfig, autoCellLayerGrid, rect.Position, rect.Size);
        return rect;
    }
    
    private static bool IsMaskCollisionGround(HashSet<Vector2I> autoCellLayerGrid, int x, int y)
    {
        for (var i = -2; i <= 2; i++)
        {
            for (var j = -2; j <= 4; j++)
            {
                if (autoCellLayerGrid.Contains(new Vector2I(x + i, y + j)))
                {
                    return true;
                }
            }
        }

        return false;
    }
    
    //生成导航网格
    private static void GenerateNavigation(NavigationRegion2D navigationRegion, Vector2I currRoomPosition, Vector2I currRoomSize)
    {
        var navigationPolygon = navigationRegion.NavigationPolygon;
        if (navigationPolygon != null)
        {
            navigationPolygon.Clear();
            navigationPolygon.ClearPolygons();
            navigationPolygon.ClearOutlines();
        }
        else
        {
            navigationPolygon = ResourceManager.Load<NavigationPolygon>(ResourcePath.resource_navigation_NavigationPolygon_tres);
            navigationRegion.NavigationPolygon = navigationPolygon;
        }

        var endPos = currRoomPosition + currRoomSize;
        navigationPolygon.AddOutline(new []
        {
            currRoomPosition * GameConfig.TileCellSize,
            new Vector2(endPos.X, currRoomPosition.Y) * GameConfig.TileCellSize,
            endPos * GameConfig.TileCellSize,
            new Vector2(currRoomPosition.X, endPos.Y) * GameConfig.TileCellSize
        });
        navigationRegion.BakeNavigationPolygon(false);
    }
    
        
    //将自动生成的图块从 MapLayer.AutoFloorLayer 移动到指定图层中
    private static void MoveTerrainCell(TileMap tileMap, AutoTileConfig autoTileConfig, HashSet<Vector2I> autoCellLayerGrid, Vector2I currRoomPosition, Vector2I currRoomSize)
    {
        tileMap.ClearLayer(MapLayer.AutoTopLayer);
        tileMap.ClearLayer(MapLayer.AutoMiddleLayer);
        
        var x = currRoomPosition.X;
        var y = currRoomPosition.Y - 1;
        var w = currRoomSize.X;
        var h = currRoomSize.Y + 1;

        for (var i = 0; i < w; i++)
        {
            for (var j = 0; j < h; j++)
            {
                var pos = new Vector2I(x + i, y + j);
                if (!autoCellLayerGrid.Contains(pos) && tileMap.GetCellSourceId(MapLayer.AutoFloorLayer, pos) != -1)
                {
                    var atlasCoords = tileMap.GetCellAtlasCoords(MapLayer.AutoFloorLayer, pos);
                    var layer = autoTileConfig.GetLayer(atlasCoords);
                    if (layer == MapLayer.AutoMiddleLayer)
                    {
                        layer = MapLayer.AutoMiddleLayer;
                    }
                    else if (layer == MapLayer.AutoTopLayer)
                    {
                        layer = MapLayer.AutoTopLayer;
                    }
                    else
                    {
                        Debug.LogError($"异常图块: {pos}, 这个图块的图集坐标'{atlasCoords}'不属于'MiddleMapLayer'和'TopMapLayer'!");
                        continue;
                    }
                    tileMap.EraseCell(MapLayer.AutoFloorLayer, pos);
                    tileMap.SetCell(layer, pos, MainSource, atlasCoords);
                }
            }
        }
    }
}