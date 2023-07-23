using Godot;

namespace UI.MapEditorTools;

public partial class MapEditorToolsPanel : MapEditorTools
{
    public override void OnCreateUi()
    {
        S_DoorToolTemplate.Instance.SetDoorDragAreaNode(S_DoorToolTemplate);
    }

    public override void OnShowUi()
    {
        S_PenTool.Instance.EmitSignal(BaseButton.SignalName.Pressed);
    }

    public override void OnHideUi()
    {
        
    }

    public void SetDoorToolTransform(Vector2 pos, Vector2 scale)
    {
        S_DoorToolTemplate.Instance.SetDoorAreaTransform(pos, scale);
    }

}
