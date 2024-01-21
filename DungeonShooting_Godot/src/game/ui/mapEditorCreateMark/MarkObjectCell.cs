using System;
using System.Collections.Generic;
using Config;

namespace UI.MapEditorCreateMark;

public class MarkObjectCell : UiCell<MapEditorCreateMark.MarkObject, MarkInfoItem>
{
    //是否展开
    private bool _isExpand = false;
    private MapEditorCreateMark.ExpandPanel _expandPanel;
    //自定义额外属性
    private List<AttributeBase> _attributeBases;
    private ExcelConfig.ActivityBase _activityObject;

    private MapEditorCreateMark.NumberBar _altitude;
    private MapEditorCreateMark.NumberBar _vSpeed;
    
    public override void OnInit()
    {
        CellNode.L_VBoxContainer.L_HBoxContainer.L_ExpandButton.Instance.Pressed += OnExpandClick;
        CellNode.L_VBoxContainer.L_HBoxContainer.L_CenterContainer.L_DeleteButton.Instance.Pressed += OnDeleteClick;
    }

    public override void OnSetData(MarkInfoItem data)
    {
        //物体Id
        CellNode.L_VBoxContainer.L_HBoxContainer.L_IdLabel.Instance.Text = data.Id;
        //权重
        CellNode.L_VBoxContainer.L_HBoxContainer.L_WeightEdit.Instance.Value = data.Weight;
        
        if (data.SpecialMarkType == SpecialMarkType.BirthPoint) //出生标记
        {
            //物体名称
            CellNode.L_VBoxContainer.L_HBoxContainer.L_NameLabel.Instance.Text = PreinstallMarkManager.GetSpecialName(data.SpecialMarkType);
            //物体类型
            CellNode.L_VBoxContainer.L_HBoxContainer.L_TypeLabel.Instance.Text = ActivityId.GetTypeName(ActivityType.Player);
            
            //图标
            CellNode.L_VBoxContainer.L_HBoxContainer.L_Icon.Instance.Visible = true;
            CellNode.L_VBoxContainer.L_HBoxContainer.L_Icon.Instance.Texture = ResourceManager.LoadTexture2D(ResourcePath.resource_sprite_ui_commonIcon_BirthMark_png);
            
            CellNode.L_VBoxContainer.L_HBoxContainer.L_CenterContainer.L_DeleteButton.Instance.Visible = false;
            CellNode.L_VBoxContainer.L_HBoxContainer.L_WeightEdit.Instance.Visible = false;
        }
        else //普通标记
        {
            //记得判断随机对象, 后面再做
            _activityObject = PreinstallMarkManager.GetMarkConfig(data.Id);
            //物体名称
            CellNode.L_VBoxContainer.L_HBoxContainer.L_NameLabel.Instance.Text = _activityObject.Name;
            //物体类型
            CellNode.L_VBoxContainer.L_HBoxContainer.L_TypeLabel.Instance.Text = NameManager.GetActivityTypeName(_activityObject.Type);
            
            //图标
            if (string.IsNullOrEmpty(_activityObject.Icon))
            {
                CellNode.L_VBoxContainer.L_HBoxContainer.L_Icon.Instance.Visible = false;
            }
            else
            {
                CellNode.L_VBoxContainer.L_HBoxContainer.L_Icon.Instance.Visible = true;
                CellNode.L_VBoxContainer.L_HBoxContainer.L_Icon.Instance.Texture = ResourceManager.LoadTexture2D(_activityObject.Icon);
            }
            
            // 包含额外属性
            if (_expandPanel == null)
            {
                CreateExpandPanel(_activityObject, data);
            }
            CellNode.L_VBoxContainer.L_HBoxContainer.L_CenterContainer.L_DeleteButton.Instance.Visible = true;
            CellNode.L_VBoxContainer.L_HBoxContainer.L_WeightEdit.Instance.Visible = true;
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
            _altitude = null;
            _vSpeed = null;
        }

        SetExpandState(false);
    }

