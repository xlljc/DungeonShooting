using Godot;

namespace UI.Main;

/// <summary>
/// 主菜单
/// </summary>
public partial class MainPanel : Main
{

    public override void OnShowUi()
    {
        S_Start.Instance.Pressed += OnStartGameClick;
        S_Exit.Instance.Pressed += OnExitClick;
        S_Tools.Instance.Pressed += OnToolsClick;
    }

    public override void OnHideUi()
    {
        S_Start.Instance.Pressed -= OnStartGameClick;
        S_Exit.Instance.Pressed -= OnExitClick;
        S_Tools.Instance.Pressed -= OnToolsClick;
    }
    
    //点击开始游戏
    private void OnStartGameClick()
    {
        GameApplication.Instance.DungeonManager.LoadDungeon(GameApplication.Instance.DungeonConfig);
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
        UiManager.Open_MapEditor();
        HideUi();
    }
}
