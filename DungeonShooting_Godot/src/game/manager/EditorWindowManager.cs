
using System;
using Godot;
using UI.EditorTips;
using UI.EditorWindow;

public static class EditorWindowManager
{
    /// <summary>
    /// 弹出通用提示面板
    /// </summary>
    /// <param name="title">标题</param>
    /// <param name="message">显示内容</param>
    /// <param name="onClose">关闭时的回调</param>
    public static void ShowTips(string title, string message, Action onClose = null)
    {
        var window = UiManager.Open_EditorWindow();
        window.SetWindowTitle(title);
        if (onClose != null)
        {
            window.CloseEvent += onClose;
        }
        window.SetButtonList(
            new EditorWindowPanel.ButtonData("确定", () =>
            {
                window.CloseWindow();
            })
        );
        var body = window.OpenBody<EditorTipsPanel>(UiManager.UiName.EditorTips);
        body.SetMessage(message);
    }

    public static void ShowSelectObject(string title)
    {
        var window = UiManager.Open_EditorWindow();
        window.S_Window.Instance.Size = new Vector2I(900, 600);
        window.SetWindowTitle(title);
        window.OpenBody(UiManager.UiName.MapEditorSelectObject);
    }
}