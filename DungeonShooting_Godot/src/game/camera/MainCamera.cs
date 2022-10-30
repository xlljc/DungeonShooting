using System.Collections.Generic;
using Godot;

public class MainCamera : Camera2D
{
    /// <summary>
    /// 当前场景的相机对象
    /// </summary>
    public static MainCamera Main { get; private set; }

    /// <summary>
    /// 恢复系数
    /// </summary>
    [Export]
    public float RecoveryCoefficient = 100f;
    /// <summary>
    /// 抖动开关
    /// </summary>
    public bool Enable { get; set; } = true;

    private long _index = 0;
    private Vector2 _prossesDistance = Vector2.Zero;
    private Vector2 _prossesDirectiona = Vector2.Zero;
    private readonly Dictionary<long, Vector2> _shakeMap = new Dictionary<long, Vector2>();

    public override void _Ready()
    {
        Main = this;
    }
    public override void _PhysicsProcess(float delta)
    {
        _Shake(delta);
    }

    /// <summary>
    /// 设置帧抖动, 结束后自动清零, 需要每一帧调用
    /// </summary>
    /// <param name="value">抖动的力度</param>
    public void ProcessShake(Vector2 value)
    {
        if (value.Length() > _prossesDistance.Length())
        {
            _prossesDistance = value;
        }
    }

    public void ProcessDirectionalShake(Vector2 value)
    {
        _prossesDirectiona += value;
    }

    /// <summary>
    /// 创建一个抖动, 并设置抖动时间
    /// </summary>
    /// <param name="value">抖动力度</param>
    /// <param name="time">抖动生效时间</param>
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
            Offset += _prossesDirectiona;
            _prossesDistance = Vector2.Zero;
            _prossesDirectiona = Vector2.Zero;
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

}
