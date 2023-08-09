
/// <summary>
/// 网格组件中单个格子的数据处理类接口, 无泛型
/// </summary>
public interface IUiCell : IDestroy
{
    /// <summary>
    /// 是否启用了当前 Cell
    /// </summary>
    bool Enable { get; }

    /// <summary>
    /// 当前 Cell 在 UiGrid 组件中的索引位置
    /// </summary>
    int Index { get; }

    /// <summary>
    /// 当前 Cell 初始化时调用
    /// </summary>
    void OnInit();

    /// <summary>
    /// 如果启用了当前 Cell, 则调用
    /// </summary>
    void Process(float delta);
    
    /// <summary>
    /// 当前Ui被点击时调用<br/>
    /// 如果 Cell 的模板为 BaseButton 类型, 则 UiCell 会自动绑定点击事件<br/>
    /// 如果需要自己绑定点击事件, 请绑定 UiCell.Click() 函数<br/>
    /// 如果当前 Cell 未被选中, 则 OnSelect() 会比 OnClick() 先调用
    /// </summary>
    void OnClick();

    /// <summary>
    /// 双击当前 Cell 调用
    /// </summary>
    void OnDoubleClick();

    /// <summary>
    /// 当启用当前 Cell 时调用
    /// </summary>
    void OnEnable();

    /// <summary>
    /// 当禁用当前 Cell 时调用, 也就是被回收时调用
    /// </summary>
    void OnDisable();
    
    /// <summary>
    /// 当检测当前 Cell 是否可以被选中时调用
    /// </summary>
    bool CanSelect();

    /// <summary>
    /// 当前 Cell 选中时调用, 设置 UiGrid.SelectIndex 时触发
    /// </summary>
    void OnSelect();

    /// <summary>
    /// 当前 Cell 取消选中时调用, 设置 UiGrid.SelectIndex 时触发
    /// </summary>
    void OnUnSelect();

    /// <summary>
    /// 当 Cell 索引发生改变时调用, 在 UiGrid 中调用 Insert(), Remove() 等函数时被动触发当前 Cell 索引值改变, Cell 业务逻辑需要用到索引值时, 那么就可以重写该函数<br/>
    /// 注意: 该函数第一次调用会在 OnSetData() 之前调用
    /// </summary>
    void OnRefreshIndex();

    /// <summary>
    /// 销毁当前cell时调用
    /// </summary>
    void OnDestroy();
}