using System.IO;

namespace UI.EditorInfo;

public partial class EditorInfoPanel : EditorInfo
{
    /// <summary>
    /// 初始化数据
    /// </summary>
    public void InitData(EditorInfoData infoData)
    {
        S_NameInput.Instance.Text = infoData.Name;
        S_RemarkInput.Instance.Text = infoData.Remark;
    }
    
    /// <summary>
    /// 获取填写的数据
    /// </summary>
    public EditorInfoData GetInfoData()
    {
        return new EditorInfoData(S_NameInput.Instance.Text, S_RemarkInput.Instance.Text);
    }
    
    /// <summary>
    /// 是否可以编辑名称输入框
    /// </summary>
    public void SetNameInputEnable(bool v)
    {
        S_NameInput.Instance.Editable = v;
    }
}
