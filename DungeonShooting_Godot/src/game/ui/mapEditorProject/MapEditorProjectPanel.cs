using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

namespace UI.MapEditorProject;

public partial class MapEditorProjectPanel : MapEditorProject
{
    //当前显示的组数据
    private UiGrid<GroupButton, DungeonRoomGroup> _groupGrid;
    //当前显示的房间数据
    private UiGrid<RoomButton, DungeonRoomSplit> _roomGrid;
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
            optionButton.AddItem(DungeonManager.DungeonRoomTypeToDescribeString(dungeonRoomType), (int)dungeonRoomType + 1);
        }

        _groupGrid = new UiGrid<MapEditorProject.GroupButton, DungeonRoomGroup>(S_GroupButton, typeof(GroupButtonCell));
        _groupGrid.SetCellOffset(new Vector2I(0, 2));
        _groupGrid.SetHorizontalExpand(true);

        _roomGrid = new UiGrid<MapEditorProject.RoomButton, DungeonRoomSplit>(S_RoomButton, typeof(RoomButtonCell));
        _roomGrid.SetAutoColumns(true);
        _roomGrid.SetCellOffset(new Vector2I(10, 10));
        _roomGrid.SetHorizontalExpand(true);

        if (PrevUi != null)
        {
            S_Back.Instance.Visible = true;
            S_Back.Instance.Pressed += OpenPrevUi;
        }
        else
        {
            S_Back.Instance.Visible = false;
        }
        
        S_GroupSearchButton.Instance.Pressed += OnSearchGroupButtonClick;
        S_RoomSearchButton.Instance.Pressed += OnSearchRoomButtonClick;
        S_RoomAddButton.Instance.Pressed += OnCreateRoomClick;
        S_RoomEditButton.Instance.Pressed += OnEditRoom;
        S_RoomDeleteButton.Instance.Pressed += OnDeleteRoom;
        S_GroupAddButton.Instance.Pressed += OnCreateGroupClick;
        
        _eventFactory = EventManager.CreateEventFactory();
        _eventFactory.AddEventListener(EventEnum.OnCreateGroupFinish, OnCreateGroupFinish);
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
        _groupGrid.Destroy();
        _groupGrid = null;
        
        _roomGrid.Destroy();
        _roomGrid = null;
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
        EditorManager.SetSelectDungeonGroup(group);
        OnSearchRoomButtonClick();
    }

    /// <summary>
    /// 选择地图并打开地图编辑器
    /// </summary>
    public void SelectRoom(DungeonRoomSplit room)
    {
        EditorManager.SetSelectRoom(room);
        HideUi();
        //创建地牢Ui
        var mapEditor = UiManager.Create_MapEditor();
        mapEditor.PrevUi = this;
        //加载地牢
        mapEditor.LoadMap(room);
        //打开Ui
        mapEditor.ShowUi();
    }
    
    //搜索组按钮点击
    private void OnSearchGroupButtonClick()
    {
        var select = _groupGrid.SelectIndex;
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
            _groupGrid.SetDataList(list.ToArray());
        }
        else
        {
            _groupGrid.SetDataList(MapProjectManager.GroupMap.Values.ToArray());
        }

        _roomGrid.SelectIndex = select;
    }

    //搜索房间按钮点击
    private void OnSearchRoomButtonClick()
    {
        if (EditorManager.SelectDungeonGroup != null)
        {
            //输入文本
            var text = S_RoomSearchInput.Instance.Text;
            //房间类型
            var roomType = S_RoomTypeButton.Instance.GetSelectedId();

            IEnumerable<DungeonRoomSplit> result = EditorManager.SelectDungeonGroup.GetAllRoomList();
            
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
            
            _roomGrid.SetDataList(result.ToArray());
        }
        else
        {
            _roomGrid.RemoveAll();
        }
    }

    //创建组按钮点击
    private void OnCreateGroupClick()
    {
        EditorWindowManager.ShowCreateGroup(CreateGroup);
    }
    
    //创建地牢房间按钮点击
    private void OnCreateRoomClick()
    {
        var groupName = EditorManager.SelectDungeonGroup != null ? EditorManager.SelectDungeonGroup.GroupName : null;
        EditorWindowManager.ShowCreateRoom(groupName, Mathf.Max(S_RoomTypeButton.Instance.Selected - 1, 0), CreateRoom);
    }
    
    
    //编辑房间
    private void OnEditRoom()
    {
        var selectRoom = _roomGrid.SelectData;
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
        var selectRoom = _roomGrid.SelectData;
        if (selectRoom == null)
        {
            EditorWindowManager.ShowTips("提示", "请选择需要删除的房间！");
        }
        else
        {
            EditorWindowManager.ShowConfirm("提示", $"是否删除房间: {selectRoom.RoomInfo.RoomName}, 该操作无法撤销！", result =>
            {
                if (result)
                {
                    //删除房间
                    if (MapProjectManager.DeleteRoom(EditorManager.SelectDungeonGroup, selectRoom))
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

    //创建地牢房间完成
    private void OnCreateRoomFinish(object roomSplit)
    {
        OnSearchRoomButtonClick();
    }
}
