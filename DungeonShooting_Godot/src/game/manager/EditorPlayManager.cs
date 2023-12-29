
using System.Collections.Generic;

public static class EditorPlayManager
{
    /// <summary>
    /// 是否正在播放
    /// </summary>
    public static bool IsPlay { get; set; }

    private static DungeonConfig _config;
    
    public static void Play(UiBase prevUi)
    {
        if (IsPlay)
        {
            return;
        }

        IsPlay = true;
        _config = new DungeonConfig();
        _config.GroupName = EditorManager.SelectDungeonGroup.GroupName;
        _config.DesignatedType = EditorManager.SelectRoom.RoomInfo.RoomType;
        _config.DesignatedRoom = new List<DungeonRoomSplit>();
        _config.DesignatedRoom.Add(EditorManager.SelectRoom);
        GameApplication.Instance.DungeonManager.EditorPlayDungeon(prevUi, _config);
    }

    public static void Exit()
    {
        if (!IsPlay)
        {
            return;
        }

        IsPlay = false;
        GameApplication.Instance.DungeonManager.EditorExitDungeon();
    }

    public static void Restart()
    {
        if (!IsPlay)
        {
            return;
        }
        
        GameApplication.Instance.DungeonManager.ExitDungeon(() =>
        {
            GameApplication.Instance.DungeonManager.EditorPlayDungeon(_config);
        });
    }
}