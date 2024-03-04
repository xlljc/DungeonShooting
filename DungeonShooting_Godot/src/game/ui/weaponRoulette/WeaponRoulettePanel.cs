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
    
    //是否展开轮盘
    private bool _pressRouletteFlag = false;
    private bool _isMagnifyRoulette = false;
    //所有武器插槽
    private List<WeaponSlotNode> _slotNodes = new List<WeaponSlotNode>();

    public override void OnCreateUi()
    {
        S_RouletteBg.Instance.Visible = false;
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
            clone.L_Control.Instance.RotationDegrees = -angle;
            clone.L_Control.L_WeaponIcon.Instance.Material = (Material)S_WeaponSlotNode.L_Control.L_WeaponIcon.Instance.Material.Duplicate();
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
            if (UiManager.GetUiInstanceCount(UiManager.UiNames.PauseMenu) == 0)
            {
                ExpandRoulette();
            }
        }
        else if (!InputManager.Roulette && _isMagnifyRoulette) //缩小轮盘
        {
            ShrinkRoulette();
        }

        
        if (InputManager.Roulette)
        {
            S_MouseArea.Instance.GlobalPosition = GetGlobalMousePosition();
        }
    }
    
    private void ExpandRoulette()
    {
        World.Current.Pause = true;
        _pressRouletteFlag = true;
        _isMagnifyRoulette = true;
        
        S_RouletteBg.Instance.Visible = true;
        S_Bg.Instance.Visible = true;
        SetEnableSectorCollision(true);
        RefreshWeapon();
    }
    
    private void ShrinkRoulette()
    {
        S_RouletteBg.Instance.Visible = false;
        S_Bg.Instance.Visible = false;
        
        _isMagnifyRoulette = false;
        World.Current.Pause = false;
        SetEnableSectorCollision(false);
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
    
    //更新显示的武器
    private void RefreshWeapon()
    {
        var current = Player.Current;
        if (current == null)
        {
            foreach (var slotNode in _slotNodes)
            {
                slotNode.L_Control.Instance.Visible = false;
            }

            return;
        }

        var weapons = current.WeaponPack.ItemSlot;
        for (var i = 0; i < _slotNodes.Count; i++)
        {
            var slotNode = _slotNodes[i];
            slotNode.L_Control.Instance.Visible = true;
            if (weapons.Length > i)
            {
                var weapon = weapons[i];
                if (weapon != null)
                {
                    slotNode.L_Control.Instance.Visible = true;
                    slotNode.L_Control.L_WeaponIcon.Instance.Texture = weapon.GetDefaultTexture();
                    slotNode.L_Control.L_AmmoLabel.Instance.Text = 
                        (weapon.CurrAmmo + weapon.ResidueAmmo).ToString() + "/" + weapon.Attribute.MaxAmmoCapacity;
                }
                else
                {
                    slotNode.L_Control.Instance.Visible = false;
                }
            }
            else
            {
                slotNode.L_Control.Instance.Visible = false;
            }
        }
    }
}
