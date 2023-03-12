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

    public override void OnCreate()
    {
        //Generator.UiGenerator.GenerateUi(this, "src/game/ui/roomUI/RoomUI.cs");
        _reloadBar = new ReloadBar(L_ReloadBar);
        _interactiveTipBar = new InteractiveTipBar(L_InteractiveTipBar);
        _healthBar = new HealthBar(L_Control.L_HealthBar);
        _gunBar = new GunBar(L_Control.L_GunBar);
    }

    public override void OnOpen(params object[] args)
    {
        _reloadBar.OnOpen();
        _interactiveTipBar.OnOpen();
        _healthBar.OnOpen();
        _gunBar.OnOpen();
    }

    public override void OnClose()
    {
        _reloadBar.OnClose();
        _interactiveTipBar.OnClose();
        _healthBar.OnClose();
        _gunBar.OnClose();
    }

    public override void _Process(double delta)
    {
        _gunBar.Process((float) delta);
    }
}