
//Activity注册类id前缀
public static class ActivityIdPrefix
{
    public enum ActivityPrefixType
    {
        /// <summary>
        /// 无前缀
        /// </summary>
        NonePrefix,
        /// <summary>
        /// 玩家
        /// </summary>
        Player,
        /// <summary>
        /// 测试对象
        /// </summary>
        Test,
        /// <summary>
        /// 角色
        /// </summary>
        Role,
        /// <summary>
        /// 敌人
        /// </summary>
        Enemy,
        /// <summary>
        /// 武器
        /// </summary>
        Weapon,
        /// <summary>
        /// 子弹
        /// </summary>
        Bullet,
        /// <summary>
        /// 弹壳
        /// </summary>
        Shell,
        /// <summary>
        /// 特效
        /// </summary>
        Effect,
        /// <summary>
        /// 其它类型
        /// </summary>
        Other,
    }
    
    /// <summary>
    /// 测试单位
    /// </summary>
    public const string Test = "test";
    /// <summary>
    /// 角色
    /// </summary>
    public const string Role = "role";
    /// <summary>
    /// 敌人
    /// </summary>
    public const string Enemy = "enemy";
    /// <summary>
    /// 武器
    /// </summary>
    public const string Weapon = "weapon";
    /// <summary>
    /// 子弹
    /// </summary>
    public const string Bullet = "bullet";
    /// <summary>
    /// 弹壳
    /// </summary>
    public const string Shell = "shell";
    /// <summary>
    /// 特效
    /// </summary>
    public const string Effect = "effect";
    /// <summary>
    /// 其他类型
    /// </summary>
    public const string Other = "other";

    /// <summary>
    /// 根据 ActivityPrefixType 中的枚举类型获取类型名称的字符串
    /// </summary>
    public static string GetNameByPrefixType(ActivityPrefixType prefixType)
    {
        switch (prefixType)
        {
            case ActivityPrefixType.NonePrefix:
                return "";
            case ActivityPrefixType.Test:
                return Test;
            case ActivityPrefixType.Role:
            case ActivityPrefixType.Player:
                return Role;
            case ActivityPrefixType.Enemy:
                return Enemy;
            case ActivityPrefixType.Weapon:
                return Weapon;
            case ActivityPrefixType.Bullet:
                return Bullet;
            case ActivityPrefixType.Shell:
                return Shell;
            case ActivityPrefixType.Effect:
                return Effect;
            case ActivityPrefixType.Other:
                return Other;
        }

        return "";
    }
}