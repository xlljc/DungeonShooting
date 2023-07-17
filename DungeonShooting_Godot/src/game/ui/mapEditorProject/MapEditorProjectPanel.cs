using Godot;

namespace UI.MapEditorProject;

public partial class MapEditorProjectPanel : MapEditorProject
{

    private UiGrid<GroupButton, string> _groupGrid;

    public override void OnCreateUi()
    {
        _groupGrid = new UiGrid<GroupButton, string>(S_GroupButton, typeof(GroupButtonCell), 1, 0, 10);
        _groupGrid.SetHorizontalExpand(true);
        _groupGrid.SetDataList(new []{ "1", "2", "3", "4", "5", "1", "2", "3", "4", "5", "1", "2", "3", "4", "5", "1", "2", "3", "4", "5", "1", "2", "3", "4", "5", "1", "2", "3", "4", "5" });
    }

    public override void OnShowUi()
    {
        
    }

    public override void OnHideUi()
    {
        
    }

    public override void OnDisposeUi()
    {
        _groupGrid.Destroy();
        _groupGrid = null;
    }
}
