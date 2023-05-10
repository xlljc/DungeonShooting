using System;
using System.Collections.Generic;
using Godot;

/// <summary>
/// 角色身上的武器袋, 存储角色携带的武器
/// </summary>
public class Holster
{
    /// <summary>
    /// 归属者
    /// </summary>
    public Role Master { get; }

    /// <summary>
    /// 当前使用的武器对象
    /// </summary>
    public Weapon ActiveWeapon { get; private set; }

    /// <summary>
    /// 当前使用的武器的索引
    /// </summary>
    public int ActiveIndex { get; private set; } = 0;

    /// <summary>
    /// 武器袋容量
    /// </summary>
    public int Capacity { get; private set; } = 0;

    /// <summary>
    /// 武器插槽
    /// </summary>
    public Weapon[] Weapons { get; private set;  }

    public Holster(Role master)
    {
        Master = master;
        //默认容量4
        SetCapacity(4);
    }

    /// <summary>
    /// 修改武器袋容量
    /// </summary>
    public void SetCapacity(int capacity)
    {
        if (capacity < 0)
        {
            capacity = 0;
        }

        if (capacity == Capacity)
        {
            return;
        }

        if (Weapons == null)
        {
            Weapons = new Weapon[capacity];
        }
        else if (Weapons.Length > capacity) //删减格子
        {
            var newArray = new Weapon[capacity];
            for (var i = 0; i < Weapons.Length; i++)
            {
                if (i < capacity)
                {
                    newArray[i] = Weapons[i];
                }
                else
                {
                    Master.ThrowWeapon(i);
                }
            }

            Weapons = newArray;
        }
        else //添加格子
        {
            var newArray = new Weapon[capacity];
            for (var i = 0; i < Weapons.Length; i++)
            {
                newArray[i] = Weapons[i];
            }
            Weapons = newArray;
        }
        Capacity = capacity;
        
    }

    /// <summary>
    /// 返回当前武器袋是否是空的
    /// </summary>
    public bool IsEmpty()
    {
        for (var i = 0; i < Weapons.Length; i++)
        {
            if (Weapons[i] != null)
            {
                return false;
            }
        }

        return true;
    }
    
    /// <summary>
    /// 返回当前武器袋是否还有空位
    /// </summary>
    public bool HasVacancy()
    {
        for (var i = 0; i < Weapons.Length; i++)
        {
            if (Weapons[i] == null)
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
        if (index < 0 || index >= Weapons.Length)
        {
            return null;
        }
        return Weapons[index];
    }

    /// <summary>
    /// 根据武器id查找武器袋中该武器所在的位置, 如果没有, 则返回 -1
    /// </summary>
    /// <param name="id">武器id</param>
    public int FindWeapon(string id)
    {
        for (var i = 0; i < Weapons.Length; i++)
        {
            var item = Weapons[i];
            if (item != null && item.ItemId == id)
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
        for (var i = 0; i < Weapons.Length; i++)
        {
            var item = Weapons[i];
            if (item != null && handler(item, i))
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
        for (var i = 0; i < Weapons.Length; i++)
        {
            var item = Weapons[i];
            if (item != null)
            {
                handler(item, i);
            }
        }
    }

    /// <summary>
    /// 从武器袋中移除所有武器, 并返回
    /// </summary>
    public Weapon[] GetAndClearWeapon()
    {
        var weapons = new List<Weapon>();
        for (var i = 0; i < Weapons.Length; i++)
        {
            var weapon = Weapons[i];
            if (weapon != null)
            {
                weapon.GetParent().RemoveChild(weapon);
                weapon.RemoveAt();
                weapons.Add(weapon);
                Weapons[i] = null;
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
        for (var i = 0; i < Weapons.Length; i++)
        {
            var item = Weapons[i];
            if (item == null)
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
        for (var i = 0; i < Weapons.Length; i++)
        {
            var item = Weapons[i];
            if (item == null)
            {
                weapon.Pickup();
                Weapons[i] = weapon;
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
        if (index < 0 || index >= Weapons.Length)
        {
            return null;
        }
        var weapon = Weapons[index];
        if (weapon == null)
        {
            return null;
        }
        weapon.GetParent().RemoveChild(weapon);
        Weapons[index] = null;

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
                index = Weapons.Length - 1;
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
            if (index >= Weapons.Length)
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
        if (index < 0 || index > Weapons.Length) return false;
        var weapon = Weapons[index];
        if (weapon == null) return false;

        //将上一把武器放到背后
        if (ActiveWeapon != null)
        {
            var tempParent = ActiveWeapon.GetParentOrNull<Node2D>();
            if (tempParent != null)
            {
                tempParent.RemoveChild(ActiveWeapon);
                Master.BackMountPoint.AddChild(ActiveWeapon);
                Master.OnPutBackMount(ActiveWeapon, ActiveIndex);
                ActiveWeapon.Conceal();
            }
        }

        //更改父节点
        var parent = weapon.GetParentOrNull<Node>();
        if (parent == null)
        {
            Master.MountPoint.AddChild(weapon);
        }
        else if (parent != Master.MountPoint)
        {
            parent.RemoveChild(weapon);
            Master.MountPoint.AddChild(weapon);
        }

        weapon.Position = Vector2.Zero;
        weapon.Scale = Vector2.One;
        weapon.RotationDegrees = 0;
        weapon.Visible = true;
        ActiveWeapon = weapon;
        ActiveIndex = index;
        ActiveWeapon.Active();
        return true;
    }
}
