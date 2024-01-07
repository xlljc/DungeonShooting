
using System.IO;
using System.Text.Json;
using Godot;

public static class EditorTileSetManager
{
    
    /// <summary>
    /// 扫描路径
    /// </summary>
    public static string CustomTileSetPath { get; private set; }
    
    private static bool _init;
    
    public static void Init()
    {
        if (_init)
        {
            return;
        }

        _init = true;
#if TOOLS
        CustomTileSetPath = GameConfig.RoomTileSetDir;
#else
        CustomMapPath = GameConfig.RoomTileSetDir;
#endif
        EventManager.AddEventListener(EventEnum.OnTileSetSave, OnTileSetSave);
    }

    /// <summary>
    /// 将地形掩码存入 TileSetTerrainInfo 中
    /// </summary>
    public static void SetTileSetTerrainBit(TileSetTerrainInfo terrain, int index, byte type, int[] cellData)
    {
        if (type == 1) //顶部墙壁
        {
            switch (index)
            {
                //第一排
                case 0: terrain._000_010_010 = cellData; break;
                case 1: terrain._000_011_010 = cellData; break;
                case 2: terrain._000_111_010 = cellData; break;
                case 3: terrain._000_110_010 = cellData; break;
                
                case 4: terrain._110_111_010 = cellData; break;
                case 5: terrain._000_111_011 = cellData; break;
                case 6: terrain._000_111_110 = cellData; break;
                case 7: terrain._011_111_010 = cellData; break;
                
                case 8: terrain._000_011_011 = cellData; break;
                case 9: terrain._010_111_111 = cellData; break;
                case 10: terrain._000_111_111 = cellData; break;
                case 11: terrain._000_110_110 = cellData; break;
                
                //第二排
                case 12: terrain._010_010_010 = cellData; break;
                case 13: terrain._010_011_010 = cellData; break;
                case 14: terrain._010_111_010 = cellData; break;
                case 15: terrain._010_110_010 = cellData; break;
                
                case 16: terrain._010_011_011 = cellData; break;
                case 17: terrain._011_111_111 = cellData; break;
                case 18: terrain._110_111_111 = cellData; break;
                case 19: terrain._010_110_110 = cellData; break;
                
                case 20: terrain._011_011_011 = cellData; break;
                case 21: terrain._011_111_110 = cellData; break;
                case 22: break;
                case 23: terrain._110_111_110 = cellData; break;
                
                //第三排
                case 24: terrain._010_010_000 = cellData; break;
                case 25: terrain._010_011_000 = cellData; break;
                case 26: terrain._010_111_000 = cellData; break;
                case 27: terrain._010_110_000 = cellData; break;
                
                case 28: terrain._011_011_010 = cellData; break;
                case 29: terrain._111_111_011 = cellData; break;
                case 30: terrain._111_111_110 = cellData; break;
                case 31: terrain._110_110_010 = cellData; break;
                
                case 32: terrain._011_111_011 = cellData; break;
                case 33: terrain._111_111_111 = cellData; break;
                case 34: terrain._110_111_011 = cellData; break;
                case 35: terrain._110_110_110 = cellData; break;
                
                //第四排
                case 36: terrain._000_010_000 = cellData; break;
                case 37: terrain._000_011_000 = cellData; break;
                case 38: terrain._000_111_000 = cellData; break;
                case 39: terrain._000_110_000 = cellData; break;
                
                case 40: terrain._010_111_110 = cellData; break;
                case 41: terrain._011_111_000 = cellData; break;
                case 42: terrain._110_111_000 = cellData; break;
                case 43: terrain._010_111_011 = cellData; break;
                
                case 44: terrain._011_011_000 = cellData; break;
                case 45: terrain._111_111_000 = cellData; break;
                case 46: terrain._111_111_010 = cellData; break;
                case 47: terrain._110_110_000 = cellData; break;
            }
        }
        else if (type == 2) //侧方墙壁
        {
            switch (index)
            {
                case 0: terrain._vs = cellData; break;
                case 1: terrain._vl = cellData; break;
                case 2: terrain._vc = cellData; break;
                case 3: terrain._vr = cellData; break;
            }
        }
        else if (type == 3) //地板
        {
            terrain._f = cellData;
        }
    }

