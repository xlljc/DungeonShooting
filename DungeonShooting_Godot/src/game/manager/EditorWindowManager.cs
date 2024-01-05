
using System;
using System.Collections.Generic;
using Config;
using Godot;
using UI.EditorColorPicker;
using UI.EditorImportCombination;
using UI.EditorInput;
using UI.EditorTips;
using UI.EditorWindow;
using UI.MapEditorCreateGroup;
using UI.MapEditorCreateMark;
using UI.MapEditorCreatePreinstall;
using UI.MapEditorCreateRoom;
using UI.MapEditorSelectObject;

/// <summary>
/// 通用弹窗管理类
/// </summary>
public static class EditorWindowManager
{
    /// <summary>
    /// 打开颜色选择器弹窗
    /// </summary>
    /// <param name="position">位置</param>
    /// <param name="color">当前选中的颜色</param>
    /// <param name="onChangeColor">颜色改变时回调</param>
    /// <param name="onClose">关闭时回调</param>
    public static void ShowColorPicker(Vector2 position, Color color, ColorPicker.ColorChangedEventHandler onChangeColor, Action onClose = null)
    {
        var window = CreateWindowInstance();
        var colorPickerPanel = window.OpenBody<EditorColorPickerPanel>(UiManager.UiNames.EditorColorPicker);
        window.SetWindowTitle("颜色选择器");
        window.SetWindowSize(new Vector2I(298, 720));
        window.S_Window.Instance.Position = new Vector2I((int)(position.X - 298f * 0.5f), (int)(position.Y + 80));
        colorPickerPanel.S_ColorPicker.Instance.Color = color;
        colorPickerPanel.S_ColorPicker.Instance.ColorChanged += onChangeColor;
        if (onClose != null)
        {
            window.CloseEvent += onClose;
        }
    }

    /// <summary>
    /// 显示打开文件窗口
    /// </summary>
    /// <param name="filters">过滤文件后缀</param>
    /// <param name="onClose">关闭回调, 回调参数为选择的文件路径, 如果选择文件, 则回调参数为null</param>
    public static void ShowOpenFileDialog(string[] filters, Action<string> onClose)
    {
        //UiManager.Open_EditorFileDialog();
        var fileDialog = new FileDialog();
        fileDialog.UseNativeDialog = true;
        fileDialog.ModeOverridesTitle = false;
        fileDialog.FileMode = FileDialog.FileModeEnum.OpenFile;
        fileDialog.Access = FileDialog.AccessEnum.Filesystem;
        fileDialog.Filters = filters;
        fileDialog.FileSelected += (path) =>
        {
            onClose(path);
            fileDialog.QueueFree();
        };
        fileDialog.Canceled += () =>
        {
            onClose(null);
            fileDialog.QueueFree();
        };
        fileDialog.Confirmed += () =>
        {
            onClose(null);
            fileDialog.QueueFree();
        };
        UiManager.GetUiLayer(UiLayer.Pop).AddChild(fileDialog);
        fileDialog.Popup();
    }
    
    /// <summary>
    /// 弹出通用提示面板
    /// </summary>
    /// <param name="title">标题</param>
    /// <param name="message">显示内容</param>
    /// <param name="onClose">关闭时的回调</param>
    /// <param name="parentUi">所属父级Ui</param>
    public static void ShowTips(string title, string message, Action onClose = null, UiBase parentUi = null)
    {
        var window = CreateWindowInstance(parentUi);
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
        var body = window.OpenBody<EditorTipsPanel>(UiManager.UiNames.EditorTips);
        body.SetMessage(message);
    }

