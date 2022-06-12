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

}