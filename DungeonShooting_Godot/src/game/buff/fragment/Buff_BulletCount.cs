
[Buff("BulletCount", "子弹数量 buff, 参数‘1’为增加子弹的数量")]
public class Buff_BulletCount : BuffFragment
{
    private int _value;

    public override void InitParam(float arg1)
    {
        _value = (int)arg1;
    }
    
    public override void OnPickUpItem()
    {
        Role.RoleState.CalcBulletCountEvent += CalcBulletCountEvent;
    }

    public override void OnRemoveItem()
    {
        Role.RoleState.CalcBulletCountEvent -= CalcBulletCountEvent;
    }

    private void CalcBulletCountEvent(int originCount, RefValue<int> refValue)
    {
        refValue.Value += _value;
    }
}