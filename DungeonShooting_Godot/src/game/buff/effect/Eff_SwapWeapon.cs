using System.Collections.Generic;
using System.Text.Json;

[EffectFragment(
    "SwapWeapon",
    "随机选择房间内的一个手持武器的敌人, 交换你们手中的武器"
)]
public class Eff_SwapWeapon : EffectFragment
{
    public override void InitParam(JsonElement[] args)
    {
        
    }

    public override void OnUse()
    {
        var list = new List<AiRole>();
        foreach (var role in Master.World.Role_InstanceList)
        {
            if (role is AiRole enemy && enemy.IsEnemy(Role) && enemy.WeaponPack.ActiveItem != null)
            {
                list.Add(enemy);
            }
        }

        var targetEnemy  = Utils.Random.RandomChoose(list);
        if (targetEnemy != null)
        {
            var enemyWeapon = targetEnemy.WeaponPack.ActiveItem;
            
            var selfWeapon = Role.WeaponPack.ActiveItem;
            targetEnemy.RemoveWeapon(enemyWeapon.PackageIndex);
            Role.RemoveWeapon(selfWeapon.PackageIndex);

            targetEnemy.PickUpWeapon(selfWeapon);
            Role.PickUpWeapon(enemyWeapon);
        }
    }

    public override bool OnCheckUse()
    {
        if (Role.World == null || Role.WeaponPack.ActiveItem == null)
        {
            return false;
        }

        foreach (var role in Master.World.Role_InstanceList)
        {
            if (role is AiRole enemy && enemy.IsEnemy(Role) && enemy.WeaponPack.ActiveItem != null)
            {
                return true;
            }
        }

        return false;
    }
}