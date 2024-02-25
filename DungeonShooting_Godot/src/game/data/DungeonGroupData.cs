
public class DungeonGroupData
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Name;
    /// <summary>
    /// 地图集
    /// </summary>
    public string TileSet;
    /// <summary>
    /// 备注
    /// </summary>
    public string Remark;

    public DungeonGroupData(string name, string tileSet, string remark)
    {
        Name = name;
        TileSet = tileSet;
        Remark = remark;
    }
}