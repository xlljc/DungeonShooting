
public class NameManager
{
    /// <summary>
    /// 获取物体属性名称字符串
    /// </summary>
    public static string GetActivityTypeName(int type)
    {
        switch ((ActivityIdPrefix.ActivityPrefixType)type)
        {
            case ActivityIdPrefix.ActivityPrefixType.NonePrefix:
                return "";
            case ActivityIdPrefix.ActivityPrefixType.Test:
                return "测试";
            case ActivityIdPrefix.ActivityPrefixType.Role:
            case ActivityIdPrefix.ActivityPrefixType.Player:
                return "角色";
            case ActivityIdPrefix.ActivityPrefixType.Enemy:
                return "敌人";
            case ActivityIdPrefix.ActivityPrefixType.Weapon:
                return "武器";
            case ActivityIdPrefix.ActivityPrefixType.Bullet:
                return "子弹";
            case ActivityIdPrefix.ActivityPrefixType.Shell:
                return "弹壳";
            case ActivityIdPrefix.ActivityPrefixType.Effect:
                return "特效";
            case ActivityIdPrefix.ActivityPrefixType.Prop:
                return "道具";
            case ActivityIdPrefix.ActivityPrefixType.Other:
                return "其他";
        }

        return "";
    }
}