
using Godot;

/// <summary>
/// 房间图块配置信息
/// </summary>
public class AutoTileConfig
{
    public TileCellInfo LT = new TileCellInfo(0, new Vector2(3, 3));
    public TileCellInfo LB = new TileCellInfo(0, new Vector2(11, 2));
    public TileCellInfo RT = new TileCellInfo(0, new Vector2(1, 3));
    public TileCellInfo RB = new TileCellInfo(0, new Vector2(13, 2));
    public TileCellInfo R = new TileCellInfo(0, new Vector2(1, 3));
    public TileCellInfo L = new TileCellInfo(0, new Vector2(3, 3));
    public TileCellInfo T = new TileCellInfo(0, new Vector2(2, 7));
    public TileCellInfo B = new TileCellInfo(0, new Vector2(2, 2));
    public TileCellInfo In = new TileCellInfo(0, new Vector2(0, 8));
}