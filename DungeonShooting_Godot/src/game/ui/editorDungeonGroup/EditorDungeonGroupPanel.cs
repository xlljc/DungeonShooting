using Godot;

namespace UI.EditorDungeonGroup;

public partial class EditorDungeonGroupPanel : EditorDungeonGroup
{

    public override void OnCreateUi()
    {
        var optionButton = S_TileSetOption.Instance;
        foreach (var keyValuePair in GameApplication.Instance.TileSetConfig)
        {
            optionButton.AddItem(keyValuePair.Key);
        }

        optionButton.Selected = 0;
    }

    public override void OnDestroyUi()
    {
        
    }

    /// <summary>
    /// 初始化数据
    /// </summary>
    public void InitData(DungeonRoomGroup dungeonRoomGroup)
    {
        S_NameInput.Instance.Text = dungeonRoomGroup.GroupName;
        var optionButton = S_TileSetOption.Instance;
        var count = optionButton.ItemCount;
        for (int i = 0; i < count; i++)
        {
            if (optionButton.GetItemText(i) == dungeonRoomGroup.TileSet)
            {
                optionButton.Selected = i;
                break;
            }
        }

        S_RemarkInput.Instance.Text = dungeonRoomGroup.Remark;
    }
    
    /// <summary>
    /// 获取数据
    /// </summary>
    public DungeonGroupData GetData()
    {
        var data = new DungeonGroupData(S_NameInput.Instance.Text, S_TileSetOption.Instance.Text, S_RemarkInput.Instance.Text);
        return data;
    }

    /// <summary>
    /// 设置为编辑模式, 禁用部分属性
    /// </summary>
    public void SetEditMode()
    {
        S_NameInput.Instance.Editable = false;
        S_TileSetOption.Instance.Disabled = true;
    }
}
