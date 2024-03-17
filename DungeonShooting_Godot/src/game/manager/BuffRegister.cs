using System.Collections.Generic;
/// <summary>
/// buff 注册类, 调用 Init() 函数初始化数据
/// 注意: 该类为 Tools 面板下自动生成的, 请不要手动编辑!
/// </summary>
public class BuffRegister
{
    /// <summary>
    /// 所有 buff 信息
    /// </summary>
    public static Dictionary<string, BuffInfo> BuffInfos { get; private set; }
    /// <summary>
    /// 初始化 buff
    /// </summary>
    public static void Init()
    {
        BuffInfos = new Dictionary<string, BuffInfo>();
        BuffInfos.Add("ActivePropsCapacity", new BuffInfo("ActivePropsCapacity", null, new List<int>() { 1 }, typeof(Buff_ActivePropsCapacity)));
        BuffInfos.Add("BulletBounceCount", new BuffInfo("BulletBounceCount", null, new List<int>() { 1 }, typeof(Buff_BulletBounceCount)));
        BuffInfos.Add("BulletCount", new BuffInfo("BulletCount", null, new List<int>() { 2 }, typeof(Buff_BulletCount)));
        BuffInfos.Add("BulletDeviationAngle", new BuffInfo("BulletDeviationAngle", null, new List<int>() { 2 }, typeof(Buff_BulletDeviationAngle)));
        BuffInfos.Add("BulletDistance", new BuffInfo("BulletDistance", null, new List<int>() { 2 }, typeof(Buff_BulletDistance)));
        BuffInfos.Add("BulletPenetration", new BuffInfo("BulletPenetration", null, new List<int>() { 1 }, typeof(Buff_BulletPenetration)));
        BuffInfos.Add("BulletRepel", new BuffInfo("BulletRepel", null, new List<int>() { 2 }, typeof(Buff_BulletRepel)));
        BuffInfos.Add("BulletSpeed", new BuffInfo("BulletSpeed", null, new List<int>() { 2 }, typeof(Buff_BulletSpeed)));
        BuffInfos.Add("Damage", new BuffInfo("Damage", null, new List<int>() { 2 }, typeof(Buff_Damage)));
        BuffInfos.Add("MaxHp", new BuffInfo("MaxHp", null, new List<int>() { 1 }, typeof(Buff_MaxHp)));
        BuffInfos.Add("MaxShield", new BuffInfo("MaxShield", null, new List<int>() { 1 }, typeof(Buff_MaxShield)));
        BuffInfos.Add("MoveSpeed", new BuffInfo("MoveSpeed", null, new List<int>() { 1 }, typeof(Buff_MoveSpeed)));
        BuffInfos.Add("OffsetInjury", new BuffInfo("OffsetInjury", null, new List<int>() { 1 }, typeof(Buff_OffsetInjury)));
        BuffInfos.Add("RandomBulletSpeed", new BuffInfo("RandomBulletSpeed", null, new List<int>() { 2 }, typeof(Buff_RandomBulletSpeed)));
        BuffInfos.Add("Scattering", new BuffInfo("Scattering", null, new List<int>() { 1 }, typeof(Buff_Scattering)));
        BuffInfos.Add("ShieldRecoveryTime", new BuffInfo("ShieldRecoveryTime", null, new List<int>() { 1 }, typeof(Buff_ShieldRecoveryTime)));
        BuffInfos.Add("WeaponCapacity", new BuffInfo("WeaponCapacity", null, new List<int>() { 1 }, typeof(Buff_WeaponCapacity)));
        BuffInfos.Add("WoundedInvincibleTime", new BuffInfo("WoundedInvincibleTime", null, new List<int>() { 1 }, typeof(Buff_WoundedInvincibleTime)));
    }
}