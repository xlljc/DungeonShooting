

/// <summary>
/// Ui网格组件基础接口, 无泛型
/// </summary>
public interface IUiGrid : IDestroy
{
    /// <summary>
    /// 当前选中的 Cell 索引
    /// </summary>
    int SelectIndex { get; set; }
    
    /// <summary>
    /// 设置网格组件是否可见
    /// </summary>
    bool Visible { get; set; }
    
    /// <summary>
    /// 当前网格组件数据大小
    /// </summary>
    int Count { get; }
}