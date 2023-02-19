
using System.Text.Json.Serialization;

public class DungeonRoomSplit
{
    [JsonInclude]
    public string ResourcePath;
    
    [JsonInclude]
    public string ConfigPath;

    [JsonInclude]
    public DungeonRoomInfo RoomInfo;
}