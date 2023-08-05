
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

/// <summary>
/// 房间配置文件相关信息, 将会在 RoomConfig.json 中汇总
/// </summary>
public class DungeonRoomSplit
{
    /// <summary>
    /// 当前房间是否绘制完成, 也就是是否可用
    /// </summary>
    [JsonInclude]
    public bool Ready;

    /// <summary>
    /// 房间配置路径
    /// </summary>
    [JsonInclude]
    public string RoomPath;

    /// <summary>
    /// 房间地块配置数据
    /// </summary>
    [JsonInclude]
    public string TilePath;

    /// <summary>
    /// 房间预设配置数据
    /// </summary>
    [JsonInclude]
    public string PreinstallPath;

    /// <summary>
    /// 房间配置数据, 第一次获取会在资源中加载数据
    /// </summary>
    [JsonIgnore]
    public DungeonRoomInfo RoomInfo
    {
        get
        {
            if (_roomInfo == null && RoomPath != null)
            {
                ReloadRoomInfo();
            }

            return _roomInfo;
        }
        set => _roomInfo = value;
    }

    private DungeonRoomInfo _roomInfo;

    /// <summary>
    /// 房间地块配置数据
    /// </summary>
    [JsonIgnore]
    public DungeonTileInfo TileInfo
    {
        get
        {
            if (_tileInfo == null && TilePath != null)
            {
                ReloadTileInfo();
            }

            return _tileInfo;
        }
        set => _tileInfo = value;
    }

    private DungeonTileInfo _tileInfo;
    
    /// <summary>
    /// 房间预设数据
    /// </summary>
    [JsonIgnore]
    public List<RoomPreinstall> Preinstall
    {
        get
        {
            if (_preinstall == null && PreinstallPath != null)
            {
                ReloadPreinstall();
            }

            return _preinstall;
        }
        set => _preinstall = value;
    }

    private List<RoomPreinstall> _preinstall;


    /// <summary>
    /// 重新加载房间数据
    /// </summary>
    public void ReloadRoomInfo()
    {
        var asText = ResourceManager.LoadText(RoomPath);
        _roomInfo = JsonSerializer.Deserialize<DungeonRoomInfo>(asText);

        // //需要处理 DoorAreaInfos 长度为 0 的房间, 并为其配置默认值
        // var areaInfos = _roomInfo.DoorAreaInfos;
        // if (areaInfos.Count == 0)
        // {
        //     areaInfos.Add(new DoorAreaInfo(DoorDirection.N, GameConfig.TileCellSize, (_roomInfo.Size.X - 1) * GameConfig.TileCellSize));
        //     areaInfos.Add(new DoorAreaInfo(DoorDirection.S, GameConfig.TileCellSize, (_roomInfo.Size.X - 1) * GameConfig.TileCellSize));
        //     areaInfos.Add(new DoorAreaInfo(DoorDirection.W, GameConfig.TileCellSize, (_roomInfo.Size.Y - 1) * GameConfig.TileCellSize));
        //     areaInfos.Add(new DoorAreaInfo(DoorDirection.E, GameConfig.TileCellSize, (_roomInfo.Size.Y - 1) * GameConfig.TileCellSize));
        // }
    }

    /// <summary>
    /// 重新加载房间地块配置数据
    /// </summary>
    public void ReloadTileInfo()
    {
        var asText = ResourceManager.LoadText(TilePath);
        _tileInfo = JsonSerializer.Deserialize<DungeonTileInfo>(asText);
    }

    /// <summary>
    /// 重新加载房间预设数据
    /// </summary>
    public void ReloadPreinstall()
    {
        var asText = ResourceManager.LoadText(PreinstallPath);
        _preinstall = JsonSerializer.Deserialize<List<RoomPreinstall>>(asText);
    }
}