using System.Collections.Generic;
using Godot;
using UI.MapEditor;

namespace UI.MapEditorMapMark;

public class EditorWaveCell : UiCell<MapEditorMapMark.WaveItem, List<MarkInfo>>
{
    /// <summary>
    /// 标记显示/隐藏状态
    /// </summary>
    public bool MarkVisible { get; private set; } = true;
    
    public UiGrid<MapEditorMapMark.MarkItem, MapEditorMapMarkPanel.MarkCellData> MarkGrid;
    
    public override void OnInit()
    {
        //这里不绑定 Click 函数, 而是绑定 OnClickHandler, 因为 Select 交给 MapEditorMapMarkPanel 处理了
        CellNode.L_WaveContainer.L_WaveButton.Instance.Pressed += OnClickHandler;
        CellNode.L_WaveContainer.L_TextureButton.Instance.Pressed += OnExpandOrClose;
        CellNode.L_WaveContainer.L_WaveVisibleButton.Instance.Pressed += OnChangeMarkVisible;
        CellNode.L_MarginContainer.L_AddMarkButton.Instance.Pressed += OnAddMark;

        CellNode.L_MarkContainer.L_MarkItem.Instance.SetHorizontalExpand(true);
        MarkGrid = new UiGrid<MapEditorMapMark.MarkItem, MapEditorMapMarkPanel.MarkCellData>(CellNode.L_MarkContainer.L_MarkItem, typeof(EditorMarkCell));
        MarkGrid.SetColumns(1);
        MarkGrid.SetHorizontalExpand(true);
        MarkGrid.SetCellOffset(new Vector2I(0, 5));

        CellNode.Instance.CustomMinimumSize = Vector2.Zero;
    }

    public override void OnSetData(List<MarkInfo> data)
    {
        var array = new MapEditorMapMarkPanel.MarkCellData[data.Count];
        for (var i = 0; i < data.Count; i++)
        {
            array[i] = new MapEditorMapMarkPanel.MarkCellData(this, data[i], Index == 0);
        }
        MarkGrid.SetDataList(array);
        //执行排序操作
        MarkGrid.Sort();
        
        if (!MarkVisible)
        {
            SetWaveVisible(true);
        }
    }

    public override void OnRefreshIndex()
    {
        if (Index == 0)
        {
            CellNode.L_WaveContainer.L_WaveButton.Instance.Text = $"提前加载波";
        }
        else
        {
            CellNode.L_WaveContainer.L_WaveButton.Instance.Text = $"第{Index}波";
        }
    }

    public override void OnDestroy()
    {
        MarkGrid.Destroy();
    }

    //添加标记
    private void OnAddMark()
    {
        //打开添加标记页面
        EditorWindowManager.ShowCreateMark(CellNode.UiPanel.EditorTileMap.GetCenterPosition(), Index == 0, OnCreateMarkInfo);
    }   

    //创建的标记完成
    private void OnCreateMarkInfo(MarkInfo markInfo)
    {
        var preinstall = CellNode.UiPanel.GetSelectPreinstall();
        preinstall.WaveList[Index].Add(markInfo);
        MarkGrid.Add(new MapEditorMapMarkPanel.MarkCellData(this, markInfo, Index == 0));
        //添加标记工具
        EventManager.EmitEvent(EventEnum.OnCreateMark, markInfo);
        //选中最后一个
        //MarkGrid.SelectIndex
        MarkGrid.Click(MarkGrid.Count - 1);
        //执行排序操作
        MarkGrid.Sort();
        //如果当前波数隐藏, 则隐藏标记
        if (!MarkVisible)
        {
            EventManager.EmitEvent(EventEnum.OnSetMarkVisible, new MarkInfoVisibleData(markInfo, false));
        }
        //派发数据修改事件
        EventManager.EmitEvent(EventEnum.OnEditorDirty);
    }

    /// <summary>
    /// 展开/收起按钮点击
    /// </summary>
    public void OnExpandOrClose()
    {
        var marginContainer = CellNode.L_MarkContainer.Instance;
        var flag = !marginContainer.Visible;
        marginContainer.Visible = flag;
        CellNode.L_MarginContainer.Instance.Visible = flag;
        var textureButton = CellNode.L_WaveContainer.L_TextureButton.Instance;
        if (flag)
        {
            textureButton.TextureNormal = ResourceManager.LoadTexture2D(ResourcePath.resource_sprite_ui_commonIcon_Down_png);
        }
        else
        {
            textureButton.TextureNormal = ResourceManager.LoadTexture2D(ResourcePath.resource_sprite_ui_commonIcon_Right_png);
        }
    }

    /// <summary>
    /// 点击显示/隐藏按钮
    /// </summary>
    public void OnChangeMarkVisible()
    {
        var val = !MarkVisible;
        SetWaveVisible(val);

        //通知标记隐藏
        for (var i = 0; i < MarkGrid.Count; i++)
        {
            var markCellData = MarkGrid.GetCell(i).Data;
            EventManager.EmitEvent(EventEnum.OnSetMarkVisible, new MarkInfoVisibleData(markCellData.MarkInfo, val));
        }
    }

    /// <summary>
    /// 设置 WaveVisibleButton 按钮显示的图标, 显示/隐藏
    /// </summary>
    public void SetWaveVisible(bool v)
    {
        MarkVisible = v;
        if (v)
        {
            CellNode.L_WaveContainer.L_WaveVisibleButton.Instance.Icon =
                ResourceManager.LoadTexture2D(ResourcePath.resource_sprite_ui_commonIcon_Visible_png);
            CellNode.L_MarkContainer.Instance.Modulate = new Color(1, 1, 1);
        }
        else
        {
            CellNode.L_WaveContainer.L_WaveVisibleButton.Instance.Icon =
                ResourceManager.LoadTexture2D(ResourcePath.resource_sprite_ui_commonIcon_Hide_png);
            CellNode.L_MarkContainer.Instance.Modulate = new Color(1, 1, 1, 0.3f);
        }
    }

    /// <summary>
    /// 是否展开
    /// </summary>
    public bool IsExpand()
    {
        return CellNode.L_MarkContainer.Instance.Visible;
    }

    public void OnClickHandler()
    {
        EditorTileMapManager.SetSelectWaveIndex(Index);
        CellNode.UiPanel.SetSelectCell(this, CellNode.L_WaveContainer.Instance, MapEditorMapMarkPanel.SelectToolType.Wave);
        //清除选中的标记
        EditorTileMapManager.SetSelectMark(null);
    }

    public override void OnSelect()
    {
        CellNode.L_WaveContainer.L_WaveButton.L_Select.Instance.Visible = true;
    }

    public override void OnUnSelect()
    {
        CellNode.L_WaveContainer.L_WaveButton.L_Select.Instance.Visible = false;
    }
}