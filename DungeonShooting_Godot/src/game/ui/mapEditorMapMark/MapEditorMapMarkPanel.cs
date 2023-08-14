using System.Collections.Generic;
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
    
    //波数网格组件
    private UiGrid<WaveItem, List<MarkInfo>> _grid;
    private EventFactory _eventFactory;

    public override void OnCreateUi()
    {
        var editorPanel = (MapEditorPanel)ParentUi;
        EditorTileMap = editorPanel.S_TileMap.Instance;

        //S_DynamicTool.Instance.GetParent().RemoveChild(S_DynamicTool.Instance);
        S_DynamicTool.Instance.Visible = false;
        
        _grid = new UiGrid<WaveItem, List<MarkInfo>>(S_WaveItem, typeof(EditorWaveCell));
        _grid.SetCellOffset(new Vector2I(0, 10));
        _grid.SetColumns(1);

        S_PreinstallOption.Instance.ItemSelected += OnItemSelected;
        S_AddPreinstall.Instance.Pressed += OnAddPreinstall;
        S_AddWaveButton.Instance.Pressed += OnAddWave;

        S_EditButton.Instance.Pressed += OnToolEditClick;
        S_DeleteButton.Instance.Pressed += OnToolDeleteClick;
    }

    public override void OnShowUi()
    {
        _eventFactory = EventManager.CreateEventFactory();
        _eventFactory.AddEventListener(EventEnum.OnSelectMark, OnSelectMark);
        RefreshPreinstallSelect();
    }

    public override void OnHideUi()
    {
        _eventFactory.RemoveAllEventListener();
        _eventFactory = null;
    }

    public override void OnDestroyUi()
    {
        _grid.Destroy();
    }

    //选中标记回调
    private void OnSelectMark(object arg)
    {
        if (arg is MarkInfo markInfo && (SelectCell is not EditorMarkCell || (SelectCell is EditorMarkCell markCell && markCell.Data != markInfo)))
        {
            var selectPreinstall = GetSelectPreinstall();
            if (selectPreinstall != null)
            {
                for (var i = 0; i < selectPreinstall.WaveList.Count; i++)
                {
                    var wave = selectPreinstall.WaveList[i];
                    for (var j = 0; j < wave.Count; j++)
                    {
                        var tempMark = wave[j];
                        if (tempMark == markInfo)
                        {
                            var waveCell = (EditorWaveCell)_grid.GetCell(i);
                            var cell = (EditorMarkCell)waveCell.MarkGrid.GetCell(j);
                            //如果没有展开, 则调用展开方法
                            if (!waveCell.IsExpand())
                            {
                                waveCell.OnExpandOrClose();
                            }
                            //选中物体
                            SetSelectCell(cell, cell.CellNode.Instance, SelectToolType.Mark);
                        }
                    }
                }
            }
        }
    }
    
    /// <summary>
    /// 获取当前选中的预设
    /// </summary>
    public RoomPreinstall GetSelectPreinstall()
    {
        var index = S_PreinstallOption.Instance.Selected;
        var preinstall = EditorTileMap.RoomSplit.Preinstall;
        if (index >= preinstall.Count)
        {
            return null;
        }
        return preinstall[index];
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
            if (item.WaveList == null)
            {
                item.WaveList = new List<List<MarkInfo>>();
            }

            optionButton.AddItem($"{item.Name} ({item.Weight})");
        }

        if (selectIndex == -1 && preinstall.Count > 0)
        {
            OnItemSelected(0);
        }
        else
        {
            OnItemSelected(selectIndex);
        }
    }

    /// <summary>
    /// 下拉框选中项
    /// </summary>
    public void OnItemSelected(long index)
    {
        EditorTileMap.SelectPreinstallIndex = (int)index;
        var preinstall = EditorTileMap.RoomSplit.Preinstall;
        if (index >= 0 && index <= preinstall.Count)
        {
            _grid.SetDataList(preinstall[(int)index].WaveList.ToArray());
        }
        else
        {
            _grid.RemoveAll();
        }
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

        if (toolType == SelectToolType.Wave) //不需要显示编辑波数按钮
        {
            S_DynamicTool.L_EditButton.Instance.Visible = false;
        }
        else
        {
            S_DynamicTool.L_EditButton.Instance.Visible = true;
        }

        //显示工具
        S_DynamicTool.Instance.Visible = true;
        
        //改变所在父节点
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
        var parent = S_DynamicTool.GetParent();
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
        var index = S_PreinstallOption.Instance.Selected;
        if (index == -1)
        {
            EditorWindowManager.ShowTips("警告", "请先选择预设!");
            return;
        }

        var preinstall = EditorTileMap.RoomSplit.Preinstall;
        if (index >= preinstall.Count)
        {
            EditorWindowManager.ShowTips("警告", "未知预设选项!");
            return;
        }
        var item = preinstall[index];
        var wave = new List<MarkInfo>();
        item.WaveList.Add(wave);
        _grid.Add(wave);
    }

    //工具节点编辑按钮点击
    private void OnToolEditClick()
    {
        if (ToolType == SelectToolType.Mark)
        {
            OnEditMark();
        }
    }
    
    //工具节点删除按钮点击
    private void OnToolDeleteClick()
    {
        if (ToolType == SelectToolType.Wave)
        {
            OnDeleteWave();
        }
        else if (ToolType == SelectToolType.Mark)
        {
            OnDeleteMark();
        }
    }


    /// <summary>
    /// 删除波数据
    /// </summary>
    public void OnDeleteWave()
    {
        var index = EditorTileMap.SelectWaveIndex;
        if (index < 0)
        {
            return;
        }
        
        var selectPreinstall = GetSelectPreinstall();
        if (selectPreinstall == null)
        {
            return;
        }
        
        //隐藏工具
        S_DynamicTool.Reparent(this);
        S_DynamicTool.Instance.Visible = false;
        //移除数据
        selectPreinstall.WaveList.RemoveAt(index);
        _grid.RemoveByIndex(index);
        EditorTileMap.SelectWaveIndex = -1;
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
