using Godot;

/// <summary>
/// 子弹接口
/// </summary>
public interface IBullet
{
    /// <summary>
    /// 攻击目标阵营
    /// </summary>
	CampEnum TargetCamp { get; }
    /// <summary>
    /// 发射该子弹的武器
    /// </summary>
    Weapon Gun { get; }
    /// <summary>
    /// 发射该子弹的物体对象
    /// </summary>
    Node2D Master { get; }
    /// <summary>
    /// 初始化基础数据
    /// </summary>
    /// <param name="target">攻击的目标阵营</param>
    /// <param name="gun">发射该子弹的枪对象</param>
    /// <param name="master">发射该子弹的角色</param>
    void Init(CampEnum target, Weapon gun, Node2D master);
}


public class Bullet : ActivityObject
{
    public Bullet(string scenePath) : base(scenePath)
    {
        
    }
}