
#if TOOLS

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Godot;

namespace Generator;

/// <summary>
/// 地牢房间数据生成器
/// </summary>
public static class DungeonRoomGenerator
{
	/// <summary>
	/// 根据名称在编辑器中创建地牢的预制房间, open 表示创建完成后是否在编辑器中打开这个房间
	/// </summary>
	public static bool CreateDungeonRoom(string groupName, string roomType, string roomName, bool open = false)
	{
		try
		{
			var path = GameConfig.RoomTileDir + groupName + "/" + roomType;
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
			//创建场景资源
			var prefabFile = path + "/" + roomName + ".tscn";
			var prefabResPath = "res://" + prefabFile;
			if (!Directory.Exists(GameConfig.RoomTileDir))
			{
				Directory.CreateDirectory(GameConfig.RoomTileDir);
			}
			
			//加载脚本资源
			
			// 这段代码在 Godot4.0.1rc1中会报错, 应该是个bug
			// var scriptRes = GD.Load<CSharpScript>("res://src/framework/map/DungeonRoomTemplate.cs");
			// var tileMap = new TileMap();
			// tileMap.Name = roomName;
			// tileMap.SetScript(scriptRes);
			// var scene = new PackedScene();
			// scene.Pack(tileMap); //报错在这一行, 报的是访问了被销毁的资源 scriptRes
			// ResourceSaver.Save(scene, prefabResPath);
			
			//临时处理
			{
				var tscnCode = $"[gd_scene load_steps=2 format=3]\n" +
				               $"\n" +
				               $"[ext_resource type=\"Script\" path=\"res://src/framework/map/DungeonRoomTemplate.cs\" id=\"dungeonRoomTemplate\"]\n" +
				               $"\n" +
				               $"[node name=\"{roomName}\" type=\"TileMap\"]\n" +
				               $"format = 2\n" +
				               $"script = ExtResource(\"dungeonRoomTemplate\")\n";
				File.WriteAllText(prefabFile, tscnCode);
				//重新保存一遍, 以让编辑器生成资源Id
				var scene = GD.Load<PackedScene>(prefabResPath);
				ResourceSaver.Save(scene, prefabResPath);
			}

			//打包房间配置
			GenerateRoomConfig();
			
			//生成 UiManager_Methods.cs 代码
			UiManagerMethodsGenerator.Generate();
			//打开房间
			if (open)
			{
				Plugin.Plugin.Instance.GetEditorInterface().OpenSceneFromPath(prefabResPath);
			}
		}
		catch (Exception e)
		{
			GD.PrintErr(e.ToString());
			return false;
		}
		
		return true;
	}
	
