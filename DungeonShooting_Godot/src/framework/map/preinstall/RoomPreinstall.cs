

using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

/// <summary>
/// 房间预设处理类
/// </summary>
public class RoomPreinstall : IDestroy
{
    public bool IsDestroyed { get; private set; }

    /// <summary>
    /// 所属房间对象
    /// </summary>
    public RoomInfo RoomInfo { get; }
    
    /// <summary>
    /// 绑定的预设数据
    /// </summary>
    public RoomPreinstallInfo RoomPreinstallInfo { get; }

    /// <summary>
    /// 总波数
    /// </summary>
    public int WaveCount => RoomPreinstallInfo.WaveList.Count;

    /// <summary>
    /// 波数和标记数据列表
    /// </summary>
    public List<List<ActivityMark>> WaveList { get; } = new List<List<ActivityMark>>();

    private bool _runPretreatment = false;

    public RoomPreinstall(RoomInfo roomInfo, RoomPreinstallInfo roomPreinstallInfo)
    {
        RoomInfo = roomInfo;
        RoomPreinstallInfo = roomPreinstallInfo;
    }

    /// <summary>
    /// 预处理操作
    /// </summary>
    public void Pretreatment(SeedRandom random)
    {
        if (_runPretreatment)
        {
            return;
        }

        _runPretreatment = true;

        //确定房间内要生成写啥
        foreach (var markInfos in RoomPreinstallInfo.WaveList)
        {
            var wave = new List<ActivityMark>();
            WaveList.Add(wave);
            foreach (var markInfo in markInfos)
            {
                var mark = new ActivityMark();
                if (markInfo.SpecialMarkType == SpecialMarkType.Normal) //普通标记
                {
                    MarkInfoItem markInfoItem;
                    if (markInfo.MarkList.Count == 0)
                    {
                        continue;
                    }
                    else if (markInfo.MarkList.Count == 1)
                    {
                        markInfoItem = markInfo.MarkList[0];
                    }
                    else
                    {
                        var tempArray = markInfo.MarkList.Select(item => item.Weight).ToArray();
                        var index = random.RandomWeight(tempArray);
                        markInfoItem = markInfo.MarkList[index];
                    }

                    mark.Id = markInfoItem.Id;
                    mark.Attr = markInfoItem.Attr;
                }
                else if (markInfo.SpecialMarkType == SpecialMarkType.BirthPoint) //玩家出生标记
                {

                }
                else
                {
                    GD.PrintErr("暂未支持的类型: " + markInfo.SpecialMarkType);
                    continue;
                }

                mark.DelayTime = markInfo.DelayTime;
                mark.MarkType = markInfo.SpecialMarkType;
                //随机刷新坐标
                var pos = markInfo.Position.AsVector2();
                var birthRect = markInfo.Size.AsVector2();
                var tempPos = new Vector2(
                    random.RandomRangeInt((int)(pos.X - birthRect.X / 2), (int)(pos.X + birthRect.X / 2)),
                    random.RandomRangeInt((int)(pos.Y - birthRect.Y / 2), (int)(pos.Y + birthRect.Y / 2))
                );
                var offset = RoomInfo.GetOffsetPosition();
                //var offset = RoomInfo.RoomSplit.RoomInfo.Position.AsVector2I();
                mark.Position = RoomInfo.GetWorldPosition() + (tempPos - offset);

                wave.Add(mark);
            }

            //排序操作
            wave.Sort((a, b) => (int)(a.DelayTime * 1000 - b.DelayTime * 1000));
        }
    }

    /// <summary>
    /// 获取房间内的玩家生成标记
    /// </summary>
    public ActivityMark GetPlayerBirthMark()
    {
        if (WaveList.Count == 0)
        {
            return null;
        }

        var activityMarks = WaveList[0];
        var activityMark = activityMarks.FirstOrDefault(mark => mark.MarkType == SpecialMarkType.BirthPoint);
        return activityMark;
    }

    public void Destroy()
    {
        if (IsDestroyed)
        {
            return;
        }

        IsDestroyed = true;
        WaveList.Clear();
    }
}