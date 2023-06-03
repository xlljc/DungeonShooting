using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Godot;
using NPOI.XSSF.UserModel;

namespace Generator;

public static class ExcelGenerator
{
    public static bool ExportExcel()
    {
        GD.Print("准备导出excel表...");

        try
        {
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
        }
        catch (Exception e)
        {
            GD.PrintErr(e.ToString());
            return false;
        }

        return true;
    }

    private static void ReadExcel(string excelPath)
    {
        var columnList = new HashSet<string>();
        
        var fileName = Path.GetFileNameWithoutExtension(excelPath).FirstToUpper();
        var outStr = $"using System.Text.Json.Serialization;\n";
        outStr += $"using System.Collections.Generic;\n\n";
        outStr += $"namespace Config;\n\n";
        outStr += $"public class {fileName}\n{{\n";
        var sourceFile = excelPath;

        //加载表数据
        var workbook = new XSSFWorkbook(sourceFile);
        using (workbook)
        {
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
                if (!columnList.Add(value))
                {
                    throw new Exception($"表'{fileName}'中存在相同名称的列: '{value}'!");
                }

                outStr += $"    /// <summary>\n";
                var descriptionCell = descriptions.GetCell(cell.ColumnIndex);
                var description = descriptionCell.StringCellValue.Replace("\n", " <br/>\n    /// ");
                var stringCellValue = types.GetCell(cell.ColumnIndex).StringCellValue;
                string type;
                try
                {
                    type = ConvertToType(stringCellValue.Replace(" ", ""));
                }
                catch (Exception e)
                {
                    throw new Exception($"表'{fileName}'中'{value}'这一列类型描述语法错误: {stringCellValue}");
                }
                outStr += $"    /// {description}\n";
                outStr += $"    /// </summary>\n";
                outStr += $"    [JsonInclude]\n";
                outStr += $"    public {type} {value} {{ get; private set; }}\n\n";
            }
        
            outStr += "}";
        }

        if (!Directory.Exists("src/config"))
        {
            Directory.CreateDirectory("src/config");
        }

        File.WriteAllText("src/config/" + fileName + ".cs", outStr);
    }

    private static string ConvertToType(string str)
    {
        if (Regex.IsMatch(str, "^\\w+$"))
        {
            return TypeMapping(str);
        }
        else if (str.StartsWith('{'))
        {
            var tempStr = str.Substring(1, str.Length - 2);
            var index = tempStr.IndexOf(':');
            if (index == -1)
            {
                throw new Exception("类型描述语法错误!");
            }
            return "Dictionary<" + ConvertToType(tempStr.Substring(0, index)) + ", " + ConvertToType(tempStr.Substring(index + 1)) + ">";
        }
        else if (str.StartsWith('['))
        {
            var tempStr = str.Substring(1, str.Length - 2);
            return ConvertToType(tempStr) + "[]";
        }
        throw new Exception("类型描述语法错误!");
    }
    
    private static string TypeMapping(string typeName)
    {
        switch (typeName)
        {
            case "boolean": return "bool";
            case "vector2": return "SerializeVector2";
            case "vector3": return "SerializeVector3";
            case "color": return "SerializeColor";
        }

        return typeName;
    }
}