using Godot;

namespace UI.MapEditorCreateMark;

public partial class MapEditorCreateMarkPanel : MapEditorCreateMark
{

    public override void OnCreateUi()
    {
        S_AddMark.Instance.Pressed += OnAddMark;
    }

    public override void OnDestroyUi()
    {
        
    }

    private void OnAddMark()
    {
        EditorWindowManager.ShowSelectObject("选择物体", this);
    }
}
