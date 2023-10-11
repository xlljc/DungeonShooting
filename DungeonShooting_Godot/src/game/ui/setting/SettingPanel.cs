using Godot;

namespace UI.Setting;

public partial class SettingPanel : Setting
{

    public override void OnCreateUi()
    {
        if (PrevUi != null)
        {
            //返回上一级UI
            S_Back.Instance.Pressed += () =>
            {
                OpenPrevUi();
            };
        }
        
        //全屏属性
        S_FullScreen.L_CheckBox.Instance.ButtonPressed = DisplayServer.WindowGetMode() == DisplayServer.WindowMode.Fullscreen;
        S_FullScreen.L_CheckBox.Instance.Pressed += OnChangeFullScreen;
    }

    public override void OnDestroyUi()
    {
        
    }

    //切换全屏/非全屏
    private void OnChangeFullScreen()
    {
        var checkBox = S_FullScreen.L_CheckBox.Instance;
        if (checkBox.ButtonPressed)
        {
            DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen);
        }
        else
        {
            DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);
        }
    }

}
