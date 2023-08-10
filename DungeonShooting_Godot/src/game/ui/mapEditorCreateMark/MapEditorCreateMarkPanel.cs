using System.Collections.Generic;
using Config;
using Godot;

namespace UI.MapEditorCreateMark;

public partial class MapEditorCreateMarkPanel : MapEditorCreateMark
{

    private UiGrid<MarkObject, ExcelConfig.ActivityObject> _grid;
    
    public override void OnCreateUi()
    {
        //隐藏模板对象
        S_ExpandPanel.Instance.Visible = false;
        S_NumberBar.Instance.Visible = false;
        
        S_AddMark.Instance.Pressed += OnAddMark;

        _grid = new UiGrid<MarkObject, ExcelConfig.ActivityObject>(S_MarkObject, typeof(MarkObjectCell));
        _grid.SetColumns(1);
        _grid.SetHorizontalExpand(true);
        _grid.SetCellOffset(new Vector2I(0, 5));

    }

    public override void OnDestroyUi()
    {
        _grid.Destroy();
    }
    
    /// <summary>
    /// 初始化数据
    /// </summary>
    public void InitData(RoomPreinstall preinstall, int waveIndex)
    {
        var optionButton = S_WaveOption.Instance;
        for (var i = 0; i < preinstall.WaveList.Count; i++)
        {
            optionButton.AddItem($"第{i + 1}波");
        }

        optionButton.Selected = waveIndex;
    }

    //点击添加标记按钮
    private void OnAddMark()
    {
        EditorWindowManager.ShowSelectObject(OnSelectObject, this);
    }

    //选中物体回调
    private void OnSelectObject(ExcelConfig.ActivityObject activityObject)
    {
        _grid.Add(activityObject);
    }
}
