
using System;

public class RegisterWeapon : RegisterActivity
{
    public WeaponAttribute WeaponAttribute { get; }
    
    public RegisterWeapon(string id, Type attribute) : base(id, null)
    {
        WeaponAttribute = (WeaponAttribute)Activator.CreateInstance(attribute);
        if (WeaponAttribute != null) PrefabPath = WeaponAttribute.WeaponPrefab;
    }
}