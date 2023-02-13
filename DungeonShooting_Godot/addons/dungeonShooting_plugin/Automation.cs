#if TOOLS
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Godot;
using File = System.IO.File;

[Tool]
public partial class Automation : Control
{
	//支持后缀
	private string[] suffix =
	{
		".png", ".jpg", ".txt", ".json", ".ini", ".tscn", ".tres", ".otf", ".gdshader", ".tmx", ".tsx", ".ogg", ".mp3", ".wav", ".svg"
	};
	//排除第一层的文件夹
	private string[] exclude =
	{
		".vscode", ".idea", ".git", ".import", ".mono", "android", "addons", ".godot"
	};
	private string currDir = System.Environment.CurrentDirectory;

	private string resultStr = "";
	
	//更新 ResourcePath
	private void _on_Button_pressed()
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
		File.WriteAllText("src/game/manager/ResourcePath.cs", resultStr);
		GD.Print("ResourcePath.cs 写出完成!");
	}

	private void EachDir(DirectoryInfo directoryInfos)
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

	private void HandleFile(FileInfo fileInfo)
	{
		if (suffix.Contains(fileInfo.Extension))
		{
			var field = fileInfo.FullName.Substring(currDir.Length + 1);
			field = field.Replace("\\", "/");
			var resPath = "res://" + field;
			field = field.Replace(".", "_");
			field = field.Replace("/", "_");
			field = Regex.Replace(field, "[^\\w_]", "");
			resultStr += $"    public const string {field} = \"{resPath}\";\n";
		}
	}
}
#endif