using Godot;

/// <summary>
/// 子弹
/// </summary>
public abstract class Bullet : Node2D
{
    /// <summary>
    /// 攻击目标阵营
    /// </summary>
	public CampEnum TargetCamp { get; private set; }
    /// <summary>
    /// 发射该子弹的武器
    /// </summary>
    public Gun Gun { get; private set; }
    /// <summary>
    /// 发射该子弹的物体对象
    /// </summary>
    public Node2D Master { get; private set; }

    public void Init(CampEnum target, Gun gun, Node2D master)
    {
        TargetCamp = target;
        Gun = gun;
        Master = master;
    }
}