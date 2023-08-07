using System.Collections.Generic;
using Godot;

namespace UI.MapEditorMapMark;

public class EditorWaveCell : UiCell<MapEditorMapMark.WaveItem, List<MarkInfo>>
{
    private UiGrid<MapEditorMapMark.MarkItem, MarkInfo> _grid;
    
    public override void OnInit()
    {
        CellNode.L_WaveContainer.L_WaveButton.Instance.Pressed += OnClick;
        CellNode.L_WaveContainer.L_TextureButton.Instance.Pressed += OnExpandOrClose;
        CellNode.L_MarginContainer.L_AddMarkButton.Instance.Pressed += OnAddMark;

        CellNode.L_MarkContainer.L_MarkItem.Instance.SetHorizontalExpand(true);
        _grid = new UiGrid<MapEditorMapMark.MarkItem, MarkInfo>(CellNode.L_MarkContainer.L_MarkItem, typeof(EditorMarkCell));
        _grid.SetColumns(1);
        _grid.SetHorizontalExpand(true);
        _grid.SetCellOffset(new Vector2I(0, 5));
    }

    public override void OnSetData(List<MarkInfo> data)
    {
        _grid.SetDataList(data.ToArray());
    }

    public override void OnRefreshIndex()
    {
        CellNode.L_WaveContainer.L_WaveButton.Instance.Text = $"第{Index + 1}波";
    }

    public override void OnDestroy()
    {
        _grid.Destroy();
    }

    private void OnAddMark()
    {
        EditorWindowManager.ShowCreateMark();
        // var info = new MarkInfo();
        // Data.Add(info);
        // _grid.Add(info);
    }
    
    //展开/收起按钮点击
    private void OnExpandOrClose()
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
            textureButton.TextureNormal = ResourceManager.LoadTexture2D(ResourcePath.resource_sprite_ui_commonIcon_Left_png);
        }
    }

    public override void OnClick()
    {
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