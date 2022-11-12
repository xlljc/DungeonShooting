
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
            //握把位置
            HoldPosition = new Vector2(10, 0);
            //关闭后坐力
            MaxBacklash = 0;
            MinBacklash = 0;
            UpliftAngleRestore = 3;
            UpliftAngle = -80;
            DefaultAngle = 0;
        }
    }
    
    public Knife(string id, WeaponAttribute attribute) : base(id, attribute)
    {
        
        
    }

    protected override void OnFire()
    {
        
    }

    protected override void OnShoot(float fireRotation)
    {
        
    }
}
