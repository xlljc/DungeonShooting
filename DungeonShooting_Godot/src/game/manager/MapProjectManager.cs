
using System;
using System.Collections.Generic;
using System.IO;
using Godot;

public static class MapProjectManager
{
    public class MapGroupInfo
    {
        /// <summary>
        /// 组名称
        /// </summary>
        public string Name;
        /// <summary>
        /// 组路径
        /// </summary>
        public string FullPath;
        /// <summary>
        /// 当前组所在的文件夹
        /// </summary>
        public string RootPath;
    }

    public class MapRoomInfo
    {
        /// <summary>
        /// 房间名称
        /// </summary>
        public string Name;
        /// <summary>
        /// 组名称
        /// </summary>
        public string Group;
        /// <summary>
        /// 房间类型
        /// </summary>
        public DungeonRoomType RoomType;
        /// <summary>
        /// 文件夹路径
        /// </summary>
        public string FullPath;
        /// <summary>
        /// 预览图片
        /// </summary>
        public string PrevImage;
        /// <summary>
        /// 当前组所在的文件夹
        /// </summary>
        public string RootPath;
    }
    
    /// <summary>
    /// 扫描路径
    /// </summary>
    public static readonly List<string> ScannerPaths = new List<string>();
    /// <summary>
    /// 组列表数据
    /// </summary>
    public static readonly Dictionary<string, MapGroupInfo> GroupData = new Dictionary<string, MapGroupInfo>();

    private static bool _init;
    
    public static void Init()
    {
        if (_init)
        {
            return;
        }

        _init = true;
#if TOOLS
        ScannerPaths.Add(GameConfig.RoomTileDir);
#endif
    }

    /// <summary>
    /// 刷新组数据
    /// </summary>
    public static void RefreshMapGroup()
    {
        GroupData.Clear();
        foreach (var path in ScannerPaths)
        {
            if (Directory.Exists(path))
            {
                var info = new DirectoryInfo(path);
                var directoryInfos = info.GetDirectories();
                foreach (var directoryInfo in directoryInfos)
                {
                    var projectInfo = new MapGroupInfo();
                    projectInfo.Name = directoryInfo.Name;
                    projectInfo.FullPath = directoryInfo.FullName;
                    projectInfo.RootPath = info.FullName;
                    GroupData.TryAdd(projectInfo.FullPath, projectInfo);
                }
            }
            else
            {
                GD.PrintErr("刷新地图组时发现不存在的路径: " + path);
            }
        }
    }

    /// <summary>
    /// 根据路径加载房间
    /// </summary>
    public static MapRoomInfo[] LoadRoom(string rootPath, string groupName)
    {
        var path = rootPath + "\\" + groupName;
        if (!Directory.Exists(path))
        {
            GD.PrintErr("加载地牢房间时发现不存在的路径: " + path);
            return new MapRoomInfo[0];
        }

        var list = new List<MapRoomInfo>();
        var dir = new DirectoryInfo(path);
        var roomTypes = Enum.GetValues<DungeonRoomType>();
        foreach (var dungeonRoomType in roomTypes)
        {
            LoadRoomByType(list, dir, rootPath, groupName, dungeonRoomType);
        }

        return list.ToArray();
    }

    private static void LoadRoomByType(List<MapRoomInfo> list, DirectoryInfo dir, string rootPath, string groupName, DungeonRoomType roomType)
    {
        var typeName = DungeonManager.DungeonRoomTypeToString(roomType);
        var path = dir.FullName + "\\" + typeName;
        if (Directory.Exists(path))
        {
            var tempDir = new DirectoryInfo(path);
            var directoryInfos = tempDir.GetDirectories();
            foreach (var directoryInfo in directoryInfos)
            {
                if (directoryInfo.GetFiles().Length > 0)
                {
                    var room = new MapRoomInfo();
                    room.Name = directoryInfo.Name;
                    room.FullPath = directoryInfo.FullName;
                    room.RoomType = roomType;
                    room.Group = groupName;
                    room.RootPath = rootPath;
                    list.Add(room);
                }
            }
        }
    }
}