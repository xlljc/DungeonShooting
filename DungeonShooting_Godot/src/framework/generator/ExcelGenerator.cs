using System;
using System.IO;
using System.Text.Json;
using Godot;
using Array = Godot.Collections.Array;

namespace Generator;

public static class ExcelGenerator
{
    public static void ExportExcel()
    {
        var arr = new Array();
        OS.Execute("excel/DungeonShooting_ExcelTool.exe", new string[0], arr);
        foreach (var message in arr)
        {
            GD.Print(message);
        }

        try
        {
            GeneratorActivityObjectInit();
            GD.Print("生成'src/framework/activity/ActivityObject_Init.cs'成功!");
        }
        catch (Exception e)
        {
            GD.PrintErr(e.ToString());
        }
    }

    //生成初始化 ActivityObject 代码
    private static void GeneratorActivityObjectInit()
    {
        var text = File.ReadAllText("resource/config/ActivityObject.json");
        var array = JsonSerializer.Deserialize<System.Collections.Generic.Dictionary<string, System.Object>[]>(text);
        
        var code1 = "";
        var code2 = "";

        foreach (var item in array)
        {
            var id = item["Id"];
            code1 += $"        public const string Id_{id} = \"{id}\";\n";
            code2 += $"        _activityRegisterMap.Add(\"{id}\", \"{item["Prefab"]}\");\n";
        }
        
        var str = $"/// <summary>\n";
        str += $"/// 根据配置表注册物体, 该类是自动生成的, 请不要手动编辑!\n";
        str += $"/// </summary>\n";
        str += $"public partial class ActivityObject\n";
        str += $"{{\n";
        
        str += $"    /// <summary>\n";
        str += $"    /// 存放所有在表中注册的物体的id\n";
        str += $"    /// </summary>\n";
        str += $"    public static class Ids\n";
        str += $"    {{\n";
        str += code1;
        str += $"    }}\n";
        
        str += $"    private static void _InitRegister()\n";
        str += $"    {{\n";
        str += code2;
        str += $"    }}\n";
        str += $"}}\n";
        
        File.WriteAllText("src/framework/activity/ActivityObject_Init.cs", str);
    }
}