
using System.Collections;
using Godot;

namespace UI.RoomUI;

/// <summary>
/// 房间中的ui
/// </summary>
public partial class RoomUIPanel : RoomUI
{
    private ReloadBar _reloadBar;
    private InteractiveTipBar _interactiveTipBar;
    private HealthBar _healthBar;
    private GunBar _gunBar;

    public override void OnCreateUi()
    {
        _reloadBar = new ReloadBar(L_ReloadBar);
        _interactiveTipBar = new InteractiveTipBar(L_InteractiveTipBar);
        _healthBar = new HealthBar(L_Control.L_HealthBar);
        _gunBar = new GunBar(L_Control.L_GunBar);
    }

    public override void OnShowUi()
    {
        _reloadBar.OnShow();
        _interactiveTipBar.OnShow();
        _healthBar.OnShow();
        _gunBar.OnShow();
    }

    public override void OnHideUi()
    {
        _reloadBar.OnHide();
        _interactiveTipBar.OnHide();
        _healthBar.OnHide();
        _gunBar.OnHide();
    }

    public override void Process(float delta)
    {
        _gunBar.Process(delta);
    }
}