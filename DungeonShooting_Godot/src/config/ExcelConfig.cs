using System;
using System.Collections.Generic;
using System.Text.Json;
using Godot;

namespace Config;

public static partial class ExcelConfig
{
    /// <summary>
    /// ActivityObject.xlsx表数据集合, 以 List 形式存储, 数据顺序与 Excel 表相同
    /// </summary>
    public static List<ActivityObject> ActivityObject_List { get; private set; }
    /// <summary>
    /// ActivityObject.xlsx表数据集合, 里 Map 形式存储, key 为 Id
    /// </summary>
    public static Dictionary<string, ActivityObject> ActivityObject_Map { get; private set; }

    /// <summary>
    /// Sound.xlsx表数据集合, 以 List 形式存储, 数据顺序与 Excel 表相同
    /// </summary>
    public static List<Sound> Sound_List { get; private set; }
    /// <summary>
    /// Sound.xlsx表数据集合, 里 Map 形式存储, key 为 Id
    /// </summary>
    public static Dictionary<string, Sound> Sound_Map { get; private set; }

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

        _InitActivityObjectConfig();
        _InitSoundConfig();
        _InitWeaponConfig();

        _InitWeaponRef();
    }
    private static void _InitActivityObjectConfig()
    {
        try
        {
            var text = _ReadConfigAsText("res://resource/config/ActivityObject.json");
            ActivityObject_List = JsonSerializer.Deserialize<List<ActivityObject>>(text);
            ActivityObject_Map = new Dictionary<string, ActivityObject>();
            foreach (var item in ActivityObject_List)
            {
                ActivityObject_Map.Add(item.Id, item);
            }
        }
        catch (Exception e)
        {
            GD.PrintErr(e.ToString());
            throw new Exception("初始化表'ActivityObject'失败!");
        }
    }
    private static void _InitSoundConfig()
    {
        try
        {
            var text = _ReadConfigAsText("res://resource/config/Sound.json");
            Sound_List = JsonSerializer.Deserialize<List<Sound>>(text);
            Sound_Map = new Dictionary<string, Sound>();
            foreach (var item in Sound_List)
            {
                Sound_Map.Add(item.Id, item);
            }
        }
        catch (Exception e)
        {
            GD.PrintErr(e.ToString());
            throw new Exception("初始化表'Sound'失败!");
        }
    }
    private static void _InitWeaponConfig()
    {
        try
        {
            var text = _ReadConfigAsText("res://resource/config/Weapon.json");
            Weapon_List = new List<Weapon>(JsonSerializer.Deserialize<List<Ref_Weapon>>(text));
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

    private static void _InitWeaponRef()
    {
        foreach (Ref_Weapon item in Weapon_List)
        {
            try
            {
                if (!string.IsNullOrEmpty(item.__ShootSound))
                {
                    item.ShootSound = Sound_Map[item.__ShootSound];
                }

                if (!string.IsNullOrEmpty(item.__BeginReloadSound))
                {
                    item.BeginReloadSound = Sound_Map[item.__BeginReloadSound];
                }

                if (!string.IsNullOrEmpty(item.__ReloadSound))
                {
                    item.ReloadSound = Sound_Map[item.__ReloadSound];
                }

                if (!string.IsNullOrEmpty(item.__ReloadFinishSound))
                {
                    item.ReloadFinishSound = Sound_Map[item.__ReloadFinishSound];
                }

                if (!string.IsNullOrEmpty(item.__EquipSound))
                {
                    item.EquipSound = Sound_Map[item.__EquipSound];
                }

                if (item.__OtherSoundMap != null)
                {
                    item.OtherSoundMap = new Dictionary<string, Sound>();
                    foreach (var pair in item.__OtherSoundMap)
                    {
                        item.OtherSoundMap.Add(pair.Key, Sound_Map[pair.Value]);
                    }
                }

                if (!string.IsNullOrEmpty(item.__AiUseAttribute))
                {
                    item.AiUseAttribute = Weapon_Map[item.__AiUseAttribute];
                }

            }
            catch (Exception e)
            {
                GD.PrintErr(e.ToString());
                throw new Exception("初始化'Weapon'引用其他表数据失败, 当前行id: " + item.Id);
            }
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