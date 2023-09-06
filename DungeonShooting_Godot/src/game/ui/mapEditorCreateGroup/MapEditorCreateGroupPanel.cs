using System.IO;
using System.Text.RegularExpressions;
using Godot;

namespace UI.MapEditorCreateGroup;

public partial class MapEditorCreateGroupPanel : MapEditorCreateGroup
{

    /// <summary>
    /// 填完数据后获取数据对象, 并进行验证, 如果验证失败, 则返回 null
    /// </summary>
    public DungeonRoomGroup GetGroupInfo()
    {
        //组名
        var groupName = S_GroupNameInput.Instance.Text;
        
        //检查名称是否合规
        if (string.IsNullOrEmpty(groupName))
        {
            EditorWindowManager.ShowTips("错误", "组名称不能为空!");
            return null;
        }
        
        //验证是否有同名组
        var path = MapProjectManager.CustomMapPath + groupName;
        var dir = new DirectoryInfo(path);
        if (dir.Exists && dir.GetDirectories().Length > 0)
        {
            EditorWindowManager.ShowTips("错误", $"已经有相同路径的房间了!");
            return null;
        }

        var group = new DungeonRoomGroup();
        group.GroupName = groupName;
        group.Remark = S_RemarkInput.Instance.Text;
        return group;
    }
}
