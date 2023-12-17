using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Godot;
using Environment = System.Environment;

#if TOOLS
using Generator;
#endif

namespace UI.EditorTools;

/// <summary>
/// Godot编辑器扩展工具
/// </summary>
[Tool]
public partial class EditorToolsPanel : EditorTools
{
    
#if TOOLS
    //Tips 关闭回调
    private Action _onTipsClose;

    //询问窗口关闭
    private Action<bool> _onConfirmClose;

    //存放创建房间中选择组的下拉框数据
    private Dictionary<int, string> _createRoomGroupValueMap;
    
    //存放创建房间中选择类型的下拉框数据
    private Dictionary<int, string> _createRoomTypeValueMap;

    public override void OnShowUi()
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
        //重新生成ui代码
        container.L_HBoxContainer4.L_Button.Instance.Pressed += OnGenerateCurrentUiCode;
        //创建ui
        container.L_HBoxContainer3.L_Button.Instance.Pressed += OnCreateUI;
        //重新生成UiManagerMethods.cs代码
        container.L_HBoxContainer5.L_Button.Instance.Pressed += GenerateUiManagerMethods;
        //导出excel表
        container.L_HBoxContainer7.L_Button.Instance.Pressed += ExportExcel;
        //打开excel表文件夹
        container.L_HBoxContainer8.L_Button.Instance.Pressed += OpenExportExcelFolder;
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
        container.L_HBoxContainer4.L_Button.Instance.Pressed -= OnGenerateCurrentUiCode;
        container.L_HBoxContainer3.L_Button.Instance.Pressed -= OnCreateUI;
        container.L_HBoxContainer5.L_Button.Instance.Pressed -= GenerateUiManagerMethods;
        container.L_HBoxContainer7.L_Button.Instance.Pressed -= ExportExcel;
        container.L_HBoxContainer8.L_Button.Instance.Pressed -= OpenExportExcelFolder;
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
        ShowTips(title, message, 350, 200, onClose);
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
        ShowConfirm(title, message, 350, 200, onClose);
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
    /// 导出excel表
    /// </summary>
    private void ExportExcel()
    {
        if (ExcelGenerator.ExportExcel())
        {
            ShowTips("提示", "导出Excel表成功！");
        }
        else
        {
            ShowTips("错误", "导出Excel表失败，请查看控制台日志！");
        }
    }

    /// <summary>
    /// 使用资源管理器打开excel表文件夹
    /// </summary>
    private void OpenExportExcelFolder()
    {
        var path = Environment.CurrentDirectory + "\\excel";
        System.Diagnostics.Process.Start("explorer.exe", path);
    }
    
    /// <summary>
    /// 在编辑器中打开一个提示窗口
    /// </summary>
    public static void ShowTipsInEditor(string title, string message, Action onClose)
    {
        var editorToolsInstance = UiManager.Get_EditorTools_Instance();
        if (editorToolsInstance.Length > 0)
        {
            editorToolsInstance[0].ShowTips(title, message, onClose);
        }
    }
    
    /// <summary>
    /// 在编辑器中打开一个询问窗口
    /// </summary>
    public static void ShowConfirmInEditor(string title, string message, Action<bool> onClose = null)
    {
        var editorToolsInstance = UiManager.Get_EditorTools_Instance();
        if (editorToolsInstance.Length > 0)
        {
            editorToolsInstance[0].ShowConfirm(title, message, onClose);
        }
    }
#else
    public override void OnShowUi()
    {
    }
    
    public override void OnHideUi()
    {
    }
#endif
}
