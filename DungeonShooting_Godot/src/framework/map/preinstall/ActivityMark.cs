
using System.Collections.Generic;
using Godot;

public class ActivityMark
{
    /// <summary>
    /// 物体 Id
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// 刷新位置, 单位: 像素
    /// </summary>
    public Vector2 Position { get; set; }

    /// <summary>
    /// 额外属性, 不要自行修改字典内的属性数据, 要改的话请在 DerivedAttr 上改
    /// </summary>
    public Dictionary<string, string> Attr { get; set; }
    
    /// <summary>
    /// 衍生属性, 可随意修改值, 通常用于存储随机出来的数据
    /// </summary>
    public Dictionary<string, string> DerivedAttr { get; set; }
    
    /// <summary>
    /// 特殊标记类型
    /// </summary>
    public SpecialMarkType MarkType { get; set; }

    /// <summary>
    /// 延时时间, 单位: 秒
    /// </summary>
    public float DelayTime { get; set; }

     /// <summary>
     /// 物体初始海拔高度
     /// </summary>
     public int Altitude { get; set; } = 8;

     /// <summary>
     /// 物体初始纵轴速度
     /// </summary>
     public float VerticalSpeed { get; set; } = 0;
    
    /// <summary>
    /// 物体类型
    /// </summary>
    public ActivityType ActivityType { get; set; }
    
    /// <summary>
    /// 快速创建标记
    /// </summary>
    /// <param name="activityType">物体类型</param>
    /// <param name="delayTime">延时时间</param>
    /// <param name="pos">位置</param>
    public static ActivityMark CreateMark(ActivityType activityType, float delayTime, Vector2 pos)
    {
        var mark = new ActivityMark();
        mark.Attr = new Dictionary<string, string>();
        mark.ActivityType = activityType;
        mark.MarkType = SpecialMarkType.Normal;
        mark.VerticalSpeed = 0;
        mark.Altitude = activityType == ActivityType.Enemy ? 0 : 8;
        mark.DelayTime = delayTime;
        mark.Position = pos;
        return mark;
    }
}