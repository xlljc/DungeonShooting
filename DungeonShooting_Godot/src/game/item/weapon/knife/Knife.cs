
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
            //UpliftAngleRestore = 2;
            UpliftAngle = -90;
        }
    }
    
    public Knife(string id, WeaponAttribute attribute) : base(id, attribute)
    {
        
    }

    public override void _Process(float delta)
    {
        base._Process(delta);
        
    }

    protected override void OnStartCharge()
    {
        RotationDegrees = -120;
    }

    protected override void OnFire()
    {
        GD.Print("蓄力时长: " + GetTriggerChargeTime() + ", 扳机按下时长: " + GetTriggerDownTime());
        
        if (Master == GameApplication.Instance.Room.Player)
        {
            //创建抖动
            //GameCamera.Main.ProcessDirectionalShake(Vector2.Right.Rotated(GlobalRotation - Mathf.Pi * 0.5f) * 1.5f);
        }
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
