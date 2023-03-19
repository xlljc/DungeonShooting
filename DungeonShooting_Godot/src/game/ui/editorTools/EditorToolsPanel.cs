using System;
using System.IO;
using System.Text.RegularExpressions;
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
        //重新打包房间配置
        container.L_HBoxContainer2.L_Button.Instance.Pressed += GenerateRoomConfig;
        //重新生成ui代码
        container.L_HBoxContainer4.L_Button.Instance.Pressed += OnGenerateCurrentUiCode;
        //创建ui
        container.L_HBoxContainer3.L_Button.Instance.Pressed += OnCreateUI;
        //重新生成UiManagerMethods.cs代码
        container.L_HBoxContainer5.L_Button.Instance.Pressed += GenerateUiManagerMethods;
        //创建地牢房间
        container.L_HBoxContainer6.L_Button.Instance.Pressed += GenerateDungeonRoom;
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
        container.L_HBoxContainer2.L_Button.Instance.Pressed -= GenerateRoomConfig;
        container.L_HBoxContainer4.L_Button.Instance.Pressed -= OnGenerateCurrentUiCode;
        container.L_HBoxContainer3.L_Button.Instance.Pressed -= OnCreateUI;
        container.L_HBoxContainer5.L_Button.Instance.Pressed -= GenerateUiManagerMethods;
        container.L_HBoxContainer6.L_Button.Instance.Pressed -= GenerateDungeonRoom;
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
    /// 重新生成当前ui的代码
    /// </summary>
    private void OnGenerateCurrentUiCode()
    {
#if TOOLS
        if (Plugin.Plugin.Instance != null)
        {
            var root = Plugin.Plugin.Instance.GetEditorInterface().GetEditedSceneRoot();
            if (root != null && Plugin.Plugin.Instance.CheckIsUi(root))
            {
                if (UiGenerator.GenerateUiCodeFromEditor(root))
                {
                    ShowTips("提示", "生成UI代码执行成功!");
                }
                else
                {
                    ShowTips("错误", "生成UI代码执行失败! 前往控制台查看错误日志!");
                }
            }
            else
            {
                ShowTips("错误", "当前的场景不是受管束的UI场景!");
            }
        }
#endif
    }
    
    /// <summary>
    /// 创建Ui
    /// </summary>
    private void OnCreateUI()
    {
        var uiName = L_ScrollContainer.L_MarginContainer.L_VBoxContainer.L_HBoxContainer3.L_LineEdit.Instance.Text;
        ShowConfirm("提示", "是否创建UI：" + uiName, (result) =>
        {
            if (result)
            {
                //检查名称是否合规
                if (!Regex.IsMatch(uiName, "^[A-Z][a-zA-Z0-9]*$"))
                {
                    ShowTips("错误", "UI名称'" + uiName + "'不符合名称约束, UI名称只允许大写字母开头, 且名称中只允许出现大小字母和数字!");
                    return;
                }

                //检查是否有同名的Ui
                var path = GameConfig.UiPrefabDir + uiName + ".tscn";
                if (File.Exists(path))
                {
                    ShowTips("错误", "已经存在相同名称'" + uiName + "'的UI了, 不能重复创建!");
                    return;
                }
                
                //执行创建操作
                if (UiGenerator.CreateUi(uiName, true))
                {
                    ShowTips("提示", "创建UI成功!");
                }
                else
                {
                    ShowTips("错误", "创建UI失败! 前往控制台查看错误日志!");
                }
                
            }
        });
    }

    /// <summary>
    /// 更新 ResourcePath
    /// </summary>
    private void GenerateResourcePath()
    {
        if (ResourcePathGenerator.Generate())
        {
            ShowTips("提示", "ResourcePath.cs生成完成!");
        }
        else
        {
            ShowTips("错误", "ResourcePath.cs生成失败! 前往控制台查看错误日志!");
        }
    }

    /// <summary>
    /// 重新打包房间配置
    /// </summary>
    private void GenerateRoomConfig()
    {
        if (DungeonRoomGenerator.GenerateRoomConfig())
        {
            ShowTips("提示", "打包地牢房间配置执行完成!");
        }
        else
        {
            ShowTips("错误", "打包地牢房间配置执行失败! 前往控制台查看错误日志!");
        }
    }

    /// <summary>
    /// 重新生成UiManagerMethods.cs代码
    /// </summary>
    private void GenerateUiManagerMethods()
    {
        if (UiManagerMethodsGenerator.Generate())
        {
            ShowTips("提示", "生成UiManagerMethods.cs代码执行完成!");
        }
        else
        {
            ShowTips("错误", "生成UiManagerMethods.cs代码执行失败! 前往控制台查看错误日志!");
        }
    }

    /// <summary>
    /// 创建地牢房间
    /// </summary>
    private void GenerateDungeonRoom()
    {
        var roomName = L_ScrollContainer.L_MarginContainer.L_VBoxContainer.L_HBoxContainer6.L_LineEdit.Instance.Text;
        ShowConfirm("提示", "是否创建房间：" + roomName, (result) =>
        {
            if (result)
            {
                //检查名称是否合规
                if (!Regex.IsMatch(roomName, "^\\w*$"))
                {
                    ShowTips("错误", "房间名称'" + roomName + "'不符合名称约束, 房间名称只允许包含大写字母和数字!");
                    return;
                }

                //检查是否有同名的Ui
                var path = GameConfig.RoomTileDir + roomName + ".tscn";
                if (File.Exists(path))
                {
                    ShowTips("错误", "已经存在相同名称'" + roomName + "'的房间了, 不能重复创建!");
                    return;
                }
                
                //执行创建操作
                if (DungeonRoomGenerator.CreateDungeonRoom(roomName, true))
                {
                    ShowTips("提示", "创建房间成功!");
                }
                else
                {
                    ShowTips("错误", "创建房间失败! 前往控制台查看错误日志!");
                }
                
            }
        });
    }
}
