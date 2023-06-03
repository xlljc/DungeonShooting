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

}