    /// <summary>
    /// 弹出询问窗口
    /// </summary>
    /// <param name="title">标题</param>
    /// <param name="message">显示内容</param>
    /// <param name="onClose">关闭时的回调, 参数如果为 true 表示点击了确定</param>
    /// <param name="parentUi">所属父级Ui</param>
    public static void ShowConfirm(string title, string message, Action<bool> onClose, UiBase parentUi = null)
    {
        var window = CreateWindowInstance(parentUi);
        window.SetWindowTitle(title);
        window.CloseEvent += () =>
        {
            onClose(false);
        };
        window.SetButtonList(
            new EditorWindowPanel.ButtonData("确定", () =>
            {
                window.CloseWindow(false);
                onClose(true);
            }),
            new EditorWindowPanel.ButtonData("取消", () =>
            {
                window.CloseWindow();
            })
        );
        var body = window.OpenBody<EditorTipsPanel>(UiManager.UiNames.EditorTips);
        body.SetMessage(message);
    }

    /// <summary>
    /// 弹出通用输入框
    /// </summary>
    /// <param name="title">标题</param>
    /// <param name="message">显示内容</param>
    /// <param name="value">输入框默认值</param>
    /// <param name="onClose">关闭时回调，参数1为输入框内容，参数2为 true 表示点击了确定，如果点击了确定但是回调函数返回 false 则不会关闭窗口</param>
    /// <param name="parentUi">所属父级Ui</param>
    public static void ShowInput(string title, string message, string value, Func<string, bool, bool> onClose, UiBase parentUi = null)
    {
        var window = CreateWindowInstance(parentUi);
        window.SetWindowTitle(title);
        var body = window.OpenBody<EditorInputPanel>(UiManager.UiNames.EditorInput);
        window.CloseEvent += () =>
        {
            onClose(body.GetValue(), false);
        };
        window.SetButtonList(
            new EditorWindowPanel.ButtonData("确定", () =>
            {
                if (onClose(body.GetValue(), true))
                {
                    window.CloseWindow(false);
                }
            }),
            new EditorWindowPanel.ButtonData("取消", () =>
            {
                window.CloseWindow();
            })
        );
        body.Init(message, value);
    }
    
    /// <summary>
    /// 弹出询问窗口, 包含3个按钮
    /// </summary>
    /// <param name="title">标题</param>
    /// <param name="message">显示内容</param>
    /// <param name="btn1">按钮1文本</param>
    /// <param name="btn2">按钮2文本</param>
    /// <param name="btn3">按钮3文本</param>
    /// <param name="onClose">关闭时的回调, 参数如果为点击按钮的索引表示点击了确定, -1表示点击了x</param>
    /// <param name="parentUi">所属父级Ui</param>
    public static void ShowConfirm(string title, string message, string btn1, string btn2, string btn3, Action<int> onClose, UiBase parentUi = null)
    {
        var window = CreateWindowInstance(parentUi);
        window.SetWindowTitle(title);
        window.CloseEvent += () =>
        {
            onClose(-1);
        };
        window.SetButtonList(
            new EditorWindowPanel.ButtonData(btn1, () =>
            {
                window.CloseWindow(false);
                onClose(0);
            }),
            new EditorWindowPanel.ButtonData(btn2, () =>
            {
                window.CloseWindow(false);
                onClose(1);
            }),
            new EditorWindowPanel.ButtonData(btn3, () =>
            {
                window.CloseWindow(false);
                onClose(2);
            })
        );
        var body = window.OpenBody<EditorTipsPanel>(UiManager.UiNames.EditorTips);
        body.SetMessage(message);
    }

    /// <summary>
    /// 打开创建地牢组弹窗
    /// </summary>
    /// <param name="onCreateGroup">创建成功时回调</param>
    /// <param name="parentUi">所属父级Ui</param>
    public static void ShowCreateGroup(Action<DungeonRoomGroup> onCreateGroup, UiBase parentUi = null)
    {
        var window = CreateWindowInstance(parentUi);
        window.SetWindowTitle("创建地牢组");
        window.SetWindowSize(new Vector2I(700, 500));
        var body = window.OpenBody<MapEditorCreateGroupPanel>(UiManager.UiNames.MapEditorCreateGroup);
        window.SetButtonList(
            new EditorWindowPanel.ButtonData("确定", () =>
            {
                //获取填写的数据, 并创建ui
                var groupInfo = body.GetGroupInfo();
                if (groupInfo != null)
                {
                    window.CloseWindow();
                    onCreateGroup(groupInfo);
                }
            }),
            new EditorWindowPanel.ButtonData("取消", () =>
            {
                window.CloseWindow();
            })
        );
    }
    
