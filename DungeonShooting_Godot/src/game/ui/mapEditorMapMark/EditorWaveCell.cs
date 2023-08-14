using System.Collections.Generic;
using Godot;
using UI.MapEditor;

namespace UI.MapEditorMapMark;

public class EditorWaveCell : UiCell<MapEditorMapMark.WaveItem, List<MarkInfo>>
{
    public UiGrid<MapEditorMapMark.MarkItem, MarkInfo> MarkGrid;
    
    public override void OnInit()
    {
        //这里不绑定 Click 函数, 而是绑定 OnClick, 因为 Select 交给 MapEditorMapMarkPanel 处理了
        CellNode.L_WaveContainer.L_WaveButton.Instance.Pressed += OnClick;
        CellNode.L_WaveContainer.L_TextureButton.Instance.Pressed += OnExpandOrClose;
        CellNode.L_MarginContainer.L_AddMarkButton.Instance.Pressed += OnAddMark;

        CellNode.L_MarkContainer.L_MarkItem.Instance.SetHorizontalExpand(true);
        MarkGrid = new UiGrid<MapEditorMapMark.MarkItem, MarkInfo>(CellNode.L_MarkContainer.L_MarkItem, typeof(EditorMarkCell));
        MarkGrid.SetColumns(1);
        MarkGrid.SetHorizontalExpand(true);
        MarkGrid.SetCellOffset(new Vector2I(0, 5));
    }

    public override void OnSetData(List<MarkInfo> data)
    {
        MarkGrid.SetDataList(data.ToArray());
    }

    public override void OnRefreshIndex()
    {
        CellNode.L_WaveContainer.L_WaveButton.Instance.Text = $"第{Index + 1}波";
    }

    public override void OnDestroy()
    {
        MarkGrid.Destroy();
    }

    //添加标记
    private void OnAddMark()
    {
        //打开添加标记页面
        EditorWindowManager.ShowCreateMark(OnCreateMarkInfo);
    }   

    //创建的标记完成
    private void OnCreateMarkInfo(MarkInfo markInfo)
    {
        var preinstall = CellNode.UiPanel.GetSelectPreinstall();
        preinstall.WaveList[Index].Add(markInfo);
        MarkGrid.Add(markInfo);
        //添加标记工具
        EventManager.EmitEvent(EventEnum.OnCreateMark, markInfo);
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
    /// 是否展开
    /// </summary>
    public bool IsExpand()
    {
        return CellNode.L_MarkContainer.Instance.Visible;
    }

    public override void OnClick()
    {
        CellNode.UiPanel.EditorTileMap.SelectWaveIndex = Index;
        CellNode.UiPanel.SetSelectCell(this, CellNode.L_WaveContainer.Instance, MapEditorMapMarkPanel.SelectToolType.Wave);
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