
using System.Collections.Generic;
using System.Text.Json;

[BuffFragment(
    "MaxShield",
    "增加护盾上限buff",
    Arg1 = "(int)增加的护盾上限值"
)]
public class Buff_MaxShield : BuffFragment
{
    private List<ulong> _cacheId = new List<ulong>();
    private int _maxShield;
    
    public override void InitParam(JsonElement[] args)
    {
        _maxShield = args[0].GetInt32();
    }
    
    public override void OnPickUpItem()
    {
        Role.MaxShield += _maxShield;
        var instanceId = Role.GetInstanceId();
        if (!_cacheId.Contains(instanceId))
        {
            _cacheId.Add(instanceId);
            Role.Shield += _maxShield;
        }
    }

    public override void OnRemoveItem()
    {
        Role.MaxShield -= _maxShield;
    }
}