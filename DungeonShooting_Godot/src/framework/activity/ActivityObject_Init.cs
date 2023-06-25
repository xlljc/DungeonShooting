using Config;

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
        /// 名称:  <br/>
        /// 备注: 玩家
        /// </summary>
        public const string Id_role0001 = "role0001";
        /// <summary>
        /// 名称:  <br/>
        /// 备注: 敌人
        /// </summary>
        public const string Id_enemy0001 = "enemy0001";
        /// <summary>
        /// 名称:  <br/>
        /// 备注: 
        /// </summary>
        public const string Id_weapon0001 = "weapon0001";
        /// <summary>
        /// 名称:  <br/>
        /// 备注: 
        /// </summary>
        public const string Id_weapon0002 = "weapon0002";
        /// <summary>
        /// 名称:  <br/>
        /// 备注: 
        /// </summary>
        public const string Id_weapon0003 = "weapon0003";
        /// <summary>
        /// 名称:  <br/>
        /// 备注: 
        /// </summary>
        public const string Id_weapon0004 = "weapon0004";
        /// <summary>
        /// 名称:  <br/>
        /// 备注: 
        /// </summary>
        public const string Id_weapon0005 = "weapon0005";
        /// <summary>
        /// 名称:  <br/>
        /// 备注: 
        /// </summary>
        public const string Id_weapon0006 = "weapon0006";
        /// <summary>
        /// 名称:  <br/>
        /// 备注: 
        /// </summary>
        public const string Id_bullet0001 = "bullet0001";
        /// <summary>
        /// 名称:  <br/>
        /// 备注: 
        /// </summary>
        public const string Id_bullet0002 = "bullet0002";
        /// <summary>
        /// 名称:  <br/>
        /// 备注: 
        /// </summary>
        public const string Id_shell0001 = "shell0001";
        /// <summary>
        /// 名称:  <br/>
        /// 备注: 
        /// </summary>
        public const string Id_shell0002 = "shell0002";
        /// <summary>
        /// 名称:  <br/>
        /// 备注: 
        /// </summary>
        public const string Id_shell0003 = "shell0003";
        /// <summary>
        /// 名称:  <br/>
        /// 备注: 敌人死亡碎片
        /// </summary>
        public const string Id_effect0001 = "effect0001";
        /// <summary>
        /// 名称: 鞋子 <br/>
        /// 备注: 增加移速的buff
        /// </summary>
        public const string Id_prop0001 = "prop0001";
        /// <summary>
        /// 名称:  <br/>
        /// 备注: 地牢房间的门(东侧)
        /// </summary>
        public const string Id_other_door_e = "other_door_e";
        /// <summary>
        /// 名称:  <br/>
        /// 备注: 地牢房间的门(西侧)
        /// </summary>
        public const string Id_other_door_w = "other_door_w";
        /// <summary>
        /// 名称:  <br/>
        /// 备注: 地牢房间的门(南侧)
        /// </summary>
        public const string Id_other_door_s = "other_door_s";
        /// <summary>
        /// 名称:  <br/>
        /// 备注: 地牢房间的门(北侧)
        /// </summary>
        public const string Id_other_door_n = "other_door_n";
    }
    private static void _InitRegister()
    {
        _activityRegisterMap.Add("role0001", new RegisterActivityData("res://prefab/role/Role0001.tscn", ExcelConfig.ActivityObject_Map["role0001"]));
        _activityRegisterMap.Add("enemy0001", new RegisterActivityData("res://prefab/role/Enemy0001.tscn", ExcelConfig.ActivityObject_Map["enemy0001"]));
        _activityRegisterMap.Add("weapon0001", new RegisterActivityData("res://prefab/weapon/Weapon0001.tscn", ExcelConfig.ActivityObject_Map["weapon0001"]));
        _activityRegisterMap.Add("weapon0002", new RegisterActivityData("res://prefab/weapon/Weapon0002.tscn", ExcelConfig.ActivityObject_Map["weapon0002"]));
        _activityRegisterMap.Add("weapon0003", new RegisterActivityData("res://prefab/weapon/Weapon0003.tscn", ExcelConfig.ActivityObject_Map["weapon0003"]));
        _activityRegisterMap.Add("weapon0004", new RegisterActivityData("res://prefab/weapon/Weapon0004.tscn", ExcelConfig.ActivityObject_Map["weapon0004"]));
        _activityRegisterMap.Add("weapon0005", new RegisterActivityData("res://prefab/weapon/Weapon0005.tscn", ExcelConfig.ActivityObject_Map["weapon0005"]));
        _activityRegisterMap.Add("weapon0006", new RegisterActivityData("res://prefab/weapon/Weapon0006.tscn", ExcelConfig.ActivityObject_Map["weapon0006"]));
        _activityRegisterMap.Add("bullet0001", new RegisterActivityData("res://prefab/bullet/Bullet0001.tscn", ExcelConfig.ActivityObject_Map["bullet0001"]));
        _activityRegisterMap.Add("bullet0002", new RegisterActivityData("res://prefab/bullet/Bullet0002.tscn", ExcelConfig.ActivityObject_Map["bullet0002"]));
        _activityRegisterMap.Add("shell0001", new RegisterActivityData("res://prefab/shell/Shell0001.tscn", ExcelConfig.ActivityObject_Map["shell0001"]));
        _activityRegisterMap.Add("shell0002", new RegisterActivityData("res://prefab/shell/Shell0002.tscn", ExcelConfig.ActivityObject_Map["shell0002"]));
        _activityRegisterMap.Add("shell0003", new RegisterActivityData("res://prefab/shell/Shell0003.tscn", ExcelConfig.ActivityObject_Map["shell0003"]));
        _activityRegisterMap.Add("effect0001", new RegisterActivityData("res://prefab/effect/activityObject/Effect0001.tscn", ExcelConfig.ActivityObject_Map["effect0001"]));
        _activityRegisterMap.Add("prop0001", new RegisterActivityData("res://prefab/prop/buff/Prop0001.tscn", ExcelConfig.ActivityObject_Map["prop0001"]));
        _activityRegisterMap.Add("other_door_e", new RegisterActivityData("res://prefab/map/RoomDoor_E.tscn", ExcelConfig.ActivityObject_Map["other_door_e"]));
        _activityRegisterMap.Add("other_door_w", new RegisterActivityData("res://prefab/map/RoomDoor_W.tscn", ExcelConfig.ActivityObject_Map["other_door_w"]));
        _activityRegisterMap.Add("other_door_s", new RegisterActivityData("res://prefab/map/RoomDoor_S.tscn", ExcelConfig.ActivityObject_Map["other_door_s"]));
        _activityRegisterMap.Add("other_door_n", new RegisterActivityData("res://prefab/map/RoomDoor_N.tscn", ExcelConfig.ActivityObject_Map["other_door_n"]));
    }
}
