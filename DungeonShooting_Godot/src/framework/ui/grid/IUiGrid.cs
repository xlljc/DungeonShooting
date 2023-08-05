

/// <summary>
/// Ui网格组件基础接口, 无泛型
/// </summary>
public interface IUiGrid : IDestroy
{
    /// <summary>
    /// 当前选中的 Cell 索引
    /// </summary>
    public int SelectIndex { get; set; }
    
    /// <summary>
    /// 设置网格组件是否可见
    /// </summary>
    public bool Visible { get; set; }
}