using Config;

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
        if (data.MarkInfo.MarkList != null)
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

        text += "\n" + data.MarkInfo.DelayTime + "秒";
        CellNode.L_MarkButton.Instance.Text = text;
    }

    public override void OnClick()
    {
        CellNode.UiPanel.SetSelectCell(this, CellNode.Instance, MapEditorMapMarkPanel.SelectToolType.Mark);
    }

    public override void OnSelect()
    {
        CellNode.L_MarkButton.L_Select.Instance.Visible = true;
        //选中标记
        EventManager.EmitEvent(EventEnum.OnSelectMark, Data);
    }

    public override void OnUnSelect()
    {
        CellNode.L_MarkButton.L_Select.Instance.Visible = false;
    }
}