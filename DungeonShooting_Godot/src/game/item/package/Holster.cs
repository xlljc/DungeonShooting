using System;
using System.Collections.Generic;
using Godot;

/// <summary>
/// 角色身上的武器袋, 存储角色携带的武器
/// </summary>
public class Holster
{
    /// <summary>
    /// 插槽类
    /// </summary>
    public class WeaponSlot
    {
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enable = false;
        /// <summary>
        /// 当前插槽存放的武器类型
        /// </summary>
        public WeaponWeightType Type = WeaponWeightType.MainWeapon;
        /// <summary>
        /// 插槽存放的武器
        /// </summary>
        public Weapon Weapon;
    }

    /// <summary>
    /// 归属者
    /// </summary>
    public Role Master { get; }
    
    //public Weapon HandheldWeapon { get; private set; }
    
    /// <summary>
    /// 当前使用的武器对象
    /// </summary>
    public Weapon ActiveWeapon { get; private set; }

    /// <summary>
    /// 当前使用的武器的索引
    /// </summary>
    public int ActiveIndex { get; private set; } = 0;

    /// <summary>
    /// 武器插槽
    /// </summary>
    public WeaponSlot[] SlotList { get; } = new WeaponSlot[4];

    public Holster(Role master)
    {
        Master = master;

        //创建武器的插槽, 默认前两个都是启用的
        WeaponSlot slot1 = new WeaponSlot();
        slot1.Enable = true;
        SlotList[0] = slot1;

        WeaponSlot slot2 = new WeaponSlot();
        slot2.Enable = true;
        slot2.Type = WeaponWeightType.DeputyWeapon;
        SlotList[1] = slot2;

        WeaponSlot slot3 = new WeaponSlot();
        SlotList[2] = slot3;

        WeaponSlot slot4 = new WeaponSlot();
        slot4.Type = WeaponWeightType.DeputyWeapon;
        SlotList[3] = slot4;
    }

