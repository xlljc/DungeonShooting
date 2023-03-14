using Godot;

namespace UI.RoomUI;

public class GunBar
{
    private RoomUI.UiNode15_GunBar _gunBar;
    private EventBinder _binder;

    private int _prevAmmo = -1;
    private int _prevResidue = -1;
    
    public GunBar(RoomUI.UiNode15_GunBar gunBar)
    {
        _gunBar = gunBar;
    }

    public void OnOpen()
    {
        _binder = EventManager.AddEventListener(EventEnum.OnPlayerRefreshWeaponTexture, OnPlayerRefreshWeaponTexture);
    }

    public void OnClose()
    {
        _binder.RemoveEventListener();
    }

    public void Process(float delta)
    {
        var weapon = Player.Current?.Holster.ActiveWeapon;
        if (weapon != null)
        {
            SetWeaponAmmunition(weapon.CurrAmmo, weapon.ResidueAmmo);
        }
    }

    /// <summary>
    /// 设置显示在 ui 上武器的纹理
    /// </summary>
    /// <param name="gun">纹理</param>
    public void SetWeaponTexture(Texture2D gun)
    {
        if (gun != null)
        {
            _gunBar.L_GunSprite.Instance.Texture = gun;
            _gunBar.L_GunSprite.Instance.Visible = true;
            _gunBar.L_BulletText.Instance.Visible = true;
        }
        else
        {
            _gunBar.L_GunSprite.Instance.Visible = false;
            _gunBar.L_BulletText.Instance.Visible = false;
        }
    }
    
    /// <summary>
    /// 设置弹药数据
    /// </summary>
    /// <param name="curr">当前弹夹弹药量</param>
    /// <param name="total">剩余弹药总数</param>
    public void SetWeaponAmmunition(int curr, int total)
    {
        if (curr != _prevAmmo || total != _prevResidue)
        {
            _gunBar.L_BulletText.Instance.Text = curr + " / " + total;
            _prevAmmo = curr;
            _prevResidue = total;
        }
    }

    private void OnPlayerRefreshWeaponTexture(object o)
    {
        if (o == null)
        {
            SetWeaponTexture(null);
        }
        else
        {
            SetWeaponTexture((Texture2D)o);
        }
    }
}