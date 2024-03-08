using System.Collections.Generic;
using Godot;

namespace UI.WeaponRoulette;

/// <summary>
/// 武器轮盘
/// </summary>
public partial class WeaponRoulettePanel : WeaponRoulette
{
    /// <summary>
    /// 武器槽数量
    /// </summary>
    public const int SlotCount = 6;
    
    /// <summary>
    /// 选中的武器
    /// </summary>
    public Weapon ActiveWeapon;    
    
    //是否展开轮盘
    private bool _pressRouletteFlag = false;
    private bool _isMagnifyRoulette = false;
    //所有武器插槽
    private List<WeaponSlotNode> _slotNodes = new List<WeaponSlotNode>();

    //当前页索引
    private int _pageIndex = 0;
    //最大页数
    private int _maxPageIndex = 0;
    
    public override void OnCreateUi()
    {
        L_Control.Instance.Visible = false;
        S_Bg.Instance.Visible = false;

        //创建武器插槽
        for (var i = 0; i < SlotCount; i++)
        {
            var angle = i * (360f / SlotCount);
            var clone = S_WeaponSlotNode.CloneAndPut();
            var collisionPolygon2D = clone.L_SlotAreaNode.L_CollisionPolygon2D.Instance;
            var sectorPolygon = Utils.CreateSectorPolygon(0, 100, 360f / SlotCount, 4);
            collisionPolygon2D.Polygon = sectorPolygon;
            clone.Instance.RotationDegrees = angle;
            clone.L_SlotUi.Instance.RotationDegrees = -angle;
            clone.L_SlotUi.L_WeaponUi.L_WeaponIcon.Instance.Material =
                (Material)S_WeaponSlotNode.L_SlotUi.L_WeaponUi.L_WeaponIcon.Instance.Material.Duplicate();
            _slotNodes.Add(clone);
        }
        
        S_WeaponSlotNode.QueueFree();

        SetEnableSectorCollision(false);
    }

    public override void OnDestroyUi()
    {
        
    }

    public override void Process(float delta)
    {
        if (!InputManager.Roulette)
        {
            _pressRouletteFlag = false;
        }

        //按下地图按键
        if (InputManager.Roulette && !_isMagnifyRoulette) //打开轮盘
        {
            if (UiManager.GetUiInstanceCount(UiManager.UiNames.PauseMenu) == 0 && !InputManager.Map)
            {
                ExpandRoulette();
            }
        }
        else if (!InputManager.Roulette && _isMagnifyRoulette) //关闭轮盘
        {
            ShrinkRoulette();
        }

        //已经打开地图
        if (InputManager.Roulette)
        {
            S_MouseArea.Instance.GlobalPosition = GetGlobalMousePosition();

            if (_maxPageIndex > 0)
            {
                if (InputManager.ExchangeWeapon) //上一页
                {
                    _pageIndex--;
                    if (_pageIndex < 0)
                    {
                        _pageIndex = _maxPageIndex;
                    }
                    
                    RefreshPageLabel();
                    RefreshWeapon();
                }
                else if (InputManager.Interactive) //下一页
                {
                    _pageIndex++;
                    if (_pageIndex > _maxPageIndex)
                    {
                        _pageIndex = 0;
                    }
                    
                    RefreshPageLabel();
                    RefreshWeapon();
                }
            }
        }
        else
        {
            ActiveWeapon = null;
        }
    }
    
    //打开轮盘
    private void ExpandRoulette()
    {
        World.Current.Pause = true;
        _pressRouletteFlag = true;
        _isMagnifyRoulette = true;
        
        L_Control.Instance.Visible = true;
        S_Bg.Instance.Visible = true;
        SetEnableSectorCollision(true);
        RefreshSlotPage();
        RefreshPageLabel();
        RefreshWeapon();
    }
    
    //关闭轮盘
    private void ShrinkRoulette()
    {
        L_Control.Instance.Visible = false;
        S_Bg.Instance.Visible = false;
        
        _isMagnifyRoulette = false;
        World.Current.Pause = false;
        SetEnableSectorCollision(false);

        //如果选中了物体
        if (ActiveWeapon != null)
        {
            Player.Current.ExchangeWeaponByIndex(ActiveWeapon.PackageIndex);
        }
    }
    
    //设置是否启用扇形碰撞检测
    private void SetEnableSectorCollision(bool enable)
    {
        S_MouseArea.Instance.Monitorable = enable;
        foreach (var weaponSlotNode in _slotNodes)
        {
            weaponSlotNode.L_SlotAreaNode.Instance.Monitorable = enable;
        }
    }

    //刷新页码文本
    private void RefreshPageLabel()
    {
        S_PageLabel.Instance.Text = $"{_pageIndex + 1}/{_maxPageIndex + 1}";
    }
    
    //刷新页码
    private void RefreshSlotPage()
    {
        var current = Player.Current;
        if (current == null)
        {
            return;
        }

        var weapons = current.WeaponPack.ItemSlot;
        //判断是否显示上一页下一页提示
        var lastIndex = 0;
        for (var i = weapons.Length - 1; i >= 0; i--)
        {
            if (weapons[i] != null)
            {
                lastIndex = i;
                break;
            }
        }
        _maxPageIndex = Mathf.CeilToInt((lastIndex + 1f) / SlotCount) - 1;
        S_ColorRect.Instance.Visible = _maxPageIndex > 0;
        
        if (_pageIndex > _maxPageIndex)
        {
            _pageIndex = _maxPageIndex;
        }
    }
    
    //更新显示的武器
    private void RefreshWeapon()
    {
        var current = Player.Current;
        if (current == null) //没有玩家对象，这是异常情况
        {
            foreach (var slotNode in _slotNodes)
            {
                slotNode.L_SlotUi.Instance.Visible = false;
            }

            return;
        }

        var weapons = current.WeaponPack.ItemSlot;
        for (var i = 0; i < _slotNodes.Count; i++)
        {
            var slotNode = _slotNodes[i];
            slotNode.L_SlotUi.Instance.Visible = true;
            slotNode.L_SlotUi.L_LockSprite.Instance.Visible = false;
            
            var weaponIndex = i + _pageIndex * SlotCount;
            if (weapons.Length > weaponIndex)
            {
                var weapon = weapons[weaponIndex];
                if (weapon != null) //有武器
                {
                    slotNode.L_SlotUi.L_WeaponUi.Instance.Visible = true;
                    slotNode.L_SlotUi.L_WeaponUi.L_WeaponIcon.Instance.Texture = weapon.GetDefaultTexture();
                    slotNode.L_SlotUi.L_WeaponUi.L_AmmoLabel.Instance.Text = 
                        (weapon.CurrAmmo + weapon.ResidueAmmo).ToString() + "/" + weapon.Attribute.MaxAmmoCapacity;
                    slotNode.Instance.SetWeapon(weapon);
                    slotNode.L_SlotAreaNode.Instance.Monitoring = true;
                }
                else //已经解锁，但是没有武器
                {
                    slotNode.L_SlotUi.L_WeaponUi.Instance.Visible = false;
                    slotNode.L_SlotAreaNode.Instance.Monitoring = false;
                    slotNode.Instance.ClearWeapon();
                }
            }
            else //未解锁
            {
                slotNode.L_SlotUi.L_LockSprite.Instance.Visible = true;
                slotNode.L_SlotUi.L_WeaponUi.Instance.Visible = false;
                slotNode.L_SlotAreaNode.Instance.Monitoring = false;
                slotNode.Instance.ClearWeapon();
            }
        }
    }
}
