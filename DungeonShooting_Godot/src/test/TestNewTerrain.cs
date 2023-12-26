using Godot;
using System;

/// <summary>
/// 测试动态烘焙导航网格
/// </summary>
public partial class TestNewTerrain : Node2D
{
    private TileMap _tileMap;
    private Vector2[][] _polygonData;
    private NavigationRegion2D _navigationRegion;
    
    public override void _Ready()
    {
        Visible = false;
        _tileMap = GetNode<TileMap>("TileMap2");
        _navigationRegion = GetNode<NavigationRegion2D>("TileMap2/NavigationRegion2D");
        _navigationRegion.BakeFinished += BakeFinished;
        RunTest();
        

        var tileSet = _tileMap.TileSet;
        
        var terrainSetsCount = tileSet.GetTerrainSetsCount();
        Debug.Log($"terrainSetsCount: {terrainSetsCount}");
        for (var i = 0; i < terrainSetsCount; i++)
        {
            Debug.Log("----------------------------------------------------");
            var count = tileSet.GetTerrainsCount(i);
            Debug.Log($"terrainSet: {i} - {count} - {tileSet.GetTerrainSetMode(i)}");
            for (int j = 0; j < count; j++)
            {
                var terrainName = tileSet.GetTerrainName(i, j);
                Debug.Log($"terrainName: {terrainName}");
            }
        }
        
        var tileSetSource = tileSet.GetSource(0);
        if (tileSetSource is TileSetAtlasSource atlasSource)
        {
            var tilesCount = tileSetSource.GetTilesCount();
            for (int i = 0; i < tilesCount; i++)
            {
                var pos = tileSetSource.GetTileId(i);
                var tileData = atlasSource.GetTileData(pos, 0);
                Debug.Log($"pos: {pos}, terrain: {tileData.Terrain}, terrainSet: {tileData.TerrainSet}, peering: {tileData.GetTerrainPeeringValue() & TerrainPeering.Top}");
                if (tileData.Terrain != -1 && tileData.TerrainSet != -1)
                {
                    var str = "";
                    str += (tileData.GetTerrainPeeringBit(TileSet.CellNeighbor.TopLeftCorner) + 1).ToString();
                    str += (tileData.GetTerrainPeeringBit(TileSet.CellNeighbor.TopSide) + 1).ToString();
                    str += (tileData.GetTerrainPeeringBit(TileSet.CellNeighbor.TopRightCorner) + 1).ToString() + "\n";
                    str += (tileData.GetTerrainPeeringBit(TileSet.CellNeighbor.LeftSide) + 1).ToString();
                    str += "1";
                    str += (tileData.GetTerrainPeeringBit(TileSet.CellNeighbor.RightSide) + 1).ToString() + "\n";
                    str += (tileData.GetTerrainPeeringBit(TileSet.CellNeighbor.BottomLeftCorner) + 1).ToString();
                    str += (tileData.GetTerrainPeeringBit(TileSet.CellNeighbor.BottomSide) + 1).ToString();
                    str += (tileData.GetTerrainPeeringBit(TileSet.CellNeighbor.BottomRightCorner) + 1).ToString();
                    GD.Print(str);
                }
                else
                {
                    GD.Print("000\n000\n000");
                }
            }
        }

    }

    private void BakeFinished()
    {
        var polygonData = _navigationRegion.NavigationPolygon;
        var polygons = polygonData.Polygons;
        var vertices = polygonData.Vertices;
        _polygonData = new Vector2[polygons.Count][];
        for (var i = 0; i < polygons.Count; i++)
        {
            var polygon = polygons[i];
            var v2Array = new Vector2[polygon.Length];
            for (var j = 0; j < polygon.Length; j++)
            {
                v2Array[j] = vertices[polygon[j]];
            }
            _polygonData[i] = v2Array;
        }
    }

    public override void _Process(double delta)
    {
        QueueRedraw();
    }

    public override void _Draw()
    {
        if (_polygonData != null)
        {
            //Utils.DrawNavigationPolygon(this, _polygonData);
            foreach (var vector2s in _polygonData)
            {
                DrawPolygon(vector2s, new Color(1, 1, 0, 0.3f).MakeArray(vector2s.Length));
            }
        }
    }

    private void RunTest()
    {
        var usedRect = _tileMap.GetUsedRect();
        var data = new NavigationPolygon();
        data.SourceGeometryMode = NavigationPolygon.SourceGeometryModeEnum.GroupsWithChildren;
        data.SourceGeometryGroupName = "navigation";
        data.CellSize = 4;
        data.AgentRadius = 6.5f;
        data.AddOutline(new []
        {
            usedRect.Position * GameConfig.TileCellSize,
            new Vector2(usedRect.End.X, usedRect.Position.Y) * GameConfig.TileCellSize,
            usedRect.End * GameConfig.TileCellSize,
            new Vector2(usedRect.Position.X, usedRect.End.Y) * GameConfig.TileCellSize
        });
        _navigationRegion.NavigationPolygon = data;
        _navigationRegion.BakeNavigationPolygon(false);
    }
}
