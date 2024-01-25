using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using UI.MapEditor;

namespace UI.MapEditorProject;

public partial class MapEditorProjectPanel : MapEditorProject
{
    /// <summary>
    /// 当前显示的组数据
    /// </summary>
    public UiGrid<GroupButton, DungeonRoomGroup> GroupGrid { get; private set; }

    /// <summary>
    /// 当前显示的房间数据
    /// </summary>
    public UiGrid<RoomButton, DungeonRoomSplit> RoomGrid { get; private set; }

    private EventFactory _eventFactory;

    public override void OnCreateUi()
    {
        //初始化枚举选项
        var roomTypes = Enum.GetValues<DungeonRoomType>();
        var optionButton = S_RoomTypeButton.Instance;
        optionButton.AddItem("全部", 0);
        for (var i = 0; i < roomTypes.Length; i++)
        {
            var dungeonRoomType = roomTypes[i];
            if (dungeonRoomType == DungeonRoomType.None) continue;
            optionButton.AddItem(DungeonManager.DungeonRoomTypeToDescribeString(dungeonRoomType),
                (int)dungeonRoomType + 1);
        }

        GroupGrid = new UiGrid<MapEditorProject.GroupButton, DungeonRoomGroup>(S_GroupButton, typeof(GroupButtonCell));
        GroupGrid.SetCellOffset(new Vector2I(0, 2));
        GroupGrid.SetHorizontalExpand(true);

        RoomGrid = new UiGrid<MapEditorProject.RoomButton, DungeonRoomSplit>(S_RoomButton, typeof(RoomButtonCell));
        RoomGrid.SetAutoColumns(true);
        RoomGrid.SetCellOffset(new Vector2I(10, 10));
        RoomGrid.SetHorizontalExpand(true);

        S_GroupSearchButton.Instance.Pressed += OnSearchGroupButtonClick;
        S_GroupEditButton.Instance.Pressed += OnEditGroup;
        S_GroupAddButton.Instance.Pressed += OnCreateGroupClick;
        S_GroupDeleteButton.Instance.Pressed += OnDeleteGroup;

        S_RoomSearchButton.Instance.Pressed += OnSearchRoomButtonClick;
        S_RoomAddButton.Instance.Pressed += OnCreateRoomClick;
        S_RoomEditButton.Instance.Pressed += OnEditRoom;
        S_RoomDeleteButton.Instance.Pressed += OnDeleteRoom;

        _eventFactory = EventManager.CreateEventFactory();
        _eventFactory.AddEventListener(EventEnum.OnCreateGroupFinish, OnCreateGroupFinish);
        _eventFactory.AddEventListener(EventEnum.OnDeleteGroupFinish, OnDeleteGroupFinish);
        _eventFactory.AddEventListener(EventEnum.OnCreateRoomFinish, OnCreateRoomFinish);
    }

    public override void OnShowUi()
    {
        RefreshGroup();
        OnSearchRoomButtonClick();
    }

    public override void OnDestroyUi()
    {
        _eventFactory.RemoveAllEventListener();
        _eventFactory = null;
        GroupGrid.Destroy();
        GroupGrid = null;

        RoomGrid.Destroy();
        RoomGrid = null;
    }

    /// <summary>
    /// 刷新组数据
    /// </summary>
    public void RefreshGroup()
    {
        MapProjectManager.RefreshMapGroup();
        OnSearchGroupButtonClick();
    }

    /// <summary>
    /// 选中地牢组
    /// </summary>
    public void SelectGroup(DungeonRoomGroup group)
    {
        EditorTileMapManager.SetSelectDungeonGroup(group);
        OnSearchRoomButtonClick();
    }

    /// <summary>
    /// 选择地图并打开地图编辑器
    /// </summary>
    public void OpenSelectRoom(DungeonRoomSplit room, TileSetSplit tileSetSplit)
    {
        //创建地牢Ui
        var mapEditor = ParentUi.OpenNextUi<MapEditorPanel>(UiManager.UiNames.MapEditor);
        //加载地牢
        mapEditor.LoadMap(room, tileSetSplit);
    }

    //搜索组按钮点击
    private void OnSearchGroupButtonClick()
    {
        var select = GroupGrid.SelectIndex;
        if (select < 0)
        {
            select = 0;
        }

        //输入文本
        var text = S_GroupSearchInput.Instance.Text;
        if (!string.IsNullOrEmpty(text))
        {
            var str = text.Trim().ToLower();
            var list = new List<DungeonRoomGroup>();
            foreach (var valuePair in MapProjectManager.GroupMap)
            {
                if (valuePair.Value.GroupName.Trim().ToLower().Contains(str))
                {
                    list.Add(valuePair.Value);
                }
            }

            GroupGrid.SetDataList(list.ToArray());
        }
        else
        {
            GroupGrid.SetDataList(MapProjectManager.GroupMap.Values.ToArray());
        }

        GroupGrid.SelectIndex = select;
    }

