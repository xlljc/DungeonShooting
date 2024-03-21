
using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// 房间预设数据
/// </summary>
public class RoomPreinstallInfo
{
    /// <summary>
    /// 预设名称
    /// </summary>
    [JsonInclude]
    public string Name;

    /// <summary>
    /// 预设权重
    /// </summary>
    [JsonInclude]
    public int Weight;

    /// <summary>
    /// 预设备注
    /// </summary>
    [JsonInclude]
    public string Remark;

    /// <summary>
    /// 是否自动填充数据
    /// </summary>
    [JsonInclude]
    public bool AutoFill;

    /// <summary>
    /// 波数数据
    /// </summary>
    [JsonInclude]
    public List<List<MarkInfo>> WaveList;

    /// <summary>
    /// 初始化波数据
    /// </summary>
    public void InitWaveList()
    {
        WaveList = new List<List<MarkInfo>>
        {
            new List<MarkInfo>()
        };
    }

    /// <summary>
    /// 初始化特殊标记
    /// </summary>
    public void InitSpecialMark(DungeonRoomType roomType)
    {
        var type = GetRoomSpecialMark(roomType);
        if (type != SpecialMarkType.Normal)
        {
            var preloading = WaveList[0];
            var markInfo = new MarkInfo();
            markInfo.Position = new SerializeVector2();
            markInfo.Size = new SerializeVector2();
            markInfo.SpecialMarkType = type;
            markInfo.MarkList = new List<MarkInfoItem>();
            preloading.Add(markInfo);
        }
    }

    /// <summary>
    /// 检查是否创建了特殊标记, 如果没有, 则会自动创建并返回false
    /// </summary>
    public bool CheckSpecialMark(DungeonRoomType roomType)
    {
        var type = GetRoomSpecialMark(roomType);
        if (type == SpecialMarkType.Normal)
        {
            return true;
        }

        if (WaveList.Count> 0)
        {
            var markInfos = WaveList[0];
            foreach (var markInfo in markInfos)
            {
                if (markInfo.SpecialMarkType == type)
                {
                    return true;
                }
            }
        }
        
        InitSpecialMark(roomType);
        return false;
    }
    
    /// <summary>
    /// 获取指定类型房间中应该存在的特殊标记数据类型
    /// </summary>
    public SpecialMarkType GetRoomSpecialMark(DungeonRoomType roomType)
    {
        if (roomType == DungeonRoomType.Inlet) //初始房间
        {
            return SpecialMarkType.BirthPoint;
        }
        else if (roomType == DungeonRoomType.Outlet) //结束房间
        {
            return SpecialMarkType.OutPoint;
        }
        else if (roomType == DungeonRoomType.Shop) //商店房间
        {
            return SpecialMarkType.ShopBoss;
        }
        else if (roomType == DungeonRoomType.Reward) //奖励房间
        {
            return SpecialMarkType.Box;
        }
        
        return SpecialMarkType.Normal;
    }
}