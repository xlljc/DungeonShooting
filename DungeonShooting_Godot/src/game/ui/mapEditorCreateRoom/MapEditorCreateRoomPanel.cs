using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Godot;

namespace UI.MapEditorCreateRoom;

public partial class MapEditorCreateRoomPanel : MapEditorCreateRoom
{
    //key: 组名称, value: 选项索引
    private Dictionary<string, int> _groupMap = new Dictionary<string, int>();

    public override void OnCreateUi()
    {
        //初始化选项
        var groupButton = S_GroupSelect.Instance;
        var index = 0;
        foreach (var mapGroupInfo in MapProjectManager.GroupData)
        {
            var id = index++;
            _groupMap.Add(mapGroupInfo.Value.Name, id);
            groupButton.AddItem(mapGroupInfo.Value.Name, id);
        }

        var selectButton = S_TypeSelect.Instance;
        var roomTypes = Enum.GetValues<DungeonRoomType>();
        for (var i = 0; i < roomTypes.Length; i++)
        {
            var item = roomTypes[i];
            var text = DungeonManager.DungeonRoomTypeToDescribeString(item);
            selectButton.AddItem(text, (int)item);
        }
    }

    /// <summary>
    /// 设置选中的组
    /// </summary>
    public void SetSelectGroup(string groupName)
    {
        if (_groupMap.TryGetValue(groupName, out var value))
        {
            S_GroupSelect.Instance.Selected = value;
            return;
        }
        
        S_GroupSelect.Instance.Selected = -1;
    }

    /// <summary>
    /// 设置选中的房间类型
    /// </summary>
    public void SetSelectType(int index)
    {
        S_TypeSelect.Instance.Selected = index;
    }

    /// <summary>
    /// 填完数据后获取数据对象, 并进行验证, 如果验证失败, 则返回 null
    /// </summary>
    public MapProjectManager.MapRoomInfo GetRoomInfo()
    {
        var mapRoomInfo = new MapProjectManager.MapRoomInfo();
        mapRoomInfo.Name = S_RoomNameInput.Instance.Text;
        //检查名称是否合规
        if (!Regex.IsMatch(mapRoomInfo.Name, "^\\w+$"))
        {
            EditorTipsManager.ShowTips("错误", "房间名称'" + mapRoomInfo.Name + "'不符合名称约束, 房间名称只允许包含大小写字母和数字!");
            return null;
        }
        
        var groupIndex = S_GroupSelect.Instance.Selected;
        foreach (var pair in _groupMap)
        {
            if (pair.Value == groupIndex)
            {
                mapRoomInfo.Group = pair.Key;
            }
        }

        if (mapRoomInfo.Group == null)
        {
            EditorTipsManager.ShowTips("错误", "组名错误!");
            return null;
        }
        
        var typeIndex = S_TypeSelect.Instance.Selected;
        mapRoomInfo.RoomType = (DungeonRoomType)typeIndex;
        
        //检测是否有同名房间
        var temp = mapRoomInfo.Group + "/" + DungeonManager.DungeonRoomTypeToString(mapRoomInfo.RoomType) + "/" + mapRoomInfo.Name;
        var path = GameConfig.RoomTileDir + temp;
        var dir = new DirectoryInfo(path);
        if (dir.Exists && dir.GetFiles().Length > 0)
        {
            EditorTipsManager.ShowTips("错误", $"已经有相同路径的房间了!\n路径: {temp}");
            return null;
        }
        
        return mapRoomInfo;
    }
}
