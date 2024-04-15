
using System.Collections.Generic;
using System.Text.Json;

[BuffFragment(
    "MaxHp",
    "增加血量上限 buff",
    Arg1 = "(int)增加血量上限值"
)]
public class Buff_MaxHp : BuffFragment
{
    private List<ulong> _cacheId = new List<ulong>();
    private int _maxHp;
    
    public override void InitParam(JsonElement[] args)
    {
        _maxHp = args[0].GetInt32();
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