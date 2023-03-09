
using Godot;

/// <summary>
/// 敌人标记
/// </summary>
[Tool]
public partial class EnemyMark : ActivityMark
{
    /// <summary>
    /// 敌人身上携带的武器id，id会自动加上武器前缀
    /// </summary>
    [Export]
    public string[] WeaponIds;

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

        //生成武器
        if (WeaponIds != null)
        {
            for (var i = 0; i < WeaponIds.Length; i++)
            {
                var weaponId = ActivityIdPrefix.GetNameByPrefixType(ActivityIdPrefix.ActivityPrefixType.Weapon) + WeaponIds[i];
                var weapon = ActivityObject.Create<Weapon>(weaponId);
                if (!instance.PickUpWeapon(weapon)) //如果不能放下， 则直接扔地上
                {
                    weapon.PutDown(pos, RoomLayerEnum.NormalLayer);
                }
            }
        }
    }
}