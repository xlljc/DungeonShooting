
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
        CustomTileSetPath = GameConfig.RoomTileSetDir;
#endif
        EventManager.AddEventListener(EventEnum.OnTileSetSave, OnTileSetSave);
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