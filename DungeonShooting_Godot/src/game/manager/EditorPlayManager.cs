
using System.Collections.Generic;

public static class EditorPlayManager
{
    /// <summary>
    /// 是否正在播放
    /// </summary>
    public static bool IsPlay { get; set; }

    private static DungeonConfig _config;

    /// <summary>
    /// 地牢编辑器中的播放按钮
    /// </summary>
    public static void Play(UiBase prevUi)
    {
        if (IsPlay)
        {
            return;
        }

        IsPlay = true;
        _config = new DungeonConfig();
        _config.GroupName = EditorTileMapManager.SelectDungeonGroup.GroupName;
        _config.DesignatedType = EditorTileMapManager.SelectRoom.RoomInfo.RoomType;
        _config.DesignatedRoom = new List<DungeonRoomSplit>();
        _config.DesignatedRoom.Add(EditorTileMapManager.SelectRoom);
        UiManager.Open_Loading();
        GameApplication.Instance.DungeonManager.EditorPlayDungeon(prevUi, _config, () =>
        {
            UiManager.Destroy_Loading();
        });
}

    public static void Exit()
    {
        if (!IsPlay)
        {
            return;
        }

        IsPlay = false;
        UiManager.Open_Loading();
        GameApplication.Instance.DungeonManager.EditorExitDungeon(false, () =>
        {
            UiManager.Destroy_Loading();
        });
    }

    public static void Restart()
    {
        if (!IsPlay)
        {
            return;
        }
        UiManager.Open_Loading();
        GameApplication.Instance.DungeonManager.ExitDungeon(false, () =>
        {
            GameApplication.Instance.DungeonManager.EditorPlayDungeon(_config, () =>
            {
                UiManager.Destroy_Loading();
            });
        });
    }
}