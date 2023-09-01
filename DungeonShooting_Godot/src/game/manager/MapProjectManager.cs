
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Godot;

public static class MapProjectManager
{
    /// <summary>
    /// 扫描路径
    /// </summary>
    public static string CustomMapPath { get; private set; }

    /// <summary>
    /// 地牢组数据, key: 组名称
    /// </summary>
    public static Dictionary<string, DungeonRoomGroup> GroupMap { get; private set; }

    private static bool _init;
    
    public static void Init()
    {
        if (_init)
        {
            return;
        }

        _init = true;
#if TOOLS
        CustomMapPath = GameConfig.RoomTileDir;
#else
        CustomMapPath = "";
#endif
        EventManager.AddEventListener(EventEnum.OnEditorSave, OnRoomSave);
    }

    //房间保存时回调
    private static void OnRoomSave(object obj)
    {
        SaveGroupMap();
    }

    /// <summary>
    /// 刷新组数据
    /// </summary>
    public static void RefreshMapGroup()
    {
        var configFile = CustomMapPath + GameConfig.RoomGroupConfigFile;
        if (File.Exists(configFile))
        {
            var configText = File.ReadAllText(configFile);
            GroupMap = JsonSerializer.Deserialize<Dictionary<string, DungeonRoomGroup>>(configText);
            foreach (var item in GroupMap)
            {
                var config = item.Value;
                foreach (var roomSplit in config.BattleList)
                {
                    _ = roomSplit.RoomInfo;
                }

                foreach (var roomSplit in config.BossList)
                {
                    _ = roomSplit.RoomInfo;
                }

                foreach (var roomSplit in config.InletList)
                {
                    _ = roomSplit.RoomInfo;
                }

                foreach (var roomSplit in config.OutletList)
                {
                    _ = roomSplit.RoomInfo;
                }

                foreach (var roomSplit in config.EventList)
                {
                    _ = roomSplit.RoomInfo;
                }

                foreach (var roomSplit in config.RewardList)
                {
                    _ = roomSplit.RoomInfo;
                }

                foreach (var roomSplit in config.ShopList)
                {
                    _ = roomSplit.RoomInfo;
                }
            }
        }
        else
        {
            GD.Print("刷新地图组时未找到配置文件: " + configFile + ", 执行创建文件");
            GroupMap = new Dictionary<string, DungeonRoomGroup>();
            File.WriteAllText(configFile, "{}");
        }
    }
    
    /// <summary>
    /// 获取地牢房间配置文件加载路径
    /// </summary>
    /// <param name="groupName">组名</param>
    /// <param name="roomType">房间类型</param>
    /// <param name="roomName">房间名称</param>
    public static string GetConfigPath(string groupName, DungeonRoomType roomType, string roomName)
    {
        return CustomMapPath + groupName + "/" + DungeonManager.DungeonRoomTypeToString(roomType) + "/" + roomName;
    }

    /// <summary>
    /// 获取房间地块配置文件名称
    /// </summary>
    public static string GetTileInfoConfigName(string roomName)
    {
        return roomName + "_tileInfo.json";
    }
    
    /// <summary>
    /// 获取房间基础配置文件名称
    /// </summary>
    public static string GetRoomInfoConfigName(string roomName)
    {
        return roomName + "_roomInfo.json";
    }

    /// <summary>
    /// 获取房间预设数据配置文件名称
    /// </summary>
    public static string GetRoomPreinstallConfigName(string roomName)
    {
        return roomName + "_preinstall.json";
    }

    /// <summary>
    /// 创建地牢组
    /// </summary>
    public static void CreateGroup(DungeonRoomGroup group)
    {
        if (GroupMap.ContainsKey(group.GroupName))
        {
            GD.PrintErr($"已经存在相同的地牢组: {group.GroupName}");
            return;
        }
        GroupMap.Add(group.GroupName, group);
        //将组数据保存为json
        SaveGroupMap();
        //创建完成事件
        EventManager.EmitEvent(EventEnum.OnCreateGroupFinish, group);
    }

    /// <summary>
    /// 创建地牢房间
    /// </summary>
    public static void CreateRoom(DungeonRoomSplit roomSplit)
    {
        var groupName = roomSplit.RoomInfo.GroupName;
        if (GroupMap.TryGetValue(groupName, out var group))
        {
            var configFile = CustomMapPath + GameConfig.RoomGroupConfigFile;
            var roomList = group.GetRoomList(roomSplit.RoomInfo.RoomType);
            roomList.Add(roomSplit);

            var configPath = GetConfigPath(roomSplit.RoomInfo.GroupName, roomSplit.RoomInfo.RoomType, roomSplit.RoomInfo.RoomName);
            if (!Directory.Exists(configPath))
            {
                Directory.CreateDirectory(configPath);
            }

            //将组数据保存为json
            var options = new JsonSerializerOptions();
            options.WriteIndented = true;
            var jsonText = JsonSerializer.Serialize(GroupMap, options);
            File.WriteAllText(configFile, jsonText);
            //将房间数据保存为json
            var jsonText2 = JsonSerializer.Serialize(roomSplit.RoomInfo);
            File.WriteAllText(roomSplit.RoomPath, jsonText2);
            //将房间地块保存为json
            var jsonText3 = JsonSerializer.Serialize(roomSplit.TileInfo);
            File.WriteAllText(roomSplit.TilePath, jsonText3);
            //将预设保存为json
            var jsonText4 = JsonSerializer.Serialize(roomSplit.Preinstall);
            File.WriteAllText(roomSplit.PreinstallPath, jsonText4);
            //创建完成事件
            EventManager.EmitEvent(EventEnum.OnCreateRoomFinish, roomSplit);
        }
        else
        {
            GD.PrintErr($"未找到地牢组: {groupName}");
        }
    }

    /// <summary>
    /// 保存所有组数据
    /// </summary>
    public static void SaveGroupMap()
    {
        var configFile = CustomMapPath + GameConfig.RoomGroupConfigFile;
        var options = new JsonSerializerOptions();
        options.WriteIndented = true;
        var jsonText = JsonSerializer.Serialize(GroupMap, options);
        File.WriteAllText(configFile, jsonText);
        
        //更新GameApplication中的房间数据
        var dic = new Dictionary<string, DungeonRoomGroup>();
        foreach (var dungeonRoomGroup in GroupMap)
        {
            dic.Add(dungeonRoomGroup.Key, dungeonRoomGroup.Value.Clone());
        }
        GameApplication.Instance.SetRoomConfig(dic);
    }

    /// <summary>
    /// 从指定组中删除房间, 返回是否删除成功
    /// </summary>
    public static bool DeleteRoom(DungeonRoomGroup group, DungeonRoomSplit roomSplit)
    {
        if (group.RemoveRoom(roomSplit))
        {
            var path = GetConfigPath(group.GroupName, roomSplit.RoomInfo.RoomType, roomSplit.RoomInfo.RoomName);
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }

            return true;
        }

        return false;
    }
}