using System;

/// <summary>
/// 用作静态函数上, 用于注册武器, 函数必须返回一个 Weapon 对象, 且参数为 string id, 
/// 那么它看起来应该像这样: static Weapon Method(string id);
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class RegisterWeaponFunction : Attribute
{
    public string Id { get; private set; }

    public RegisterWeaponFunction(string id)
    {
        Id = id;
    }
}