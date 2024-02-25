using Godot;

namespace UI.EditorImportCombination;

public partial class EditorImportCombinationPanel : EditorImportCombination
{
    public override void OnShowUi()
    {
        //监听拖拽
        S_PreviewBg.Instance.AddDragListener(DragButtonEnum.Left | DragButtonEnum.Right | DragButtonEnum.Middle, OnDragPreview);
        //监听鼠标滚轮
        S_PreviewBg.Instance.AddMouseWheelListener(OnMouseCallback);
    }
    
    //拖拽纹理
    private void OnDragPreview(DragState state, Vector2 delta)
    {
        if (state == DragState.DragMove)
        {
            S_PreviewTexture.Instance.Position += delta;
        }
    }

    //缩放/放大纹理
    private void OnMouseCallback(int value)
    {
        if (value < 0) //缩小
        {
            Utils.DoShrinkByMousePosition(S_PreviewTexture.Instance, 0.2f);
        }
        else //放大
        {
            Utils.DoMagnifyByMousePosition(S_PreviewTexture.Instance, 20);
        }
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
