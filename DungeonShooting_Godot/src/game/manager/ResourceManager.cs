using System;
using System.Collections.Generic;
using Config;
using Godot;

/// <summary>
/// 资源管理器
/// </summary>
public static class ResourceManager
{
    /// <summary>
    /// 默认权重值
    /// </summary>
    public const int DefaultWeight = 100;
    
    /// <summary>
    /// 颜色混合材质
    /// </summary>
    public static ShaderMaterial BlendMaterial
    {
        get
        {
            if (_shadowMaterial == null)
            {
                _shadowMaterial = Load<ShaderMaterial>(ResourcePath.resource_material_Blend_tres);
            }

            return _shadowMaterial;
        }
    }

    private static ShaderMaterial _shadowMaterial;
    
    public static ShaderMaterial NewBlendMaterial => Load<ShaderMaterial>(ResourcePath.resource_material_Blend_tres, false);

    /// <summary>
    /// 颜色混合Shader
    /// </summary>
    public static Shader BlendShader
    {
        get
        {
            if (_shadowShader == null)
            {
                _shadowShader = Load<Shader>(ResourcePath.resource_material_Blend_tres);
            }

            return _shadowShader;
        }
    }

    private static Shader _shadowShader;

    /// <summary>
    /// 默认字体资源, 字体大小16px
    /// </summary>
    public static Font DefaultFont16Px
    {
        get
        {
            if (_defaultFont16Px == null)
            {
                _defaultFont16Px = Load<Font>(ResourcePath.resource_font_VonwaonBitmap16px_ttf);
            }

            return _defaultFont16Px;
        }
    }
    private static Font _defaultFont16Px;
    
    /// <summary>
    /// 默认字体资源, 字体大小12px
    /// </summary>
    public static Font DefaultFont12Px
    {
        get
        {
            if (_defaultFont12Px == null)
            {
                _defaultFont12Px = Load<Font>(ResourcePath.resource_font_VonwaonBitmap12px_ttf);
            }

            return _defaultFont12Px;
        }
    }
    private static Font _defaultFont12Px;
    
    //缓存的资源
    private static readonly Dictionary<string, object> CachePack = new Dictionary<string, object>();

    /// <summary>
    /// 加载资源对象, 并且缓存当前资源对象, 可频繁获取
    /// </summary>
    /// <param name="path">资源路径</param>
    /// <param name="useCache">是否使用缓存中的资源</param>
    public static T Load<T>(string path, bool useCache = true) where T : class
    {
        if (!useCache)
        {
            var res = ResourceLoader.Load<T>(path, null, ResourceLoader.CacheMode.Ignore);
            if (res == null)
            {
                Debug.LogError("加载资源失败, 未找到资源: " + path);
                return default;
            }

            return res;
        }

        if (CachePack.TryGetValue(path, out var pack))
        {
            return pack as T;
        }

        pack = ResourceLoader.Load<T>(path);
        if (pack != null)
        {
            CachePack.Add(path, pack);
            return pack as T;
        }
        else
        {
            Debug.LogError("加载资源失败, 未找到资源: " + path);
        }

        return default;
    }

    /// <summary>
    /// 加载并且实例化场景, 并返回
    /// </summary>
    /// <param name="path">场景路径</param>
    /// <param name="useCache">是否使用缓存中的资源</param>
    public static Node LoadAndInstantiate(string path, bool useCache = true)
    {
        var packedScene = Load<PackedScene>(path, useCache);
        return packedScene.Instantiate();
    }
    
    /// <summary>
    /// 加载并且实例化场景, 并返回
    /// </summary>
    /// <param name="path">场景路径</param>
    /// <param name="useCache">是否使用缓存中的资源</param>
    public static T LoadAndInstantiate<T>(string path, bool useCache = true) where T : Node
    {
        var packedScene = Load<PackedScene>(path, useCache);
        return packedScene.Instantiate<T>();
    }

    /// <summary>
    /// 读取文本资源
    /// </summary>
    public static string LoadText(string path)
    {
        string text;
        using (var fileAccess = FileAccess.Open(path, FileAccess.ModeFlags.Read))
        {
            text = fileAccess.GetAsText();
        }
        return text;
    }

    /// <summary>
    /// 加载2d纹理资源
    /// </summary>
    /// <param name="path">资源路径</param>
    /// <param name="useCache">是否使用缓存中的资源</param>
    public static Texture2D LoadTexture2D(string path, bool useCache = true)
    {
        return Load<Texture2D>(path, useCache);
    }

    /// <summary>
    /// 根据标记数据获取标记图标
    /// </summary>
    public static Texture2D GetMarkIcon(MarkInfo markInfo)
    {
        if (markInfo != null)
        {
            if (markInfo.SpecialMarkType == SpecialMarkType.BirthPoint) //出生标记
            {
                return LoadTexture2D(ResourcePath.resource_sprite_ui_commonIcon_BirthMark_png);
            }
            else if (markInfo.SpecialMarkType == SpecialMarkType.OutPoint) //出口标记
            {
                return LoadTexture2D(ResourcePath.resource_sprite_ui_commonIcon_BirthMark_png);
            }
            else if (markInfo.SpecialMarkType == SpecialMarkType.Box) //宝箱
            {
                return LoadTexture2D(ResourcePath.resource_sprite_ui_commonIcon_BirthMark_png);
            }
            else if (markInfo.SpecialMarkType == SpecialMarkType.ShopBoss) //商店老板
            {
                return LoadTexture2D(ResourcePath.resource_sprite_ui_commonIcon_BirthMark_png);
            }
            else if (markInfo.MarkList != null) //普通标记
            {
                if (markInfo.MarkList.Count > 1) //多个物体
                {
                    return LoadTexture2D(ResourcePath.resource_sprite_ui_commonIcon_PackageMark_png);
                }
                else if (markInfo.MarkList.Count == 1) //单个物体
                {
                    var id = markInfo.MarkList[0].Id;
                    if (id != null)
                    {
                        var activityBase = PreinstallMarkManager.GetMarkConfig(id);
                        if (activityBase != null)
                        {
                            return LoadTexture2D(activityBase.Icon);
                        }
                    }
                }
            }
        }
        //未知物体
        return LoadTexture2D(ResourcePath.resource_sprite_ui_commonIcon_UnknownActivity_png);
    }
    
    /// <summary>
    /// 将普通路径转化为 Godot 资源路径
    /// </summary>
    public static string ToResPath(string path)
    {
        var field = path.Replace("\\", "/");
        return "res://" + field;
    }

    /// <summary>
    /// 移除资源后缀名
    /// </summary>
    public static string RemoveExtension(string name)
    {
        var index = name.LastIndexOf(".", StringComparison.Ordinal);
        if (index >= 0)
        {
            return name.Substring(0, index);
        }

        return name;
    }
}