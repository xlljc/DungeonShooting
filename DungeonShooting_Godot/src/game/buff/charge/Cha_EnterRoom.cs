
using System.Text.Json;

[ChargeFragment(
    "EnterRoom",
    "玩家第一次进入某个房间充能, 该条充能件仅对玩家生效",
    Arg1 = "(float)充能量, 范围内0-1"
)]
public class Cha_EnterRoom : ChargeFragment
{
    private float _value = 0.2f;
    
    public override void InitParam(JsonElement[] args)
    {
        _value = args[0].GetSingle();
    }

    public override void OnUse()
    {
        Master.ChargeProgress = 0;
    }

    public override void OnPickUpItem()
    {
        if (Role is Player player)
        {
            player.OnFirstEnterRoomEvent += OnFirstEnterRoom;
        }
    }

    public override void OnRemoveItem()
    {
        if (Role is Player player)
        {
            player.OnFirstEnterRoomEvent -= OnFirstEnterRoom;
        }
    }

    private void OnFirstEnterRoom(RoomInfo roomInfo)
    {
        Master.ChargeProgress += _value;
    }
}