using Godot;
using Godot.Collections;

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
    }
}