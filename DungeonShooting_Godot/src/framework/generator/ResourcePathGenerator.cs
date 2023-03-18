
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Godot;

namespace Generator;

/// <summary>
/// ResourcePath 类文件生成器
/// </summary>
public static class ResourcePathGenerator
{
    //支持后缀
    private static string[] suffix =
    {
        ".png", ".jpg", ".txt", ".json", ".ini", ".tscn", ".tres", ".otf", ".gdshader", ".ogg", ".mp3", ".wav", ".svg"
    };
    //排除第一层的文件夹
    private static string[] exclude =
    {
        ".vscode", ".idea", ".git", ".import", ".mono", "android", "addons", ".godot", ".vs"
    };
    private static string currDir = System.Environment.CurrentDirectory;

    private static string resultStr = "";

    //保存路径
    public static string savePath = "src/game/manager/ResourcePath.cs";

    /// <summary>
    /// 执行生成操作, 返回是否执行成功
    /// </summary>
    public static bool Generate()
    {
        try
        {
            resultStr = "/// <summary>\n" +
                        "/// 编辑器下所有资源路径, 该类为 Automation 面板下自动生成的, 请不要手动编辑!\n" +
                        "/// </summary>\n" +
                        "public class ResourcePath\n" +
                        "{\n";

            GD.Print("更新 ResourcePath...");

            var directoryInfo = new DirectoryInfo(currDir);

            var directories = directoryInfo.GetDirectories();
            for (int i = 0; i < directories.Length; i++)
            {
                var directory = directories[i];
                if (!exclude.Contains(directory.Name))
                {
                    EachDir(directory);
                }
            }

            var fileInfos = directoryInfo.GetFiles();
            for (var i = 0; i < fileInfos.Length; i++)
            {
                HandleFile(fileInfos[i]);
            }

            resultStr += "}";
            File.WriteAllText(savePath, resultStr);
            GD.Print("ResourcePath.cs 写出完成!");
        }
        catch (Exception e)
        {
            GD.PrintErr(e.ToString());
            return false;
        }

        return true;
    }
    
    private static void EachDir(DirectoryInfo directoryInfos)
    {
        var fileInfos = directoryInfos.GetFiles();
        for (var i = 0; i < fileInfos.Length; i++)
        {
            HandleFile(fileInfos[i]);
        }

        var directories = directoryInfos.GetDirectories();
        for (var i = 0; i < directories.Length; i++)
        {
            EachDir(directories[i]);
        }
    }

    private static void HandleFile(FileInfo fileInfo)
    {
        if (suffix.Contains(fileInfo.Extension))
        {
            var field = fileInfo.FullName.Substring(currDir.Length + 1);
            field = field.Replace("\\", "/");
            var resPath = "res://" + field;
            field = field.Replace(".", "_");
            field = field.Replace("/", "_");
            field = Regex.Replace(field, "[^\\w]", "");
            resultStr += $"    public const string {field} = \"{resPath}\";\n";
        }
    }
    
}