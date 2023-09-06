using Godot;

namespace UI.RoomUI;

public class WeaponBar
{
    private RoomUI.RoomUI_WeaponBar _weaponBar;

    private int _prevAmmo = -1;
    private int _prevResidue = -1;
    
    public WeaponBar(RoomUI.RoomUI_WeaponBar weaponBar)
    {
        _weaponBar = weaponBar;
        SetWeaponTexture(null);
    }

    public void OnShow()
    {
    }

    public void OnHide()
    {
    }

    public void Process(float delta)
    {
        var weapon = Player.Current?.WeaponPack.ActiveItem;
        if (weapon != null)
        {
            SetWeaponTexture(weapon.GetCurrentTexture());
            SetWeaponAmmunition(weapon.CurrAmmo, weapon.ResidueAmmo);
        }
        else
        {
            SetWeaponTexture(null);
        }
    }

    /// <summary>
    /// 设置显示在 ui 上武器的纹理
    /// </summary>
    /// <param name="texture">纹理</param>
    public void SetWeaponTexture(Texture2D texture)
    {
        if (texture != null)
        {
            _weaponBar.L_WeaponPanel.L_WeaponSprite.Instance.Texture = texture;
            _weaponBar.Instance.Visible = true;
        }
        else
        {
            _weaponBar.Instance.Visible = false;
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
            _weaponBar.L_AmmoCount.Instance.Text = curr + " / " + total;
            _prevAmmo = curr;
            _prevResidue = total;
        }
    }
}