    /// <summary>
    /// 返回当前武器袋是否还有空位
    /// </summary>
    public bool HasVacancy()
    {
        for (int i = 0; i < SlotList.Length; i++)
        {
            var item = SlotList[i];
            if (item.Enable && item.Weapon == null)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 根据索引获取武器
    /// </summary>
    public Weapon GetWeapon(int index)
    {
        if (index < 0 || index >= SlotList.Length)
        {
            return null;
        }
        return SlotList[index].Weapon;
    }

    /// <summary>
    /// 根据武器id查找武器袋中该武器所在的位置, 如果没有, 则返回 -1
    /// </summary>
    /// <param name="id">武器id</param>
    public int FindWeapon(string id)
    {
        for (int i = 0; i < SlotList.Length; i++)
        {
            var item = SlotList[i];
            if (item.Weapon != null && item.Weapon.TypeId == id)
            {
                return i;
            }
        }
        return -1;
    }

    /// <summary>
    /// 通过回调函数查询武器在武器袋中的位置, 如果没有, 则返回 -1
    /// </summary>
    public int FindWeapon(Func<Weapon, int, bool> handler)
    {
        for (int i = 0; i < SlotList.Length; i++)
        {
            var item = SlotList[i];
            if (item.Weapon != null && handler(item.Weapon, i))
            {
                return i;
            }
        }
        return -1;
    }

    /// <summary>
    /// 遍历所有武器
    /// </summary>
    public void ForEach(Action<Weapon, int> handler)
    {
        for (int i = 0; i < SlotList.Length; i++)
        {
            var item = SlotList[i];
            if (item.Weapon != null)
            {
                handler(item.Weapon, i);
            }
        }
    }

    /// <summary>
    /// 从武器袋中移除所有武器, 并返回
    /// </summary>
    public Weapon[] GetAndClearWeapon()
    {
        List<Weapon> weapons = new List<Weapon>();
        for (int i = 0; i < SlotList.Length; i++)
        {
            var slot = SlotList[i];
            var weapon = slot.Weapon;
            if (weapon != null)
            {
                weapon.GetParent().RemoveChild(weapon);
                weapon.RemoveAt();
                weapons.Add(weapon);
                slot.Weapon = null;
            }
        }

        return weapons.ToArray();
    }

    /// <summary>
    /// 返回是否能放入武器
    /// </summary>
    /// <param name="weapon">武器对象</param>
    public bool CanPickupWeapon(Weapon weapon)
    {
        for (int i = 0; i < SlotList.Length; i++)
        {
            var item = SlotList[i];
            if (item.Enable && weapon.Attribute.WeightType == item.Type && item.Weapon == null)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 拾起武器, 存入武器袋中, 返回存放在武器袋的位置, 如果容不下这把武器, 则会返回 -1
    /// </summary>
    /// <param name="weapon">武器对象</param>
    /// <param name="exchange">是否立即切换到该武器, 默认 true </param>
    public int PickupWeapon(Weapon weapon, bool exchange = true)
    {
        //已经被拾起了
        if (weapon.Master != null)
        {
            return -1;
        }
        for (int i = 0; i < SlotList.Length; i++)
        {
            var item = SlotList[i];
            if (item.Enable && weapon.Attribute.WeightType == item.Type && item.Weapon == null)
            {
                weapon.Pickup();
                item.Weapon = weapon;
                weapon.PickUpWeapon(Master);
                if (exchange)
                {
                    ExchangeByIndex(i);
                }

                return i;
            }
        }
        return -1;
    }

    /// <summary>
    /// 移除指定位置的武器, 并返回这个武器对象, 如果移除正在使用的这把武器, 则会自动切换到上一把武器
    /// </summary>
    /// <param name="index">所在武器袋的位置索引</param>
    public Weapon RemoveWeapon(int index)
    {
        if (index < 0 || index >= SlotList.Length)
        {
            return null;
        }
        var slot = SlotList[index];
        if (slot.Weapon == null)
        {
            return null;
        }
        var weapon = slot.Weapon;
        weapon.GetParent().RemoveChild(weapon);
        slot.Weapon = null;

        //如果是当前手持的武器, 就需要调用切换武器操作
        if (index == ActiveIndex)
        {
            //没有其他武器了
            if (ExchangePrev() == index)
            {
                ActiveIndex = 0;
                ActiveWeapon = null;
            }
        }
        weapon.RemoveAt();
        return weapon;
    }

    /// <summary>
    /// 切换到上一个武器
    /// </summary>
    public int ExchangePrev()
    {
        var index = ActiveIndex - 1;
        do
        {
            if (index < 0)
            {
                index = SlotList.Length - 1;
            }
            if (ExchangeByIndex(index))
            {
                return index;
            }
        } while (index-- != ActiveIndex);
        return -1;
    }

    /// <summary>
    /// 切换到下一个武器, 
    /// </summary>
    public int ExchangeNext()
    {
        var index = ActiveIndex + 1;
        do
        {
            if (index >= SlotList.Length)
            {
                index = 0;
            }
            if (ExchangeByIndex(index))
            {
                return index;
            }
        }
        while (index++ != ActiveIndex);
        return -1;
    }

    /// <summary>
    /// 切换到指定索引的武器
    /// </summary>
    public bool ExchangeByIndex(int index)
    {
        if (index == ActiveIndex && ActiveWeapon != null) return true;
        if (index < 0 || index > SlotList.Length) return false;
        var slot = SlotList[index];
        if (slot == null || slot.Weapon == null) return false;

        //将上一把武器放到背后
        if (ActiveWeapon != null)
        {
            var tempParent = ActiveWeapon.GetParentOrNull<Node2D>();
            if (tempParent != null)
            {
                tempParent.RemoveChild(ActiveWeapon);
                Master.BackMountPoint.AddChild(ActiveWeapon);
                if (ActiveIndex == 0)
                {
                    ActiveWeapon.Position = new Vector2(0, 5);
                    ActiveWeapon.RotationDegrees = 50;
                    ActiveWeapon.Scale = new Vector2(-1, 1);
                }
                else if (ActiveIndex == 1)
                {
                    ActiveWeapon.Position = new Vector2(0, 0);
                    ActiveWeapon.RotationDegrees = 120;
                    ActiveWeapon.Scale = new Vector2(1, -1);
                }
                else if (ActiveIndex == 2)
                {
                    ActiveWeapon.Position = new Vector2(0, 5);
                    ActiveWeapon.RotationDegrees = 310;
                    ActiveWeapon.Scale = new Vector2(1, 1);
                }
                else if (ActiveIndex == 3)
                {
                    ActiveWeapon.Position = new Vector2(0, 0);
                    ActiveWeapon.RotationDegrees = 60;
                    ActiveWeapon.Scale = new Vector2(1, 1);
                }
                ActiveWeapon.Conceal();
            }
        }

        //更改父节点
        var parent = slot.Weapon.GetParentOrNull<Node>();
        if (parent == null)
        {
            Master.MountPoint.AddChild(slot.Weapon);
        }
        else if (parent != Master.MountPoint)
        {
            parent.RemoveChild(slot.Weapon);
            Master.MountPoint.AddChild(slot.Weapon);
        }

        slot.Weapon.Position = Vector2.Zero;
        slot.Weapon.Scale = Vector2.One;
        slot.Weapon.RotationDegrees = 0;
        ActiveWeapon = slot.Weapon;
        ActiveIndex = index;
        ActiveWeapon.Active();
        return true;
    }
}
