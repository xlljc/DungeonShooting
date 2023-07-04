using System;
using System.Collections.Generic;

public class Package<T> where T : ActivityObject, IPackageItem
{
    /// <summary>
    /// 归属者
    /// </summary>
    public Role Master { get; }

    /// <summary>
    /// 当前使用的物体对象
    /// </summary>
    public T ActiveItem { get; private set; }

    /// <summary>
    /// 当前使用的物体的索引
    /// </summary>
    public int ActiveIndex { get; private set; } = 0;

    /// <summary>
    /// 物体袋容量
    /// </summary>
    public int Capacity { get; private set; } = 0;

    /// <summary>
    /// 物体插槽
    /// </summary>
    public T[] ItemSlot { get; private set; }

    public Package(Role master)
    {
        Master = master;
        //默认容量4
        SetCapacity(4);
    }

    /// <summary>
    /// 修改物体袋容量
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

        if (ItemSlot == null)
        {
            ItemSlot = new T[capacity];
        }
        else if (ItemSlot.Length > capacity) //删减格子
        {
            var newArray = new T[capacity];
            for (var i = 0; i < ItemSlot.Length; i++)
            {
                var packageItem = ItemSlot[i];
                if (i < capacity)
                {
                    newArray[i] = packageItem;
                }
                else
                {
                    //溢出的item
                    packageItem.OnOverflowItem();
                    packageItem.Master = null;
                    packageItem.PackageIndex = -1;
                }
            }

            ItemSlot = newArray;
        }
        else //添加格子
        {
            var newArray = new T[capacity];
            for (var i = 0; i < ItemSlot.Length; i++)
            {
                newArray[i] = ItemSlot[i];
            }
            ItemSlot = newArray;
        }
        Capacity = capacity;
        
    }

    /// <summary>
    /// 返回当前物体袋是否是空的
    /// </summary>
    public bool IsEmpty()
    {
        for (var i = 0; i < ItemSlot.Length; i++)
        {
            if (ItemSlot[i] != null)
            {
                return false;
            }
        }

        return true;
    }
    
    /// <summary>
    /// 返回当前物体袋是否还有空位
    /// </summary>
    public bool HasVacancy()
    {
        for (var i = 0; i < ItemSlot.Length; i++)
        {
            if (ItemSlot[i] == null)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 根据索引获取物体
    /// </summary>
    public T GetItem(int index)
    {
        if (index < 0 || index >= ItemSlot.Length)
        {
            return null;
        }
        return ItemSlot[index];
    }

    /// <summary>
    /// 根据物体id查找物体袋中该物体所在的位置, 如果没有, 则返回 -1
    /// </summary>
    /// <param name="id">物体id</param>
    public int FindItem(string id)
    {
        for (var i = 0; i < ItemSlot.Length; i++)
        {
            var item = ItemSlot[i];
            if (item != null && item.ItemConfig.Id == id)
            {
                return i;
            }
        }
        return -1;
    }

    /// <summary>
    /// 通过回调函数查询物体在物体袋中的位置, 如果没有, 则返回 -1
    /// </summary>
    public int FindItem(Func<T, int, bool> handler)
    {
        for (var i = 0; i < ItemSlot.Length; i++)
        {
            var item = ItemSlot[i];
            if (item != null && handler(item, i))
            {
                return i;
            }
        }
        return -1;
    }

    /// <summary>
    /// 遍历所有物体
    /// </summary>
    public void ForEach(Action<T, int> handler)
    {
        for (var i = 0; i < ItemSlot.Length; i++)
        {
            var item = ItemSlot[i];
            if (item != null)
            {
                handler(item, i);
            }
        }
    }

    /// <summary>
    /// 从物体袋中移除所有物体, 并返回
    /// </summary>
    public T[] GetAndClearItem()
    {
        var items = new List<T>();
        for (var i = 0; i < ItemSlot.Length; i++)
        {
            var item = ItemSlot[i];
            if (item != null)
            {
                //触发移除物体
                item.OnRemoveItem();
                item.Master = null;
                item.PackageIndex = -1;

                items.Add(item);
                ItemSlot[i] = null;
            }
        }

        return items.ToArray();
    }

    /// <summary>
    /// 返回是否能放入物体
    /// </summary>
    /// <param name="item">物体对象</param>
    public bool CanPickupItem(T item)
    {
        for (var i = 0; i < ItemSlot.Length; i++)
        {
            var tempItem = ItemSlot[i];
            if (tempItem == null)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 拾起物体, 存入物体袋中, 返回存放在物体袋的位置, 如果容不下这个物体, 则会返回 -1
    /// </summary>
    /// <param name="item">物体对象</param>
    /// <param name="exchange">是否立即切换到该物体, 默认 true </param>
    public int PickupItem(T item, bool exchange = true)
    {
        //已经被拾起了
        if (item.Master != null)
        {
            return -1;
        }
        for (var i = 0; i < ItemSlot.Length; i++)
        {
            var tempItem = ItemSlot[i];
            if (tempItem == null)
            {
                ItemSlot[i] = item;
                item.Master = Master;
                item.PackageIndex = i;
                item.OnPickUpItem();
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
    /// 移除指定位置的物体, 并返回这个物体对象, 如果移除正在使用的这个物体, 则会自动切换到上一个物体
    /// </summary>
    /// <param name="index">所在物体袋的位置索引</param>
    public T RemoveItem(int index)
    {
        if (index < 0 || index >= ItemSlot.Length)
        {
            return null;
        }
        var item = ItemSlot[index];
        if (item == null)
        {
            return null;
        }
        ItemSlot[index] = null;

        //如果是当前手持的物体, 就需要调用切换物体操作
        if (index == ActiveIndex)
        {
            //没有其他物体了
            if (ExchangePrev() == index)
            {
                ActiveIndex = 0;
                ActiveItem = null;
            }
        }
        //移除物体
        item.OnRemoveItem();
        item.Master = null;
        item.PackageIndex = -1;
        // item.GetParent().RemoveChild(item);
        // item.Remove();
        return item;
    }

    /// <summary>
    /// 切换到上一个物体
    /// </summary>
    public int ExchangePrev()
    {
        var index = ActiveIndex - 1;
        do
        {
            if (index < 0)
            {
                index = ItemSlot.Length - 1;
            }
            if (ExchangeByIndex(index))
            {
                return index;
            }
        } while (index-- != ActiveIndex);
        return -1;
    }

    /// <summary>
    /// 切换到下一个物体, 
    /// </summary>
    public int ExchangeNext()
    {
        var index = ActiveIndex + 1;
        do
        {
            if (index >= ItemSlot.Length)
            {
                index = 0;
            }
            if (ExchangeByIndex(index))
            {
                return index;
            }
        } while (index++ != ActiveIndex);
        return -1;
    }

    /// <summary>
    /// 切换到指定索引的物体
    /// </summary>
    public bool ExchangeByIndex(int index)
    {
        if (index == ActiveIndex && ActiveItem != null) return true;
        if (index < 0 || index > ItemSlot.Length) return false;
        var item = ItemSlot[index];
        if (item == null) return false;

        //将上一个物体放到背后
        if (ActiveItem != null)
        {
            //收起物体
            ActiveItem.OnConcealItem();
        }

        //切换物体
        ActiveItem = item;
        ActiveIndex = index;
        ActiveItem.OnActiveItem();
        return true;
    }
}