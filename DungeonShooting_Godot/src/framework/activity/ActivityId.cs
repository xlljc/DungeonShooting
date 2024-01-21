
/// <summary>
/// Activity注册类id前缀
/// </summary>
public static class ActivityId
{
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
    /// 道具
    /// </summary>
    public const string Prop = "prop";
    /// <summary>
    /// 其他类型
    /// </summary>
    public const string Other = "other";

    /// <summary>
    /// 根据 ActivityType 中的枚举类型获取类型名称的字符串
    /// </summary>
    public static string GetIdPrefix(ActivityType activityType)
    {
        switch (activityType)
        {
            case ActivityType.None:
                return "";
            case ActivityType.Test:
                return Test;
            case ActivityType.Role:
            case ActivityType.Player:
                return Role;
            case ActivityType.Enemy:
                return Enemy;
            case ActivityType.Weapon:
                return Weapon;
            case ActivityType.Bullet:
                return Bullet;
            case ActivityType.Shell:
                return Shell;
            case ActivityType.Effect:
                return Effect;
            case ActivityType.Prop:
                return Prop;
            case ActivityType.Other:
                return Other;
        }

        return "";
    }

    /// <summary>
    /// 根据 ActivityType 中的枚举类型获取类型名称的字符串
    /// </summary>
    public static string GetTypeName(ActivityType activityType)
    {
        switch (activityType)
        {
            case ActivityType.None:
                return "";
            case ActivityType.Test:
                return "测试";
            case ActivityType.Role:
            case ActivityType.Player:
                return "角色";
            case ActivityType.Enemy:
                return "敌人";
            case ActivityType.Weapon:
                return "武器";
            case ActivityType.Bullet:
                return "子弹";
            case ActivityType.Shell:
                return "弹壳";
            case ActivityType.Effect:
                return "特效";
            case ActivityType.Prop:
                return "道具";
            case ActivityType.Other:
                return "其他";
        }

        return "";
    }
}