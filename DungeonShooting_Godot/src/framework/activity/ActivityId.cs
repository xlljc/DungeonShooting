
/// <summary>
/// Activity注册类id前缀
/// </summary>
public static class ActivityId
{
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
            case ActivityType.Treasure:
                return "宝箱";
            case ActivityType.Other:
                return "其他";
        }

        return "";
    }
}