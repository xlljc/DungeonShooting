
using System.Collections;
using Godot;

public class SubLine : Component<Role>
{
    private Line2D _line2D;
    private RayCast2D _rayCast2D;

    private bool _enableSubLine;
    private float _range;
    private long _cid;
    
    public override void Ready()
    {
        //初始化瞄准辅助线
        _line2D = new Line2D();
        _line2D.Width = 1;
        _line2D.DefaultColor = Colors.Orange;
        AddChild(_line2D);

        _rayCast2D = new RayCast2D();
        _rayCast2D.CollisionMask = PhysicsLayer.Wall;
        AddChild(_rayCast2D);

        Master.WeaponPack.ChangeActiveItemEvent += OnChangeWeapon;
    }

    public override void OnEnable()
    {
        OnChangeWeapon(Master.WeaponPack.ActiveItem);
    }
    
    public override void OnDisable()
    {
        _enableSubLine = false;
        _line2D.Visible = false;
        _rayCast2D.Enabled = false;
        if (_cid > 0)
        {
            StopCoroutine(_cid);
        }
    }

    public void SetColor(Color color)
    {
        _line2D.DefaultColor = color;
    }

    public void PlayWarnAnimation(float time)
    {
        if (_cid > 0)
        {
            StopCoroutine(_cid);
        }

        _cid = StartCoroutine(RunWarnAnimation(time));
    }

    private IEnumerator RunWarnAnimation(float time)
    {
        var now = 0f;
        while (now < time)
        {
            now += GetProcessDeltaTime();
            yield return null;
        }
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
            if (_cid > 0)
            {
                StopCoroutine(_cid);
            }
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
        _line2D.QueueFree();
        _rayCast2D.QueueFree();
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

        var r = master.ConvertRotation(master.MountPoint.RealRotation);
        //更新 Ray 的位置角度
        _rayCast2D.GlobalPosition = firePointGlobalPosition;
        _rayCast2D.TargetPosition = new Vector2(_range, 0);
        _rayCast2D.Rotation = r;

        //计算 line2D 的点
        var position = _line2D.ToLocal(firePointGlobalPosition);
        Vector2 position2 = Vector2.FromAngle(r) * length;

        _line2D.Points = new Vector2[]
        {
            position,
            position + position2
        };
    }
}