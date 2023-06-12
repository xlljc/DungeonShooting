using System;
using System.Collections.Generic;
using Godot;

/// <summary>
/// 游戏相机
/// </summary>
public partial class GameCamera : Camera2D
{
    /// <summary>
    /// 当前场景的相机对象
    /// </summary>
    public static GameCamera Main { get; private set; }

    /// <summary>
    /// 相机坐标更新完成事件
    /// </summary>
    public event Action<float> OnPositionUpdateEvent;

    /// <summary>
    /// 恢复系数
    /// </summary>
    [Export]
    public float RecoveryCoefficient = 25f;
    
    /// <summary>
    /// 抖动开关
    /// </summary>
    public bool EnableShake { get; set; } = true;

    /// <summary>
    /// 相机跟随目标
    /// </summary>
    private Role _followTarget;
    
    // 3.5
    //public Vector2 SubPixelPosition { get; private set; }

    private long _index = 0;
    
    private Vector2 _processDistanceSquared = Vector2.Zero;
    private Vector2 _processDirection = Vector2.Zero;
    //抖动数据
    private readonly Dictionary<long, Vector2> _shakeMap = new Dictionary<long, Vector2>();
    
    private Vector2 _camPos;
    private Vector2 _shakeOffset = Vector2.Zero;

    public GameCamera()
    {
        Main = this;
    }
    
    public override void _Ready()
    {
        _camPos = GlobalPosition;
    }

    public override void _Process(double delta)
    {
        var newDelta = (float)delta;
        _Shake(newDelta);
        
        // 3.5 写法
        // var player = GameApplication.Instance.RoomManager.Player;
        // var viewportContainer = GameApplication.Instance.SubViewportContainer;
        // var camPos = player.GlobalPosition;
        // _camPos = _camPos.Lerp(camPos, Mathf.Min(6 * newDelta, 1)) + _shakeOffset;
        // SubPixelPosition = _camPos.Round() - _camPos;
        // (viewportContainer.Material as ShaderMaterial)?.SetShaderParameter("offset", SubPixelPosition);
        // GlobalPosition = _camPos.Round();


        var world = GameApplication.Instance.World;
        if (world != null && !world.Pause && _followTarget != null)
        {
            var mousePosition = InputManager.CursorPosition;
            var targetPosition = _followTarget.GlobalPosition;
            Vector2 targetPos;
            if (targetPosition.DistanceSquaredTo(mousePosition) >= (60 / 0.3f) * (60 / 0.3f))
            {
                targetPos = targetPosition.MoveToward(mousePosition, 60);
            }
            else
            {
                targetPos = targetPosition.Lerp(mousePosition, 0.3f);
            }
            _camPos = (_camPos.Lerp(targetPos, Mathf.Min(6 * newDelta, 1))).Round();
            GlobalPosition = _camPos;
            
            Offset = _shakeOffset.Round();
        
            //_temp = _camPos - targetPosition;

            //调用相机更新事件
            if (OnPositionUpdateEvent != null)
            {
                OnPositionUpdateEvent(newDelta);
            }
        }
    }

    /// <summary>
    /// 设置相机跟随目标
    /// </summary>
    public void SetFollowTarget(Role target)
    {
        _followTarget = target;
        if (target != null)
        {
            _camPos = target.GlobalPosition;
            GlobalPosition = _camPos;
        }
    }

    /// <summary>
    /// 获取相机跟随目标
    /// </summary>
    public Role GetFollowTarget()
    {
        return _followTarget;
    }
    
    /// <summary>
    /// 设置帧抖动, 结束后自动清零, 需要每一帧调用
    /// </summary>
    /// <param name="value">抖动的力度</param>
    public void Shake(Vector2 value)
    {
        if (value.LengthSquared() > _processDistanceSquared.LengthSquared())
        {
            _processDistanceSquared = value;
        }
    }
    
    /// <summary>
    /// 添加一个单方向上的抖动, 该帧结束后自动清零
    /// </summary>
    public void DirectionalShake(Vector2 value)
    {
        _processDirection += value;
    }
    
    /// <summary>
    /// 创建一个抖动, 并设置抖动时间
    /// </summary>
    public async void CreateShake(Vector2 value, float time)
    {
        if (time > 0)
        {
            value.X = Mathf.Abs(value.X);
            value.Y = Mathf.Abs(value.Y);
            var tempIndex = _index++;
            var sceneTreeTimer = GetTree().CreateTimer(time);
            _shakeMap[tempIndex] = value;
            await ToSignal(sceneTreeTimer, Timer.SignalName.Timeout);
            _shakeMap.Remove(tempIndex);
        }
    }

    /// <summary>
    /// 播放玩家死亡特写镜头
    /// </summary>
    public void PlayPlayerDieFeatures()
    {
        
    }

    //抖动调用
    private void _Shake(float delta)
    {
        if (EnableShake)
        {
            var distance = _CalculateDistanceSquared();
            distance = new Vector2(Mathf.Sqrt(distance.X), Mathf.Sqrt(distance.Y));
            _shakeOffset += _processDirection + new Vector2(
                (float)GD.RandRange(-distance.X, distance.X) - Offset.X,
                (float)GD.RandRange(-distance.Y, distance.Y) - Offset.Y
            );
            _processDistanceSquared = Vector2.Zero;
            _processDirection = _processDirection.Lerp(Vector2.Zero, RecoveryCoefficient * delta);
        }
        else
        {
            _shakeOffset = _shakeOffset.Lerp(Vector2.Zero, RecoveryCoefficient * delta);
        }
    }

    //计算相机需要抖动的值
    private Vector2 _CalculateDistanceSquared()
    {
        var temp = Vector2.Zero;
        float length = 0;
        
        foreach (var keyValuePair in _shakeMap)
        {
            var tempLenght = keyValuePair.Value.LengthSquared();
            if (tempLenght > length)
            {
                length = tempLenght;
                temp = keyValuePair.Value;
            }
        }
        
        return _processDistanceSquared.LengthSquared() > length ? _processDistanceSquared : temp;
    }
}
