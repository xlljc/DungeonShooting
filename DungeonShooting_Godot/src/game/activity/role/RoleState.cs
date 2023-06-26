
/// <summary>
/// 角色属性类
/// </summary>
public class RoleState
{
    /// <summary>
    /// 移动速度
    /// </summary>
    public float MoveSpeed = 120f;
        
    /// <summary>
    /// 移动加速度
    /// </summary>
    public float Acceleration = 1500f;
    
    /// <summary>
    /// 移动摩擦力
    /// </summary>
    public float Friction = 900f;
    
    /// <summary>
    /// 单格护盾恢复时间, 单位: 秒
    /// </summary>
    public float ShieldRecoveryTime = 8;
}