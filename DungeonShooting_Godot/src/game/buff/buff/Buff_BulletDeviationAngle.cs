
using System.Text.Json;

[BuffFragment(
    "BulletDeviationAngle", 
    "子弹偏移角度 buff, 单位角度制, 会从上限和下限随机抽取值",
    Arg1 = "(float)增加子弹偏移角度下限",
    Arg2 = "(float)增加子弹偏移角度上限"
)]
public class Buff_BulletDeviationAngle : BuffFragment
{
    private float _min;
    private float _max;

    public override void InitParam(JsonElement[] args)
    {
        _min = args[0].GetSingle();
        _max = args[1].GetSingle();
    }

    public override void OnPickUpItem()
    {
        Role.RoleState.CalcBulletDeviationAngleEvent += CalcBulletDeviationAngleEvent;
    }
    
    public override void OnRemoveItem()
    {
        Role.RoleState.CalcBulletDeviationAngleEvent -= CalcBulletDeviationAngleEvent;
    }
    
    private void CalcBulletDeviationAngleEvent(float originAngle, RefValue<float> refValue)
    {
        refValue.Value += Utils.Random.RandomRangeFloat(_min, _max);
    }
}