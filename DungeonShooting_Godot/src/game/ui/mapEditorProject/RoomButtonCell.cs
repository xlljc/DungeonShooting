using Godot;

namespace UI.MapEditorProject;

public class RoomButtonCell : UiCell<MapEditorProject.RoomButton, DungeonRoomSplit>
{
    public override void OnInit()
    {
        CellNode.L_SelectTexture.Instance.Visible = false;
    }
    
    public override void OnSetData(DungeonRoomSplit data)
    {
        CellNode.L_RoomName.Instance.Text = data.RoomInfo.RoomName;
        CellNode.L_RoomType.Instance.Text = DungeonManager.DungeonRoomTypeToDescribeString(data.RoomInfo.RoomType);
        
        //预览图
        CellNode.L_PreviewImage.Instance.Texture = data.PreviewImage;
        
        //提示
        var tipText = "权重: " + data.RoomInfo.Weight;
        
        //错误消息
        if (data.ErrorType == RoomErrorType.None)
        {
            CellNode.L_ErrorTexture.Instance.Visible = false;
        }
        else
        {
            CellNode.L_ErrorTexture.Instance.Visible = true;
            tipText += "\n错误: " + EditorTileMapManager.GetRoomErrorTypeMessage(data.ErrorType);
        }
        
        if (!string.IsNullOrEmpty(data.RoomInfo.Remark))
        {
            tipText += "\n备注: " + data.RoomInfo.Remark;
        }
        CellNode.Instance.TooltipText = tipText;
    }

    public override void OnDisable()
    {
        CellNode.L_PreviewImage.Instance.Texture = null;
    }

    public override void OnDoubleClick()
    {
        //打开房间编辑器
        CellNode.UiPanel.OpenSelectRoom(Data);
    }
    
    public override void OnSelect()
    {
        EditorTileMapManager.SetSelectRoom(Data);
        CellNode.L_SelectTexture.Instance.Visible = true;
    }

    public override void OnUnSelect()
    {
        CellNode.L_SelectTexture.Instance.Visible = false;
    }
}