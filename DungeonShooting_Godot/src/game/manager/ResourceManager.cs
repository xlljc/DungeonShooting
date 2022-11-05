using System.Collections.Generic;
using Godot;

public static class ResourceManager
{
    /// <summary>
    /// 颜色混合材质
    /// </summary>
    public static ShaderMaterial BlendMaterial
    {
        get
        {
            if (_shadowMaterial == null)
            {
                _shadowMaterial = ResourceLoader.Load<ShaderMaterial>(ResourcePath.resource_materlal_Blend_tres);
            }
            return _shadowMaterial;
        }
    }
    private static ShaderMaterial _shadowMaterial;

    /// <summary>
    /// 颜色混合Shader
    /// </summary>
    public static Shader BlendShader
    {
        get
        {
            if (_shadowShader == null)
            {
                _shadowShader = ResourceLoader.Load<Shader>(ResourcePath.resource_materlal_Blend_gdshader);
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
            return pack as T;
        }
        else
        {
            pack = ResourceLoader.Load(path);
            if (pack != null)
            {
                CachePack.Add(path, pack);
                return pack as T;
            }
        }
        return default(T);
    }
}