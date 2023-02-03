
using Godot;

public class TileCellInfo
{
    public TileCellInfo(int id, Vector2? autotileCoord)
    {
        Id = id;
        AutotileCoord = autotileCoord;
    }

    public int Id;
    public Vector2? AutotileCoord;
}