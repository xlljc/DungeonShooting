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
        _tileMap = GetNode<TileMap>("TileMap2");
        _navigationRegion = GetNode<NavigationRegion2D>("NavigationRegion2D");
        _navigationRegion.BakeFinished += BakeFinished;
        RunTest();
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
            Debug.Log($"IsPolygonClockwise: {Geometry2D.IsPolygonClockwise(v2Array)}");
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
