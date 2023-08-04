using Godot;

namespace UI.MapEditorMapMark;

public partial class MapEditorMapMarkPanel : MapEditorMapMark
{

    private UiGrid<WaveTemplate, object> _grid;
    
    public override void OnCreateUi()
    {
        _grid = new UiGrid<WaveTemplate, object>(S_WaveTemplate, typeof(EditorMarkWaveCell));
        _grid.SetCellOffset(new Vector2I(0, 10));
        _grid.SetColumns(1);
        
        _grid.SetDataList(new object[] { 1, 2, 3, 4});
    }

    public override void OnDestroyUi()
    {
        _grid.Destroy();
    }

}
