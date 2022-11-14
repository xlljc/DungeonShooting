
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
            //攻速设置
            StartFiringSpeed = 180;
            FinalFiringSpeed = StartFiringSpeed;
            //关闭连发
            ContinuousShoot = false;
            //设置成松发开火
            LooseShoot = true;
            //弹药量, 可以理解为耐久度
            AmmoCapacity = 180;
            MaxAmmoCapacity = AmmoCapacity;
            //握把位置
            HoldPosition = new Vector2(10, 0);
            //后坐力改为向前, 模拟手伸长的效果
            MaxBacklash = -8;
            MinBacklash = -8;
            BacklashRegressionSpeed = 24;
            UpliftAngle = -90;
        }
    }
    
    public Knife(string id, WeaponAttribute attribute) : base(id, attribute)
    {
        
    }
    
    protected override void OnStartCharge()
    {
        RotationDegrees = -120;
    }

    protected override void OnFire()
    {
        GD.Print("近战武器攻击! 蓄力时长: " + GetTriggerChargeTime() + ", 扳机按下时长: " + GetTriggerDownTime());
        
        //这里写播放挥刀特效和碰撞检测代码
        
        if (Master == GameApplication.Instance.Room.Player)
        {
            //创建抖动
            //GameCamera.Main.ProcessDirectionalShake(Vector2.Right.Rotated(GlobalRotation - Mathf.Pi * 0.5f) * 1.5f);
        }
    }

    protected override void OnShoot(float fireRotation)
    {
        
    }

    protected override int UseAmmoCount()
    {
        //这里要做判断, 如果没有碰到敌人, 则不消耗弹药 (耐久)
        return 0;
    }
}
