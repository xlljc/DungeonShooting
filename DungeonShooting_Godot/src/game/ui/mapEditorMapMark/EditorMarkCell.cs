using System;
using Config;
using Godot;
using UI.MapEditor;

namespace UI.MapEditorMapMark;

public class EditorMarkCell : UiCell<MapEditorMapMark.MarkItem, MapEditorMapMarkPanel.MarkCellData>
{
    //上一次点击的时间
    private long _prevClickTime2 = -1;
    
    public override void OnInit()
    {
        //这里不绑定 Click 函数, 而是绑定 OnClickHandler, 因为 Select 交给 MapEditorMapMarkPanel 处理了
        CellNode.L_MarkButton.Instance.Pressed += OnClickHandler;
    }

    public override void OnSetData(MapEditorMapMarkPanel.MarkCellData data)
    {
        var textureRect = CellNode.L_MarkButton.L_MarkIcon.Instance;
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

                str += PreinstallMarkManager.GetMarkConfig(markInfoItem.Id).Name;
            }
            text += str;
        }
        else
        {
            if (data.MarkInfo.SpecialMarkType != SpecialMarkType.Normal)
            {
                text = PreinstallMarkManager.GetSpecialName(data.MarkInfo.SpecialMarkType);
            }
            else
            {
                text = "空";
            }
        }

        //延时时间
        if (data.Preloading)
        {
            text += "\n提前加载";
        }
        else
        {
            text += "\n" + data.MarkInfo.DelayTime + "秒";
        }
        
        //显示文本
        CellNode.L_MarkButton.Instance.Text = text;
        //显示图标
        textureRect.Texture = ResourceManager.GetMarkIcon(data.MarkInfo);
    }

    public void OnClickHandler()
    {
        EditorTileMapManager.SetSelectWaveIndex(Data.ParentCell.Index);
        CellNode.UiPanel.SetSelectCell(this, CellNode.Instance, MapEditorMapMarkPanel.SelectToolType.Mark);
        //选中标记
        EditorTileMapManager.SetSelectMark(Data.MarkInfo);
        
        //双击判定
        if (_prevClickTime2 >= 0)
        {
            var now = DateTime.Now.Ticks / 10000;
            if (now <= _prevClickTime2 + 500)
            {
                OnDoubleClickHandler();
            }

            _prevClickTime2 = now;
        }
        else
        {
            _prevClickTime2 = DateTime.Now.Ticks / 10000;
        }
    }

    public void OnDoubleClickHandler()
    {
        //双击聚焦标记
        var position = Data.MarkInfo.Position.AsVector2();
        CellNode.UiPanel.EditorTileMap.SetLookPosition(position);
    }

    public override void OnSelect()
    {
        CellNode.L_MarkButton.L_Select.Instance.Visible = true;
        //选中标记
        EditorTileMapManager.SetSelectMark(Data.MarkInfo);
    }

    public override void OnUnSelect()
    {
        CellNode.L_MarkButton.L_Select.Instance.Visible = false;
    }

    public override int OnSort(UiCell<MapEditorMapMark.MarkItem, MapEditorMapMarkPanel.MarkCellData> other)
    {
        if (!Data.Preloading && other.Data.Preloading)
        {
            return 0;
        }
        else if (Data.Preloading)
        {
            return -1;
        }
        else if (other.Data.Preloading)
        {
            return 1;
        }
        return (int)(Data.MarkInfo.DelayTime * 1000 - other.Data.MarkInfo.DelayTime * 1000);
    }
}