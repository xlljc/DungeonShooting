using Godot;

namespace UI.Setting;

public partial class SettingPanel : Setting
{

    public override void OnCreateUi()
    {
        if (PrevUi != null)
        {
            //返回上一级UI
            L_Back.Instance.Pressed += () =>
            {
                OpenPrevUi();
            };
        }
        
        //视频设置
        S_VideoItem.Instance.Pressed += () =>
        {
            S_SettingMenu.Instance.Visible = false;
            S_VideoSetting.Instance.Visible = true;
        };
        //键位设置
        S_InputItem.Instance.Pressed += () =>
        {
            S_SettingMenu.Instance.Visible = false;
            S_KeySetting.Instance.Visible = true;
        };
        //视频设置返回
        S_VideoSetting.L_Back.Instance.Pressed += () =>
        {
            S_SettingMenu.Instance.Visible = true;
            S_VideoSetting.Instance.Visible = false;
        };
        //键位设置返回
        S_KeySetting.L_Back.Instance.Pressed += () =>
        {
            S_SettingMenu.Instance.Visible = true;
            S_KeySetting.Instance.Visible = false;
        };
        
        //---------------------- 视频设置 -----------------------------
        //全屏属性
        S_FullScreen.L_CheckBox.Instance.ButtonPressed = DisplayServer.WindowGetMode() == DisplayServer.WindowMode.Fullscreen;
        S_FullScreen.L_CheckBox.Instance.Pressed += OnChangeFullScreen;
        //-----------------------------------------------------------
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
