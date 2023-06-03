using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;
using Godot;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Generator;

public static class ExcelGenerator
{
    
    private class MappingData
    {
        public string TypeStr;
        public string TypeName;

        public MappingData(string typeStr, string typeName)
        {
            TypeStr = typeStr;
            TypeName = typeName;
        }
    }

    private class ExcelData
    {
        public string TableName;
        public string OutCode;
        public List<string> ColumnNames = new List<string>();
        public Dictionary<string, MappingData> ColumnMappingData = new Dictionary<string, MappingData>();
        public Dictionary<string, Type> ColumnType = new Dictionary<string, Type>();
        public List<Dictionary<string, object>> DataList = new List<Dictionary<string, object>>();
    }
    
    public static bool ExportExcel()
    {
        GD.Print("准备导出excel表...");
        try
        {
            var excelDataList = new List<ExcelData>();
            
            var directoryInfo = new DirectoryInfo(GameConfig.ExcelFilePath);
            if (directoryInfo.Exists)
            {
                var fileInfos = directoryInfo.GetFiles();
                foreach (var fileInfo in fileInfos)
                {
                    if (fileInfo.Extension == ".xlsx")
                    {
                        GD.Print("excel表: " + fileInfo.FullName);
                        excelDataList.Add(ReadExcel(fileInfo.FullName));
                    }
                }
            }
            
            if (Directory.Exists("src/config"))
            {
                Directory.Delete("src/config", true);
            }
            if (Directory.Exists("resource/config"))
            {
                Directory.Delete("resource/config", true);
            }
            Directory.CreateDirectory("resource/config");
            Directory.CreateDirectory("src/config");
            
            //保存配置和代码
            foreach (var excelData in excelDataList)
            {
                File.WriteAllText("src/config/" + excelData.TableName + ".cs", excelData.OutCode);
                var config = new JsonSerializerOptions();
                config.WriteIndented = true;
                File.WriteAllText("resource/config/" + excelData.TableName + ".json", JsonSerializer.Serialize(excelData.DataList, config));
            }
        }
        catch (Exception e)
        {
            GD.PrintErr(e.ToString());
            return false;
        }

        return true;
    }

