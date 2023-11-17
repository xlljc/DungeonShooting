
using Godot;
using UI.BottomTips;

namespace UI.RoomUI;

/// <summary>
/// 地牢房间中的ui
/// </summary>
public partial class RoomUIPanel : RoomUI
{
    private ReloadBar _reloadBar;
    private InteractiveTipBar _interactiveTipBar;
    private WeaponBar _weaponBar;
    private ActivePropBar _activePropBar;
    private LifeBar _lifeBar;
    
    private EventFactory _factory;

    public override void OnCreateUi()
    {
        _reloadBar = new ReloadBar(L_ReloadBar);
        _interactiveTipBar = new InteractiveTipBar(L_InteractiveTipBar);
        _weaponBar = new WeaponBar(L_Control.L_WeaponBar);
        _activePropBar = new ActivePropBar(L_Control.L_ActivePropBar);
        _lifeBar = new LifeBar(L_Control.L_LifeBar);
    }

    public override void OnShowUi()
    {
        _reloadBar.OnShow();
        _interactiveTipBar.OnShow();
        _weaponBar.OnShow();
        _activePropBar.OnShow();
        _lifeBar.OnShow();

        _factory = EventManager.CreateEventFactory();
        _factory.AddEventListener(EventEnum.OnPlayerPickUpProp, OnPlayerPickUpProp);
    }

    public override void OnHideUi()
    {
        _reloadBar.OnHide();
        _interactiveTipBar.OnHide();
        _weaponBar.OnHide();
        _activePropBar.OnHide();
        _lifeBar.OnHide();
        
        _factory.RemoveAllEventListener();
        _factory = null;
    }

    public override void Process(float delta)
    {
        _weaponBar.Process(delta);
        _activePropBar.Process(delta);
        _lifeBar.Process(delta);
        
        QueueRedraw();
    }

    //玩家拾起道具, 弹出提示
    private void OnPlayerPickUpProp(object propObj)
    {
        var prop = (Prop)propObj;
        var message = $"{prop.ActivityBase.Name}\n{prop.ActivityBase.Intro}";
        BottomTipsPanel.ShowTips(prop.GetDefaultTexture(), message);
    }

    public override void _Draw()
    {
        foreach (var role in World.Current.Enemy_InstanceList)
        {
            if (!role.IsDestroyed && role is AdvancedEnemy advancedEnemy)
            {
                var position = GameApplication.Instance.ViewToGlobalPosition(advancedEnemy.Position);
                DrawString(ResourceManager.DefaultFont16Px, position, advancedEnemy.StateController.CurrState.ToString());
            }
        }
    }
}