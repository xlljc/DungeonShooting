using Godot;

namespace UI.EditorImportCombination;

public partial class EditorImportCombinationPanel : EditorImportCombination
{
    /// <summary>
    /// 初始化页面数据
    /// </summary>
    /// <param name="name">显示名称</param>
    /// <param name="texture">显示预览纹理</param>
    public void InitData(string name, Texture2D texture)
    {
        S_NameInput.Instance.Text = name;
        S_PreviewTexture.Instance.Texture = texture;
    }

    /// <summary>
    /// 获取输入框内的名称
    /// </summary>
    public string GetName()
    {
        return S_NameInput.Instance.Text;
    }

}
