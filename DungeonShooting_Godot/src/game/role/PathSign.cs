
using Godot;

/// <summary>
/// 寻路标记, 记录下Role移动过的位置, 用于Ai寻路
/// </summary>
public class PathSign : Node2D
{
    /// <summary>
    /// 是否启用
    /// </summary>
    public bool Enable { get; set; }
    
    /// <summary>
    /// 创建标记
    /// </summary>
    /// <param name="pos">坐标</param>
    /// <param name="debug">用于debug, 显示箭头</param>
    public PathSign(Vector2 pos, bool debug = false)
    {
        GameApplication.Instance.Room.GetRoot(false).AddChild(this);
        GlobalPosition = pos;

        if (debug)
        {
            var sprite = new Sprite();
            sprite.Texture = ResourceManager.Load<Texture>(ResourcePath.resource_effects_debug_arrows_png);
            sprite.Position = new Vector2(0, -sprite.Texture.GetHeight() * 0.5f);
            AddChild(sprite);
        }
    }
}
