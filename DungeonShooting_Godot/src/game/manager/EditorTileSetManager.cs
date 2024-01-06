
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