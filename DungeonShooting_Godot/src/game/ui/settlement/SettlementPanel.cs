using Godot;

namespace UI.Settlement;

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

    private void OnRestartClick()
    {
        
    }

    private void OnToMenuClick()
    {
        HideUi();
        GameApplication.Instance.DungeonManager.ExitDungeon();
        UiManager.Open_Main();
    }

}
