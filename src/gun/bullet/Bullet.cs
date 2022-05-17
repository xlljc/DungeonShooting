using Godot;

/// <summary>
/// 子弹
/// </summary>
public class Bullet : Line2D
{
    /// <summary>
    /// 存在弹道特效存在时间
    /// </summary>
    [Export] public float LifeTime = 1f;

    /// <summary>
    /// 攻击目标
    /// </summary>
	public CampEnum Target;

    /// <summary>
    /// 最大飞行距离
    /// </summary>
    public float Distance;

    private float a = 1;

    public void Init(CampEnum target, float distance, Color color)
    {
        Target = target;
        Distance = distance;
        Modulate = color;
        SetPointPosition(1, new Vector2(distance, 0));
    }

    public override void _Process(float delta)
    {
        a -= 12 * delta;
        if (a <= 0) {
            QueueFree();
            return;
        }
        Color c = Modulate;
        c.a = a;
        Modulate = c;
    }

}