using System;
using System.Collections.Generic;
using Config;

namespace UI.MapEditorCreateMark;

public class MarkObjectCell : UiCell<MapEditorCreateMark.MarkObject, MarkInfoItem>
{
    //是否展开
    private bool _isExpand = false;
    private MapEditorCreateMark.ExpandPanel _expandPanel;
    private List<AttributeBase> _attributeBases;
    private ExcelConfig.ActivityObject _activityObject;
    
    public override void OnInit()
    {
        CellNode.L_VBoxContainer.L_HBoxContainer.L_ExpandButton.Instance.Pressed += OnExpandClick;
        CellNode.L_VBoxContainer.L_HBoxContainer.L_CenterContainer.L_DeleteButton.Instance.Pressed += OnDeleteClick;
    }

    public override void OnSetData(MarkInfoItem data)
    {
        //记得判断随机对象, 后面再做
        

        _activityObject = ExcelConfig.ActivityObject_Map[data.Id];
        if (string.IsNullOrEmpty(_activityObject.Icon))
        {
            CellNode.L_VBoxContainer.L_HBoxContainer.L_Icon.Instance.Visible = false;
        }
        else
        {
            CellNode.L_VBoxContainer.L_HBoxContainer.L_Icon.Instance.Visible = true;
            CellNode.L_VBoxContainer.L_HBoxContainer.L_Icon.Instance.Texture = ResourceManager.LoadTexture2D(_activityObject.Icon);
        }
        //物体Id
        CellNode.L_VBoxContainer.L_HBoxContainer.L_IdLabel.Instance.Text = data.Id;
        //物体名称
        CellNode.L_VBoxContainer.L_HBoxContainer.L_NameLabel.Instance.Text = _activityObject.Name;
        //物体类型
        CellNode.L_VBoxContainer.L_HBoxContainer.L_TypeLabel.Instance.Text = NameManager.GetActivityTypeName(_activityObject.Type);
        
        // 包含额外属性
        if (_activityObject.Type == 5 || _activityObject.Type == 4)
        {
            if (_expandPanel == null)
            {
                CreateExpandPanel(_activityObject, data);
            }
        }
    }

    public override void OnDisable()
    {
        if (_expandPanel != null)
        {
            _attributeBases.Clear();
            _attributeBases = null;
            _expandPanel.QueueFree();
            _expandPanel = null;
        }

        SetExpandState(false);
    }

    /// <summary>
    /// 获取标记数据对象
    /// </summary>
    public MarkInfoItem GetMarkInfoItem()
    {
        var markInfoItem = Data;
        if (_activityObject.Type == 5 || _activityObject.Type == 4)
        {
            markInfoItem.Attr = new Dictionary<string, string>();
            foreach (var attributeBase in _attributeBases)
            {
                if (attributeBase.Visible)
                {
                    markInfoItem.Attr.Add(attributeBase.AttrName, attributeBase.GetAttributeValue());
                }
            }
        }
        return markInfoItem;
    }

    //点击删除按钮
    private void OnDeleteClick()
    {
        Grid.RemoveByIndex(Index);
    }

    //点击展开按钮
    private void OnExpandClick()
    {
        //展开图标
        SetExpandState(!_isExpand);
    }

    //设置展开状态
    private void SetExpandState(bool flag)
    {
        _isExpand = flag;
        if (_isExpand)
        {
            CellNode.L_VBoxContainer.L_HBoxContainer.L_ExpandButton.Instance.Icon =
                ResourceManager.LoadTexture2D(ResourcePath.resource_sprite_ui_commonIcon_Down_png);
        }
        else
        {
            CellNode.L_VBoxContainer.L_HBoxContainer.L_ExpandButton.Instance.Icon =
                ResourceManager.LoadTexture2D(ResourcePath.resource_sprite_ui_commonIcon_Right_png);
        }

        if (_expandPanel != null)
        {
            _expandPanel.Instance.Visible = _isExpand;
        }
    }