    /// <summary>
    /// 获取标记数据对象
    /// </summary>
    public MarkInfoItem GetMarkInfoItem()
    {
        var markInfoItem = Data;
        
        //额外属性
        if (_attributeBases != null)
        {
            markInfoItem.Attr = null;
            foreach (var attributeBase in _attributeBases)
            {
                if (attributeBase.Visible)
                {
                    if (attributeBase.AttrName == "VSpeed" || attributeBase.AttrName == "Altitude") //不能是公共属性
                    {
                        continue;
                    }

                    if (markInfoItem.Attr == null)
                    {
                        markInfoItem.Attr = new Dictionary<string, string>();
                    }
                    markInfoItem.Attr.Add(attributeBase.AttrName, attributeBase.GetAttributeValue());
                }
            }
        }


        if (Data.SpecialMarkType == SpecialMarkType.Normal)
        {
            //权重
            markInfoItem.Weight = (int)CellNode.L_VBoxContainer.L_HBoxContainer.L_WeightEdit.Instance.Value;
        }

        //海拔高度
        if (_altitude != null && _altitude.Instance.Visible)
        {
            markInfoItem.Altitude = (int)_altitude.L_NumInput.Instance.Value;
        }
        //纵轴速度
        if (_vSpeed != null && _vSpeed.Instance.Visible)//海拔高度
        {
            markInfoItem.VerticalSpeed = (float)_vSpeed.L_NumInput.Instance.Value;
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

    private void CreateExpandPanel(ExcelConfig.ActivityBase activityObject, MarkInfoItem markInfoItem)
    {
        if (_expandPanel != null)
        {
            throw new Exception("已经创建过ExpandPanel, 不能重复创建!");
        }
        
        _expandPanel = CellNode.UiPanel.S_ExpandPanel.Clone();
        _expandPanel.Instance.Visible = _isExpand;
        CellNode.L_VBoxContainer.AddChild(_expandPanel);
        
        //公有类型
        _altitude = CellNode.UiPanel.CreateNumberBar("Altitude", "初始纵轴高度：");
        _vSpeed = CellNode.UiPanel.CreateNumberBar("VSpeed", "初始纵轴速度：");
        _altitude.L_NumInput.Instance.MaxValue = 128;
        _altitude.L_NumInput.Instance.MinValue = 0;
        _altitude.L_NumInput.Instance.Step = 1;
        _vSpeed.L_NumInput.Instance.MaxValue = 1000;
        _vSpeed.L_NumInput.Instance.MinValue = -1000;
        _vSpeed.L_NumInput.Instance.Step = 0.1;
        _expandPanel.L_ExpandGrid.AddChild(_altitude);
        _expandPanel.L_ExpandGrid.AddChild(_vSpeed);

        if (markInfoItem != null)
        {
            //海拔高度
            _altitude.L_NumInput.Instance.Value = markInfoItem.Altitude;
            //纵轴速度
            _vSpeed.L_NumInput.Instance.Value = markInfoItem.VerticalSpeed;
        }
        
        if (activityObject.Type == (int)ActivityType.Weapon) //武器类型
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
                var weapon = Weapon.GetWeaponAttribute(activityObject.Id);
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
        else if (activityObject.Type == (int)ActivityType.Enemy) //敌人
        {
            var faceBar = CellNode.UiPanel.CreateOptionBar("Face", "脸朝向：");
            faceBar.Instance.AddItem("随机", 0);
            faceBar.Instance.AddItem("左", (int)FaceDirection.Left);
            faceBar.Instance.AddItem("右", (int)FaceDirection.Right);
            var weaponBar = CellNode.UiPanel.CreateObjectBar("Weapon", "携带武器：", ActivityType.Weapon);
            var numberBar2 = CellNode.UiPanel.CreateNumberBar("CurrAmmon", "弹夹弹药量：");
            var numberBar3 = CellNode.UiPanel.CreateNumberBar("ResidueAmmo", "剩余弹药量：");
            weaponBar.Instance.SetRelevancyAttr(numberBar2, numberBar3);
            _expandPanel.L_ExpandGrid.AddChild(faceBar);
            _expandPanel.L_ExpandGrid.AddChild(weaponBar);
            _expandPanel.L_ExpandGrid.AddChild(numberBar2);
            _expandPanel.L_ExpandGrid.AddChild(numberBar3);
            _attributeBases = new List<AttributeBase>();
            _attributeBases.Add(faceBar.Instance);
            _attributeBases.Add(weaponBar.Instance);
            _attributeBases.Add(numberBar2.Instance);
            _attributeBases.Add(numberBar3.Instance);
            
            if (markInfoItem != null) //初始化数据
            {
                numberBar2.L_NumInput.Instance.MinValue = 0;
                numberBar3.L_NumInput.Instance.MinValue = 0;
                
                if (markInfoItem.Attr != null)
                {
                    if (markInfoItem.Attr.TryGetValue("Face", out var face)) //朝向
                    {
                        faceBar.Instance.SetSelectItem(int.Parse(face));
                    }
                    if (markInfoItem.Attr.TryGetValue("Weapon", out var weaponId)) //武器
                    {
                        weaponBar.Instance.SelectWeapon(Weapon.GetWeaponAttribute(weaponId));
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
                    faceBar.Instance.SetSelectItem(0);
                    numberBar2.L_NumInput.Instance.Value = numberBar2.L_NumInput.Instance.MaxValue;
                    numberBar3.L_NumInput.Instance.Value = (int)(numberBar3.L_NumInput.Instance.MaxValue / 2);
                }
            }
        }
    }
}