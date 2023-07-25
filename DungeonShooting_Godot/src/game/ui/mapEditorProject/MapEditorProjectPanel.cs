using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using UI.EditorWindow;
using UI.MapEditorCreateGroup;
using UI.MapEditorCreateRoom;

namespace UI.MapEditorProject;

public partial class MapEditorProjectPanel : MapEditorProject
{
    /// <summary>
    /// 当前选中的组
    /// </summary>
    public DungeonRoomGroup SelectGroupInfo;

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
        optionButton.AddItem("全部", -1);
        for (var i = 0; i < roomTypes.Length; i++)
        {
            var dungeonRoomType = roomTypes[i];
            optionButton.AddItem(DungeonManager.DungeonRoomTypeToDescribeString(dungeonRoomType), (int)dungeonRoomType);
        }

        _groupGrid = new UiGrid<GroupButton, DungeonRoomGroup>(S_GroupButton, typeof(GroupButtonCell));
        _groupGrid.SetCellOffset(new Vector2I(0, 2));
        _groupGrid.SetHorizontalExpand(true);

        _roomGrid = new UiGrid<RoomButton, DungeonRoomSplit>(S_RoomButton, typeof(RoomButtonCell));
        _roomGrid.SetAutoColumns(true);
        _roomGrid.SetCellOffset(new Vector2I(10, 10));
        _roomGrid.SetHorizontalExpand(true);
    }

    public override void OnShowUi()
    {
        S_GroupSearchButton.Instance.Pressed += OnSearchGroupButtonClick;
        S_RoomSearchButton.Instance.Pressed += OnSearchRoomButtonClick;
        S_RoomAddButton.Instance.Pressed += OnCreateRoomClick;
        S_GroupAddButton.Instance.Pressed += OnCreateGroupClick;
        RefreshGroup();
        _eventFactory = EventManager.CreateEventFactory();
        _eventFactory.AddEventListener(EventEnum.OnCreateGroupFinish, OnCreateGroupFinish);
        _eventFactory.AddEventListener(EventEnum.OnCreateRoomFinish, OnCreateRoomFinish);
    }

    public override void OnHideUi()
    {
        S_GroupSearchButton.Instance.Pressed -= OnSearchGroupButtonClick;
        S_RoomSearchButton.Instance.Pressed -= OnSearchRoomButtonClick;
        S_RoomAddButton.Instance.Pressed -= OnCreateRoomClick;
        S_GroupAddButton.Instance.Pressed -= OnCreateGroupClick;
        _eventFactory.RemoveAllEventListener();
    }

    public override void OnDestroyUi()
    {
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
        SelectGroupInfo = group;
        OnSearchRoomButtonClick();
    }

    /// <summary>
    /// 选择地图并打开地图编辑器
    /// </summary>
    public void SelectRoom(DungeonRoomSplit room)
    {
        HideUi();
        //打开地牢Ui
        var mapEditor = UiManager.Open_MapEditor();
        mapEditor.PrevUi = this;
        //加载地牢
        mapEditor.LoadMap(room);
    }
    
    //搜索组按钮点击
    private void OnSearchGroupButtonClick()
    {
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
    }

    //搜索房间按钮点击
    private void OnSearchRoomButtonClick()
    {
        if (SelectGroupInfo != null)
        {
            //输入文本
            var text = S_RoomSearchInput.Instance.Text;
            //房间类型
            var roomType = S_RoomTypeButton.Instance.GetSelectedId();

            IEnumerable<DungeonRoomSplit> result = SelectGroupInfo.GetAllRoomList();
            
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
                var type = (DungeonRoomType)roomType;
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
        var window = UiManager.Open_EditorWindow();
        window.SetWindowTitle("创建地牢组");
        window.SetWindowSize(new Vector2I(700, 500));
        var body = window.OpenBody<MapEditorCreateGroupPanel>(UiManager.UiName.MapEditorCreateGroup);
        window.SetButtonList(
            new EditorWindowPanel.ButtonData("确定", () =>
            {
                //获取填写的数据, 并创建ui
                var groupInfo = body.GetGroupInfo();
                if (groupInfo != null)
                {
                    window.CloseWindow();
                    CreateGroup(groupInfo);
                }
            }),
            new EditorWindowPanel.ButtonData("取消", () =>
            {
                window.CloseWindow();
            })
        );
    }
    
    //创建地牢房间按钮点击
    private void OnCreateRoomClick()
    {
        var window = UiManager.Open_EditorWindow();
        window.SetWindowTitle("创建地牢房间");
        window.SetWindowSize(new Vector2I(700, 600));
        var body = window.OpenBody<MapEditorCreateRoomPanel>(UiManager.UiName.MapEditorCreateRoom);
        if (SelectGroupInfo != null)
        {
            body.SetSelectGroup(SelectGroupInfo.GroupName);
        }
        body.SetSelectType(Mathf.Max(S_RoomTypeButton.Instance.Selected - 1, 0));
        
        window.SetButtonList(
            new EditorWindowPanel.ButtonData("确定", () =>
            {
                //获取填写的数据, 并创建ui
                var roomSplit = body.GetRoomInfo();
                if (roomSplit != null)
                {
                    window.CloseWindow();
                    CreateRoom(roomSplit);
                }
            }),
            new EditorWindowPanel.ButtonData("取消", () =>
            {
                window.CloseWindow();
            })
        );
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
