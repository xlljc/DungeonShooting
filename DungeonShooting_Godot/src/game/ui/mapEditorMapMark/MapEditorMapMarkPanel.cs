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

    public class MarkCellData
    {
        /// <summary>
        /// 所在的父级 Cell
        /// </summary>
        public EditorWaveCell ParentCell;
        /// <summary>
        /// 标记数据对象
        /// </summary>
        public MarkInfo MarkInfo;
        /// <summary>
        /// 是否提前加载
        /// </summary>
        public bool Preloading;

        public MarkCellData(EditorWaveCell parentCell, MarkInfo markInfo, bool preloading)
        {
            ParentCell = parentCell;
            MarkInfo = markInfo;
            Preloading = preloading;
        }
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
        S_EditPreinstall.Instance.Pressed += OnEditPreinstall;
        S_DeletePreinstall.Instance.Pressed += OnDeletePreinstall;
        S_AddWaveButton.Instance.Pressed += OnAddWave;

        S_EditButton.Instance.Pressed += OnToolEditClick;
        S_DeleteButton.Instance.Pressed += OnToolDeleteClick;
        
        _eventFactory = EventManager.CreateEventFactory();
        _eventFactory.AddEventListener(EventEnum.OnSelectMark, OnSelectMark);
    }

    public override void OnDestroyUi()
    {
        _eventFactory.RemoveAllEventListener();
        _eventFactory = null;
        _grid.Destroy();
        S_DynamicTool.QueueFree();
    }

    //选中标记回调
    private void OnSelectMark(object arg)
    {
        if (arg is MarkInfo markInfo && (SelectCell is not EditorMarkCell || (SelectCell is EditorMarkCell markCell && markCell.Data.MarkInfo != markInfo)))
        {
            var selectPreinstall = GetSelectPreinstall();
            if (selectPreinstall != null)
            {
                var waveCells = _grid.GetAllCell();
                foreach (var waveCell in waveCells)
                {
                    var tempWaveCell = (EditorWaveCell)waveCell;
                    var markCells = tempWaveCell.MarkGrid.GetAllCell();
                    for (var i = 0; i < markCells.Length; i++)
                    {
                        var tempMarkCell = (EditorMarkCell)markCells[i];
                        if (tempMarkCell.Data.MarkInfo == markInfo)
                        {
                            //如果没有展开, 则调用展开方法
                            if (!tempWaveCell.IsExpand())
                            {
                                tempWaveCell.OnExpandOrClose();
                            }
                            //选中物体
                            tempMarkCell.OnClickHandler();
                            return;
                        }
                    }
                }
            }
        }
    }
    
    /// <summary>
    /// 获取当前选中的预设
    /// </summary>
    public RoomPreinstallInfo GetSelectPreinstall()
    {
        var index = S_PreinstallOption.Instance.Selected;
        var preinstall = EditorManager.SelectRoom.Preinstall;
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
        var preinstall = EditorManager.SelectRoom.Preinstall;
        var optionButton = S_PreinstallOption.Instance;
        var selectIndex = index < 0 ? (preinstall.Count > 0 ? 0 : -1) : index;
        optionButton.Clear();
        foreach (var item in preinstall)
        {
            if (item.WaveList == null)
            {
                item.InitWaveList();
            }

            optionButton.AddItem($"{item.Name} ({item.Weight})");
        }
        
        //下拉框选中项
        optionButton.Selected = selectIndex;
        OnItemSelected(selectIndex);
    }

    /// <summary>
    /// 下拉框选中项
    /// </summary>
    public void OnItemSelected(long index)
    {
        //清除选中项
        RemoveSelectCell();
        //记录选中波数
        EditorManager.SetSelectWaveIndex(-1);
        //记录选中的预设
        EditorManager.SetSelectPreinstallIndex((int)index);
        var preinstall = EditorManager.SelectRoom.Preinstall;
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

        if (toolType == SelectToolType.Wave)
        {
            //不需要显示编辑波数按钮
            S_DynamicTool.L_EditButton.Instance.Visible = false;
            //预加载波不能删除
            S_DynamicTool.L_DeleteButton.Instance.Visible = uiCell.Index != 0;
        }
        else //显示编辑按钮
        {
            var markCell = (EditorMarkCell)uiCell;
            var markType = markCell.Data.MarkInfo.SpecialMarkType;
            if (markType == SpecialMarkType.BirthPoint) //某些特殊标记不能删除
            {
                S_DynamicTool.L_EditButton.Instance.Visible = true;
                S_DynamicTool.L_DeleteButton.Instance.Visible = false;
            }
            else //普通标记
            {
                S_DynamicTool.L_EditButton.Instance.Visible = true;
                S_DynamicTool.L_DeleteButton.Instance.Visible = true;
            }
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
        var roomInfoRoomType = EditorManager.SelectRoom.RoomInfo.RoomType;
        var roomSplitPreinstall = EditorManager.SelectRoom.Preinstall;
        EditorWindowManager.ShowCreatePreinstall(roomInfoRoomType, roomSplitPreinstall, preinstall =>
        {
            //创建逻辑
            roomSplitPreinstall.Add(preinstall);
            RefreshPreinstallSelect(roomSplitPreinstall.Count - 1);
            //派发数据修改事件
            EventManager.EmitEvent(EventEnum.OnEditorDirty);
        });
    }

    /// <summary>
    /// 编辑预设
    /// </summary>
    public void OnEditPreinstall()
    {
        var roomInfoRoomType = EditorManager.SelectRoom.RoomInfo.RoomType;
        var roomSplitPreinstall = EditorManager.SelectRoom.Preinstall;
        var selectPreinstall = GetSelectPreinstall();
        EditorWindowManager.ShowEditPreinstall(roomInfoRoomType, roomSplitPreinstall, selectPreinstall, preinstall =>
        {
            //修改下拉菜单数据
            var optionButton = S_PreinstallOption.Instance;
            optionButton.SetItemText(optionButton.Selected, $"{preinstall.Name} ({preinstall.Weight})");
            //派发数据修改事件
            EventManager.EmitEvent(EventEnum.OnEditorDirty);
        });
    }

    /// <summary>
    /// 删除预设
    /// </summary>
    public void OnDeletePreinstall()
    {
        var index = EditorManager.SelectPreinstallIndex;
        if (index < 0)
        {
            return;
        }
        
        EditorWindowManager.ShowConfirm("提示", "是否删除当前预设？", v =>
        {
            if (v)
            {
                //先把选中项置为-1
                EditorManager.SetSelectPreinstallIndex(-1);
                //移除预设数据
                EditorManager.SelectRoom.Preinstall.RemoveAt(index);
                //刷新选项
                RefreshPreinstallSelect(EditorManager.SelectRoom.Preinstall.Count - 1);
                //派发数据修改事件
                EventManager.EmitEvent(EventEnum.OnEditorDirty);
            }
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

        var preinstall = EditorManager.SelectRoom.Preinstall;
        if (index >= preinstall.Count)
        {
            EditorWindowManager.ShowTips("警告", "未知预设选项!");
            return;
        }
        var item = preinstall[index];
        var wave = new List<MarkInfo>();
        item.WaveList.Add(wave);
        _grid.Add(wave);
        //派发数据修改事件
        EventManager.EmitEvent(EventEnum.OnEditorDirty);
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
        var index = EditorManager.SelectWaveIndex;
        if (index < 0)
        {
            return;
        }
        
        var selectPreinstall = GetSelectPreinstall();
        if (selectPreinstall == null)
        {
            return;
        }

        var wave = selectPreinstall.WaveList[index];
        EditorWindowManager.ShowConfirm("提示", $"是否删除当前波？\n当前波数包含{wave.Count}个标记", v =>
        {
            if (v)
            {
                //隐藏工具
                S_DynamicTool.Reparent(this);
                S_DynamicTool.Instance.Visible = false;
                //派发移除标记事件
                foreach (var markInfo in wave)
                {
                    EventManager.EmitEvent(EventEnum.OnDeleteMark, markInfo);
                }
                //移除数据
                selectPreinstall.WaveList.RemoveAt(index);
                _grid.RemoveByIndex(index);
                EditorManager.SetSelectWaveIndex(-1);
                //派发数据修改事件
                EventManager.EmitEvent(EventEnum.OnEditorDirty);
            }
        });
    }
    
    /// <summary>
    /// 编辑标记数据
    /// </summary>
    public void OnEditMark()
    {
        if (SelectCell is EditorMarkCell markCell)
        {
            var dataMarkInfo = markCell.Data.MarkInfo;
            //打开编辑面板
            EditorWindowManager.ShowEditMark(dataMarkInfo, markCell.Data.Preloading, (mark) =>
            {
                //为了引用不变, 所以这里使用克隆数据
                dataMarkInfo.CloneFrom(mark);
                //刷新 Cell
                markCell.SetData(markCell.Data);
                //执行排序
                markCell.Grid.Sort();
                EventManager.EmitEvent(EventEnum.OnEditMark, dataMarkInfo);
                //派发数据修改事件
                EventManager.EmitEvent(EventEnum.OnEditorDirty);
            });
        }
    }
    
    /// <summary>
    /// 删除标记数据
    /// </summary>
    public void OnDeleteMark()
    {
        if (SelectCell is EditorMarkCell markCell)
        {
            var index = EditorManager.SelectWaveIndex;
            if (index < 0)
            {
                return;
            }

            var selectPreinstall = GetSelectPreinstall();
            if (selectPreinstall == null)
            {
                return;
            }

            EditorWindowManager.ShowConfirm("提示", "是否删除当前标记？", v =>
            {
                if (v)
                {
                    var waveCell = (EditorWaveCell)_grid.GetCell(index);
                    //隐藏工具
                    S_DynamicTool.Reparent(this);
                    S_DynamicTool.Instance.Visible = false;
                    var markCellIndex = markCell.Index;
                    var markInfo = waveCell.MarkGrid.GetData(markCellIndex).MarkInfo;
                    //派发移除标记事件
                    EventManager.EmitEvent(EventEnum.OnDeleteMark, markInfo);
                    waveCell.MarkGrid.RemoveByIndex(markCellIndex);
                    waveCell.Data.Remove(markInfo);
                    //派发数据修改事件
                    EventManager.EmitEvent(EventEnum.OnEditorDirty);
                }
            });
        }
    }
}
