using System;
using System.Collections.Generic;
using System.Text.Json;
using Godot;

namespace Config;

public static partial class ExcelConfig
{
    /// <summary>
    /// ActivePropBase.xlsx表数据集合, 以 List 形式存储, 数据顺序与 Excel 表相同
    /// </summary>
    public static List<ActivePropBase> ActivePropBase_List { get; private set; }
    /// <summary>
    /// ActivePropBase.xlsx表数据集合, 里 Map 形式存储, key 为 Id
    /// </summary>
    public static Dictionary<string, ActivePropBase> ActivePropBase_Map { get; private set; }

    /// <summary>
    /// ActivityBase.xlsx表数据集合, 以 List 形式存储, 数据顺序与 Excel 表相同
    /// </summary>
    public static List<ActivityBase> ActivityBase_List { get; private set; }
    /// <summary>
    /// ActivityBase.xlsx表数据集合, 里 Map 形式存储, key 为 Id
    /// </summary>
    public static Dictionary<string, ActivityBase> ActivityBase_Map { get; private set; }

    /// <summary>
    /// ActivityMaterial.xlsx表数据集合, 以 List 形式存储, 数据顺序与 Excel 表相同
    /// </summary>
    public static List<ActivityMaterial> ActivityMaterial_List { get; private set; }
    /// <summary>
    /// ActivityMaterial.xlsx表数据集合, 里 Map 形式存储, key 为 Id
    /// </summary>
    public static Dictionary<string, ActivityMaterial> ActivityMaterial_Map { get; private set; }

    /// <summary>
    /// AiAttackAttr.xlsx表数据集合, 以 List 形式存储, 数据顺序与 Excel 表相同
    /// </summary>
    public static List<AiAttackAttr> AiAttackAttr_List { get; private set; }
    /// <summary>
    /// AiAttackAttr.xlsx表数据集合, 里 Map 形式存储, key 为 Id
    /// </summary>
    public static Dictionary<string, AiAttackAttr> AiAttackAttr_Map { get; private set; }

    /// <summary>
    /// BuffPropBase.xlsx表数据集合, 以 List 形式存储, 数据顺序与 Excel 表相同
    /// </summary>
    public static List<BuffPropBase> BuffPropBase_List { get; private set; }
    /// <summary>
    /// BuffPropBase.xlsx表数据集合, 里 Map 形式存储, key 为 Id
    /// </summary>
    public static Dictionary<string, BuffPropBase> BuffPropBase_Map { get; private set; }

    /// <summary>
    /// BulletBase.xlsx表数据集合, 以 List 形式存储, 数据顺序与 Excel 表相同
    /// </summary>
    public static List<BulletBase> BulletBase_List { get; private set; }
    /// <summary>
    /// BulletBase.xlsx表数据集合, 里 Map 形式存储, key 为 Id
    /// </summary>
    public static Dictionary<string, BulletBase> BulletBase_Map { get; private set; }

    /// <summary>
    /// EnemyBase.xlsx表数据集合, 以 List 形式存储, 数据顺序与 Excel 表相同
    /// </summary>
    public static List<EnemyBase> EnemyBase_List { get; private set; }
    /// <summary>
    /// EnemyBase.xlsx表数据集合, 里 Map 形式存储, key 为 Id
    /// </summary>
    public static Dictionary<string, EnemyBase> EnemyBase_Map { get; private set; }

    /// <summary>
    /// LiquidMaterial.xlsx表数据集合, 以 List 形式存储, 数据顺序与 Excel 表相同
    /// </summary>
    public static List<LiquidMaterial> LiquidMaterial_List { get; private set; }
    /// <summary>
    /// LiquidMaterial.xlsx表数据集合, 里 Map 形式存储, key 为 Id
    /// </summary>
    public static Dictionary<string, LiquidMaterial> LiquidMaterial_Map { get; private set; }

    /// <summary>
    /// Sound.xlsx表数据集合, 以 List 形式存储, 数据顺序与 Excel 表相同
    /// </summary>
    public static List<Sound> Sound_List { get; private set; }
    /// <summary>
    /// Sound.xlsx表数据集合, 里 Map 形式存储, key 为 Id
    /// </summary>
    public static Dictionary<string, Sound> Sound_Map { get; private set; }

    /// <summary>
    /// WeaponBase.xlsx表数据集合, 以 List 形式存储, 数据顺序与 Excel 表相同
    /// </summary>
    public static List<WeaponBase> WeaponBase_List { get; private set; }
    /// <summary>
    /// WeaponBase.xlsx表数据集合, 里 Map 形式存储, key 为 Id
    /// </summary>
    public static Dictionary<string, WeaponBase> WeaponBase_Map { get; private set; }


