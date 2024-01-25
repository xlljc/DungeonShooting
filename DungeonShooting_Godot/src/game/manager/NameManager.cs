
public class NameManager
{
    /// <summary>
    /// 获取物体属性名称字符串
    /// </summary>
    public static string GetActivityTypeName(ActivityType type)
    {
        switch (type)
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