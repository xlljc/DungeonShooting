using Config;
using UI.MapEditor;

namespace UI.MapEditorMapMark;

public class EditorMarkCell : UiCell<MapEditorMapMark.MarkItem, MapEditorMapMarkPanel.MarkCellData>
{
    public override void OnInit()
    {
        CellNode.L_MarkButton.Instance.Pressed += OnClick;
    }

    public override void OnSetData(MapEditorMapMarkPanel.MarkCellData data)
    {
        var text = "";
        //物体名称
        if (data.MarkInfo.MarkList != null && data.MarkInfo.MarkList.Count > 0)
        {
            var str = "";
            for (var i = 0; i < data.MarkInfo.MarkList.Count; i++)
            {
                var markInfoItem = data.MarkInfo.MarkList[i];
                if (i > 0)
                {
                    str += "，";
                }

                str += ExcelConfig.ActivityObject_Map[markInfoItem.Id].Name;
            }
            text += str;
        }
        else
        {
            text += "空";
        }

        //延时时间
        if (data.MarkInfo.Preloading)
        {
            text += "\n提前加载";
        }
        else
        {
            text += "\n" + data.MarkInfo.DelayTime + "秒";
        }
        
        CellNode.L_MarkButton.Instance.Text = text;
    }

    public override void OnClick()
    {
        CellNode.UiPanel.EditorTileMap.SelectWaveIndex = Data.ParentCell.Index;
        CellNode.UiPanel.SetSelectCell(this, CellNode.Instance, MapEditorMapMarkPanel.SelectToolType.Mark);
        //需要切换回编辑工具
        if (CellNode.UiPanel.EditorTileMap.MouseType != EditorTileMap.MouseButtonType.Edit)
        {
            //选中标记
            EventManager.EmitEvent(EventEnum.OnSelectMark, Data.MarkInfo);
        }
    }

    public override void OnSelect()
    {
        CellNode.L_MarkButton.L_Select.Instance.Visible = true;
        //选中标记
        EventManager.EmitEvent(EventEnum.OnSelectMark, Data.MarkInfo);
    }

    public override void OnUnSelect()
    {
        CellNode.L_MarkButton.L_Select.Instance.Visible = false;
    }

    public override int OnSort(UiCell<MapEditorMapMark.MarkItem, MapEditorMapMarkPanel.MarkCellData> other)
    {
        if (!Data.MarkInfo.Preloading && other.Data.MarkInfo.Preloading)
        {
            return 0;
        }
        else if (Data.MarkInfo.Preloading)
        {
            return -1;
        }
        else if (other.Data.MarkInfo.Preloading)
        {
            return 1;
        }
        return (int)(Data.MarkInfo.DelayTime * 1000 - other.Data.MarkInfo.DelayTime * 1000);
    }
}