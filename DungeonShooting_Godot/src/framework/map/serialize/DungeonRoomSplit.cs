
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Godot;

/// <summary>
/// 房间配置文件相关信息, 将会在 RoomConfig.json 中汇总
/// </summary>
public class DungeonRoomSplit
{
    /// <summary>
    /// 房间异常类型
    /// </summary>
    [JsonInclude]
    public RoomErrorType ErrorType;

    /// <summary>
    /// 该房间的文件夹路径
    /// </summary>
    [JsonInclude]
    public string Path;

    /// <summary>
    /// 房间配置路径
    /// </summary>
    [JsonIgnore]
    public string RoomPath => Path + "/RoomInfo.json";

    /// <summary>
    /// 房间地块配置数据
    /// </summary>
    [JsonIgnore]
    public string TilePath => Path + "/TileInfo.json";

    /// <summary>
    /// 房间预设配置数据
    /// </summary>
    [JsonIgnore]
    public string PreinstallPath => Path + "/Preinstall.json";

    /// <summary>
    /// 预览图路径
    /// </summary>
    [JsonIgnore]
    public string PreviewPath => Path + "/Preview.png";

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
    public List<RoomPreinstallInfo> Preinstall
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

    private List<RoomPreinstallInfo> _preinstall;

    /// <summary>
    /// 预览图
    /// </summary>
    [JsonIgnore]
    public Texture2D PreviewImage
    {
        get
        {
            if (_previewImage == null)
            {
                ReloadPreviewImage();
            }

            return _previewImage;
        }
        set
        {
            if (_previewImage != null)
            {
                _previewImage.Dispose();
            }

            _previewImage = value;
        }
    }
    
    private Texture2D _previewImage;
    
    /// <summary>
    /// 重新加载房间数据
    /// </summary>
    public void ReloadRoomInfo()
    {
        var asText = ResourceManager.LoadText(RoomPath);
        _roomInfo = JsonSerializer.Deserialize<DungeonRoomInfo>(asText);
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
        _preinstall = JsonSerializer.Deserialize<List<RoomPreinstallInfo>>(asText);
    }

    /// <summary>
    /// 重新加载预览图片
    /// </summary>
    public void ReloadPreviewImage()
    {
        if (_previewImage != null)
        {
            _previewImage.Dispose();
        }
        
        if (File.Exists(PreviewPath))
        {
            var bytes = File.ReadAllBytes(PreviewPath);
            
            var image = Image.Create(GameConfig.PreviewImageSize, GameConfig.PreviewImageSize, false, Image.Format.Rgb8);
            image.LoadPngFromBuffer(bytes);
            _previewImage = ImageTexture.CreateFromImage(image);
        }
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            return false;
        }
        
        if (obj is DungeonRoomSplit roomSplit)
        {
            return roomSplit.Path == Path;
        }

        return this == obj;
    }

    public override int GetHashCode()
    {
        return Path.GetHashCode();
    }
}