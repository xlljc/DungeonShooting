using System.Linq;
using Config;
using Godot;

namespace UI.Encyclopedia;

public partial class EncyclopediaPanel : Encyclopedia
{
    //tab网格
    private UiGrid<TabButton, TabData> _tab;
    //item网格
    private UiGrid<ObjectButton, ExcelConfig.ActivityBase> _grid;
    private long _id;

    public override void OnCreateUi()
    {
        _tab = CreateUiGrid<TabButton, TabData, TabCell>(S_TabButton);
        _tab.SetColumns(10);
        _tab.SetCellOffset(new Vector2I(0, 0));
        _tab.Add(new TabData(ResourcePath.resource_sprite_ui_encyclopedia_TabIcon1_png, ActivityType.Weapon));
        _tab.Add(new TabData(ResourcePath.resource_sprite_ui_encyclopedia_TabIcon1_png, ActivityType.Prop));
        _tab.Add(new TabData(ResourcePath.resource_sprite_ui_encyclopedia_TabIcon1_png, ActivityType.Enemy));
        
        _grid = CreateUiGrid<ObjectButton, ExcelConfig.ActivityBase, ItemCell>(S_ObjectButton);
        _grid.SetHorizontalExpand(true);
        _grid.SetAutoColumns(true);
        _grid.SetCellOffset(new Vector2I(10, 10));

        _tab.SelectIndex = 0;
    }

    public override void OnDestroyUi()
    {
        
    }
    
    /// <summary>
    /// 设置选中的tab
    /// </summary>
    public void SelectTab(ActivityType type)
    {
        StopCoroutine(_id);
        _id = StartCoroutine(
            _grid.SetDataListCoroutine(
                ExcelConfig.ActivityBase_List.Where(data => data.Type == type).ToArray()
            )
        );

        SelectItem(null);
    }

    /// <summary>
    /// 设置选中的物体
    /// </summary>
    public void SelectItem(ExcelConfig.ActivityBase config)
    {
        if (config != null)
        {
            S_ItemName.Instance.Text = config.Name;
            S_ItemTexture.Instance.Texture = ResourceManager.LoadTexture2D(config.Icon);
            S_ItemDes.Instance.Text = config.Intro;
            //S_ItemDes.Instance.Text = config.Details;
        }
        else
        {
            S_ItemName.Instance.Text = null;
            S_ItemTexture.Instance.Texture = null;
            S_ItemDes.Instance.Text = null;
        }
    }

}
