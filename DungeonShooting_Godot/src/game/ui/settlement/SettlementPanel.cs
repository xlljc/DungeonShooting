using Godot;

namespace UI.Settlement;

/// <summary>
/// 结算面板
/// </summary>
public partial class SettlementPanel : Settlement
{

    public override void OnShowUi()
    {
        L_ButtonList.L_Restart.Instance.Pressed += OnRestartClick;
        L_ButtonList.L_ToMenu.Instance.Pressed += OnToMenuClick;
    }

    public override void OnHideUi()
    {
        L_ButtonList.L_Restart.Instance.Pressed -= OnRestartClick;
        L_ButtonList.L_ToMenu.Instance.Pressed -= OnToMenuClick;
    }

    //重新开始
    private void OnRestartClick()
    {
        HideUi();
        GameApplication.Instance.DungeonManager.ExitDungeon(() =>
        {
            GameApplication.Instance.DungeonManager.LoadDungeon(GameApplication.Instance.DungeonConfig);
        });
    }

    //回到主菜单
    private void OnToMenuClick()
    {
        HideUi();
        GameApplication.Instance.DungeonManager.ExitDungeon(() =>
        {
            UiManager.Open_Main();
        });
    }

}
