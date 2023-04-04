using Godot;

namespace UI.RoomUI;

/// <summary>
/// 换弹进度组件
/// </summary>
public class ReloadBar
{
    private RoomUI.UiNode_ReloadBar _reloadBar;
    private int width;
    private float startX;

    public ReloadBar(RoomUI.UiNode_ReloadBar reloadBar)
    {
        reloadBar.Instance.Visible = false;
        _reloadBar = reloadBar;
        width = _reloadBar.L_Slot.Instance.Texture.GetWidth();
        startX = -(width - 3) / 2f;
    }
    
    public void OnShow()
    {
        GameCamera.Main.OnPositionUpdateEvent += OnCameraPositionUpdate;
    }

    public void OnHide()
    {
        GameCamera.Main.OnPositionUpdateEvent -= OnCameraPositionUpdate;
    }

    /// <summary>
    /// 隐藏换弹进度组件
    /// </summary>
    public void HideBar()
    {
        _reloadBar.Instance.Visible = false;
    }

    /// <summary>
    /// 显示换弹进度组件
    /// </summary>
    /// <param name="position">坐标</param>
    /// <param name="progress">进度, 0 - 1</param>
    public void ShowBar(Vector2 position, float progress)
    {
        _reloadBar.Instance.Visible = true;
        _reloadBar.Instance.GlobalPosition = GameApplication.Instance.ViewToGlobalPosition(position);
        progress = Mathf.Clamp(progress, 0, 1);
        _reloadBar.L_Slot.L_Block.Instance.Position = new Vector2(startX + (width - 3) * progress, 0);
    }

    /// <summary>
    /// 相机更新回调
    /// </summary>
    public void OnCameraPositionUpdate(float delta)
    {
        var player = Player.Current;
        if (player.Holster.ActiveWeapon != null && player.Holster.ActiveWeapon.Reloading)
        {
            ShowBar(player.GlobalPosition, 1 - player.Holster.ActiveWeapon.ReloadProgress);
        }
        else
        {
            HideBar();
        }
    }
}