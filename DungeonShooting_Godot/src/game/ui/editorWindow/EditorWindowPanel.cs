using Godot;

namespace UI.EditorWindow;

public partial class EditorWindowPanel : EditorWindow
{

    public override void OnCreateUi()
    {
        
    }

    public override void OnDisposeUi()
    {
        
    }

    /// <summary>
    /// 打开子Ui并放入 Body 节点中
    /// </summary>
    /// <param name="uiName">Ui名称</param>
    public UiBase OpenBody(string uiName)
    {
        var nestedUi = S_Body.OpenNestedUi(uiName);
        S_Window.Instance.Popup();
        return nestedUi;
    }
    
    /// <summary>
    /// 打开子Ui并放入 Body 节点中
    /// </summary>
    /// <param name="uiName">Ui名称</param>
    public T OpenBody<T>(string uiName) where T : UiBase
    {
        return (T)OpenBody(uiName);
    }

    /// <summary>
    /// 设置标题
    /// </summary>
    public void SetWindowTitle(string title)
    {
        S_Window.Instance.Title = title;
    }

    /// <summary>
    /// 设置窗体大小
    /// </summary>
    public void SetWindowSize(Vector2I size)
    {
        S_Window.Instance.Size = size;
    }
}
