
/// <summary>
/// 地牢类
/// </summary>
public partial class Dungeon : World
{
    /// <summary>
    /// 初始化 TileMap 中的层级
    /// </summary>
    public void InitLayer()
    {
        MapLayerManager.InitMapLayer(TileRoot);
    }
}