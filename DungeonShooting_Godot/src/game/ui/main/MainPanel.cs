using Godot;

namespace UI.Main;

public partial class MainPanel : Main
{

    public override void OnShowUi()
    {
        L_ButtonList.L_Start.Instance.Pressed += OnStartGameClick;
        L_ButtonList.L_Exit.Instance.Pressed += OnExitClick;
    }

    public override void OnHideUi()
    {
        L_ButtonList.L_Start.Instance.Pressed -= OnStartGameClick;
        L_ButtonList.L_Exit.Instance.Pressed -= OnExitClick;
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
}
