
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
            if (!Directory.Exists(dir))
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