
using Godot;

/// <summary>
/// 用于限定 Marker2D 节点的旋转角度
/// </summary>
public partial class MountRotation : Marker2D
{
    /// <summary>
    /// 吸附角度
    /// </summary>
    private int _adsorption = 6;
    
    /// <summary>
    /// 所在的角色
    /// </summary>
    public Role Master { get; set; }

    /// <summary>
    /// 当前节点真实的旋转角度, 角度制
    /// </summary>
    public float RealRotationDegrees { get; private set; }
    
    /// <summary>
    /// 当前节点真实的旋转角度, 弧度制
    /// </summary>
    public float RealRotation => Mathf.DegToRad(RealRotationDegrees);

    /// <summary>
    /// 设置看向的目标点
    /// </summary>
    public void SetLookAt(Vector2 target)
    {
        var myPos = GlobalPosition;
        var angle = Mathf.RadToDeg((target - myPos).Angle());

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

        RealRotationDegrees = angle;

        // if (Master.GlobalPosition.X >= target.X)
        // {
        //     angle = -angle;
        // }
        GlobalRotationDegrees = AdsorptionAngle(angle);
    }

    private float AdsorptionAngle(float angle)
    {
        return Mathf.Round(angle / _adsorption) * _adsorption;
    }
}