using Config;

namespace UI.MapEditorCreateMark;

public partial class ObjectAttribute : AttributeBase
{
    /// <summary>
    /// 可选择的物体类型
    /// </summary>
    public ActivityType ActivityType { get; set; }
    private MapEditorCreateMark.ObjectBar _objectBar;
    //选择的武器数据
    private ExcelConfig.Weapon _selectWeapon;
    //关联属性
    private MapEditorCreateMark.NumberBar _currAmmonAttr;
    private MapEditorCreateMark.NumberBar _residueAmmoAttr;

    public override void SetUiNode(IUiNode uiNode)
    {
        _objectBar = (MapEditorCreateMark.ObjectBar)uiNode;
        _objectBar.L_HBoxContainer.L_SelectButton.Instance.Pressed += OnClickEdit;
        _objectBar.L_HBoxContainer.L_DeleteButton.Instance.Pressed += OnClickDelete;
    }

    public override string GetAttributeValue()
    {
        if (_selectWeapon == null)
        {
            return null;
        }
        return _selectWeapon.WeaponId;
    }

    //点击编辑按钮
    private void OnClickEdit()
    {
        EditorWindowManager.ShowSelectObject(ActivityType.Weapon, OnSelectObject, _objectBar.UiPanel);
    }

    //点击删除按钮
    private void OnClickDelete()
    {
        SelectWeapon(null);
    }

    private void OnSelectObject(ExcelConfig.ActivityObject activityObject)
    {
        var weapon = ExcelConfig.Weapon_List.Find(weapon => weapon.WeaponId == activityObject.Id);
        if (weapon != null)
        {
            SelectWeapon(weapon);
        }
    }

    /// <summary>
    /// 设置选择的武器物体
    /// </summary>
    public void SelectWeapon(ExcelConfig.Weapon weapon)
    {
        if (weapon == null)
        {
            _objectBar.L_HBoxContainer.L_DeleteButton.Instance.Visible = false;
            _selectWeapon = null;
            //隐藏关联属性
            _currAmmonAttr.Instance.Visible = false;
            _residueAmmoAttr.Instance.Visible = false;
            _objectBar.L_HBoxContainer.L_ObjectIcon.Instance.Visible = false;
            _objectBar.L_HBoxContainer.L_ObjectName.Instance.Text = "<未选择>";
        }
        else
        {
            _objectBar.L_HBoxContainer.L_DeleteButton.Instance.Visible = true;
            _selectWeapon = weapon;
            var o = ExcelConfig.ActivityObject_Map[weapon.WeaponId];
            //显示关联属性
            _currAmmonAttr.Instance.Visible = true;
            _residueAmmoAttr.Instance.Visible = true;
            //显示数据
            _objectBar.L_HBoxContainer.L_ObjectName.Instance.Text = o.Name;
            _objectBar.L_HBoxContainer.L_ObjectIcon.Instance.Visible = true;
            _objectBar.L_HBoxContainer.L_ObjectIcon.Instance.Texture = ResourceManager.LoadTexture2D(o.Icon);
            //弹药
            _currAmmonAttr.L_NumInput.Instance.MaxValue = weapon.AmmoCapacity;
            _residueAmmoAttr.L_NumInput.Instance.MaxValue = weapon.MaxAmmoCapacity;
        }
    }

    /// <summary>
    /// 设置关联的属性
    /// </summary>
    public void SetRelevancyAttr(MapEditorCreateMark.NumberBar currAmmonAttr, MapEditorCreateMark.NumberBar residueAmmoAttr)
    {
        _currAmmonAttr = currAmmonAttr;
        _residueAmmoAttr = residueAmmoAttr;
        currAmmonAttr.Instance.Visible = false;
        residueAmmoAttr.Instance.Visible = false;
    }
}