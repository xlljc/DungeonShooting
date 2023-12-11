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
        //验证该组是否满足生成地牢的条件
        var config = GameApplication.Instance.DungeonConfig;
        var result = DungeonManager.CheckDungeon(config.GroupName);
        if (result.HasError)
        {
            EditorWindowManager.ShowTips("警告", "当前组'" + config.GroupName + "'" + result.ErrorMessage + ", 不能生成地牢!");
        }
        else
        {
            GameApplication.Instance.DungeonManager.LoadDungeon(config);
            HideUi();
        }
    }

    //退出游戏
    private void OnExitClick()
    {
        GetTree().Quit();
    }

    //点击开发者工具
    private void OnToolsClick()
    {
        OpenNextUi(UiManager.UiNames.MapEditorProject);
    }

    //点击设置按钮
    private void OnSettingClick()
    {
        OpenNextUi(UiManager.UiNames.Setting);
    }
}
