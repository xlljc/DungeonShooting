using Godot;

namespace UI.MapEditorCreateRoom;

public partial class MapEditorCreateRoomPanel : MapEditorCreateRoom
{

    public override void OnCreateUi()
    {
        //S_ConfirmationDialog.Instance.Title = "创建地牢房间";
    }

    public override void OnShowUi()
    {
        //S_ConfirmationDialog.Instance.VisibilityChanged += OnVisibilityChanged;
    }

    public override void OnHideUi()
    {
        //S_ConfirmationDialog.Instance.VisibilityChanged -= OnVisibilityChanged;
    }

    public override void OnDisposeUi()
    {
        
    }

    private void OnVisibilityChanged()
    {
        //if (!S_ConfirmationDialog.Instance.Visible)
        {
            GD.Print("关闭UI");
            DisposeUi();
        }
    }
}
