
using System;

/// <summary>
/// 注册武器
/// </summary>
public class RegisterWeapon : RegisterActivity
{
    /// <summary>
    /// 武器属性
    /// </summary>
    private readonly WeaponAttribute _weaponAttribute;
    
    public RegisterWeapon(string id, Type attribute) : base(id, null)
    {
        _weaponAttribute = (WeaponAttribute)Activator.CreateInstance(attribute);
        if (_weaponAttribute != null) PrefabPath = _weaponAttribute.WeaponPrefab;
    }

    public override void CustomHandler(ActivityObject instance)
    {
        if (instance is Weapon weapon)
        {
            weapon.InitWeapon(_weaponAttribute.Clone());
        }
    }
}