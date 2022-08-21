using Godot;

[RegisterWeapon("1002", typeof(Shotgun.ShotgunAttribute))]
public class Shotgun : Weapon
{

    private class ShotgunAttribute : WeaponAttribute
    {
        public ShotgunAttribute()
        {
            Name = "霰弹枪";
            Sprite = "res://resource/sprite/gun/gun2.png";
            Weight = 40;
            CenterPosition = new Vector2(0.4f, -2.6f);
            StartFiringSpeed = 120;
            StartScatteringRange = 30;
            FinalScatteringRange = 90;
            ScatteringRangeAddValue = 50f;
            ScatteringRangeBackSpeed = 50;
            //连发
            ContinuousShoot = false;
            AmmoCapacity = 7;
            MaxAmmoCapacity = 42;
            AloneReload = true;
            AloneReloadCanShoot = true;
            ReloadTime = 0.3f;
            //连发数量
            MinContinuousCount = 1;
            MaxContinuousCount = 1;
            //开火前延时
            DelayedTime = 0f;
            //攻击距离
            MinDistance = 500;
            MaxDistance = 600;
            //发射子弹数量
            MinFireBulletCount = 1;
            MaxFireBulletCount = 1;
            //抬起角度
            UpliftAngle = 15;
            MaxBacklash = 6;
            MinBacklash = 5;
            //枪身长度
            FirePosition = new Vector2(16, 1.5f);
        }
    }

    /// <summary>
    /// 子弹预制体
    /// </summary>
    public PackedScene BulletPack;
    /// <summary>
    /// 弹壳预制体
    /// </summary>
    public PackedScene ShellPack;

    public Shotgun(string id, WeaponAttribute attribute) : base(id, attribute)
    {
        BulletPack = ResourceManager.Load<PackedScene>("res://prefab/weapon/bullet/OrdinaryBullets.tscn");
        ShellPack = ResourceManager.Load<PackedScene>("res://prefab/weapon/shell/ShellCase.tscn");
    }

    protected override void OnFire()
    {
        //创建一个弹壳
        var startPos = GlobalPosition + new Vector2(0, 5);
        var startHeight = 6;
        var direction = GlobalRotationDegrees + MathUtils.RandRangeInt(-30, 30) + 180;
        var xf = MathUtils.RandRangeInt(20, 60);
        var yf = MathUtils.RandRangeInt(60, 120);
        var rotate = MathUtils.RandRangeInt(-720, 720);
        var sprite = ShellPack.Instance<Sprite>();
        sprite.StartThrow<ThrowWeapon>(new Vector2(5, 10), startPos, startHeight, direction, xf, yf, rotate, sprite);
        //创建抖动
        MainCamera.Main.ProssesDirectionalShake(Vector2.Right.Rotated(GlobalRotation) * 1.5f);
    }

    protected override void OnShoot()
    {
        for (int i = 0; i < 5; i++)
        {
            //创建子弹
            CreateBullet(BulletPack, FirePoint.GlobalPosition, (FirePoint.GlobalPosition - OriginPoint.GlobalPosition).Angle() + MathUtils.RandRange(-20 / 180f * Mathf.Pi, 20 / 180f * Mathf.Pi));
        }
    }

    protected override void OnReload()
    {

    }

    protected override void OnReloadFinish()
    {

    }

    protected override void OnDownTrigger()
    {
        
    }

    protected override void OnUpTrigger()
    {

    }

    protected override void OnPickUp(Role master)
    {

    }

    protected override void OnThrowOut()
    {

    }

    protected override void OnActive()
    {

    }

    protected override void OnConceal()
    {

    }
}
