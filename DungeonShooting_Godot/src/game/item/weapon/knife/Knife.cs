
using Godot;

[RegisterWeapon("1004", typeof(KnifeAttribute))]
public class Knife : Weapon
{

    private class KnifeAttribute : WeaponAttribute
    {
        public KnifeAttribute()
        {
            Sprite = ResourcePath.resource_sprite_gun_knife1_png;
            WeaponPrefab = ResourcePath.prefab_weapon_Knife_tscn;
            //连发关闭
            ContinuousShoot = false;
            //松发开火
            LooseShoot = true;
            //弹药量
            AmmoCapacity = 180;
            MaxAmmoCapacity = 180;
            //握把位置
            HoldPosition = new Vector2(10, 0);
            //关闭后坐力
            MaxBacklash = 0;
            MinBacklash = 0;
            //我们需要自己来控制角度
            UpliftAngleRestore = 3;
            UpliftAngle = -80;
            DefaultAngle = 20;
        }
    }
    
    public Knife(string id, WeaponAttribute attribute) : base(id, attribute)
    {
        
    }

    protected override void OnFire()
    {
        GD.Print("蓄力时长: " + GetTriggerChargeTime() + ", 扳机按下时长: " + GetTriggerDownTime());
    }

    protected override void OnShoot(float fireRotation)
    {
        
    }

    protected override void OnUpTrigger()
    {
        GD.Print("松开扳机");
    }

    protected override void OnDownTrigger()
    {
        GD.Print("开始按下扳机");
    }

    protected override int UseAmmoCount()
    {
        //这里要做判断, 如果没有碰到敌人, 则不消耗弹药 (耐久)
        return 0;
    }
}
