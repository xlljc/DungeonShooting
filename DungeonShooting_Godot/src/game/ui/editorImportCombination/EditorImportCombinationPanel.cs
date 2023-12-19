using Godot;

namespace UI.EditorImportCombination;

public partial class EditorImportCombinationPanel : EditorImportCombination
{
    private DragBinder _dragBinder;
    public override void OnShowUi()
    {
        _dragBinder = UiDragManager.BindDrag(S_PreviewBg.Instance, (state, delta) =>
        {
            if (state == DragState.DragMove)
            {
                S_PreviewTexture.Instance.Position += delta;
            }
        });
    }

    public override void OnDestroyUi()
    {
        _dragBinder.UnBind();
    }

    /// <summary>
    /// 初始化页面数据
    /// </summary>
    /// <param name="name">显示名称</param>
    /// <param name="bgColor">预览纹理背景颜色</param>
    /// <param name="texture">显示预览纹理</param>
    public void InitData(string name, Color bgColor, Texture2D texture)
    {
        S_PreviewBg.Instance.Color = bgColor;
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
