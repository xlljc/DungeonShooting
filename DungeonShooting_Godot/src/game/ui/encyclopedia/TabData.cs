namespace UI.Encyclopedia;

public class TabData
{
    /// <summary>
    /// 图标
    /// </summary>
    public string Icon;
    /// <summary>
    /// 物体类型
    /// </summary>
    public ActivityType Type;

    public TabData(string icon, ActivityType type)
    {
        Icon = icon;
        Type = type;
    }
}