
using Godot;

namespace UI.RoomUI;

/// <summary>
/// 房间中的ui
/// </summary>
public partial class RoomUIPanel : RoomUI
{
    private ReloadBar _reloadBar;
    private InteractiveTipBar _interactiveTipBar;
    private GunBar _gunBar;
    private LifeBar _lifeBar;

    public override void OnCreateUi()
    {
        _reloadBar = new ReloadBar(L_ReloadBar);
        _interactiveTipBar = new InteractiveTipBar(L_InteractiveTipBar);
        _gunBar = new GunBar(L_Control.L_GunBar);
        _lifeBar = new LifeBar(L_Control.L_LifeBar);
    }

    public override void OnShowUi()
    {
        _reloadBar.OnShow();
        _interactiveTipBar.OnShow();
        _gunBar.OnShow();
        _lifeBar.OnShow();
    }

    public override void OnHideUi()
    {
        _reloadBar.OnHide();
        _interactiveTipBar.OnHide();
        _gunBar.OnHide();
        _lifeBar.OnHide();
    }

    public void InitData()
    {
        _lifeBar.RefreshLife();
    }

    public override void Process(float delta)
    {
        _gunBar.Process(delta);
        _lifeBar.Process(delta);
    }
}