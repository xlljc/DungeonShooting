using Godot;

namespace UI.Settlement;

/// <summary>
/// 结算面板
/// </summary>
public partial class SettlementPanel : Settlement
{
    public override void OnCreateUi()
    {
        S_Restart.Instance.Pressed += OnRestartClick;
        S_ToMenu.Instance.Pressed += OnBackClick;

        if (GameApplication.Instance.DungeonManager.IsEditorMode) //在编辑器模式下打开的Ui
        {
            S_ToMenu.Instance.Text = "返回编辑器";
        }
    }
    
    //重新开始
    private void OnRestartClick()
    {
        Destroy();
        if (GameApplication.Instance.DungeonManager.IsEditorMode) //在编辑器模式下打开的Ui
        {
            EditorPlayManager.Restart();
        }
        else //正常重新开始
        {
            UiManager.Open_Loading();
            GameApplication.Instance.DungeonManager.RestartDungeon(false, GameApplication.Instance.DungeonConfig, () =>
            {
                UiManager.Destroy_Loading();
            });
        }
    }

    //回到上一级
    private void OnBackClick()
    {
        Destroy();
        if (GameApplication.Instance.DungeonManager.IsEditorMode) //在编辑器模式下打开的Ui
        {
            EditorPlayManager.Exit();
        }
        else //正常关闭Ui, 回到大厅
        {
            UiManager.Open_Loading();
            GameApplication.Instance.DungeonManager.ExitDungeon(false, () =>
            {
                GameApplication.Instance.DungeonManager.LoadHall(() =>
                {
                    UiManager.Destroy_Loading();
                });
            });
        }
    }

}
