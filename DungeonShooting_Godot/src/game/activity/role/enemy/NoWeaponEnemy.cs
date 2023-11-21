
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
        NoWeaponAttack = true;
        AnimationPlayer.AnimationFinished += OnAnimationFinished;
    }

    public override void Attack()
    {
        if (AnimationPlayer.CurrentAnimation != AnimatorNames.Attack)
        {
            AnimationPlayer.Play(AnimatorNames.Attack);
        }
    }

    public void ShootBullet()
    {
        var targetPosition = LookTarget.GetCenterPosition();
        var bulletData = FireManager.GetBulletData(this, 0, ExcelConfig.BulletBase_Map["0006"]);
        for (var i = 0; i < 8; i++)
        {
            var data = bulletData.Clone();
            var tempPos = new Vector2(targetPosition.X + Utils.Random.RandomRangeInt(-30, 30), targetPosition.Y +  + Utils.Random.RandomRangeInt(-30, 30));
            FireManager.SetParabolaTarget(data, tempPos);
            FireManager.ShootBullet(data, AttackLayer);
        }
    }

    protected override void OnDie()
    {
        var effPos = Position;
        var debris = Create(Ids.Id_enemy_dead0002);
        debris.PutDown(effPos, RoomLayerEnum.NormalLayer);
        debris.InheritVelocity(this);
        debris.SetFace(Face);
        
        //派发敌人死亡信号
        EventManager.EmitEvent(EventEnum.OnEnemyDie, this);
        Destroy();
    }

    private void OnAnimationFinished(StringName name)
    {
        if (name == AnimatorNames.Attack)
        {
            AttackTimer = 2f;
        }
    }
}