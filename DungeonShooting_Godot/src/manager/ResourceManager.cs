using System.Collections.Generic;
using Godot;

public static class ResourceManager
{

    /// <summary>
    /// 2D阴影的材质
    /// </summary>
    public static ShaderMaterial ShadowMaterial
    {
        get
        {
            if (_shadowMaterial == null)
            {
                _shadowMaterial = ResourceLoader.Load<ShaderMaterial>("res://resource/materlal/Shadow.tres");
            }
            return _shadowMaterial;
        }
    }
    private static ShaderMaterial _shadowMaterial;

    /// <summary>
    /// 2D阴影的Shader
    /// </summary>
    public static Shader ShadowShader
    {
        get
        {
            if (_shadowShader == null)
            {
                _shadowShader = ResourceLoader.Load<Shader>("res://resource/materlal/Shadow.gdshader");
            }
            return _shadowShader;
        }
    }
    private static Shader _shadowShader;

    private static readonly Dictionary<string, object> CachePack = new Dictionary<string, object>();

    /// <summary>
    /// 加载资源对象, 并且缓存当前资源对象, 可频繁获取
    /// </summary>
    /// <param name="path">资源路径</param>
    public static T Load<T>(string path) where T : class
    {
        if (CachePack.TryGetValue(path, out var pack))
        {
            return (T)pack;
        }
        else
        {
            pack = ResourceLoader.Load(path);
            if (pack != null)
            {
                CachePack.Add(path, pack);
                return (T)pack;
            }
        }
        return default(T);
    }

    /// <summary>
    /// 加载并实例化一个武器对象
    /// </summary>
    /// <param name="path">资源路径</param>
    public static Gun LoadGunAndInstance(string path)
    {
        var pack = Load<PackedScene>(path);
        if (pack != null)
        {
            return pack.Instance<Gun>();
        }
        return null;
    }

}