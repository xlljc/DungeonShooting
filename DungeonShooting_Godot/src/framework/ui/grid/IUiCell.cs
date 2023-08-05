
/// <summary>
/// 网格组件中单个格子的数据处理类接口, 无泛型
/// </summary>
public interface IUiCell : IDestroy
{
    /// <summary>
    /// 当前 Cell 在 UiGrid 组件中的索引位置
    /// </summary>
    public int Index { get; }

    /// <summary>
    /// 当检测当前 Cell 是否可以被选中时调用
    /// </summary>
    public bool CanSelect();

    /// <summary>
    /// 当前 Cell 选中时调用, 设置 UiGrid.SelectIndex 时触发
    /// </summary>
    public void OnSelect();

    /// <summary>
    /// 当前 Cell 取消选中时调用, 设置 UiGrid.SelectIndex 时触发
    /// </summary>
    public void OnUnSelect();

}