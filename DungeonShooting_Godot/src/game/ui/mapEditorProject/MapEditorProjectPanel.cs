using System;
using System.Linq;
using Godot;

namespace UI.MapEditorProject;

public partial class MapEditorProjectPanel : MapEditorProject
{
    /// <summary>
    /// 当前选中的组
    /// </summary>
    public MapProjectManager.MapGroupInfo SelectGroupInfo;
    /// <summary>
    /// 选中的组所包含的房间数据
    /// </summary>
    public MapProjectManager.MapRoomInfo[] SelectRoomInfo;
    
    private UiGrid<GroupButton, MapProjectManager.MapGroupInfo> _groupGrid;
    private UiGrid<RoomButton, MapProjectManager.MapRoomInfo> _roomGrid;

    public override void OnCreateUi()
    {
        //初始化枚举选项
        var roomTypes = Enum.GetValues<DungeonRoomType>();
        var optionButton = S_RoomTypeButton.Instance;
        optionButton.AddItem("全部", 1);
        for (var i = 0; i < roomTypes.Length; i++)
        {
            var dungeonRoomType = roomTypes[i];
            optionButton.AddItem(DungeonManager.DungeonRoomTypeToDescribeString(dungeonRoomType), i + 1);
        }

        _groupGrid = new UiGrid<GroupButton, MapProjectManager.MapGroupInfo>(S_GroupButton, typeof(GroupButtonCell));
        _groupGrid.SetCellOffset(new Vector2I(0, 2));
        _groupGrid.SetHorizontalExpand(true);

        _roomGrid = new UiGrid<RoomButton, MapProjectManager.MapRoomInfo>(S_RoomButton, typeof(RoomButtonCell));
        _roomGrid.SetAutoColumns(true);
        _roomGrid.SetCellOffset(new Vector2I(10, 10));
        _roomGrid.SetHorizontalExpand(true);
    }

    public override void OnShowUi()
    {
        RefreshGroup();
    }

    public override void OnDisposeUi()
    {
        _groupGrid.Destroy();
        _groupGrid = null;
        
        _roomGrid.Destroy();
        _roomGrid = null;
    }

    public void RefreshGroup()
    {
        MapProjectManager.RefreshMapGroup();
        _groupGrid.SetDataList(MapProjectManager.GroupData.Values.ToArray());
    }

    public void SelectGroup(MapProjectManager.MapGroupInfo group)
    {
        SelectGroupInfo = group;
        SelectRoomInfo = MapProjectManager.LoadRoom(group.RootPath, group.Name);
        _roomGrid.SetDataList(SelectRoomInfo);
    }

    public void SelectRoom(MapProjectManager.MapRoomInfo room)
    {
        HideUi();
        //打开地牢Ui
        var mapEditor = UiManager.Open_MapEditor();
        mapEditor.PrevUi = this;
        //加载地牢
        mapEditor.LoadMap(room.RootPath, room.Group, room.RoomType, room.Name);
    }
}
