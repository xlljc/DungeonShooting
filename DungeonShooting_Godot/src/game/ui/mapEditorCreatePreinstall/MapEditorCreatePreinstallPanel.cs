using System.Collections.Generic;
using System.Text.RegularExpressions;
using Godot;

namespace UI.MapEditorCreatePreinstall;

public partial class MapEditorCreatePreinstallPanel : MapEditorCreatePreinstall
{
    private RoomPreinstallInfo _roomPreinstallInfo;
    private DungeonRoomType _roomType;

    /// <summary>
    /// 初始化房间预设数据, 用于创建预设
    /// </summary>
    public void InitData(DungeonRoomType roomType)
    {
        _roomType = roomType;
    }
    
    /// <summary>
    /// 初始化房间预设数据, 用于编辑预设
    /// </summary>
    public void InitData(DungeonRoomType roomType, RoomPreinstallInfo preinstallInfo)
    {
        InitData(roomType);
        _roomPreinstallInfo = preinstallInfo;
        S_PreinstallNameInput.Instance.Text = preinstallInfo.Name;
        S_AutoCheckInput.Instance.ButtonPressed = preinstallInfo.AutoFill;
        S_WeightInput.Instance.Value = preinstallInfo.Weight;
        S_RemarkInput.Instance.Text = preinstallInfo.Remark;
    }

    /// <summary>
    /// 填完数据后创建数据进行验证并创建数据对象, 如果验证失败, 则返回null
    /// </summary>
    public RoomPreinstallInfo GetRoomPreinstall(List<RoomPreinstallInfo> roomPreinstalls)
    {
        RoomPreinstallInfo data;
        if (_roomPreinstallInfo != null) //编辑数据
        {
            data = _roomPreinstallInfo;
            data.Name = S_PreinstallNameInput.Instance.Text;
            //检查名称是否合规
            if (string.IsNullOrEmpty(data.Name))
            {
                EditorWindowManager.ShowTips("错误", "预设名称不能为空!");
                return null;
            }
            var index = roomPreinstalls.FindIndex(preinstall => preinstall.Name == data.Name && preinstall != _roomPreinstallInfo);
            if (index >= 0)
            {
                EditorWindowManager.ShowTips("错误", "当前房间已经存在预设名称'" + data.Name + "', 请使用其他名称!");
                return null;
            }
            
            data.AutoFill = S_AutoCheckInput.Instance.ButtonPressed;
            data.Remark = S_RemarkInput.Instance.Text;
            data.Weight = (int)S_WeightInput.Instance.Value;
        }
        else //创建数据
        {
            data = new RoomPreinstallInfo();
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

            data.AutoFill = S_AutoCheckInput.Instance.ButtonPressed;
            data.Remark = S_RemarkInput.Instance.Text;
            data.Weight = (int)S_WeightInput.Instance.Value;
            //预加载波
            data.InitWaveList();
            //初始化特殊标记
            data.InitSpecialMark(_roomType);
        }
        return data;
    }
}
