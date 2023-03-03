#if TOOLS
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using Godot;
using File = System.IO.File;

[Tool]
public partial class Automation : Control
{
	//支持后缀
	private string[] suffix =
	{
		".png", ".jpg", ".txt", ".json", ".ini", ".tscn", ".tres", ".otf", ".gdshader", ".ogg", ".mp3", ".wav", ".svg"
	};
	//排除第一层的文件夹
	private string[] exclude =
	{
		".vscode", ".idea", ".git", ".import", ".mono", "android", "addons", ".godot"
	};
	private string currDir = System.Environment.CurrentDirectory;

	private string resultStr = "";
	
	/// <summary>
	/// 更新 ResourcePath
	/// </summary>
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

	/// <summary>
	/// 重新打包房间配置
	/// </summary>
	private void _on_Button2_pressed()
	{
		//地图路径
		var tileDir = DungeonRoomTemplate.RoomTileDir;
		//地图描述数据路径
		var tileDataDir = DungeonRoomTemplate.RoomTileDataDir;
		
		var tileDirInfo = new DirectoryInfo(tileDir);
		var tileDataDirInfo = new DirectoryInfo(tileDataDir);

		//所有地图列表
		var mapList = new HashSet<string>();
		
		//收集所有名称
		var fileDataInfos = tileDataDirInfo.GetFiles();
		foreach (var fileInfo in fileDataInfos)
		{
			mapList.Add(RemoveExtension(fileInfo.Name));
		}
		//收集所有名称
		var fileInfos = tileDirInfo.GetFiles();
		foreach (var fileInfo in fileInfos)
		{
			if (fileInfo.Extension == ".tscn")
			{
				mapList.Add(RemoveExtension(fileInfo.Name));
			}
		}
		
		//剔除多余的 tile.json
		var arrays = mapList.ToArray();
		foreach (var item in arrays)
		{
			if (!File.Exists(tileDir + item + ".tscn"))
			{
				mapList.Remove(item);
				var filePath = tileDataDir + item + ".json";
				if (File.Exists(filePath))
				{
					GD.Print($"未找到'{tileDir + item}.tscn', 删除配置文件: {filePath}");
					File.Delete(filePath);
				}
			}
		}

		//手动生成缺失的 tile.json
		foreach (var item in mapList)
		{
			if (!File.Exists(tileDataDir + item + ".json"))
			{
				var tscnName = tileDir + item + ".tscn";
				var packedScene = ResourceManager.Load<PackedScene>(tscnName, false);
				if (packedScene != null)
				{
					var dungeonRoomTemplate = packedScene.Instantiate<DungeonRoomTemplate>();
					var usedRect = dungeonRoomTemplate.GetUsedRect();
					var dungeonTile = new DungeonTile(dungeonRoomTemplate);
					dungeonTile.SetFloorAtlasCoords(new List<Vector2I>() { new Vector2I(0, 8) });
					//计算导航网格
					dungeonTile.GenerateNavigationPolygon(0);
					var polygonData = dungeonTile.GetPolygonData();
					DungeonRoomTemplate.SaveConfig(new List<DoorAreaInfo>(), usedRect.Position, usedRect.Size, polygonData.ToList(), item);
					dungeonRoomTemplate.QueueFree();
				}
			}
		}

		var list = new List<DungeonRoomSplit>();
		//整合操作
		foreach (var item in mapList)
		{
			var configPath = tileDataDir + item + ".json";
			var configText = File.ReadAllText(configPath);
			var roomInfo = DungeonRoomTemplate.DeserializeDungeonRoomInfo(configText);
			var split = new DungeonRoomSplit();
			split.ScenePath = ToResPath(tileDir + item + ".tscn");
			split.ConfigPath = ToResPath(configPath);
			split.RoomInfo = roomInfo;
			list.Add(split);
		}

		//写出配置
		var config = new JsonSerializerOptions();
		config.WriteIndented = true;
		var text = JsonSerializer.Serialize(list, config);
		File.WriteAllText(DungeonRoomTemplate.RoomTileConfigFile, text);

		GD.Print("地牢房间配置, 重新打包完成!");
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

	private string ToResPath(string path)
	{
		var field = path.Substring(currDir.Length + 1);
		field = field.Replace("\\", "/");
		return "res://" + field;
	}

	private string RemoveExtension(string name)
	{
		var index = name.LastIndexOf(".", StringComparison.Ordinal);
		if (index >= 0)
		{
			return name.Substring(0, index);
		}

		return name;
	}
}
#endif