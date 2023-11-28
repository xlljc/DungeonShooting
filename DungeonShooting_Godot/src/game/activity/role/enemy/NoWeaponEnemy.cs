
using Config;
using Godot;

/// <summary>
/// 没有武器的敌人
/// </summary>
[Tool]
public partial class NoWeaponEnemy : Enemy
{
    private Vector2I? _prevPosition = null;
    private BrushImageData _brushData3;
    
    public override void OnInit()
    {
        base.OnInit();
        NoWeaponAttack = true;
        AnimationPlayer.AnimationFinished += OnAnimationFinished;
        
        _brushData3 = new BrushImageData(ResourceManager.LoadTexture2D(ResourcePath.resource_test_Brush3_png).GetImage(), 3, 0.7f, 4, 0.2f);
    }

    protected override void Process(float delta)
    {
        base.Process(delta);

        if (AffiliationArea != null)
        {
            var pos = AffiliationArea.RoomInfo.LiquidCanvas.ToLiquidCanvasPosition(Position);
            AffiliationArea.RoomInfo.LiquidCanvas.DrawBrush(_brushData3, _prevPosition, pos, 0);
            _prevPosition = pos;
        }
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
        var realVelocity = GetRealVelocity();
        var effPos = Position;
        var debris = Create(Ids.Id_enemy_dead0002);
        debris.PutDown(effPos, RoomLayerEnum.NormalLayer);
        debris.MoveController.AddForce(Velocity + realVelocity);
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