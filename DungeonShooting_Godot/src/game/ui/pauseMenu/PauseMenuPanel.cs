using Godot;

namespace UI.PauseMenu;

public partial class PauseMenuPanel : PauseMenu
{

    public override void OnCreateUi()
    {
        S_Continue.Instance.Pressed += OnContinueClick;
        S_Setting.Instance.Pressed += OnSettingClick;
        S_Encyclopedia.Instance.Pressed += OnEncyclopediaClick;
        S_Restart.Instance.Pressed += OnRestartClick;
        S_Exit.Instance.Pressed += OnExitClick;
        
        if (GameApplication.Instance.DungeonManager.IsEditorMode) //在编辑器模式下打开的Ui
        {
            S_Exit.Instance.Text = "返回编辑器";
        }
        else if (World.Current is Dungeon) //在游戏地牢中
        {
            S_Exit.Instance.Text = "退出地牢";
        }
        else //在大厅中
        {
            S_Restart.Instance.Visible = false;
        }
    }

    public override void Process(float delta)
    {
        if (Input.IsActionJustPressed("ui_cancel")) //返回游戏
        {
            OnContinueClick();
        }
    }

    //继续游戏
    private void OnContinueClick()
    {
        World.Current.Pause = false;
        GameApplication.Instance.Cursor.SetGuiMode(false);
        Destroy();
    }

    //设置
    private void OnSettingClick()
    {
        OpenNextUi(UiManager.UiNames.Setting);
    }

    //图鉴
    private void OnEncyclopediaClick()
    {
        OpenNextUi(UiManager.UiNames.Encyclopedia);
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

    //退出地牢
    private void OnExitClick()
    {
        Destroy();
        if (GameApplication.Instance.DungeonManager.IsEditorMode) //在编辑器模式下打开的Ui
        {
            EditorPlayManager.Exit();
        }
        else if (World.Current is Dungeon) //在游戏地牢中
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
        else //在大厅中
        {
            UiManager.Open_Loading();
            GameApplication.Instance.DungeonManager.ExitHall(false, () =>
            {
                UiManager.Destroy_Loading();
                UiManager.Open_Main();
            });
        }
    }
}