    /// <summary>
    /// 获取指定索引的地形掩码存储的数据
    /// </summary>
    public static int[] GetTileSetTerrainBit(TileSetTerrainInfo terrain, int index, byte type)
    {
        if (type == 1) //顶部墙壁
        {
            switch (index)
            {
                //第一排
                case 0: return terrain._000_010_010;
                case 1: return terrain._000_011_010;
                case 2: return terrain._000_111_010;
                case 3: return terrain._000_110_010;
                
                case 4: return terrain._110_111_010;
                case 5: return terrain._000_111_011;
                case 6: return terrain._000_111_110;
                case 7: return terrain._011_111_010;
                
                case 8: return terrain._000_011_011;
                case 9: return terrain._010_111_111;
                case 10: return terrain._000_111_111;
                case 11: return terrain._000_110_110;
                
                //第二排
                case 12: return terrain._010_010_010;
                case 13: return terrain._010_011_010;
                case 14: return terrain._010_111_010;
                case 15: return terrain._010_110_010;
                
                case 16: return terrain._010_011_011;
                case 17: return terrain._011_111_111;
                case 18: return terrain._110_111_111;
                case 19: return terrain._010_110_110;
                
                case 20: return terrain._011_011_011;
                case 21: return terrain._011_111_110;
                case 22: break;
                case 23: return terrain._110_111_110;
                
                //第三排
                case 24: return terrain._010_010_000;
                case 25: return terrain._010_011_000;
                case 26: return terrain._010_111_000;
                case 27: return terrain._010_110_000;
                
                case 28: return terrain._011_011_010;
                case 29: return terrain._111_111_011;
                case 30: return terrain._111_111_110;
                case 31: return terrain._110_110_010;
                
                case 32: return terrain._011_111_011;
                case 33: return terrain._111_111_111;
                case 34: return terrain._110_111_011;
                case 35: return terrain._110_110_110;
                
                //第四排
                case 36: return terrain._000_010_000;
                case 37: return terrain._000_011_000;
                case 38: return terrain._000_111_000;
                case 39: return terrain._000_110_000;
                
                case 40: return terrain._010_111_110;
                case 41: return terrain._011_111_000;
                case 42: return terrain._110_111_000;
                case 43: return terrain._010_111_011;
                
                case 44: return terrain._011_011_000;
                case 45: return terrain._111_111_000;
                case 46: return terrain._111_111_010;
                case 47: return terrain._110_110_000;
            }
        }
        else if (type == 2) //侧方墙壁
        {
            switch (index)
            {
                case 0: return terrain._vs;
                case 1: return terrain._vl;
                case 2: return terrain._vc;
                case 3: return terrain._vr;
            }
        }
        else if (type == 3) //地板
        {
            return terrain._f;
        }

        return null;
    }

    /// <summary>
    /// 保存TileSetConfig数据
    /// </summary>
    public static void SaveTileSetConfig()
    {
        var options = new JsonSerializerOptions();
        options.WriteIndented = true;
        var jsonText = JsonSerializer.Serialize(GameApplication.Instance.TileSetConfig, options);
        File.WriteAllText(GameConfig.RoomTileSetDir + GameConfig.TileSetConfigFile, jsonText);
    }

    /// <summary>
    /// 保存TileSetInfo数据
    /// </summary>
    public static void SaveTileSetInfo(TileSetInfo tileSetInfo)
    {
        var dir = CustomTileSetPath + tileSetInfo.Name;
        if (Directory.Exists(dir))
        {
            //删除多余文件
            if (tileSetInfo.Sources == null)
            {
                Directory.Delete(dir, true);
                Directory.CreateDirectory(dir);
            }
            else
            {
                var directoryInfo = new DirectoryInfo(dir);
                var fileInfos = directoryInfo.GetFiles();
                foreach (var fileInfo in fileInfos)
                {
                    if (fileInfo.Name.EndsWith(".png"))
                    {
                        var name = fileInfo.Name.Substring(0, fileInfo.Name.Length - 4);
                        if (tileSetInfo.Sources.FindIndex(info => info.Name == name) < 0)
                        {
                            fileInfo.Delete();
                        }
                    }
                }
            }
        }
        else
        {
            Directory.CreateDirectory(dir);
        }

        var path = dir + "/TileSet.json";

        //保存json
        var options = new JsonSerializerOptions();
        options.WriteIndented = true;
        var jsonText = JsonSerializer.Serialize(tileSetInfo, options);
        File.WriteAllText(path, jsonText);

        //保存资源
        if (tileSetInfo.Sources != null)
        {
            foreach (var sourceInfo in tileSetInfo.Sources)
            {
                var image = sourceInfo.GetSourceImage();
                if (image != null)
                {
                    image.SavePng(dir + "/" + sourceInfo.Name + ".png");
                }
            }
        }
    }

    //保存图块集
    private static void OnTileSetSave(object o)
    {
        if (o is TileSetSplit tileSetSplit)
        {
            var tileSetInfo = tileSetSplit.TileSetInfo;
            SaveTileSetInfo(tileSetInfo);
        }
    }
}