
using Godot;

/// <summary>
/// Shader 中的参数名称
/// </summary>
public static class ShaderParamNames
{
    /// <summary>
    /// 灰度
    /// </summary>
    public static readonly StringName Grey = "grey";
    /// <summary>
    /// 轮廓颜色
    /// </summary>
    public static readonly StringName OutlineColor = "outline_color";
    /// <summary>
    /// 是否显示轮廓
    /// </summary>
    public static readonly StringName ShowOutline = "show_outline";
    /// <summary>
    /// 纹理大小
    /// </summary>
    public static readonly StringName Size = "size";
    /// <summary>
    /// 线段宽度
    /// </summary>
    public static readonly StringName LineWidth = "line_width";
    /// <summary>
    /// 偏移
    /// </summary>
    public static readonly StringName Offset = "offset";
    /// <summary>
    /// 网格大小
    /// </summary>
    public static readonly StringName GridSize = "grid_size";
    /// <summary>
    /// 颜色
    /// </summary>
    public static readonly StringName Color = "color";

    /// <summary>
    /// 快速设置一个材质的 shader 材质参数
    /// </summary>
    public static void SetShaderMaterialParameter(this Material material, StringName param, Variant value)
    {
        if (material is ShaderMaterial shaderMaterial)
        {
            shaderMaterial.SetShaderParameter(param, value);
        }
    }
}