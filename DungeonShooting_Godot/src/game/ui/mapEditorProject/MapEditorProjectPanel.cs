using Godot;

namespace UI.MapEditorProject;

public partial class MapEditorProjectPanel : MapEditorProject
{

    private UiGrid<GroupButton, string> _groupGrid;
    private UiGrid<RoomButton, string> _roomGrid;

    public override void OnCreateUi()
    {
        _groupGrid = new UiGrid<GroupButton, string>(S_GroupButton, typeof(GroupButtonCell), 1, 0, 2);
        _groupGrid.SetHorizontalExpand(true);
        _groupGrid.SetDataList(new []{ "1", "2", "3", "4", "5", "1", "2", "3", "4", "5", "1", "2", "3", "4", "5", "1", "2", "3", "4", "5", "1", "2", "3", "4", "5", "1", "2", "3", "4", "5" });

        _roomGrid = new UiGrid<RoomButton, string>(S_RoomButton, typeof(RoomButtonCell), 5, 5, 5);
        _roomGrid.SetHorizontalExpand(true);
        _roomGrid.SetDataList(new []{ "1", "2", "3", "4", "5", "1", "2", "3", "4", "5", "1", "2", "3", "4", "5", "1", "2", "3", "4", "5", "1", "2", "3", "4", "5", "1", "2", "3", "4", "5" });
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
        
        _roomGrid.Destroy();
        _roomGrid = null;
    }
}
