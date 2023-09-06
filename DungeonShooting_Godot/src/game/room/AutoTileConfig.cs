
using System.Collections.Generic;
using Godot;

/// <summary>
/// 房间图块配置信息
/// </summary>
public class AutoTileConfig
{
    public TileCellInfo IN_LT = new TileCellInfo(0, new Vector2I(3, 3));
    public TileCellInfo IN_LB = new TileCellInfo(0, new Vector2I(11, 2));
    public TileCellInfo IN_RT = new TileCellInfo(0, new Vector2I(1, 3));
    public TileCellInfo IN_RB = new TileCellInfo(0, new Vector2I(13, 2));
    public TileCellInfo R = new TileCellInfo(0, new Vector2I(1, 3));
    public TileCellInfo L = new TileCellInfo(0, new Vector2I(3, 3));
    public TileCellInfo T = new TileCellInfo(0, new Vector2I(2, 7));
    public TileCellInfo B = new TileCellInfo(0, new Vector2I(2, 2));
    public TileCellInfo Floor = new TileCellInfo(0, new Vector2I(0, 8));
    
    public TileCellInfo OUT_LT = new TileCellInfo(0, new Vector2I(1, 2));
    public TileCellInfo OUT_LB = new TileCellInfo(0, new Vector2I(1, 7));
    public TileCellInfo OUT_RT = new TileCellInfo(0, new Vector2I(3, 2));
    public TileCellInfo OUT_RB = new TileCellInfo(0, new Vector2I(3, 7));
    
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