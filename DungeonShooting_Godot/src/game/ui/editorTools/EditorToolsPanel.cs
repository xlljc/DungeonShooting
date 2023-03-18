using System;
using Generator;
using Godot;

namespace UI.EditorTools;

[Tool]
public partial class EditorToolsPanel : EditorTools
{
    //Tips 关闭回调
    private Action _onTipsClose;

    //询问窗口关闭
    private Action<bool> _onConfirmClose;
    
    public override void OnShowUi(params object[] args)
    {
        //tips
        _onTipsClose = null;
        L_Tips.Instance.OkButtonText = "确定";
        L_Tips.Instance.CloseRequested += OnTipsClose;
        L_Tips.Instance.Confirmed += OnTipsClose;
        L_Tips.Instance.Canceled += OnTipsClose;

        //confirm
        _onConfirmClose = null;
        L_Confirm.Instance.OkButtonText = "确定";
        L_Confirm.Instance.CancelButtonText = "取消";
        L_Confirm.Instance.Canceled += OnCanceled;
        L_Confirm.Instance.CloseRequested += OnCanceled;
        L_Confirm.Instance.Confirmed += OnConfirm;

        var container = L_ScrollContainer.L_MarginContainer.L_VBoxContainer;
        //重新生成 ResourcePath
        container.L_HBoxContainer.L_Button.Instance.Pressed += GenerateResourcePath;
        //重新生成 RoomPack
        container.L_HBoxContainer2.L_Button.Instance.Pressed += GenerateRoomPack;
        //创建ui
        container.L_HBoxContainer3.L_Button.Instance.Pressed += OnCreateUI;
    }

    public override void OnHideUi()
    {
        L_Tips.Instance.CloseRequested -= OnTipsClose;
        L_Tips.Instance.Confirmed -= OnTipsClose;
        L_Tips.Instance.Canceled -= OnTipsClose;
        
        L_Confirm.Instance.Canceled -= OnCanceled;
        L_Confirm.Instance.CloseRequested -= OnCanceled;
        L_Confirm.Instance.Confirmed -= OnConfirm;
        
        var container = L_ScrollContainer.L_MarginContainer.L_VBoxContainer;
        container.L_HBoxContainer.L_Button.Instance.Pressed -= GenerateResourcePath;
        container.L_HBoxContainer2.L_Button.Instance.Pressed -= GenerateRoomPack;
        container.L_HBoxContainer3.L_Button.Instance.Pressed -= OnCreateUI;
    }

    /// <summary>
    /// Tips 关闭信号回调
    /// </summary>
    private void OnTipsClose()
    {
        if (_onTipsClose != null)
        {
            _onTipsClose();
            _onTipsClose = null;
        }
    }

    /// <summary>
    /// Confirm 确认信号回调
    /// </summary>
    private void OnConfirm()
    {
        if (_onConfirmClose != null)
        {
            _onConfirmClose(true);
            _onConfirmClose = null;
        }
    }

    /// <summary>
    /// Confirm 取消信号回调
    /// </summary>
    private void OnCanceled()
    {
        if (_onConfirmClose != null)
        {
            _onConfirmClose(false);
            _onConfirmClose = null;
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
        ShowTips(title, message, 200, 124, onClose);
    }

    /// <summary>
    /// 关闭提示窗口
    /// </summary>
    public void CloseTips()
    {
        L_Tips.Instance.Hide();
        _onTipsClose = null;
    }

    /// <summary>
    /// 打开询问窗口, 并设置宽高
    /// </summary>
    /// <param name="title">窗口标题</param>
    /// <param name="message">显示内容</param>
    /// <param name="width">窗口宽度</param>
    /// <param name="height">窗口高度</param>
    /// <param name="onClose">当窗口关闭时的回调, 参数如果为 true 表示点击了确定按钮</param>
    public void ShowConfirm(string title, string message, int width, int height, Action<bool> onClose = null)
    {
        var confirm = L_Confirm.Instance;
        confirm.Size = new Vector2I(width, height);
        confirm.Title = title;
        confirm.DialogText = message;
        _onConfirmClose = onClose;
        confirm.Show();
    }
    
    /// <summary>
    /// 打开询问窗口
    /// </summary>
    /// <param name="title">窗口标题</param>
    /// <param name="message">显示内容</param>
    /// <param name="onClose">当窗口关闭时的回调, 参数如果为 true 表示点击了确定按钮</param>
    public void ShowConfirm(string title, string message, Action<bool> onClose = null)
    {
        ShowConfirm(title, message, 200, 124, onClose);
    }

    /// <summary>
    /// 关闭询问窗口
    /// </summary>
    public void CloseConfirm()
    {
        L_Confirm.Instance.Hide();
        _onConfirmClose = null;
    }
    
    /// <summary>
    /// 创建Ui
    /// </summary>
    private void OnCreateUI()
    {
        var text = L_ScrollContainer.L_MarginContainer.L_VBoxContainer.L_HBoxContainer3.L_LineEdit.Instance.Text;
        ShowConfirm("提示", "是否创建UI：" + text, (result) =>
        {
            if (result)
            {
                ShowTips("提示", "创建Ui成功!");
            }
        });
    }

    /// <summary>
    /// 更新 ResourcePath
    /// </summary>
    private void GenerateResourcePath()
    {
        ResourcePathGenerator.Generate();
    }

    /// <summary>
    /// 重新打包房间配置
    /// </summary>
    private void GenerateRoomPack()
    {
        RoomPackGenerator.Generate();
    }
}
