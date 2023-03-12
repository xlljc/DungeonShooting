using Godot;

/// <summary>
/// 鼠标指针
/// </summary>
public partial class Cursor : Node2D
{
    /// <summary>
    /// 是否是GUI模式
    /// </summary>
    private bool _isGuiMode = true;

    /// <summary>
    /// 非GUI模式下鼠标指针所挂载的角色
    /// </summary>
    private Role _mountRole;

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
        if (_isGuiMode)
        {
            SetScope(0, null);
        }
        else
        {
            var targetGun = _mountRole?.Holster.ActiveWeapon;
            if (targetGun != null)
            {
                SetScope(targetGun.CurrScatteringRange, targetGun);
            }
            else
            {
                SetScope(0, null);
            }
        }

        SetCursorPos();
    }

    /// <summary>
    /// 设置是否是Gui模式
    /// </summary>
    public void SetGuiMode(bool flag)
    {
        _isGuiMode = flag;
    }
    
    /// <summary>
    /// 获取是否是Gui模式
    /// </summary>
    public bool GetGuiMode()
    {
        return _isGuiMode;
    }
    
    /// <summary>
    /// 设置非GUI模式下鼠标指针所挂载的角色
    /// </summary>
    public void SetMountRole(Role role)
    {
        _mountRole = role;
    }

    /// <summary>
    /// 获取非GUI模式下鼠标指针所挂载的角色
    /// </summary>
    public Role GetMountRole()
    {
        return _mountRole;
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
        scope = Mathf.Clamp(scope, 0, 200);
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