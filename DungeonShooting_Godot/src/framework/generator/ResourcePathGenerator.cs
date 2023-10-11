#if TOOLS

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
        ".png",
        ".jpg",
        ".txt",
        ".json",
        ".ini",
        ".tscn",
        ".tres",
        ".otf",
        ".gdshader",
        ".ogg",
        ".mp3",
        ".wav",
        ".svg",
        ".ttf",
        ".otf"
    };
    //排除的文件夹, 斜杠用: /
    private static string[] exclude =
    {
        ".vscode",
        ".idea",
        ".git",
        ".import",
        ".mono",
        "android",
        "addons",
        ".godot",
        ".vs",
        "resource/map/tiledata",
        "resource/map/tileMaps"
    };

    private static string resultStr = "";

    //保存路径
    private static string savePath = "src/game/manager/ResourcePath.cs";

    /// <summary>
    /// 执行生成操作, 返回是否执行成功
    /// </summary>
    public static bool Generate()
    {
        try
        {
            resultStr = "/// <summary>\n" +
                        "/// 编辑器下所有资源路径, 该类为 Tools 面板下自动生成的, 请不要手动编辑!\n" +
                        "/// </summary>\n" +
                        "public class ResourcePath\n" +
                        "{\n";

            Debug.Log("更新 ResourcePath...");

            var directoryInfo = new DirectoryInfo(System.Environment.CurrentDirectory);
            EachDir(directoryInfo);

            resultStr += "}";
            File.WriteAllText(savePath, resultStr);
            Debug.Log("ResourcePath.cs 写出完成!");
        }
        catch (Exception e)
        {
            Debug.LogError(e.ToString());
            return false;
        }

        return true;
    }
    
    private static void EachDir(DirectoryInfo directoryInfos)
    {
        if (directoryInfos.FullName.Length > System.Environment.CurrentDirectory.Length)
        {
            var path = directoryInfos.FullName.Substring(System.Environment.CurrentDirectory.Length + 1);
            path = path.Replace('\\', '/');
            if (exclude.Contains(path))
            {
                Debug.Log("扫描排除路径: " + path);
                return;
            }
        }

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
            var field = fileInfo.FullName.Substring(System.Environment.CurrentDirectory.Length + 1);
            field = field.Replace("\\", "/");
            var resPath = "res://" + field;
            field = field.Replace(".", "_");
            field = field.Replace("/", "_");
            field = Regex.Replace(field, "[^\\w]", "");
            resultStr += $"    public const string {field} = \"{resPath}\";\n";
        }
    }
    
}

#endif