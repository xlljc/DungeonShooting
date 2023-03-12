using System;
using Godot;

namespace UI.RoomUI;

public class HealthBar
{
    private RoomUI.UiNode9_HealthBar _healthBar;
    // 当前血量
    private int _hp;
    // 最大血量
    private int _maxHp;
    // 当前护盾值
    private int _shield;
    // 最大护盾值
    private int _maxShield;

    private EventFactory _eventFactory;
    
    public HealthBar(RoomUI.UiNode9_HealthBar healthBar)
    {
        _healthBar = healthBar;
    }

    public void OnOpen()
    {
        _eventFactory = EventManager.CreateEventFactory();
        _eventFactory.AddEventListener(EventEnum.OnPlayerHpChange, OnPlayerHpChange);
        _eventFactory.AddEventListener(EventEnum.OnPlayerMaxHpChange, OnPlayerMaxHpChange);
        _eventFactory.AddEventListener(EventEnum.OnPlayerShieldChange, OnPlayerShieldChange);
        _eventFactory.AddEventListener(EventEnum.OnPlayerMaxShieldChange, OnPlayerMaxShieldChange);
    }

    public void OnClose()
    {
        _eventFactory.RemoveAllEventListener();
    }

    /// <summary>
    /// 设置最大血量
    /// </summary>
    public void SetMaxHp(int maxHp)
    {
        _maxHp = Mathf.Max(maxHp, 0);
        _healthBar.L_HpSlot.Instance.Size = new Vector2(maxHp + 3, _healthBar.L_HpSlot.Instance.Size.Y);
        if (_hp > maxHp)
        {
            SetHp(maxHp);
        }
    }

    /// <summary>
    /// 设置最大护盾值
    /// </summary>
    public void SetMaxShield(int maxShield)
    {
        _maxShield = Mathf.Max(maxShield, 0);
        _healthBar.L_ShieldSlot.Instance.Size = new Vector2(maxShield + 2, _healthBar.L_ShieldSlot.Instance.Size.Y);
        if (_shield > _maxShield)
        {
            SetShield(maxShield);
        }
    }

    /// <summary>
    /// 设置当前血量
    /// </summary>
    public void SetHp(int hp)
    {
        _hp = Mathf.Clamp(hp, 0, _maxHp);
        var textureRect = _healthBar.L_HpSlot.L_HpBar.Instance;
        textureRect.Size = new Vector2(hp, textureRect.Size.Y);
    }

    /// <summary>
    /// 设置护盾值
    /// </summary>
    public void SetShield(int shield)
    {
        _shield = Mathf.Clamp(shield, 0, _maxShield);
        var textureRect = _healthBar.L_ShieldSlot.L_ShieldBar.Instance;
        textureRect.Size = new Vector2(shield, textureRect.Size.Y);
    }

    private void OnPlayerHpChange(object o)
    {
        SetHp(Convert.ToInt32(o));
    }

    private void OnPlayerMaxHpChange(object o)
    {
        SetMaxHp(Convert.ToInt32(o));
    }

    private void OnPlayerShieldChange(object o)
    {
        SetShield(Convert.ToInt32(o));
    }
    
    private void OnPlayerMaxShieldChange(object o)
    {
        SetMaxShield(Convert.ToInt32(o));
    }
}