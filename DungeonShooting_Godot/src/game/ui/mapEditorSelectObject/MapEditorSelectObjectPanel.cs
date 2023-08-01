using System.Linq;
using Config;
using Godot;

namespace UI.MapEditorSelectObject;

public partial class MapEditorSelectObjectPanel : MapEditorSelectObject
{

    private UiGrid<ObjectButton, ExcelConfig.ActivityObject> _grid;

    public override void OnCreateUi()
    {
        _grid = new UiGrid<ObjectButton, ExcelConfig.ActivityObject>(S_ObjectButton, typeof(ObjectButtonCell));
        _grid.SetAutoColumns(true);
        _grid.SetHorizontalExpand(true);
        
        _grid.SetDataList(ExcelConfig.ActivityObject_List.Where(o =>
        {
            return o.Type == (int)ActivityIdPrefix.ActivityPrefixType.Weapon;
        }).ToArray());
    }

    public override void OnDestroyUi()
    {
        
    }

}
