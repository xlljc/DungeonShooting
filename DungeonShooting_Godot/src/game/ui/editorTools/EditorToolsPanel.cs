using System;
using Generator;
using Godot;

namespace UI.EditorTools;

[Tool]
public partial class EditorToolsPanel : EditorTools
{
    //Tips 关闭回调
    private Action _onTipsClose;
    
    public override void OnShowUi(params object[] args)
    {
        L_Tips.Instance.OkButtonText = "确定";
        L_Tips.Instance.CloseRequested += OnTipsCloseRequested;
        L_ScrollContainer.L_MarginContainer.L_VBoxContainer.L_HBoxContainer3.L_Button.Instance.Pressed += OnCreateUI;
    }

    public override void OnHideUi()
    {
        L_Tips.Instance.CloseRequested -= OnTipsCloseRequested;
        L_ScrollContainer.L_MarginContainer.L_VBoxContainer.L_HBoxContainer3.L_Button.Instance.Pressed -= OnCreateUI;
    }

    /// <summary>
    /// Tips 关闭信号回调
    /// </summary>
    private void OnTipsCloseRequested()
    {
        if (_onTipsClose != null)
        {
            _onTipsClose();
            _onTipsClose = null;
        }
    }
    
    /// <summary>
    /// 打开提示窗口, 并设置宽高
    /// </summary>
    /// <param name="title">窗口标题</param>
    /// <param name="message">显示内容</param>
    /// <param name="width">窗口宽度</param>
    /// <param name="height">窗口高度</param>
    /// <param name="onClose">当窗口关闭时的回调</param>
    public void ShowTips(string title, string message, int width, int height, Action onClose = null)
    {
        var tips = L_Tips.Instance;
        tips.Size = new Vector2I(width, height);
        tips.Title = title;
        tips.DialogText = message;
        _onTipsClose = onClose;
        tips.Show();
    }
    
    /// <summary>
    /// 打开提示窗口
    /// </summary>
    /// <param name="title">窗口标题</param>
    /// <param name="message">显示内容</param>
    /// <param name="onClose">当窗口关闭时的回调</param>
    public void ShowTips(string title, string message, Action onClose = null)
    {
        var tips = L_Tips.Instance;
        tips.Title = title;
        tips.DialogText = message;
        _onTipsClose = onClose;
        tips.Show();
    }

    /// <summary>
    /// 关闭提示窗口
    /// </summary>
    public void CloseTips()
    {
        L_Tips.Instance.Hide();
        _onTipsClose = null;
    }
    
    private void OnCreateUI()
    {
        var text = L_ScrollContainer.L_MarginContainer.L_VBoxContainer.L_HBoxContainer3.L_LineEdit.Instance.Text;
        GD.Print("ui名称: " + text);
        ShowTips("创建Ui", "Ui名称: " + text, () =>
        {
            GD.Print("关闭Tips");
        });
    }
    
    private void OnCloseCreateUiConfirm()
    {
        //L_CreateUiConfirm.Instance.Hide();
    }
    
    /// <summary>
    /// 更新 ResourcePath
    /// </summary>
    private void _on_Button_pressed()
    {
        ResourcePathGenerator.Generate();
    }

    /// <summary>
    /// 重新打包房间配置
    /// </summary>
    private void _on_Button2_pressed()
    {
        RoomPackGenerator.Generate();
    }
}
