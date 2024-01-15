
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
    /// Map 层级 Z 索引
    /// </summary>
    public int ZIndex;
    /// <summary>
    /// 是否锁定
    /// </summary>
    public bool IsLock;
    /// <summary>
    /// 是否可以删除
    /// </summary>
    public bool CanDelete;
    /// <summary>
    /// 自定义层数据
    /// </summary>
    public CustomLayerInfo CustomLayerInfo;

    public TileMapLayerData(string title, int layer, int zIndex, bool isLock)
    {
        Title = title;
        Layer = layer;
        ZIndex = zIndex;
        IsLock = isLock;
        CanDelete = false;
    }

    public TileMapLayerData(int layer, CustomLayerInfo customLayerInfo)
    {
        Title = customLayerInfo.Name;
        Layer = layer;
        ZIndex = customLayerInfo.ZIndex;
        IsLock = false;
        CanDelete = true;
        CustomLayerInfo = customLayerInfo;
    }
}