    private static ExcelData ReadExcel(string excelPath)
    {
        var excelData = new ExcelData();
        //文件名称
        var fileName = Path.GetFileNameWithoutExtension(excelPath).FirstToUpper();
        excelData.TableName = fileName;
        //输出代码
        var outStr = $"using System.Text.Json.Serialization;\n";
        outStr += $"using System.Collections.Generic;\n\n";
        outStr += $"namespace Config;\n\n";
        outStr += $"public class {fileName}\n{{\n";
        var sourceFile = excelPath;

        //行数
        var rowCount = -1;
        //列数
        var columnCount = -1;
        
        //加载表数据
        var workbook = new XSSFWorkbook(sourceFile);
        using (workbook)
        {
            var sheet1 = workbook.GetSheet("Sheet1");
            rowCount = sheet1.LastRowNum;
            //先解析表中的列名, 注释, 类型
            var names = sheet1.GetRow(0);
            var descriptions = sheet1.GetRow(1);
            var types = sheet1.GetRow(2);
            columnCount = names.LastCellNum;
            foreach (var cell in names)
            {
                //字段名称
                var field = GetCellStringValue(cell);
                if (string.IsNullOrEmpty(field))
                {
                    //到达最后一列了
                    columnCount = cell.ColumnIndex;
                    break;
                }
                field = field.FirstToUpper();
                excelData.ColumnNames.Add(field);

                outStr += $"    /// <summary>\n";
                var descriptionCell = descriptions.GetCell(cell.ColumnIndex);
                //描述
                string description;
                if (descriptionCell != null)
                {
                    description = GetCellStringValue(descriptionCell).Replace("\n", " <br/>\n    /// ");
                }
                else
                {
                    description = "";
                }
                //类型
                var typeString = GetCellStringValue(types.GetCell(cell.ColumnIndex));
                if (string.IsNullOrEmpty(typeString))
                {
                    throw new Exception($"表'{fileName}'中'{field}'这一列类型为空!");
                }
                
                //尝试解析类型
                MappingData mappingData;
                try
                {
                    mappingData = ConvertToType(typeString.Replace(" ", ""));
                }
                catch (Exception e)
                {
                    GD.PrintErr(e.ToString());
                    throw new Exception($"表'{fileName}'中'{field}'这一列类型描述语法错误: {typeString}");
                }
                
                if (!excelData.ColumnMappingData.TryAdd(field, mappingData))
                {
                    throw new Exception($"表'{fileName}'中存在相同名称的列: '{field}'!");
                }
                outStr += $"    /// {description}\n";
                outStr += $"    /// </summary>\n";
                outStr += $"    [JsonInclude]\n";
                outStr += $"    public {mappingData.TypeStr} {field} {{ get; private set; }}\n\n";
            }
        
            outStr += "}";
            
            //解析字段类型
            foreach (var kv in excelData.ColumnMappingData)
            {
                var typeName = kv.Value.TypeName;
                var type = Type.GetType(typeName);
                if (type == null)
                {
                    throw new Exception($"表'{fileName}'中'{kv.Key}'这一列类型未知! " + kv.Value.TypeStr);
                }
                excelData.ColumnType.Add(kv.Key, type);
            }

            //解析数据
            for (int i = 3; i <= rowCount; i++)
            {
                Dictionary<string, object> data = null;
                var row = sheet1.GetRow(i);
                for (int j = 0; j < columnCount; j++)
                {
                    var cell = row.GetCell(j);
                    var strValue = GetCellStringValue(cell);
                    //如果这一行的第一列数据为空, 则跳过这一行
                    if (j == 0 && string.IsNullOrEmpty(strValue))
                    {
                        break;
                    }
                    else if (data == null)
                    {
                        data = new Dictionary<string, object>();
                        excelData.DataList.Add(data);
                    }

                    var fieldName = excelData.ColumnNames[j];
                    var mappingData = excelData.ColumnMappingData[fieldName];
                    try
                    {
                        switch (mappingData.TypeStr)
                        {
                            case "bool":
                            case "boolean":
                                data.Add(fieldName, GetCellBooleanValue(cell));
                                break;
                            case "byte":
                                data.Add(fieldName, Convert.ToByte(GetCellNumberValue(cell)));
                                break;
                            case "sbyte":
                                data.Add(fieldName, Convert.ToSByte(GetCellNumberValue(cell)));
                                break;
                            case "short":
                                data.Add(fieldName, Convert.ToInt16(GetCellNumberValue(cell)));
                                break;
                            case "ushort":
                                data.Add(fieldName, Convert.ToUInt16(GetCellNumberValue(cell)));
                                break;
                            case "int":
                                data.Add(fieldName, Convert.ToInt32(GetCellNumberValue(cell)));
                                break;
                            case "uint":
                                data.Add(fieldName, Convert.ToUInt32(GetCellNumberValue(cell)));
                                break;
                            case "long":
                                data.Add(fieldName, Convert.ToInt64(GetCellNumberValue(cell)));
                                break;
                            case "ulong":
                                data.Add(fieldName, Convert.ToUInt64(GetCellNumberValue(cell)));
                                break;
                            case "float":
                                data.Add(fieldName, Convert.ToSingle(GetCellNumberValue(cell)));
                                break;
                            case "double":
                                data.Add(fieldName, GetCellNumberValue(cell));
                                break;
                            case "string":
                                data.Add(fieldName, GetCellStringValue(cell));
                                break;
                            default:
                            {
                                var cellStringValue = GetCellStringValue(cell);
                                if (cellStringValue.Length == 0)
                                {
                                    data.Add(fieldName, null);
                                }
                                else
                                {
                                    data.Add(fieldName, JsonSerializer.Deserialize(cellStringValue, excelData.ColumnType[fieldName]));
                                }
                            }
                                break;
                        }
                    }
                    catch (Exception e)
                    {
                        GD.PrintErr(e.ToString());
                        throw new Exception($"解析表'{fileName}'第'{i + 1}'行第'{j + 1}'列数据时发生异常");
                    }
                }
            }
        }

        excelData.OutCode = outStr;
        return excelData;
    }

