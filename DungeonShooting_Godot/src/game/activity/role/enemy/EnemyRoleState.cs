
public class EnemyRoleState : RoleState
{
    /// <summary>
    /// 视野半径, 单位像素, 发现玩家后改视野范围可以穿墙
    /// </summary>
    public float ViewRange = 250;

    /// <summary>
    /// 发现玩家后跟随玩家的视野半径
    /// </summary>
    public float TailAfterViewRange = 400;

    /// <summary>
    /// 背后的视野半径, 单位像素
    /// </summary>
    public float BackViewRange = 50;
}