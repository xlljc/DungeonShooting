
public class TileMapLayerData
{
    /// <summary>
    /// 显示文本
    /// </summary>
    public string Title;
    /// <summary>
    /// Map层级
    /// </summary>
    public int Layer;
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

    public TileMapLayerData(string title, int layer, bool isLock)
    {
        Title = title;
        Layer = layer;
        IsLock = isLock;
        CanDelete = false;
    }

    public TileMapLayerData(string title, int layer, CustomLayerInfo customLayerInfo)
    {
        Title = title;
        Layer = layer;
        IsLock = false;
        CanDelete = true;
        CustomLayerInfo = customLayerInfo;
    }
}