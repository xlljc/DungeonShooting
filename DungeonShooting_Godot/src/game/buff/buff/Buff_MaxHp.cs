
using System.Collections.Generic;

[Buff("MaxHp", "血量上限 buff, 参数‘1’为血量上限值")]
public class Buff_MaxHp : BuffFragment
{
    private List<ulong> _cacheId = new List<ulong>();
    private int _maxHp;
    
    public override void InitParam(float arg1)
    {
        _maxHp = (int)arg1;
    }
    
    public override void OnPickUpItem()
    {
        Role.MaxHp += _maxHp;
        var instanceId = Role.GetInstanceId();
        if (!_cacheId.Contains(instanceId))
        {
            _cacheId.Add(instanceId);
            Role.Hp += _maxHp;
        }
    }

    public override void OnRemoveItem()
    {
        Role.MaxHp -= _maxHp;
    }
}