    private static bool _init = false;
    /// <summary>
    /// 初始化所有配置表数据
    /// </summary>
    public static void Init()
    {
        if (_init) return;
        _init = true;

        _InitActivePropBaseConfig();
        _InitActivityBaseConfig();
        _InitActivityMaterialConfig();
        _InitAiAttackAttrConfig();
        _InitBuffPropBaseConfig();
        _InitBulletBaseConfig();
        _InitEnemyBaseConfig();
        _InitLiquidMaterialConfig();
        _InitSoundConfig();
        _InitWeaponBaseConfig();

        _InitActivePropBaseRef();
        _InitActivityBaseRef();
        _InitBuffPropBaseRef();
        _InitEnemyBaseRef();
        _InitWeaponBaseRef();
    }
    private static void _InitActivePropBaseConfig()
    {
        try
        {
            var text = _ReadConfigAsText("res://resource/config/ActivePropBase.json");
            ActivePropBase_List = new List<ActivePropBase>(JsonSerializer.Deserialize<List<Ref_ActivePropBase>>(text));
            ActivePropBase_Map = new Dictionary<string, ActivePropBase>();
            foreach (var item in ActivePropBase_List)
            {
                ActivePropBase_Map.Add(item.Id, item);
            }
        }
        catch (Exception e)
        {
            GD.PrintErr(e.ToString());
            throw new Exception("初始化表'ActivePropBase'失败!");
        }
    }
    private static void _InitActivityBaseConfig()
    {
        try
        {
            var text = _ReadConfigAsText("res://resource/config/ActivityBase.json");
            ActivityBase_List = new List<ActivityBase>(JsonSerializer.Deserialize<List<Ref_ActivityBase>>(text));
            ActivityBase_Map = new Dictionary<string, ActivityBase>();
            foreach (var item in ActivityBase_List)
            {
                ActivityBase_Map.Add(item.Id, item);
            }
        }
        catch (Exception e)
        {
            GD.PrintErr(e.ToString());
            throw new Exception("初始化表'ActivityBase'失败!");
        }
    }
    private static void _InitActivityMaterialConfig()
    {
        try
        {
            var text = _ReadConfigAsText("res://resource/config/ActivityMaterial.json");
            ActivityMaterial_List = JsonSerializer.Deserialize<List<ActivityMaterial>>(text);
            ActivityMaterial_Map = new Dictionary<string, ActivityMaterial>();
            foreach (var item in ActivityMaterial_List)
            {
                ActivityMaterial_Map.Add(item.Id, item);
            }
        }
        catch (Exception e)
        {
            GD.PrintErr(e.ToString());
            throw new Exception("初始化表'ActivityMaterial'失败!");
        }
    }
    private static void _InitAiAttackAttrConfig()
    {
        try
        {
            var text = _ReadConfigAsText("res://resource/config/AiAttackAttr.json");
            AiAttackAttr_List = JsonSerializer.Deserialize<List<AiAttackAttr>>(text);
            AiAttackAttr_Map = new Dictionary<string, AiAttackAttr>();
            foreach (var item in AiAttackAttr_List)
            {
                AiAttackAttr_Map.Add(item.Id, item);
            }
        }
        catch (Exception e)
        {
            GD.PrintErr(e.ToString());
            throw new Exception("初始化表'AiAttackAttr'失败!");
        }
    }
    private static void _InitBuffPropBaseConfig()
    {
        try
        {
            var text = _ReadConfigAsText("res://resource/config/BuffPropBase.json");
            BuffPropBase_List = new List<BuffPropBase>(JsonSerializer.Deserialize<List<Ref_BuffPropBase>>(text));
            BuffPropBase_Map = new Dictionary<string, BuffPropBase>();
            foreach (var item in BuffPropBase_List)
            {
                BuffPropBase_Map.Add(item.Id, item);
            }
        }
        catch (Exception e)
        {
            GD.PrintErr(e.ToString());
            throw new Exception("初始化表'BuffPropBase'失败!");
        }
    }
    private static void _InitBulletBaseConfig()
    {
        try
        {
            var text = _ReadConfigAsText("res://resource/config/BulletBase.json");
            BulletBase_List = JsonSerializer.Deserialize<List<BulletBase>>(text);
            BulletBase_Map = new Dictionary<string, BulletBase>();
            foreach (var item in BulletBase_List)
            {
                BulletBase_Map.Add(item.Id, item);
            }
        }
        catch (Exception e)
        {
            GD.PrintErr(e.ToString());
            throw new Exception("初始化表'BulletBase'失败!");
        }
    }
    private static void _InitEnemyBaseConfig()
    {
        try
        {
            var text = _ReadConfigAsText("res://resource/config/EnemyBase.json");
            EnemyBase_List = new List<EnemyBase>(JsonSerializer.Deserialize<List<Ref_EnemyBase>>(text));
            EnemyBase_Map = new Dictionary<string, EnemyBase>();
            foreach (var item in EnemyBase_List)
            {
                EnemyBase_Map.Add(item.Id, item);
            }
        }
        catch (Exception e)
        {
            GD.PrintErr(e.ToString());
            throw new Exception("初始化表'EnemyBase'失败!");
        }
    }
    private static void _InitLiquidMaterialConfig()
    {
        try
        {
            var text = _ReadConfigAsText("res://resource/config/LiquidMaterial.json");
            LiquidMaterial_List = JsonSerializer.Deserialize<List<LiquidMaterial>>(text);
            LiquidMaterial_Map = new Dictionary<string, LiquidMaterial>();
            foreach (var item in LiquidMaterial_List)
            {
                LiquidMaterial_Map.Add(item.Id, item);
            }
        }
        catch (Exception e)
        {
            GD.PrintErr(e.ToString());
            throw new Exception("初始化表'LiquidMaterial'失败!");
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
    private static void _InitWeaponBaseConfig()
    {
        try
        {
            var text = _ReadConfigAsText("res://resource/config/WeaponBase.json");
            WeaponBase_List = new List<WeaponBase>(JsonSerializer.Deserialize<List<Ref_WeaponBase>>(text));
            WeaponBase_Map = new Dictionary<string, WeaponBase>();
            foreach (var item in WeaponBase_List)
            {
                WeaponBase_Map.Add(item.Id, item);
            }
        }
        catch (Exception e)
        {
            GD.PrintErr(e.ToString());
            throw new Exception("初始化表'WeaponBase'失败!");
        }
    }

    private static void _InitActivePropBaseRef()
    {
        foreach (Ref_ActivePropBase item in ActivePropBase_List)
        {
            try
            {
                if (!string.IsNullOrEmpty(item.__Activity))
                {
                    item.Activity = ActivityBase_Map[item.__Activity];
                }

            }
            catch (Exception e)
            {
                GD.PrintErr(e.ToString());
                throw new Exception("初始化'ActivePropBase'引用其他表数据失败, 当前行id: " + item.Id);
            }
        }
    }
    private static void _InitActivityBaseRef()
    {
        foreach (Ref_ActivityBase item in ActivityBase_List)
        {
            try
            {
                if (!string.IsNullOrEmpty(item.__Material))
                {
                    item.Material = ActivityMaterial_Map[item.__Material];
                }

            }
            catch (Exception e)
            {
                GD.PrintErr(e.ToString());
                throw new Exception("初始化'ActivityBase'引用其他表数据失败, 当前行id: " + item.Id);
            }
        }
    }
    private static void _InitBuffPropBaseRef()
    {
        foreach (Ref_BuffPropBase item in BuffPropBase_List)
        {
            try
            {
                if (!string.IsNullOrEmpty(item.__Activity))
                {
                    item.Activity = ActivityBase_Map[item.__Activity];
                }

            }
            catch (Exception e)
            {
                GD.PrintErr(e.ToString());
                throw new Exception("初始化'BuffPropBase'引用其他表数据失败, 当前行id: " + item.Id);
            }
        }
    }
    private static void _InitEnemyBaseRef()
    {
        foreach (Ref_EnemyBase item in EnemyBase_List)
        {
            try
            {
                if (!string.IsNullOrEmpty(item.__Activity))
                {
                    item.Activity = ActivityBase_Map[item.__Activity];
                }

            }
            catch (Exception e)
            {
                GD.PrintErr(e.ToString());
                throw new Exception("初始化'EnemyBase'引用其他表数据失败, 当前行id: " + item.Id);
            }
        }
    }
    private static void _InitWeaponBaseRef()
    {
        foreach (Ref_WeaponBase item in WeaponBase_List)
        {
            try
            {
                if (!string.IsNullOrEmpty(item.__Activity))
                {
                    item.Activity = ActivityBase_Map[item.__Activity];
                }

                if (!string.IsNullOrEmpty(item.__Bullet))
                {
                    item.Bullet = BulletBase_Map[item.__Bullet];
                }

                if (!string.IsNullOrEmpty(item.__Shell))
                {
                    item.Shell = ActivityBase_Map[item.__Shell];
                }

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

                if (!string.IsNullOrEmpty(item.__BeLoadedSound))
                {
                    item.BeLoadedSound = Sound_Map[item.__BeLoadedSound];
                }

                if (item.__OtherSoundMap != null)
                {
                    item.OtherSoundMap = new Dictionary<string, Sound>();
                    foreach (var pair in item.__OtherSoundMap)
                    {
                        item.OtherSoundMap.Add(pair.Key, Sound_Map[pair.Value]);
                    }
                }

                if (!string.IsNullOrEmpty(item.__AiAttackAttr))
                {
                    item.AiAttackAttr = AiAttackAttr_Map[item.__AiAttackAttr];
                }

            }
            catch (Exception e)
            {
                GD.PrintErr(e.ToString());
                throw new Exception("初始化'WeaponBase'引用其他表数据失败, 当前行id: " + item.Id);
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