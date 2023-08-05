using System.Collections.Generic;
using System.Text.RegularExpressions;
using Godot;

namespace UI.MapEditorCreatePreinstall;

public partial class MapEditorCreatePreinstallPanel : MapEditorCreatePreinstall
{
    /// <summary>
    /// 填完数据后创建数据进行验证并创建数据对象, 如果验证失败, 则返回null
    /// </summary>
    /// <param name="roomPreinstalls"></param>
    /// <returns></returns>
    public RoomPreinstall GetRoomPreinstall(List<RoomPreinstall> roomPreinstalls)
    {
        var data = new RoomPreinstall();
        data.Name = S_PreinstallNameInput.Instance.Text;
        //检查名称是否合规
        if (string.IsNullOrEmpty(data.Name))
        {
            EditorWindowManager.ShowTips("错误", "预设名称不能为空!");
            return null;
        }

        var index = roomPreinstalls.FindIndex(preinstall => preinstall.Name == data.Name);
        if (index >= 0)
        {
            EditorWindowManager.ShowTips("错误", "当前房间已经存在预设名称'" + data.Name + "', 请使用其他名称!");
            return null;
        }

        data.Remark = S_RemarkInput.Instance.Text;
        data.Weight = (int)S_WeightInput.Instance.Value;
        return data;
    }
    
}
