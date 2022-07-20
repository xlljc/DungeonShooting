using System.Collections.Generic;
using Godot;

public class MainCamera : Camera2D
{
    //当前场景的相机对象
    public static MainCamera CurrentCamera { get; private set; }

    //抖动力度(px)
    [Export] public Vector2 ShakeDistance = Vector2.Zero;
    //恢复系数
    [Export] public float RecoveryCoefficient = 100f;
    //抖动开关
    public bool Enable = true;

    private long _index = 0;
    private Vector2 _prossesDistance = Vector2.Zero;
    private readonly Dictionary<long, Vector2> _shakeMap = new Dictionary<long, Vector2>();

    public override void _Ready()
    {
        CurrentCamera = this;
    }
    public override void _PhysicsProcess(float delta)
    {
        _Shake(delta);
    }

    //抖动调用
    private void _Shake(float delta)
    {
        if (Enable)
        {
            var _distance = _CalculateDistance();
            Offset += new Vector2(
                (float)GD.RandRange(-_distance.x, _distance.x) - Offset.x,
                (float)GD.RandRange(-_distance.y, _distance.y) - Offset.y
            );
            _prossesDistance = Vector2.Zero;
        }
        else
        {
            Offset = Offset.LinearInterpolate(Vector2.Zero, RecoveryCoefficient * delta);
        }
    }

    //计算相机需要抖动的值
    private Vector2 _CalculateDistance()
    {
        Vector2 temp = Vector2.Zero;
        float length = 0;
        foreach (var item in _shakeMap)
        {
            var value = item.Value;
            float tempLenght = value.Length();
            if (tempLenght > length)
            {
                length = tempLenght;
                temp = value;
            }
        }
        return _prossesDistance.Length() > length ? _prossesDistance : temp;
    }

    //设置帧抖动, 结束后自动清零, 需要每一帧调用
    public void ProssesShakeDistance(Vector2 value)
    {
        if (value.Length() > _prossesDistance.Length())
        {
            _prossesDistance = value;
        }
    }

    //创建一个抖动, 并设置抖动时间
    public async void CreateShake(Vector2 value, float time)
    {
        if (time > 0)
        {
            long tempIndex = _index++;
            SceneTreeTimer sceneTreeTimer = GetTree().CreateTimer(time);
            _shakeMap[tempIndex] = value;
            await ToSignal(sceneTreeTimer, "timeout");
            _shakeMap.Remove(tempIndex);
        }
    }

}
