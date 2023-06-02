using System.Data;
using System.IO;
using Godot;
using NPOI.XSSF.UserModel;

namespace Generator;

public static class ExcelGenerator
{
    // private class ExcelTableData
    // {
    //     public string[] ColumnName;
    //     public string[] TypeName;
    //     public string[][] Data;
    // }
    
    public static bool ExportExcel()
    {
        GD.Print("准备导出excel表...");
        var directoryInfo = new DirectoryInfo(GameConfig.ExcelFilePath);
        if (directoryInfo.Exists)
        {
            var fileInfos = directoryInfo.GetFiles();
            foreach (var fileInfo in fileInfos)
            {
                if (fileInfo.Extension == ".xlsx")
                {
                    GD.Print("excel表: " + fileInfo.FullName);
                    ReadExcel(fileInfo.FullName);
                }
            }
        }

        return true;
    }

    private static void ReadExcel(string excelPath)
    {
        var fileName = Path.GetFileNameWithoutExtension(excelPath).FirstToUpper();
        var outStr = "namespace Config;\n\n";
        outStr += $"public class {fileName} \n{{\n";
        var sourceFile = excelPath;
 
        //加载表数据
        var workbook = new XSSFWorkbook(sourceFile);
        var sheet1 = workbook.GetSheet("Sheet1");

        //解析表
        var rowCount = sheet1.LastRowNum;
        
        //先解析表中的列名, 注释, 类型
        var columnCount = -1;
        var names = sheet1.GetRow(0);
        var descriptions = sheet1.GetRow(1);
        var types = sheet1.GetRow(2);
        foreach (var cell in names)
        {
            var value = cell.StringCellValue;
            if (string.IsNullOrEmpty(value))
            {
                columnCount = cell.ColumnIndex;
                break;
            }

            value = value.FirstToUpper();
            
            outStr += $"    /// <summary>\n";
            var descriptionCell = descriptions.GetCell(cell.ColumnIndex);
            var description = descriptionCell.StringCellValue.Replace("\n", " <br/>\n    /// ");
            var type = TypeMapping(types.GetCell(cell.ColumnIndex).StringCellValue);
            outStr += $"    /// {description}\n";
            outStr += $"    /// </summary>\n";
            outStr += $"    public {type} {value};\n\n";
        }

        outStr += "}";
        workbook.Close();

        if (!Directory.Exists("src/config"))
        {
            Directory.CreateDirectory("src/config");
        }
        File.WriteAllText("src/config/" + fileName + ".cs", outStr);
    }

    private static string TypeMapping(string typeName)
    {
        switch (typeName)
        {
            case "boolean": return "bool";
            case "boolean[]": return "bool[]";
            case "vector2": return "Godot.Vector2";
            case "vector2[]": return "Godot.Vector2[]";
            case "vector3": return "Godot.Vector3";
            case "vector3[]": return "Godot.Vector3[]";
            case "color[]": return "Godot.Color[]";
        }

        return typeName;
    }
}