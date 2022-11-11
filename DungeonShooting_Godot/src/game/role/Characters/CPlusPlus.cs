#region 设计思路
// c/c++ 
// 被动：易于精通   随着刷的房间的增多 提升对于 道具的加成 增加攻击力 或者 增强对应效果
// 
// 速度：中偏上
// 血量：中
// 护盾：下
// 
// 专属武器：指针  近战武器  
// 武器描述：短按 向目标方向 刺出 对路径中的 敌人或可破坏建筑 造成伤害 
//          长按 1.8 秒 向目标方向 冲刺 并消除 途中的弹幕 蓄力过程会被打断 打断后强制取消攻击
// 
// 
// 每个角色都应该有对应的被动  属性 专属武器 
#endregion

public class CPlusPlus : Role
{


    public override void _Ready()
    {
        base._Ready();
        #region 初始属性

        MaxHp = 55;
        MoveSpeed = 130f;
        #endregion
    }

    // public CPlusPlus() : base(ResourcePath.prefab_role_CPlusPlus_tscn)
    // {

    // }

}