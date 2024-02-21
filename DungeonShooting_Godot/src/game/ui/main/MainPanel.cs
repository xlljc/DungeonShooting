using Godot;

namespace UI.Main;

/// <summary>
/// 主菜单
/// </summary>
public partial class MainPanel : Main
{

    public override void OnCreateUi()
    {
        S_Start.Instance.Pressed += OnStartGameClick;
        S_Exit.Instance.Pressed += OnExitClick;
        S_Tools.Instance.Pressed += OnToolsClick;
        S_Setting.Instance.Pressed += OnSettingClick;
    }
    
    //点击开始游戏
    private void OnStartGameClick()
    {
        GameApplication.Instance.DungeonManager.LoadHall();
        HideUi();
    }

    //退出游戏
    private void OnExitClick()
    {
        GetTree().Quit();
    }

    //点击开发者工具
    private void OnToolsClick()
    {
        OpenNextUi(UiManager.UiNames.EditorManager);
    }

    //点击设置按钮
    private void OnSettingClick()
    {
        OpenNextUi(UiManager.UiNames.Setting);
    }
}
