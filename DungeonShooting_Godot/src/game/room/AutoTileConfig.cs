
using Godot;

/// <summary>
/// 房间图块配置信息
/// </summary>
public class AutoTileConfig
{
    public TileCellInfo IN_LT = new TileCellInfo(0, new Vector2(3, 3));
    public TileCellInfo IN_LB = new TileCellInfo(0, new Vector2(11, 2));
    public TileCellInfo IN_RT = new TileCellInfo(0, new Vector2(1, 3));
    public TileCellInfo IN_RB = new TileCellInfo(0, new Vector2(13, 2));
    public TileCellInfo R = new TileCellInfo(0, new Vector2(1, 3));
    public TileCellInfo L = new TileCellInfo(0, new Vector2(3, 3));
    public TileCellInfo T = new TileCellInfo(0, new Vector2(2, 7));
    public TileCellInfo B = new TileCellInfo(0, new Vector2(2, 2));
    public TileCellInfo Ground = new TileCellInfo(0, new Vector2(0, 8));
    
    public TileCellInfo OUT_LT = new TileCellInfo(0, new Vector2(1, 2));
    public TileCellInfo OUT_LB = new TileCellInfo(0, new Vector2(1, 7));
    public TileCellInfo OUT_RT = new TileCellInfo(0, new Vector2(3, 2));
    public TileCellInfo OUT_RB = new TileCellInfo(0, new Vector2(3, 7));
}