
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