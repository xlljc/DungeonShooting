using System;
using System.Linq;
using Godot;
using UI.EditorWindow;
using UI.MapEditorCreateRoom;

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
    
    //当前显示的组数据
    private UiGrid<GroupButton, MapProjectManager.MapGroupInfo> _groupGrid;
    //当前显示的房间数据
    private UiGrid<RoomButton, MapProjectManager.MapRoomInfo> _roomGrid;

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
        S_GroupSearchButton.Instance.Pressed += OnSearchButtonClick;
        S_RoomAddButton.Instance.Pressed += OnCreateRoomClick;
        RefreshGroup();
    }

    public override void OnHideUi()
    {
        S_GroupSearchButton.Instance.Pressed -= OnSearchButtonClick;
        S_RoomAddButton.Instance.Pressed -= OnCreateRoomClick;
    }

    public override void OnDisposeUi()
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
        OnSearchButtonClick();
    }

    public void SelectGroup(MapProjectManager.MapGroupInfo group)
    {
        SelectGroupInfo = group;
        SelectRoomInfo = MapProjectManager.LoadRoom(group.RootPath, group.Name);
        _roomGrid.SetDataList(SelectRoomInfo);
    }

    /// <summary>
    /// 选择地图并打开地图编辑器
    /// </summary>
    public void SelectRoom(MapProjectManager.MapRoomInfo room)
    {
        HideUi();
        //打开地牢Ui
        var mapEditor = UiManager.Open_MapEditor();
        mapEditor.PrevUi = this;
        //加载地牢
        mapEditor.LoadMap(room.RootPath, room.Group, room.RoomType, room.Name);
    }
    
    //搜索组按钮点击
    private void OnSearchButtonClick()
    {
        //输入文本
        var text = S_GroupSearchInput.Instance.Text;
        if (!string.IsNullOrEmpty(text))
        {
            var str = text.Trim().ToLower();
            var result = MapProjectManager.GroupData.Values.Where(info =>
            {
                return info.Name.Trim().ToLower().Contains(str);
            });
            _groupGrid.SetDataList(result.ToArray());
        }
        else
        {
            _groupGrid.SetDataList(MapProjectManager.GroupData.Values.ToArray());
        }
    }
    
    //创建地牢房间按钮点击
    private void OnCreateRoomClick()
    {
        var window = UiManager.Open_EditorWindow();
        window.SetWindowTitle("创建地牢房间");
        window.SetWindowSize(new Vector2I(700, 500));
        var body = window.OpenBody<MapEditorCreateRoomPanel>(UiManager.UiName.MapEditorCreateRoom);
        if (SelectGroupInfo != null)
        {
            body.SetSelectGroup(SelectGroupInfo.Name);
        }
        body.SetSelectType(S_RoomTypeButton.Instance.Selected);
        
        window.SetButtonList(
            new EditorWindowPanel.ButtonData("确定", () =>
            {
                //获取填写的数据, 并创建ui
                var mapRoomInfo = body.GetRoomInfo();
                if (mapRoomInfo != null)
                {
                    window.CloseWindow();
                    CreateRoom(mapRoomInfo);
                }
            }),
            new EditorWindowPanel.ButtonData("取消", () =>
            {
                window.CloseWindow();
            })
        );
    }

    //创建房间
    private void CreateRoom(MapProjectManager.MapRoomInfo roomInfo)
    {
        
    }
}