    /// <summary>
    /// 打开创建地牢房间弹窗
    /// </summary>
    /// <param name="groupName">选择的组名称, 如果不需要有选择的项, 则传 null</param>
    /// <param name="roomType">选择的房间类型</param>
    /// <param name="onCreateRoom">创建成功时回调</param>
    public static void ShowCreateRoom(string groupName, int roomType, Action<DungeonRoomSplit> onCreateRoom)
    {
        var window = UiManager.Open_EditorWindow();
        window.SetWindowTitle("创建地牢房间");
        window.SetWindowSize(new Vector2I(700, 600));
        var body = window.OpenBody<MapEditorCreateRoomPanel>(UiManager.UiNames.MapEditorCreateRoom);
        if (groupName != null)
        {
            body.SetSelectGroup(groupName);
        }
        body.SetSelectType(roomType);
        
        window.SetButtonList(
            new EditorWindowPanel.ButtonData("确定", () =>
            {
                //获取填写的数据, 并创建ui
                var roomSplit = body.GetRoomInfo();
                if (roomSplit != null)
                {
                    window.CloseWindow();
                    onCreateRoom(roomSplit);
                }
            }),
            new EditorWindowPanel.ButtonData("取消", () =>
            {
                window.CloseWindow();
            })
        );
    }

    /// <summary>
    /// 编辑地牢房间
    /// </summary>
    /// <param name="roomSplit">原数据</param>
    /// <param name="onSave">保存时回调</param>
    public static void ShowEditRoom(DungeonRoomSplit roomSplit, Action<DungeonRoomSplit> onSave)
    {
        var window = UiManager.Open_EditorWindow();
        window.SetWindowTitle("编辑地牢房间");
        window.SetWindowSize(new Vector2I(700, 600));
        var body = window.OpenBody<MapEditorCreateRoomPanel>(UiManager.UiNames.MapEditorCreateRoom);
        body.InitEditData(roomSplit);
        
        window.SetButtonList(
            new EditorWindowPanel.ButtonData("确定", () =>
            {
                //获取填写的数据, 并创建ui
                var saveData = body.GetRoomInfo();
                if (saveData != null)
                {
                    window.CloseWindow();
                    onSave(saveData);
                }
            }),
            new EditorWindowPanel.ButtonData("取消", () =>
            {
                window.CloseWindow();
            })
        );
    }

    /// <summary>
    /// 打开创建房间预设弹窗
    /// </summary>
    /// <param name="roomType">当前房间的类型</param>
    /// <param name="list">当前房间已经包含的所有预设列表</param>
    /// <param name="onCreatePreinstall">创建成功时的回调</param>
    public static void ShowCreatePreinstall(DungeonRoomType roomType, List<RoomPreinstallInfo> list, Action<RoomPreinstallInfo> onCreatePreinstall)
    {
        var window = UiManager.Open_EditorWindow();
        window.SetWindowTitle("创建房间预设");
        window.SetWindowSize(new Vector2I(700, 600));
        var body = window.OpenBody<MapEditorCreatePreinstallPanel>(UiManager.UiNames.MapEditorCreatePreinstall);
        body.InitData(roomType);
        window.SetButtonList(
            new EditorWindowPanel.ButtonData("确定", () =>
            {
                var roomPreinstall = body.GetRoomPreinstall(list);
                if (roomPreinstall != null)
                {
                    window.CloseWindow();
                    onCreatePreinstall(roomPreinstall);
                }
            }),
            new EditorWindowPanel.ButtonData("取消", () =>
            {
                window.CloseWindow();
            })
        );
    }

