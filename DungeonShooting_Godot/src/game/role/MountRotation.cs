
using Godot;

/// <summary>
/// 用于限定 Position2D 节点的旋转角度
/// </summary>
public class MountRotation : Position2D
{
    /// <summary>
    /// 所在的角色
    /// </summary>
    public Role Master { get; set; }

    /// <summary>
    /// 当前节点真实的旋转角度
    /// </summary>
    public float RealAngle
    {
        get => _readAngle;
        // set
        // {
        //     
        // }
    }

    private float _readAngle = 0;

    /// <summary>
    /// 设置看向的目标点
    /// </summary>
    /// <param name="target"></param>
    public void SetLookAt(Vector2 target)
    {
        var myPos = GlobalPosition;
        var angle = Mathf.Rad2Deg((target - myPos).Angle());

        if (Master.Face == FaceDirection.Left)
        {
            if (angle < 0 && angle > -80)
            {
                angle = -80;
            }
            else if (angle >= 0 && angle < 80)
            {
                angle = 80;
            }
        }
        else
        {
            angle = Mathf.Clamp(angle, -100, 100);
        }

        _readAngle = angle;

        if (Master.GlobalPosition.x >= target.x)
        {
            angle = -angle;
        }

        GlobalRotationDegrees = AdsorptionAngle(angle);
    }

    private float AdsorptionAngle(float angle)
    {
        return ((int)angle / 5) * 5;
    }
}