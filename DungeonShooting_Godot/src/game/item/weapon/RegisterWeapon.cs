
using System;

/// <summary>
/// 注册武器
/// </summary>
public class RegisterWeapon : RegisterActivity
{
    public WeaponAttribute WeaponAttribute { get; }
    
    public RegisterWeapon(string id, Type attribute) : base(id, null)
    {
        WeaponAttribute = (WeaponAttribute)Activator.CreateInstance(attribute);
        if (WeaponAttribute != null) PrefabPath = WeaponAttribute.WeaponPrefab;
    }

    public override void CustomHandler(ActivityObject instance)
    {
        if (instance is Weapon weapon)
        {
            weapon.InitWeapon(WeaponAttribute.Clone());
        }
    }
}