    /// <summary>
    /// 打开编辑房间预设弹窗
    /// </summary>
    /// <param name="roomType">当前房间的类型</param>
    /// <param name="list">当前房间已经包含的所有预设列表</param>
    /// <param name="preinstallInfo">需要编辑的预设数据</param>
    /// <param name="onSavePreinstall">保存时的回调</param>
    public static void ShowEditPreinstall(DungeonRoomType roomType, List<RoomPreinstallInfo> list, RoomPreinstallInfo preinstallInfo, Action<RoomPreinstallInfo> onSavePreinstall)
    {
        var window = UiManager.Open_EditorWindow();
        window.SetWindowTitle("创建房间预设");
        window.SetWindowSize(new Vector2I(700, 600));
        var body = window.OpenBody<MapEditorCreatePreinstallPanel>(UiManager.UiNames.MapEditorCreatePreinstall);
        body.InitData(roomType, preinstallInfo);
        window.SetButtonList(
            new EditorWindowPanel.ButtonData("确定", () =>
            {
                var roomPreinstall = body.GetRoomPreinstall(list);
                if (roomPreinstall != null)
                {
                    window.CloseWindow();
                    onSavePreinstall(roomPreinstall);
                }
            }),
            new EditorWindowPanel.ButtonData("取消", () =>
            {
                window.CloseWindow();
            })
        );
    }

    /// <summary>
    /// 打开创建标记页面
    /// </summary>
    /// <param name="position">初始坐标</param>
    /// <param name="preloading">是否提前加载</param>
    /// <param name="onCreateMarkInfo">创建标记回调</param>
    /// <param name="parentUi">所属父级Ui</param>
    public static void ShowCreateMark(Vector2I position, bool preloading, Action<MarkInfo> onCreateMarkInfo , UiBase parentUi = null)
    {
        var window = CreateWindowInstance(parentUi);
        window.SetWindowTitle("创建标记");
        window.SetWindowSize(new Vector2I(1400, 900));
        var body = window.OpenBody<MapEditorCreateMarkPanel>(UiManager.UiNames.MapEditorCreateMark);
        body.InitData(position, preloading);
        window.SetButtonList(
            new EditorWindowPanel.ButtonData("确定", () =>
            {
                var markInfo = body.GetMarkInfo();
                if (markInfo != null)
                {
                    window.CloseWindow();
                    onCreateMarkInfo(markInfo);
                }
            }),
            new EditorWindowPanel.ButtonData("取消", () =>
            {
                window.CloseWindow();
            })
        );
    }

    /// <summary>
    /// 打开编辑标记页面
    /// </summary>
    /// <param name="data">标记数据对象</param>
    /// <param name="preloading">是否提前加载</param>
    /// <param name="onSaveMarkInfo">保存时回调</param>
    /// <param name="parentUi">所属父级Ui</param>
    public static void ShowEditMark(MarkInfo data, bool preloading, Action<MarkInfo> onSaveMarkInfo, UiBase parentUi = null)
    {
        var window = CreateWindowInstance(parentUi);
        window.SetWindowTitle("编辑标记");
        window.SetWindowSize(new Vector2I(1400, 900));
        var body = window.OpenBody<MapEditorCreateMarkPanel>(UiManager.UiNames.MapEditorCreateMark);
        body.InitData(data, preloading);
        window.SetButtonList(
            new EditorWindowPanel.ButtonData("确定", () =>
            {
                var markInfo = body.GetMarkInfo();
                if (markInfo != null)
                {
                    window.CloseWindow();
                    onSaveMarkInfo(markInfo);
                }
            }),
            new EditorWindowPanel.ButtonData("取消", () =>
            {
                window.CloseWindow();
            })
        );
    }

