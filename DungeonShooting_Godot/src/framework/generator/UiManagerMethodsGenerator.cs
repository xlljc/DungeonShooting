using System;
using System.IO;
using System.Text.RegularExpressions;
using Godot;

namespace Generator;

/// <summary>
/// 生成 UiManager 中打开Ui相关的函数代码
/// </summary>
public static class UiManagerMethodsGenerator
{
    private static string savePath = "src/game/manager/UiManager_Methods.cs";
    
    /// <summary>
    /// 执行生成操作, 并返回执行结果
    /// </summary>
    /// <returns></returns>
    public static bool Generate()
    {
        //扫描所有ui
        if (!Directory.Exists(GameConfig.UiPrefabDir))
        {
            return true;
        }

        try
        {
            var directoryInfo = new DirectoryInfo(GameConfig.UiPrefabDir);
            var fileInfos = directoryInfo.GetFiles();

            var code = $"/**\n" +
                       $" * 该类为自动生成的, 请不要手动编辑, 以免造成代码丢失\n" +
                       $" */\n" +
                       $"public static partial class UiManager\n" +
                       $"{{\n" +
                       $"\n";

            foreach (var fileInfo in fileInfos)
            {
                if (fileInfo.Extension == ".tscn")
                {
                    var name = fileInfo.Name.Substring(0, fileInfo.Name.Length - 5);
                    var field = fileInfo.FullName.Substring(System.Environment.CurrentDirectory.Length + 1);
                    field = field.Replace("\\", "/");
                    field = field.Replace(".", "_");
                    field = field.Replace("/", "_");
                    field = Regex.Replace(field, "[^\\w]", "");
                    code += $"    public static UI.{name}.{name}Panel Open_{name}()\n" +
                            $"    {{\n" +
                            $"        return OpenUi<UI.{name}.{name}Panel>(ResourcePath.{field});\n" +
                            $"    }}\n" +
                            $"\n" +
                            $"    public static UiBase[] Get_{name}_Instance()\n" +
                            $"    {{\n" +
                            $"        return GetUiInstance(nameof(UI.{name}.{name}));\n" +
                            $"    }}\n" +
                            $"\n";
                }
            }

            code += $"}}\n";
            
            File.WriteAllText(savePath, code);
        }
        catch (Exception e)
        {
            GD.PrintErr(e.ToString());
            return false;
        }

        return true;
    }
}