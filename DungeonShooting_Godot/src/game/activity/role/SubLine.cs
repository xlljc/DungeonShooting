
using Godot;

public class SubLine : Component<Role>
{
    private Line2D _line2D;
    private RayCast2D _rayCast2D;

    private bool _enableSubLine;
    private float _range;
    
    public override void Ready()
    {
        //初始化瞄准辅助线
        _line2D = new Line2D();
        _line2D.Width = 1;
        _line2D.DefaultColor = Colors.Red;
        AddChild(_line2D);

        _rayCast2D = new RayCast2D();
        _rayCast2D.CollisionMask = PhysicsLayer.Wall;
        //_rayCast2D.Position = Master.MountPoint.Position;
        AddChild(_rayCast2D);

        Master.WeaponPack.ChangeActiveItemEvent += OnChangeWeapon;
    }

    public override void OnEnable()
    {
        OnChangeWeapon(Master.WeaponPack.ActiveItem);
    }
    
    public override void OnDisable()
    {
        _line2D.Visible = false;
        _rayCast2D.Enabled = false;
    }

    //切换武器
    private void OnChangeWeapon(Weapon weapon)
    {
        if (!Enable)
        {
            return;
        }
        if (weapon == null)
        {
            _enableSubLine = false;
        }
        else
        {
            _enableSubLine = true;
            _range = Utils.GetConfigRangeEnd(weapon.Attribute.BulletDistanceRange);
        }
    }

    public override void PhysicsProcess(float delta)
    {
        if (_enableSubLine)
        {
            _line2D.Visible = true;
            _rayCast2D.Enabled = true;
            UpdateSubLine();
        }
        else
        {
            _line2D.Visible = false;
            _rayCast2D.Enabled = false;
        }
    }

    public override void OnDestroy()
    {
        Master.WeaponPack.ChangeActiveItemEvent -= OnChangeWeapon;
    }

    private void UpdateSubLine()
    {
        var master = Master;
        var weapon = master.WeaponPack.ActiveItem;
        float length;
        var firePointGlobalPosition = weapon.FirePoint.GlobalPosition;
        if (_rayCast2D.IsColliding())
        {
            length = firePointGlobalPosition.DistanceTo(_rayCast2D.GetCollisionPoint());
        }
        else
        {
            length = _range;
        }

        //更新 Ray 的位置角度
        _rayCast2D.GlobalPosition = weapon.FirePoint.GlobalPosition;
        _rayCast2D.TargetPosition = new Vector2(_range, 0);
        _rayCast2D.Rotation = master.ConvertRotation(master.MountPoint.RealRotation);

        //计算 line2D 的点
        var position = _line2D.ToLocal(firePointGlobalPosition);
        Vector2 position2;
        if (master.Face == FaceDirection.Right)
        {
            position2 = Vector2.FromAngle(master.MountPoint.RealRotation) * length;
        }
        else
        {
            position2 = Vector2.FromAngle(Mathf.Pi - master.MountPoint.RealRotation) * length;
        }

        _line2D.Points = new Vector2[]
        {
            position,
            position + position2
        };
    }
}