
[BuffFragment("MoveSpeed", "移速 buff, 参数‘1’为移动速度值")]
public class Buff_MoveSpeed : BuffFragment
{
    private float _moveSpeed;
    
    public override void InitParam(float arg1)
    {
        _moveSpeed = arg1;
    }

    public override void OnPickUpItem()
    {
        Role.RoleState.MoveSpeed += _moveSpeed;
        Role.RoleState.Acceleration += _moveSpeed * 1.4f;
        Role.RoleState.Friction += _moveSpeed * 10;
    }

    public override void OnRemoveItem()
    {
        Role.RoleState.MoveSpeed -= _moveSpeed;
        Role.RoleState.Acceleration -= _moveSpeed * 1.4f;
        Role.RoleState.Friction -= _moveSpeed * 10;
    }
}