using Godot;

/// <summary>
/// 鼠标指针
/// </summary>
public partial class Cursor : Node2D
{
    private Sprite2D lt;
    private Sprite2D lb;
    private Sprite2D rt;
    private Sprite2D rb;

    public override void _Ready()
    {
        lt = GetNode<Sprite2D>("LT");
        lb = GetNode<Sprite2D>("LB");
        rt = GetNode<Sprite2D>("RT");
        rb = GetNode<Sprite2D>("RB");
    }

    public override void _Process(double delta)
    {
        var targetGun = GameApplication.Instance.RoomManager.Player?.Holster.ActiveWeapon;
        if (targetGun != null)
        {
            SetScope(targetGun.CurrScatteringRange, targetGun);
        }
        else
        {
            SetScope(0, targetGun);
        }
        SetCursorPos();
    }

    public Vector2 GetPos()
    {
        return Vector2.Zero;
    }

    /// <summary>
    /// 设置光标半径范围
    /// </summary>
    private void SetScope(float scope, Weapon targetGun)
    {
        if (targetGun != null)
        {
            var tunPos = GameApplication.Instance.ViewToGlobalPosition(targetGun.GlobalPosition);
            var len = GlobalPosition.DistanceTo(tunPos);
            if (targetGun.Attribute != null)
            {
                len = Mathf.Max(0, len - targetGun.Attribute.FirePosition.X);
            }
            scope = len / GameConfig.ScatteringDistance * scope;
        }
        lt.Position = new Vector2(-scope, -scope);
        lb.Position = new Vector2(-scope, scope);
        rt.Position = new Vector2(scope, -scope);
        rb.Position = new Vector2(scope, scope);
    }

    private void SetCursorPos()
    {
        GlobalPosition = GetGlobalMousePosition();
    }
}