using Godot;

namespace UI.PauseMenu;

public partial class PauseMenuPanel : PauseMenu
{

    public override void OnCreateUi()
    {
        S_Continue.Instance.Pressed += OnContinueClick;
        S_Restart.Instance.Pressed += OnRestartClick;
        S_Exit.Instance.Pressed += OnExitClick;
        
        if (GameApplication.Instance.DungeonManager.IsEditorMode) //在编辑器模式下打开的Ui
        {
            S_Exit.Instance.Text = "返回编辑器";
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
            GameApplication.Instance.DungeonManager.RestartDungeon(GameApplication.Instance.DungeonConfig);
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
        else //正常关闭Ui
        {
            GameApplication.Instance.DungeonManager.ExitDungeon(() =>
            {
                UiManager.Open_Main();
            });
        }
    }
}
