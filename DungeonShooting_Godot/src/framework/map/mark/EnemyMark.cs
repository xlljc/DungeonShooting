
using Godot;

/// <summary>
/// 敌人标记
/// </summary>
[Tool]
public partial class EnemyMark : ActivityMark
{
    /// <summary>
    /// 武器1 id, id会自动加上武器前缀
    /// </summary>
    [Export(default, "副武器1 id, id会自动加上武器前缀")]
    public string Weapon1Id;
    /// <summary>
    /// 武器1弹药量, 如果值小于0, 则使用默认弹药量
    /// </summary>
    [Export]
    public int Weapon1Ammo = -1;
    /// <summary>
    /// 武器2 id, id会自动加上武器前缀
    /// </summary>
    [Export]
    public string Weapon2Id;
    /// <summary>
    /// 武器2弹药量, 如果值小于0, 则使用默认弹药量
    /// </summary>
    [Export]
    public int Weapon2Ammo = -1;
    /// <summary>
    /// 武器3 id, id会自动加上武器前缀
    /// </summary>
    [Export]
    public string Weapon3Id;
    /// <summary>
    /// 武器3弹药量, 如果值小于0, 则使用默认弹药量
    /// </summary>
    [Export]
    public int Weapon3Ammo = -1;
    /// <summary>
    /// 武器4 id, id会自动加上武器前缀
    /// </summary>
    [Export]
    public string Weapon4Id;
    /// <summary>
    /// 武器4弹药量, 如果值小于0, 则使用默认弹药量
    /// </summary>
    [Export]
    public int Weapon4Ammo = -1;

    public override void _Ready()
    {
        Type = ActivityIdPrefix.ActivityPrefixType.Enemy;
        Layer = RoomLayerEnum.YSortLayer;
    }

    public override void BeReady(RoomInfo roomInfo)
    {
        var pos = GlobalPosition;
        //创建敌人
        Type = ActivityIdPrefix.ActivityPrefixType.Enemy;
        var id = GetItemId();
        var instance = ActivityObject.Create<Enemy>(id);
        instance.PutDown(pos, Layer);
        Visible = false;

        if (!string.IsNullOrWhiteSpace(Weapon1Id))
            CreateWeapon(instance, pos, Weapon1Id, Weapon1Ammo);
        if (!string.IsNullOrWhiteSpace(Weapon2Id))
            CreateWeapon(instance, pos, Weapon2Id, Weapon2Ammo);
        if (!string.IsNullOrWhiteSpace(Weapon3Id))
            CreateWeapon(instance, pos, Weapon3Id, Weapon3Ammo);
        if (!string.IsNullOrWhiteSpace(Weapon4Id))
            CreateWeapon(instance, pos, Weapon4Id, Weapon4Ammo);
    }

    //生成武器
    private void CreateWeapon(Enemy instance, Vector2 pos, string id, int ammon)
    {
        var weaponId = ActivityIdPrefix.GetNameByPrefixType(ActivityIdPrefix.ActivityPrefixType.Weapon) + id;
        var weapon = ActivityObject.Create<Weapon>(weaponId);
        //设置弹药量
        if (ammon >= 0)
        {
            weapon.SetTotalAmmo(ammon);
        }
        //如果不能放下， 则直接扔地上
        if (!instance.PickUpWeapon(weapon))
        {
            weapon.PutDown(pos, RoomLayerEnum.NormalLayer);
        }
    }
}