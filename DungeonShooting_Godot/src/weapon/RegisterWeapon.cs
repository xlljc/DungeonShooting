using System;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class RegisterWeapon : Attribute
{
    public RegisterWeapon(string id)
    {

    }
}