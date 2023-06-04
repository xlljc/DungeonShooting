using System;
using System.Collections.Generic;
using System.Text.Json;
using Godot;

namespace Config;

public static partial class ExcelConfig
{
    /// <summary>
    /// Role.xlsx表数据集合, 以 List 形式存储, 数据顺序与 Excel 表相同
    /// </summary>
    public static List<Role> Role_List { get; private set; }
    /// <summary>
    /// Role.xlsx表数据集合, 里 Map 形式存储, key 为 Id
    /// </summary>
    public static Dictionary<string, Role> Role_Map { get; private set; }

    /// <summary>
    /// Weapon.xlsx表数据集合, 以 List 形式存储, 数据顺序与 Excel 表相同
    /// </summary>
    public static List<Weapon> Weapon_List { get; private set; }
    /// <summary>
    /// Weapon.xlsx表数据集合, 里 Map 形式存储, key 为 Id
    /// </summary>
    public static Dictionary<string, Weapon> Weapon_Map { get; private set; }


    private static bool _init = false;
    /// <summary>
    /// 初始化所有配置表数据
    /// </summary>
    public static void Init()
    {
        if (_init) return;
        _init = true;

        _InitRoleConfig();
        _InitWeaponConfig();
    }
    private static void _InitRoleConfig()
    {
        try
        {
            var text = _ReadConfigAsText("res://resource/config/Role.json");
            Role_List = JsonSerializer.Deserialize<List<Role>>(text);
            Role_Map = new Dictionary<string, Role>();
            foreach (var item in Role_List)
            {
                Role_Map.Add(item.Id, item);
            }
        }
        catch (Exception e)
        {
            GD.PrintErr(e.ToString());
            throw new Exception("初始化表'Role'失败!");
        }
    }
    private static void _InitWeaponConfig()
    {
        try
        {
            var text = _ReadConfigAsText("res://resource/config/Weapon.json");
            Weapon_List = JsonSerializer.Deserialize<List<Weapon>>(text);
            Weapon_Map = new Dictionary<string, Weapon>();
            foreach (var item in Weapon_List)
            {
                Weapon_Map.Add(item.Id, item);
            }
        }
        catch (Exception e)
        {
            GD.PrintErr(e.ToString());
            throw new Exception("初始化表'Weapon'失败!");
        }
    }
    private static string _ReadConfigAsText(string path)
    {
        var file = FileAccess.Open(path, FileAccess.ModeFlags.Read);
        var asText = file.GetAsText();
        file.Dispose();
        return asText;
    }
}