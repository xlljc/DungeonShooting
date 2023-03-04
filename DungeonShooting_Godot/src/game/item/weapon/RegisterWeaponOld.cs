using System;

/// <summary>
/// 用作 Weapon 子类上, 用于注册武器, 允许同时存在多个 RegisterWeapon 特性
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterWeaponOld : Attribute
{
    public string Id { get; private set; }
    public Type AttributeType { get; private set; }

    public RegisterWeaponOld(string id)
    {
        Id = id;
    }

    public RegisterWeaponOld(string id, Type attributeType)
    {
        Id = id;
        AttributeType = attributeType;
    }
}