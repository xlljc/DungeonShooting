
using System.Collections.Generic;

public static class EditorManager
{
    /// <summary>
    /// 当前使用的地牢组
    /// </summary>
    public static DungeonRoomGroup SelectDungeonGroup { get; private set; }

    /// <summary>
    /// 当使用的地牢房间
    /// </summary>
    public static DungeonRoomSplit SelectRoom { get; private set; }

    /// <summary>
    /// 当前使用的预设索引
    /// </summary>
    public static int SelectPreinstallIndex { get; private set; } = -1;

    /// <summary>
    /// 当前使用的预设
    /// </summary>
    public static RoomPreinstallInfo SelectPreinstall
    {
        get
        {
            if (SelectPreinstallIndex < 0 || SelectPreinstallIndex >= SelectRoom.Preinstall.Count)
            {
                return null;
            }

            return SelectRoom.Preinstall[SelectPreinstallIndex];
        }
    }

    /// <summary>
    /// 当前选中的波索引
    /// </summary>
    public static int SelectWaveIndex { get; private set; } = -1;

    /// <summary>
    /// 当前选中的波
    /// </summary>
    public static List<MarkInfo> SelectWave
    {
        get
        {
            var preinstall = SelectPreinstall;
            if (preinstall == null)
            {
                return null;
            }

            if (SelectWaveIndex < 0 || SelectWaveIndex > preinstall.WaveList.Count)
            {
                return null;
            }

            return preinstall.WaveList[SelectWaveIndex];
        }
    }
    
    /// <summary>
    /// 当前选中的标记
    /// </summary>
    public static MarkInfo SelectMark { get; private set; }

    public static void SetSelectDungeonGroup(DungeonRoomGroup roomGroup)
    {
        if (SelectDungeonGroup != roomGroup)
        {
            SelectDungeonGroup = roomGroup;
            EventManager.EmitEvent(EventEnum.OnSelectGroup, SelectDungeonGroup);
        }
    }

    public static void SetSelectRoom(DungeonRoomSplit roomSplit)
    {
        if (!ReferenceEquals(SelectRoom, roomSplit))
        {
            SelectRoom = roomSplit;
            EventManager.EmitEvent(EventEnum.OnSelectRoom, SelectRoom);
        }
    }

    /// <summary>
    /// 选中预设
    /// </summary>
    /// <param name="index">预设索引</param>
    public static void SetSelectPreinstallIndex(int index)
    {
        if (SelectRoom == null)
        {
            if (SelectPreinstallIndex != -1)
            {
                SelectPreinstallIndex = -1;
                EventManager.EmitEvent(EventEnum.OnSelectPreinstall, SelectPreinstall);
            }
        }
        else if (index < 0 || SelectRoom.Preinstall == null || index >= SelectRoom.Preinstall.Count)
        {
            if (SelectPreinstallIndex != -1)
            {
                SelectPreinstallIndex = -1;
                EventManager.EmitEvent(EventEnum.OnSelectPreinstall, SelectPreinstall);
            }
        }
        else if (SelectPreinstallIndex != index)
        {
            SelectPreinstallIndex = index;
            EventManager.EmitEvent(EventEnum.OnSelectPreinstall, SelectPreinstall);
        }
    }
    
    /// <summary>
    /// 选中波数
    /// </summary>
    /// <param name="index">波数索引</param>
    public static void SetSelectWaveIndex(int index)
    {
        if (SelectPreinstall == null)
        {
            if (SelectWaveIndex != -1)
            {
                SelectWaveIndex = -1;
                EventManager.EmitEvent(EventEnum.OnSelectWave, SelectWave);
            }
        }
        else if (SelectWaveIndex != index)
        {
            if (index < 0 || SelectPreinstall.WaveList == null || index >= SelectPreinstall.WaveList.Count)
            {
                if (SelectWaveIndex != -1)
                {
                    SelectWaveIndex = -1;
                    EventManager.EmitEvent(EventEnum.OnSelectWave, SelectWave);
                }
            }
            else
            {
                if (SelectWaveIndex != index)
                {
                    SelectWaveIndex = index;
                    EventManager.EmitEvent(EventEnum.OnSelectWave, SelectWave);
                }
            }
        }
    }

    /// <summary>
    /// 选中某个标记
    /// </summary>
    public static void SetSelectMark(MarkInfo markInfo)
    {
        if (SelectMark != markInfo)
        {
            SelectMark = markInfo;
            EventManager.EmitEvent(EventEnum.OnSelectMark, markInfo);
        }
    }
    
    /// <summary>
    /// 根据 RoomErrorType 枚举获取房间错误信息
    /// </summary>
    public static string GetRoomErrorTypeMessage(RoomErrorType errorType)
    {
        switch (errorType)
        {
            case RoomErrorType.None:
                return "";
            case RoomErrorType.Empty:
                return "房间没有绘制地块";
            case RoomErrorType.TileError:
                return "房间地块存在绘制错误";
            case RoomErrorType.DoorAreaError:
                return "房间至少要有两个不同方向的门区域";
            case RoomErrorType.NoPreinstallError:
                return "房间没有预设";
        }

        return null;
    }
}