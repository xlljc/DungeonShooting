
/// <summary>
/// 编辑器标记数据显示隐藏状态数据
/// </summary>
public struct MarkInfoVisibleData
{
    public MarkInfo MarkInfo;
    public bool Visible;

    public MarkInfoVisibleData(MarkInfo markInfo, bool visible)
    {
        MarkInfo = markInfo;
        Visible = visible;
    }
}