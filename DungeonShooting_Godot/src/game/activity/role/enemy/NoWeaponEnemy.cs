
using Config;
using Godot;

/// <summary>
/// 没有武器的敌人
/// </summary>
[Tool]
public partial class NoWeaponEnemy : Enemy
{
    public override void OnInit()
    {
        base.OnInit();
        AnimationPlayer.AnimationFinished += OnAnimationFinished;
    }

    public override void Attack()
    {
        if (AnimatedSprite.Animation != AnimatorNames.Attack)
        {
            Debug.Log("attack...");
            AnimatedSprite.Play(AnimatorNames.Attack);
        }
    }

    public void ShootBullet()
    {
        var bulletData = FireManager.GetBulletData(this, FirePoint.GlobalPosition.AngleTo(LookTarget.Position), ExcelConfig.BulletBase_Map["0006"]);
        FireManager.ShootBullet(bulletData, AttackLayer);
    }

    private void OnAnimationFinished(StringName name)
    {
        if (name == AnimatorNames.Attack)
        {
            AttackTimer = 2f;
        }
    }
}