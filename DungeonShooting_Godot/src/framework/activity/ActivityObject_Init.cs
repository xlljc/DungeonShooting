/// <summary>
/// 根据配置表注册物体, 该类是自动生成的, 请不要手动编辑!
/// </summary>
public partial class ActivityObject
{
    /// <summary>
    /// 存放所有在表中注册的物体的id
    /// </summary>
    public static class Ids
    {
        /// <summary>
        /// 玩家
        /// </summary>
        public const string Id_role0001 = "role0001";
        /// <summary>
        /// 敌人
        /// </summary>
        public const string Id_enemy0001 = "enemy0001";
        public const string Id_weapon0001 = "weapon0001";
        public const string Id_weapon0002 = "weapon0002";
        public const string Id_weapon0003 = "weapon0003";
        public const string Id_weapon0004 = "weapon0004";
        public const string Id_bullet0001 = "bullet0001";
        public const string Id_bullet0002 = "bullet0002";
        public const string Id_shell0001 = "shell0001";
        /// <summary>
        /// 敌人死亡碎片
        /// </summary>
        public const string Id_effect0001 = "effect0001";
        /// <summary>
        /// 地牢房间的门(东侧)
        /// </summary>
        public const string Id_other_door_e = "other_door_e";
        /// <summary>
        /// 地牢房间的门(西侧)
        /// </summary>
        public const string Id_other_door_w = "other_door_w";
        /// <summary>
        /// 地牢房间的门(南侧)
        /// </summary>
        public const string Id_other_door_s = "other_door_s";
        /// <summary>
        /// 地牢房间的门(北侧)
        /// </summary>
        public const string Id_other_door_n = "other_door_n";
    }
    private static void _InitRegister()
    {
        _activityRegisterMap.Add("role0001", "res://prefab/role/Role0001.tscn");
        _activityRegisterMap.Add("enemy0001", "res://prefab/role/Enemy0001.tscn");
        _activityRegisterMap.Add("weapon0001", "res://prefab/weapon/Weapon0001.tscn");
        _activityRegisterMap.Add("weapon0002", "res://prefab/weapon/Weapon0002.tscn");
        _activityRegisterMap.Add("weapon0003", "res://prefab/weapon/Weapon0003.tscn");
        _activityRegisterMap.Add("weapon0004", "res://prefab/weapon/Weapon0004.tscn");
        _activityRegisterMap.Add("bullet0001", "res://prefab/bullet/Bullet0001.tscn");
        _activityRegisterMap.Add("bullet0002", "res://prefab/bullet/Bullet0002.tscn");
        _activityRegisterMap.Add("shell0001", "res://prefab/shell/Shell0001.tscn");
        _activityRegisterMap.Add("effect0001", "res://prefab/effect/activityObject/Effect0001.tscn");
        _activityRegisterMap.Add("other_door_e", "res://prefab/map/RoomDoor_E.tscn");
        _activityRegisterMap.Add("other_door_w", "res://prefab/map/RoomDoor_W.tscn");
        _activityRegisterMap.Add("other_door_s", "res://prefab/map/RoomDoor_S.tscn");
        _activityRegisterMap.Add("other_door_n", "res://prefab/map/RoomDoor_N.tscn");
    }
}