    /// <summary>
    /// 打开选中的物体
    /// </summary>
    /// <param name="findType">查找的类型, 如果为 none, 则查找所有类型数据</param>
    /// <param name="onSelectObject">选中物体时回调</param>
    /// <param name="parentUi">所属父级Ui</param>
    public static void ShowSelectObject(ActivityType findType, Action<ExcelConfig.ActivityBase> onSelectObject, UiBase parentUi = null)
    {
        var window = CreateWindowInstance(parentUi);
        window.S_Window.Instance.Size = new Vector2I(1000, 700);
        window.SetWindowTitle("选择物体");
        var body = window.OpenBody<MapEditorSelectObjectPanel>(UiManager.UiNames.MapEditorSelectObject);
        //设置显示的物体类型
        body.SetShowType(findType);
        window.SetButtonList(
            new EditorWindowPanel.ButtonData("确定", () =>
            {
                var selectObject = body.GetSelectData();
                if (selectObject == null)
                {
                    ShowTips("提示", "您未选择任何物体");
                }
                else
                {
                    window.CloseWindow();
                    onSelectObject(selectObject);
                }
            }),
            new EditorWindowPanel.ButtonData("取消", () =>
            {
                window.CloseWindow();
            })
        );
        //绑定双击选中物体事件
        body.SelectObjectEvent += selectObject =>
        {
            window.CloseWindow();
            onSelectObject(selectObject);
        };
    }

    /// <summary>
    /// 显示导入组合确认弹窗
    /// </summary>
    /// <param name="showName">组合名称</param>
    /// <param name="color">预览纹理背景颜色</param>
    /// <param name="texture">显示纹理</param>
    /// <param name="onAccept">确定时回调</param>
    /// <param name="onCancel">取消时回调</param>
    /// <param name="parentUi">所属父级Ui</param>
    public static void ShowImportCombination(string showName, Color color, Texture2D texture, Action<string> onAccept, Action onCancel, UiBase parentUi = null)
    {
        var window = CreateWindowInstance(parentUi);
        window.S_Window.Instance.Size = new Vector2I(750, 650);
        window.SetWindowTitle("导入组合");
        var body = window.OpenBody<EditorImportCombinationPanel>(UiManager.UiNames.EditorImportCombination);
        body.InitData(showName, color, texture);
        var accept = false;
        if (onCancel != null)
        {
            window.CloseEvent += () =>
            {
                if (!accept) onCancel();
            };
        }

        window.SetButtonList(
            new EditorWindowPanel.ButtonData("确定", () =>
            {
                accept = true;
                var selectObject = body.GetName();
                window.CloseWindow();
                onAccept(selectObject);
            }),
            new EditorWindowPanel.ButtonData("取消", () =>
            {
                window.CloseWindow();
            })
        );
    }
    
    /// <summary>
    /// 显示编辑组合弹窗
    /// </summary>
    /// <param name="showName">组合名称</param>
    /// <param name="color">预览纹理背景颜色</param>
    /// <param name="texture">显示纹理</param>
    /// <param name="onAccept">确定时回调</param>
    /// <param name="onDelete">删除时回调</param>
    /// <param name="parentUi">所属父级Ui</param>
    public static void ShowEditCombination(string showName, Color color, Texture2D texture, Action<string> onAccept, Action onDelete, UiBase parentUi = null)
    {
        var window = CreateWindowInstance(parentUi);
        window.S_Window.Instance.Size = new Vector2I(750, 650);
        window.SetWindowTitle("编辑组合");
        var body = window.OpenBody<EditorImportCombinationPanel>(UiManager.UiNames.EditorImportCombination);
        body.InitData(showName, color, texture);
        window.SetButtonList(
            new EditorWindowPanel.ButtonData("删除", () =>
            {
                ShowConfirm("提示", "您确定要删除该组合吗，该操作不能取消！", (flag) =>
                {
                    if (flag)
                    {
                        window.CloseWindow();
                        onDelete();
                    }
                }, window);
            }),
            new EditorWindowPanel.ButtonData("保存", () =>
            {
                var selectObject = body.GetName();
                window.CloseWindow();
                onAccept(selectObject);
            }),
            new EditorWindowPanel.ButtonData("取消", () =>
            {
                window.CloseWindow();
            })
        );
    }
    
    private static EditorWindowPanel CreateWindowInstance(UiBase parentUi = null)
    {
        if (parentUi != null)
        {
            return parentUi.OpenNestedUi<EditorWindowPanel>(UiManager.UiNames.EditorWindow);
        }

        return UiManager.Open_EditorWindow();
    }
}