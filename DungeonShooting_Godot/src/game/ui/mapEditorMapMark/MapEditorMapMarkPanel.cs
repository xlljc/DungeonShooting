using Godot;
using UI.MapEditor;

namespace UI.MapEditorMapMark;

public partial class MapEditorMapMarkPanel : MapEditorMapMark
{
    public enum SelectToolType
    {
        None,
        Wave,
        Mark
    }
    
    /// <summary>
    /// 选中的cell选项
    /// </summary>
    public IUiCell SelectCell { get; private set; }

    /// <summary>
    /// Cell 上的工具类型
    /// </summary>
    public SelectToolType ToolType { get; private set; } = SelectToolType.None;
    
    /// <summary>
    /// 编辑器Tile对象
    /// </summary>
    public EditorTileMap EditorTileMap { get; private set; }
    
    private UiGrid<WaveItem, object> _grid;

    public override void OnCreateUi()
    {
        var editorPanel = (MapEditorPanel)ParentUi;
        EditorTileMap = editorPanel.S_TileMap.Instance;

        S_DynamicTool.Instance.GetParent().RemoveChild(S_DynamicTool.Instance);
        S_DynamicTool.Instance.Visible = true;
        
        _grid = new UiGrid<WaveItem, object>(S_WaveItem, typeof(EditorWaveCell));
        _grid.SetCellOffset(new Vector2I(0, 10));
        _grid.SetColumns(1);
        
        //_grid.SetDataList(new object[] { 1, 2, 3, 4});
        S_AddPreinstall.Instance.Pressed += OnAddPreinstall;
        S_AddWaveButton.Instance.Pressed += OnAddWave;
        
        //S_Test.Instance.
    }

    public override void OnShowUi()
    {
        RefreshPreinstallSelect();
    }

    public override void OnDestroyUi()
    {
        _grid.Destroy();
    }

    /// <summary>
    /// 刷新预设下拉框
    /// </summary>
    public void RefreshPreinstallSelect(int index = -1)
    {
        var preinstall = EditorTileMap.RoomSplit.Preinstall;
        var optionButton = S_PreinstallOption.Instance;
        var selectIndex = index < 0 ? optionButton.Selected : index;
        optionButton.Clear();
        foreach (var item in preinstall)
        {
            optionButton.AddItem($"{item.Name} ({item.Weight})");
        }

        optionButton.Selected = selectIndex;
    }
    
    /// <summary>
    /// 选中 cell, 并设置显示的工具
    /// </summary>
    /// <param name="uiCell">选中 cell 对象</param>
    /// <param name="toolRoot">按钮工具绑定的父节点</param>
    /// <param name="toolType">选择工具类型</param>
    public void SetSelectCell(IUiCell uiCell, Node toolRoot, SelectToolType toolType)
    {
        if (SelectCell == uiCell)
        {
            return;
        }

        if (uiCell == null)
        {
            RemoveSelectCell();
            return;
        }

        var parent = S_DynamicTool.Instance.GetParent();
        if (parent != null)
        {
            parent.RemoveChild(S_DynamicTool.Instance);
        }

        toolRoot.AddChild(S_DynamicTool.Instance);
        if (SelectCell != null)
        {
            SelectCell.OnUnSelect();
        }
        SelectCell = uiCell;
        ToolType = toolType;
        SelectCell.OnSelect();
    }

    /// <summary>
    /// 移除选中的 cell 对象
    /// </summary>
    public void RemoveSelectCell()
    {
        if (SelectCell == null)
        {
            return;
        }
        var parent = S_DynamicTool.Instance.GetParent();
        if (parent != null)
        {
            parent.RemoveChild(S_DynamicTool.Instance);
        }
        SelectCell.OnUnSelect();
        SelectCell = null;
    }

    /// <summary>
    /// 创建预设
    /// </summary>
    public void OnAddPreinstall()
    {
        var roomSplitPreinstall = EditorTileMap.RoomSplit.Preinstall;
        EditorWindowManager.ShowCreatePreinstall(roomSplitPreinstall, preinstall =>
        {
            roomSplitPreinstall.Add(preinstall);
            RefreshPreinstallSelect(roomSplitPreinstall.Count - 1);
        });
    }

    /// <summary>
    /// 添加波数
    /// </summary>
    public void OnAddWave()
    {
        if (S_PreinstallOption.Instance.Selected == -1)
        {
            EditorWindowManager.ShowTips("警告", "请先选择预设!");
            return;
        }
    }
    
    /// <summary>
    /// 编辑波数据
    /// </summary>
    public void OnEditWave()
    {
        
    }


    /// <summary>
    /// 删除波数据
    /// </summary>
    public void OnDeleteWave()
    {
        
    }

    /// <summary>
    /// 添加标记
    /// </summary>
    public void OnAddMark()
    {
        
    }
    
    /// <summary>
    /// 编辑标记数据
    /// </summary>
    public void OnEditMark()
    {
        
    }
    
    /// <summary>
    /// 删除标记数据
    /// </summary>
    public void OnDeleteMark()
    {
        
    }
}
