using Godot;

/// <summary>
/// 角色身上的枪套, 存储角色携带的武器
/// </summary>
public class Holster
{
    /// <summary>
    /// 插槽类
    /// </summary>
    public class GunSlot
    {
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enable = false;
        /// <summary>
        /// 当前插槽存放的武器类型
        /// </summary>
        public GunWeightType Type = GunWeightType.MainWeapon;
        /// <summary>
        /// 插槽存放的武器
        /// </summary>
        public Gun Gun;
    }

    /// <summary>
    /// 归属者
    /// </summary>
    public Role Master { get; }

    public Gun ActiveGun { get; private set; }
    public int ActiveIndex { get; private set; }

    public GunSlot[] SlotList { get; } = new GunSlot[4];

    public Holster(Role master)
    {
        Master = master;

        //创建枪的插槽, 默认前两个都是启用的
        GunSlot slot1 = new GunSlot();
        slot1.Enable = true;
        SlotList[0] = slot1;

        GunSlot slot2 = new GunSlot();
        slot2.Enable = true;
        slot2.Type = GunWeightType.DeputyWeapon;
        SlotList[1] = slot2;

        GunSlot slot3 = new GunSlot();
        SlotList[2] = slot3;

        GunSlot slot4 = new GunSlot();
        slot4.Type = GunWeightType.DeputyWeapon;
        SlotList[3] = slot4;
    }

    /// <summary>
    /// 拾起武器, 存入枪套中, 返回存放在枪套的位置, 如果容不下这把武器, 则会返回 -1
    /// </summary>
    /// <param name="gun">武器对象</param>
    public int PickupGun(Gun gun)
    {
        for (int i = 0; i < SlotList.Length; i++)
        {
            var item = SlotList[i];
            if (item.Enable && gun.Attribute.WeightType == item.Type && item.Gun == null)
            {
                item.Gun = gun;
                ActiveGun = gun;
                ActiveIndex = i;
                return i;
            }
        }
        GD.PrintErr("存入武器失败!");
        return -1;
    }

    /// <summary>
    /// 切换到上一个武器
    /// </summary>
    public int ExchangePrev()
    {
        return 0;
    }

    /// <summary>
    /// 切换到下一个武器
    /// </summary>
    public int ExchangeNext()
    {
        var index = ActiveIndex + 1;
        while (index != ActiveIndex)
        {
            if (index >= SlotList.Length)
            {
                index = 0;
            }
            var slot = SlotList[index];
            if (slot != null && slot.Gun != null)
            {
                //更改父节点
                var parent = slot.Gun.GetParentOrNull<Node>();
                if (parent == null)
                {
                    Master.MountPoint.AddChild(slot.Gun);
                }
                else if (parent != Master.MountPoint)
                {
                    parent.RemoveChild(slot.Gun);
                    Master.MountPoint.AddChild(slot.Gun);
                }

                ActiveGun = slot.Gun;
                ActiveIndex = index;
                return index;
            }
            index++;
        }
        return -1;
    }

    /// <summary>
    /// 切换到指定索引的武器
    /// </summary>
    public bool ExchangeByIndex(int index)
    {
        if (index > SlotList.Length) return false;
        var slot = SlotList[index];
        if (slot == null || slot.Gun == null) return false;
        //更改父节点
        var parent = slot.Gun.GetParentOrNull<Node>();
        if (parent == null)
        {
            Master.MountPoint.AddChild(slot.Gun);
        }
        else if (parent != Master.MountPoint)
        {
            parent.RemoveChild(slot.Gun);
            Master.MountPoint.AddChild(slot.Gun);
        }

        ActiveGun = slot.Gun;
        ActiveIndex = index;
        return true;
    }
}
