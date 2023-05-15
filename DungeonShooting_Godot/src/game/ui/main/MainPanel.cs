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
        var config = new DungeonConfig();
        config.GroupName = "testGroup";
        config.RoomCount = 2;
        GameApplication.Instance.DungeonManager.LoadDungeon(config);
        HideUi();
    }

    //退出游戏
    private void OnExitClick()
    {
        GetTree().Quit();
    }
}
