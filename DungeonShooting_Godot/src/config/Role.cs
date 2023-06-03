using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Config;

public class Role
{
    /// <summary>
    /// 物体唯一id <br/>
    /// 不需要添加类型前缀
    /// </summary>
    [JsonInclude]
    public string Id { get; private set; }

    /// <summary>
    /// 222
    /// </summary>
    [JsonInclude]
    public string[] A { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonInclude]
    public Dictionary<string, int> B { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonInclude]
    public Dictionary<string, string[]>[] C { get; private set; }

    /// <summary>
    /// 123
    /// </summary>
    [JsonInclude]
    public SerializeVector2 D { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonInclude]
    public SerializeVector2[] E { get; private set; }

}