	/// <summary>
	/// 执行生成 RoomConfig.json 操作, 返回是否执行成功
	/// </summary>
    public static bool GenerateRoomConfig()
    {
	    try
	    {
		    //地图路径
		    var tileDir = GameConfig.RoomTileDir;
		    //地图描述数据路径
		    var tileDataDir = GameConfig.RoomTileDataDir;
		
		    var tileGroup = new DirectoryInfo(tileDir).GetDirectories();
		    var tileDataGroup = new DirectoryInfo(tileDataDir).GetDirectories();

		    //所有地图列表
		    var map = new Dictionary<string, FileInfo>();
		    
		    //地图场景
		    foreach (var groupDir in tileGroup)
		    {
			    var groupName = groupDir.Name;
			    var typeDirArray = groupDir.GetDirectories();
			    //遍历枚举, 获取指定路径文件
			    foreach (DungeonRoomType roomType in Enum.GetValues(typeof(DungeonRoomType)))
			    {
				    var typeName = DungeonRoomTemplate.DungeonRoomTypeToString(roomType);

				    //收集所有文件名称
				    var tempFileDataInfos = typeDirArray.FirstOrDefault(dirInfo => dirInfo.Name == typeName);
				    if (tempFileDataInfos != null)
				    {
					    foreach (var fileInfo in tempFileDataInfos.GetFiles())
					    {
						    if (fileInfo.Extension == ".tscn")
						    {
							    var pathInfo = new FileInfo(groupName, roomType, typeName, RemoveExtension(fileInfo.Name));
							    map.TryAdd(pathInfo.GetPath(), pathInfo);
						    }
					    }
				    }
			    }
		    }

		    //地图配置数据
		    foreach (var groupDir in tileDataGroup)
		    {
			    var groupName = groupDir.Name;
			    var typeDirArray = groupDir.GetDirectories();
			    //遍历枚举, 获取指定路径文件
			    foreach (DungeonRoomType roomType in Enum.GetValues(typeof(DungeonRoomType)))
			    {
				    var typeName = DungeonRoomTemplate.DungeonRoomTypeToString(roomType);

				    //收集所有文件名称
				    var tempFileDataInfos = typeDirArray.FirstOrDefault(dirInfo => dirInfo.Name == typeName);
				    if (tempFileDataInfos != null)
				    {
					    foreach (var fileInfo in tempFileDataInfos.GetFiles())
					    {
						    if (fileInfo.Extension == ".json")
						    {
							    var pathInfo = new FileInfo(groupName, roomType, typeName, RemoveExtension(fileInfo.Name));
							    map.TryAdd(pathInfo.GetPath(), pathInfo);
						    }
					    }
				    }
			    }
		    }

		    //剔除多余的 tile.json
		    foreach (var item in map)
		    {
			    var path = item.Key;
			    if (!File.Exists(tileDir + path + ".tscn"))
			    {
				    map.Remove(path);
				    var filePath = tileDataDir + path + ".json";
				    if (File.Exists(filePath))
				    {
					    GD.Print($"未找到'{tileDir + path}.tscn', 删除配置文件: {filePath}");
					    File.Delete(filePath);
				    }
			    }
		    }

		    //手动生成缺失的 tile.json
		    foreach (var item in map)
		    {
			    if (!File.Exists(tileDataDir + item.Key + ".json"))
			    {
				    var tscnName = tileDir + item.Key + ".tscn";
				    var packedScene = ResourceManager.Load<PackedScene>(tscnName, false);
				    if (packedScene != null)
				    {
					    var dungeonRoomTemplate = packedScene.Instantiate<DungeonRoomTemplate>();
					    var usedRect = dungeonRoomTemplate.GetUsedRect();
					    var dungeonTile = new DungeonTileMap(dungeonRoomTemplate);
					    dungeonTile.SetFloorAtlasCoords(new List<Vector2I>() { new Vector2I(0, 8) });
					    //计算导航网格
					    dungeonTile.GenerateNavigationPolygon(0);
					    var polygonData = dungeonTile.GetPolygonData();
					    
					    DungeonRoomTemplate.SaveConfig(new List<DoorAreaInfo>(), usedRect.Position, usedRect.Size, polygonData.ToList(),
						    item.Value.GroupName, item.Value.RoomType, item.Value.FileName, dungeonRoomTemplate.Weight);
					    dungeonRoomTemplate.QueueFree();
				    }
			    }
		    }

		    var roomGroupMap = new Dictionary<string, DungeonRoomGroup>();
		    //var list = new List<DungeonRoomSplit>();
		    //整合操作
		    foreach (var item in map)
		    {
			    var path = item.Key;
			    var configPath = tileDataDir + path + ".json";
			    var split = new DungeonRoomSplit();
			    split.ScenePath = ToResPath(tileDir + path + ".tscn");
			    split.RoomPath = ToResPath(configPath);

			    if (!roomGroupMap.TryGetValue(item.Value.GroupName, out var group))
			    {
				    group = new DungeonRoomGroup();
				    group.GroupName = item.Value.GroupName;
				    roomGroupMap.Add(group.GroupName, group);
			    }
			    
			    group.GetRoomList(item.Value.RoomType).Add(split);
		    }

		    //写出配置
		    var config = new JsonSerializerOptions();
		    config.WriteIndented = true;
		    var text = JsonSerializer.Serialize(roomGroupMap, config);
		    File.WriteAllText(GameConfig.RoomTileConfigFile, text);

		    GD.Print("地牢房间配置, 重新打包完成!");
	    }
	    catch (Exception e)
	    {
		    GD.PrintErr(e.ToString());
		    return false;
	    }

	    return true;
    }

	private class FileInfo
	{
		public FileInfo(string groupName, DungeonRoomType roomType, string typeName, string fileName)
		{
			GroupName = groupName;
			RoomType = roomType;
			TypeName = typeName;
			FileName = fileName;
		}

		public string GroupName;
		public DungeonRoomType RoomType;
		public string TypeName;
		public string FileName;

		public string GetPath()
		{
			return GroupName + "/" + TypeName + "/" + FileName;
		}
	}
	
	private static string ToResPath(string path)
    {
	    var field = path.Replace("\\", "/");
	    return "res://" + field;
    }

    private static string RemoveExtension(string name)
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