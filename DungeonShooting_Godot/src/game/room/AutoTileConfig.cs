
using System.Collections.Generic;
using Godot;

/// <summary>
/// 房间图块配置信息
/// </summary>
public class AutoTileConfig
{
    public TileCellData IN_LT = new TileCellData(0, new Vector2I(3, 3));
    public TileCellData IN_LB = new TileCellData(0, new Vector2I(11, 2));
    public TileCellData IN_RT = new TileCellData(0, new Vector2I(1, 3));
    public TileCellData IN_RB = new TileCellData(0, new Vector2I(13, 2));
    public TileCellData R = new TileCellData(0, new Vector2I(1, 3));
    public TileCellData L = new TileCellData(0, new Vector2I(3, 3));
    public TileCellData T = new TileCellData(0, new Vector2I(2, 7));
    public TileCellData B = new TileCellData(0, new Vector2I(2, 2));
    public TileCellData Floor = new TileCellData(0, new Vector2I(0, 8));
    
    public TileCellData OUT_LT = new TileCellData(0, new Vector2I(1, 2));
    public TileCellData OUT_LB = new TileCellData(0, new Vector2I(1, 7));
    public TileCellData OUT_RT = new TileCellData(0, new Vector2I(3, 2));
    public TileCellData OUT_RB = new TileCellData(0, new Vector2I(3, 7));
    
    public TileCellData WALL_BLOCK = new TileCellData(0, new Vector2I(2, 3));
    
    private List<Vector2I> _middleLayerAtlasCoords = new List<Vector2I>()
    {
        new Vector2I(1, 6),
        new Vector2I(2, 6),
        new Vector2I(3, 6),
        new Vector2I(1, 7),
        new Vector2I(2, 7),
        new Vector2I(3, 7),
    };
    
    private List<Vector2I> _topLayerAtlasCoords = new List<Vector2I>()
    {
        new Vector2I(1, 4),
        new Vector2I(1, 3),
        new Vector2I(1, 2),
        new Vector2I(2, 2),
        new Vector2I(3, 2),
        new Vector2I(3, 3),
        new Vector2I(3, 4),
        new Vector2I(11, 2),
        new Vector2I(13, 2),
    };

    public int GetLayer(Vector2I atlasCoords)
    {
        var layer = GameConfig.FloorMapLayer;
        if (_middleLayerAtlasCoords.Contains(atlasCoords))
        {
            layer = GameConfig.MiddleMapLayer;
        }
        else if (_topLayerAtlasCoords.Contains(atlasCoords))
        {
            layer = GameConfig.TopMapLayer;
        }

        return layer;
    }
}