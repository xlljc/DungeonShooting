
public class TileMapLayerData
{
    /// <summary>
    /// 显示文本
    /// </summary>
    public string Title;
    /// <summary>
    /// Map 层级索引
    /// </summary>
    public int Layer;
    /// <summary>
    /// 是否锁定
    /// </summary>
    public bool IsLock;

    public TileMapLayerData(string title, int layer, bool isLock)
    {
        Title = title;
        Layer = layer;
        IsLock = isLock;
    }
}