    //搜索房间按钮点击
    private void OnSearchRoomButtonClick()
    {
        if (EditorTileMapManager.SelectDungeonGroup != null)
        {
            //输入文本
            var text = S_RoomSearchInput.Instance.Text;
            //房间类型
            var roomType = S_RoomTypeButton.Instance.GetSelectedId();

            IEnumerable<DungeonRoomSplit> result = EditorTileMapManager.SelectDungeonGroup.GetAllRoomList();

            //名称搜索
            if (!string.IsNullOrEmpty(text))
            {
                var queryText = text.Trim().ToLower();
                result = result.Where(split =>
                {
                    return split.RoomInfo.RoomName.Trim().ToLower().Contains(queryText);
                });
            }

            //类型搜索
            if (roomType > 0)
            {
                var type = (DungeonRoomType)(roomType - 1);
                result = result.Where(split => split.RoomInfo.RoomType == type);
            }

            RoomGrid.SetDataList(result.ToArray());
        }
        else
        {
            RoomGrid.RemoveAll();
        }
    }

    //创建组按钮点击
    private void OnCreateGroupClick()
    {
        EditorWindowManager.ShowCreateGroup(CreateGroup);
    }

    //编辑组按钮点击
    private void OnEditGroup()
    {
        if (GroupGrid.SelectIndex != -1)
        {
            EditorWindowManager.ShowEditGroup(GroupGrid.SelectData, EditGroup);
        }
        else
        {
            EditorWindowManager.ShowTips("提示", "请选择需要编辑的组！");
        }
    }

    //删除组按钮点击
    private void OnDeleteGroup()
    {
        if (GroupGrid.SelectIndex != -1)
        {
            EditorWindowManager.ShowDelayConfirm("提示", "确定删除该组吗？\n该操作不可取消！", 5, DeleteGroup);
        }
        else
        {
            EditorWindowManager.ShowTips("提示", "请选择需要删除的组！");
        }
    }

    //创建地牢房间按钮点击
    private void OnCreateRoomClick()
    {
        var groupName = EditorTileMapManager.SelectDungeonGroup != null
            ? EditorTileMapManager.SelectDungeonGroup.GroupName
            : null;
        EditorWindowManager.ShowCreateRoom(groupName, Mathf.Max(S_RoomTypeButton.Instance.Selected - 1, 0), CreateRoom);
    }


    //编辑房间
    private void OnEditRoom()
    {
        var selectRoom = RoomGrid.SelectData;
        if (selectRoom == null)
        {
            EditorWindowManager.ShowTips("提示", "请选择需要编辑的房间！");
        }
        else
        {
            EditorWindowManager.ShowEditRoom(selectRoom, (room) =>
            {
                //保存房间数据
                MapProjectManager.SaveRoomInfo(room);
                OnSearchRoomButtonClick();
            });
        }
    }

    //删除房间
    private void OnDeleteRoom()
    {
        var selectRoom = RoomGrid.SelectData;
        if (selectRoom == null)
        {
            EditorWindowManager.ShowTips("提示", "请选择需要删除的房间！");
        }
        else
        {
            EditorWindowManager.ShowDelayConfirm("提示", $"是否删除房间: {selectRoom.RoomInfo.RoomName}, 该操作无法撤销！", 3, result =>
            {
                if (result)
                {
                    //删除房间
                    if (MapProjectManager.DeleteRoom(EditorTileMapManager.SelectDungeonGroup, selectRoom))
                    {
                        MapProjectManager.SaveGroupMap();
                        OnSearchRoomButtonClick();
                    }
                }
            });
        }
    }

    //创建地牢组
    private void CreateGroup(DungeonRoomGroup group)
    {
        MapProjectManager.CreateGroup(group);
    }

    //编辑地牢组
    private void EditGroup(DungeonRoomGroup group)
    {
        MapProjectManager.SaveGroupMap();
    }

    //删除地牢组
    private void DeleteGroup(bool v)
    {
        MapProjectManager.DeleteGroup(GroupGrid.SelectData.GroupName);
    }

    //创建房间
    private void CreateRoom(DungeonRoomSplit roomSplit)
    {
        MapProjectManager.CreateRoom(roomSplit);
    }

    //创建地牢组完成
    private void OnCreateGroupFinish(object group)
    {
        OnSearchGroupButtonClick();
    }

    //删除地牢组完成
    private void OnDeleteGroupFinish(object group)
    {
        OnSearchGroupButtonClick();
    }

//创建地牢房间完成
    private void OnCreateRoomFinish(object roomSplit)
    {
        OnSearchRoomButtonClick();
    }
}