    private void CreateExpandPanel(ExcelConfig.ActivityObject activityObject, MarkInfoItem markInfoItem)
    {
        if (_expandPanel != null)
        {
            throw new Exception("已经创建过ExpandPanel, 不能重复创建!");
        }
        
        _expandPanel = CellNode.UiPanel.S_ExpandPanel.Clone();
        _expandPanel.Instance.Visible = _isExpand;
        CellNode.L_VBoxContainer.AddChild(_expandPanel);

        if (activityObject.Type == 5) //武器类型
        {
            var numberBar = CellNode.UiPanel.CreateNumberBar("CurrAmmon", "弹夹弹药量：");
            var numberBar2 = CellNode.UiPanel.CreateNumberBar("ResidueAmmo", "剩余弹药量：");
            _expandPanel.L_ExpandGrid.AddChild(numberBar);
            _expandPanel.L_ExpandGrid.AddChild(numberBar2);
            _attributeBases = new List<AttributeBase>();
            _attributeBases.Add(numberBar.Instance);
            _attributeBases.Add(numberBar2.Instance);
            
            if (markInfoItem != null) //初始化数据
            {
                numberBar.L_NumInput.Instance.MinValue = 0;
                numberBar2.L_NumInput.Instance.MinValue = 0;
                //武器配置数据
                var weapon = ExcelConfig.Weapon_List.Find(weapon => weapon.WeaponId == activityObject.Id);
                if (weapon != null)
                {
                    numberBar.L_NumInput.Instance.MaxValue = weapon.AmmoCapacity; //弹夹上限
                    numberBar2.L_NumInput.Instance.MaxValue = weapon.MaxAmmoCapacity; //容量上限
                }

                if (markInfoItem.Attr != null)
                {
                    if (markInfoItem.Attr.TryGetValue("CurrAmmon", out var currAmmon)) //弹夹弹药量
                    {
                        numberBar.L_NumInput.Instance.Value = float.Parse(currAmmon);
                    }
                    if (markInfoItem.Attr.TryGetValue("ResidueAmmo", out var residueAmmo)) //剩余弹药量
                    {
                        numberBar2.L_NumInput.Instance.Value = float.Parse(residueAmmo);
                    }
                }
                else
                {
                    numberBar.L_NumInput.Instance.Value = numberBar.L_NumInput.Instance.MaxValue;
                    numberBar2.L_NumInput.Instance.Value = (int)(numberBar2.L_NumInput.Instance.MaxValue / 2);
                }
            }
        }
        else if (activityObject.Type == 4) //敌人
        {
            var weaponBar = CellNode.UiPanel.CreateObjectBar("Weapon", "携带武器：", ActivityType.Weapon);
            var numberBar2 = CellNode.UiPanel.CreateNumberBar("CurrAmmon", "弹夹弹药量：");
            var numberBar3 = CellNode.UiPanel.CreateNumberBar("ResidueAmmo", "剩余弹药量：");
            weaponBar.Instance.SetRelevancyAttr(numberBar2, numberBar3);
            _expandPanel.L_ExpandGrid.AddChild(weaponBar);
            _expandPanel.L_ExpandGrid.AddChild(numberBar2);
            _expandPanel.L_ExpandGrid.AddChild(numberBar3);
            _attributeBases = new List<AttributeBase>();
            _attributeBases.Add(weaponBar.Instance);
            _attributeBases.Add(numberBar2.Instance);
            _attributeBases.Add(numberBar3.Instance);
            
            if (markInfoItem != null) //初始化数据
            {
                numberBar2.L_NumInput.Instance.MinValue = 0;
                numberBar3.L_NumInput.Instance.MinValue = 0;
                //武器配置数据
                var weapon = ExcelConfig.Weapon_List.Find(weapon => weapon.WeaponId == activityObject.Id);
                if (weapon != null)
                {
                    numberBar2.L_NumInput.Instance.MaxValue = weapon.AmmoCapacity; //弹夹上限
                    numberBar3.L_NumInput.Instance.MaxValue = weapon.MaxAmmoCapacity; //容量上限
                }
                
                if (markInfoItem.Attr != null)
                {
                    if (markInfoItem.Attr.TryGetValue("Weapon", out var weaponId)) //武器
                    {
                        weaponBar.Instance.SelectWeapon(ExcelConfig.Weapon_List.Find(w => w.WeaponId == weaponId));
                    }
                    if (markInfoItem.Attr.TryGetValue("CurrAmmon", out var currAmmon)) //弹夹弹药量
                    {
                        numberBar2.L_NumInput.Instance.Value = float.Parse(currAmmon);
                    }
                    if (markInfoItem.Attr.TryGetValue("ResidueAmmo", out var residueAmmo)) //剩余弹药量
                    {
                        numberBar3.L_NumInput.Instance.Value = float.Parse(residueAmmo);
                    }
                }
                else
                {
                    numberBar2.L_NumInput.Instance.Value = numberBar2.L_NumInput.Instance.MaxValue;
                    numberBar3.L_NumInput.Instance.Value = (int)(numberBar3.L_NumInput.Instance.MaxValue / 2);
                }
            }
        }
    }
}