using Godot;

namespace UI.MapEditorTools;

public partial class MapEditorToolsPanel : MapEditorTools
{

    public override void OnShowUi()
    {
        S_PenTool.Instance.EmitSignal(BaseButton.SignalName.Pressed);
    }

    public override void OnHideUi()
    {
        
    }

}
