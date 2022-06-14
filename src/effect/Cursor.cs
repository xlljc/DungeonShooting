using Godot;

/// <summary>
/// 鼠标指针
/// </summary>
public class Cursor : Node2D
{

    private Sprite lt;
    private Sprite lb;
    private Sprite rt;
    private Sprite rb;

    public override void _Ready()
    {
        lt = GetNode<Sprite>("LT");
        lb = GetNode<Sprite>("LB");
        rt = GetNode<Sprite>("RT");
        rb = GetNode<Sprite>("RB");
    }

    public override void _Process(float delta)
    {
        var targetGun = RoomManager.Current?.Player?.Holster.ActiveGun;
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

    /// <summary>
    /// 设置光标半径范围
    /// </summary>
    private void SetScope(float scope, Gun targetGun)
    {
        if (targetGun != null)
        {
            var len = GlobalPosition.DistanceTo(targetGun.GlobalPosition);
            if (targetGun.Attribute != null)
            {
                len = Mathf.Max(0, len - targetGun.Attribute.FirePosition.x);
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
        Position = GetGlobalMousePosition();
    }
}