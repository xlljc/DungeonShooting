
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

        if (_config.DesignatedType == DungeonRoomType.Inlet) //初始房间
        {
            var dungeonRoomGroup = GameApplication.Instance.RoomConfig[_config.GroupName];
            _config.DesignatedRoom.Add(EditorTileMapManager.SelectRoom);
            //随机选择战斗房间
            for (var i = 0; i < 5; i++)
            {
                _config.DesignatedRoom.Add(Utils.Random.RandomChoose(dungeonRoomGroup.BattleList));
            }
            //结束房间
            _config.DesignatedRoom.Add(Utils.Random.RandomChoose(dungeonRoomGroup.OutletList));
        }
        else if (_config.DesignatedType == DungeonRoomType.Outlet) //结束房间
        {
            var dungeonRoomGroup = GameApplication.Instance.RoomConfig[_config.GroupName];
            //随机选择初始房间
            _config.DesignatedRoom.Add(Utils.Random.RandomChoose(dungeonRoomGroup.InletList));
            _config.DesignatedRoom.Add(EditorTileMapManager.SelectRoom);
        }
        else //其他类型房间
        {
            var dungeonRoomGroup = GameApplication.Instance.RoomConfig[_config.GroupName];
            //随机选择初始房间
            _config.DesignatedRoom.Add(Utils.Random.RandomChoose(dungeonRoomGroup.InletList));
            for (var i = 0; i < 5; i++)
            {
                _config.DesignatedRoom.Add(EditorTileMapManager.SelectRoom);
            }
            //结束房间
            _config.DesignatedRoom.Add(Utils.Random.RandomChoose(dungeonRoomGroup.OutletList));
        }
        
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