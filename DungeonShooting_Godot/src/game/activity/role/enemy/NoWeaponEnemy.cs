
using Config;
using Godot;

/// <summary>
/// 没有武器的敌人
/// </summary>
[Tool]
public partial class NoWeaponEnemy : Enemy
{
    private BrushImageData _brushData;
    
    public override void OnInit()
    {
        base.OnInit();
        NoWeaponAttack = true;
        WeaponPack.SetCapacity(0);
        AnimationPlayer.AnimationFinished += OnAnimationFinished;
        
        _brushData = LiquidBrushManager.GetBrush("0002");
    }

    protected override void Process(float delta)
    {
        base.Process(delta);

        //测试笔刷
        DrawLiquid(_brushData);
    }

    public override void Attack()
    {
        if (AnimationPlayer.CurrentAnimation != AnimatorNames.Attack)
        {
            AnimationPlayer.Play(AnimatorNames.Attack);
        }
    }
    
    /// <summary>
    /// 开始执行攻击逻辑
    /// </summary>
    public virtual void OnAttack()
    {
        //攻击特效
        var effect = ObjectManager.GetPoolItem<IEffect>(ResourcePath.prefab_effect_weapon_ShotFire0003_tscn);
        var node = (Node2D)effect;
        node.GlobalPosition = FirePoint.GlobalPosition;
        node.Rotation = MountPoint.Rotation;
        node.AddToActivityRoot(RoomLayerEnum.YSortLayer);
        effect.PlayEffect();
        
        var targetPosition = LookTarget.GetCenterPosition();
        var bulletData = FireManager.GetBulletData(this, 0, ExcelConfig.BulletBase_Map["0006"]);
        for (var i = 0; i < 8; i++)
        {
            var data = bulletData.Clone();
            var tempPos = new Vector2(targetPosition.X + Utils.Random.RandomRangeInt(-30, 30), targetPosition.Y + Utils.Random.RandomRangeInt(-30, 30));
            FireManager.SetParabolaTarget(data, tempPos);
            FireManager.ShootBullet(data, AttackLayer);
        }
    }

    protected override void OnDie()
    {
        var realVelocity = GetRealVelocity();
        var effPos = Position;
        var debris = Create<EnemyDead0002>(Ids.Id_enemy_dead0002);
        debris.PutDown(effPos, RoomLayerEnum.NormalLayer);
        debris.MoveController.AddForce(Velocity + realVelocity);
        debris.SetForwardDirection(Face);
        debris.BrushPrevPosition =  BrushPrevPosition;
        
        //创建金币
        CreateGold();
        
        //派发敌人死亡信号
        EventManager.EmitEvent(EventEnum.OnEnemyDie, this);
        Destroy();
    }

    private void OnAnimationFinished(StringName name)
    {
        if (name == AnimatorNames.Attack)
        {
            AttackTimer = 3f;
        }
    }
}