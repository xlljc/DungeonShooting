using System.Linq;
using Config;
using Godot;

namespace UI.Encyclopedia;

public partial class EncyclopediaPanel : Encyclopedia
{

    private UiGrid<ObjectButton, ExcelConfig.ActivityBase> _grid;
    private long _id;

    public override void OnCreateUi()
    {
        _grid = CreateUiGrid<ObjectButton, ExcelConfig.ActivityBase, ItemCell>(S_ObjectButton);
        _grid.SetHorizontalExpand(true);
        _grid.SetAutoColumns(true);
        _grid.SetCellOffset(new Vector2I(10, 10));
        ShowWeaponItem();
    }

    public override void OnDestroyUi()
    {
        
    }

    private void ShowWeaponItem()
    {
        StopCoroutine(_id);
        _id = StartCoroutine(
            _grid.SetDataListCoroutine(
                ExcelConfig.ActivityBase_List.Where(data => data.Type == ActivityType.Weapon).ToArray()
            )
        );
    }

}
