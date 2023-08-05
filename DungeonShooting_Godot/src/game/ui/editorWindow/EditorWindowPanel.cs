using System;
using Godot;

namespace UI.EditorWindow;

public partial class EditorWindowPanel : EditorWindow
{

    public class ButtonData
    {
        /// <summary>
        /// 显示文本
        /// </summary>
        public string Text;
        /// <summary>
        /// 点击的回调
        /// </summary>
        public Action Callback;

        public ButtonData(string text, Action callback)
        {
            Text = text;
            Callback = callback;
        }
    }
    
    /// <summary>
    /// 关闭窗口时回调
    /// </summary>
    public event Action CloseEvent;

    private UiGrid<EditorWindow.CustomButton, ButtonData> _uiGrid;

    public override void OnCreateUi()
    {
        S_CustomButton.Instance.Visible = false;
        S_Window.Instance.CloseRequested += CloseWindow;
    }

    /// <summary>
    /// 设置自定义按钮
    /// </summary>
    public void SetButtonList(params ButtonData[] buttons)
    {
        if (_uiGrid == null)
        {
            S_CustomButton.Instance.Visible = true;
            _uiGrid = new UiGrid<CustomButton, ButtonData>(S_CustomButton, typeof(CustomButtonCell));
            _uiGrid.SetHorizontalExpand(true);
        }
        _uiGrid.SetColumns(buttons.Length);
        _uiGrid.SetDataList(buttons);
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

    /// <summary>
    /// 设置窗体最小大小
    /// </summary>
    public void SetWindowMinSize(Vector2I size)
    {
        S_Window.Instance.MinSize = size;
    }

    /// <summary>
    /// 关闭窗口
    /// </summary>
    public void CloseWindow()
    {
        if (CloseEvent != null)
        {
            CloseEvent();
        }
        Destroy();
    }
}