    private static string GetCellStringValue(ICell cell)
    {
        if (cell == null)
        {
            return "";
        }
        switch (cell.CellType)
        {
            case CellType.Numeric:
                return cell.NumericCellValue.ToString();
            case CellType.String:
                return cell.StringCellValue;
            case CellType.Formula:
                return cell.CellFormula;
            case CellType.Boolean:
                return cell.BooleanCellValue ? "true" : "false";
        }

        return "";
    }

    private static double GetCellNumberValue(ICell cell)
    {
        if (cell == null)
        {
            return 0;
        }

        return cell.NumericCellValue;
    }

    private static bool GetCellBooleanValue(ICell cell)
    {
        if (cell == null)
        {
            return false;
        }

        return cell.BooleanCellValue;
    }
    
    private static MappingData ConvertToType(string str)
    {
        if (Regex.IsMatch(str, "^\\w+$"))
        {
            var typeStr = TypeStrMapping(str);
            var typeName = TypeNameMapping(str);
            return new MappingData(typeStr, typeName);
        }
        else if (str.StartsWith('{'))
        {
            var tempStr = str.Substring(1, str.Length - 2);
            var index = tempStr.IndexOf(':');
            if (index == -1)
            {
                throw new Exception("类型描述语法错误!");
            }

            var keyStr = tempStr.Substring(0, index);
            if (!IsBaseType(keyStr))
            {
                throw new Exception($"字典key类型必须是基础类型!");
            }
            var type1 = ConvertToType(keyStr);
            var type2 = ConvertToType(tempStr.Substring(index + 1));
            var typeStr = $"Dictionary<{type1.TypeStr}, {type2.TypeStr}>";
            var typeName = $"System.Collections.Generic.Dictionary`2[[{type1.TypeName}],[{type2.TypeName}]]";
            return new MappingData(typeStr, typeName);
        }
        else if (str.StartsWith('['))
        {
            var tempStr = str.Substring(1, str.Length - 2);
            var typeData = ConvertToType(tempStr);
            var typeStr = typeData.TypeStr + "[]";
            var typeName = typeData.TypeName + "[]";
            return new MappingData(typeStr, typeName);
        }
        throw new Exception("类型描述语法错误!");
    }
    
    private static string TypeStrMapping(string typeName)
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

    private static string TypeNameMapping(string typeName)
    {
        switch (typeName)
        {
            case "bool":
            case "boolean": return typeof(bool).FullName;
            case "byte": return typeof(byte).FullName;
            case "sbyte": return typeof(sbyte).FullName;
            case "short": return typeof(short).FullName;
            case "ushort": return typeof(ushort).FullName;
            case "int": return typeof(int).FullName;
            case "uint": return typeof(uint).FullName;
            case "long": return typeof(long).FullName;
            case "ulong": return typeof(ulong).FullName;
            case "string": return typeof(string).FullName;
            case "float": return typeof(float).FullName;
            case "double": return typeof(double).FullName;
            case "vector2": return typeof(SerializeVector2).FullName;
            case "vector3": return typeof(SerializeVector3).FullName;
            case "color": return typeof(SerializeColor).FullName;
        }

        return typeName;
    }

    private static bool IsBaseType(string typeName)
    {
        switch (typeName)
        {
            case "bool":
            case "boolean":
            case "byte":
            case "sbyte":
            case "short":
            case "ushort":
            case "int":
            case "uint":
            case "long":
            case "ulong":
            case "string":
            case "float":
            case "double":
                return true;
        }

        return false;
    }
}