using Godot;

namespace UI.MapEditorCreateRoom;

public partial class MapEditorCreateRoomPanel : MapEditorCreateRoom
{

    public override void OnCreateUi()
    {
        
    }

    public override void OnShowUi()
    {
        
    }

    public override void OnHideUi()
    {
        
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
