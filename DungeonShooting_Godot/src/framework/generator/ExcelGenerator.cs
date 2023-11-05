#if TOOLS

using System;
using System.IO;
using System.Text.Json;
using Config;
using Godot;
using Array = Godot.Collections.Array;

namespace Generator;

public static class ExcelGenerator
{
    public static void ExportExcel()
    {
        var arr = new Array();
        var excelPath = "excel/excelFile/";
        var jsonPath = "resource/config/";
        var codePath = "src/config/";
        OS.Execute("excel/DungeonShooting_ExcelTool.exe", new string[] { excelPath, jsonPath, codePath }, arr);
        foreach (var message in arr)
        {
            Debug.Log(message);
        }

        try
        {
            GeneratorActivityObjectInit();
            Debug.Log("生成'src/framework/activity/ActivityObject_Init.cs'成功!");
        }
        catch (Exception e)
        {
            Debug.LogError(e.ToString());
        }
    }

    //生成初始化 ActivityObject 代码
    private static void GeneratorActivityObjectInit()
    {
        var text = File.ReadAllText($"resource/config/{nameof(ExcelConfig.ActivityBase)}.json");
        var array = JsonSerializer.Deserialize<System.Collections.Generic.Dictionary<string, object>[]>(text);
        
        var code1 = "";

        foreach (var item in array)
        {
            var id = item["Id"];
            var name = item["Name"] + "";
            var intro = item["Intro"] + "";
            code1 += $"        /// <summary>\n";
            code1 += $"        /// 名称: {name} <br/>\n";
            code1 += $"        /// 简介: {intro.Replace("\n", " <br/>\n        /// ")}\n";
            code1 += $"        /// </summary>\n";
            code1 += $"        public const string Id_{id} = \"{id}\";\n";
        }
        
        var str = $"using Config;\n\n";
        str += $"// 根据配置表注册物体, 该类是自动生成的, 请不要手动编辑!\n";
        str += $"public partial class ActivityObject\n";
        str += $"{{\n";
        
        str += $"    /// <summary>\n";
        str += $"    /// 存放所有在表中注册的物体的id\n";
        str += $"    /// </summary>\n";
        str += $"    public static class Ids\n";
        str += $"    {{\n";
        str += code1;
        str += $"    }}\n";
        
        str += $"}}\n";
        
        File.WriteAllText("src/framework/activity/ActivityObject_Init.cs", str);
    }
}

#endif