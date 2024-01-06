
using System.IO;
using System.Text.Json;
using Godot;

public static class EditorTileSetManager
{
    
    /// <summary>
    /// 扫描路径
    /// </summary>
    public static string CustomMapPath { get; private set; }
    
    private static bool _init;
    
    public static void Init()
    {
        if (_init)
        {
            return;
        }

        _init = true;
#if TOOLS
        CustomMapPath = GameConfig.RoomTileSetDir;
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

    //保存图块集
    private static void OnTileSetSave(object o)
    {
        if (o is TileSetInfo tileSetInfo)
        {
            var dir = CustomMapPath + tileSetInfo.Name;
            if (Directory.Exists(dir))
            {
                Directory.Delete(dir, true);
            }
            Directory.CreateDirectory(dir);

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
                    if (sourceInfo.IsOverWriteImage())
                    {
                        var image = sourceInfo.GetSourceImage();
                        image.SavePng(dir + "/" + sourceInfo.Name + ".png");
                    }
                }
            }
        }
    }
}