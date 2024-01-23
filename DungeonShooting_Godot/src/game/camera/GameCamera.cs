using System;
using System.Collections.Generic;
using Godot;

/// <summary>
/// 游戏相机
/// </summary>
public partial class GameCamera : Camera2D
{
    private class ShakeData
    {
        public Vector2 Value;
        public bool Decline;
        public float DataDelta;

        public ShakeData(Vector2 value, bool decline, float dataDelta)
        {
            Value = value;
            Decline = decline;
            DataDelta = dataDelta;
        }
    }

    /// <summary>
    /// 当前场景的相机对象
    /// </summary>
    public static GameCamera Main { get; private set; }

    /// <summary>
    /// 相机坐标更新完成事件, 参数为 delta
    /// </summary>
    public event Action<float> OnPositionUpdateEvent;

    /// <summary>
    /// 恢复系数
    /// </summary>
    [Export] public float RecoveryCoefficient = 25f;

    /// <summary>
    /// 抖动开关
    /// </summary>
    public bool EnableShake { get; set; } = true;

    /// <summary>
    /// 镜头跟随鼠标进度 (0 - 1)
    /// </summary>
    public float FollowsMouseAmount = 0.15f;

    /// <summary>
    /// 相机跟随目标
    /// </summary>
    private Role _followTarget;

    /// <summary>
    /// SubViewportContainer 中的像素偏移, 因为游戏开启了完美像素, SubViewport 节点下的相机运动会造成非常大的抖动,
    /// 为了解决这个问题, 在 SubViewport 父节点中对 SubViewport 进行整体偏移, 以抵消相机造成的巨大抖动
    /// </summary>
    public Vector2 PixelOffset { get; private set; }

    private long _index = 0;
    
    private Vector2 _processDistanceSquared = Vector2.Zero;
    private Vector2 _processDirection = Vector2.Zero;
    //抖动数据
    private readonly Dictionary<long, ShakeData> _shakeMap = new Dictionary<long, ShakeData>();
    
    private Vector2 _camPos;
    private Vector2 _shakeOffset = Vector2.Zero;
    
    public ShaderMaterial _offsetShader;

    public GameCamera()
    {
        Main = this;
    }
    
    public override void _Ready()
    {
        _offsetShader = (ShaderMaterial)GameApplication.Instance.SubViewportContainer.Material;
        _camPos = GlobalPosition;
    }
    
    //_PhysicsProcess
    public override void _PhysicsProcess(double delta)
    {
        var newDelta = (float)delta;
        _Shake(newDelta);
        
        var world = GameApplication.Instance.World;
        if (world != null && !world.Pause && _followTarget != null)
        {
            var mousePosition = InputManager.CursorPosition;
            var targetPosition = _followTarget.GlobalPosition;
            if (targetPosition.DistanceSquaredTo(mousePosition) >= (60 / FollowsMouseAmount) * (60 / FollowsMouseAmount))
            {
                _camPos = targetPosition.MoveToward(mousePosition, 60);
            }
            else
            {
                _camPos = targetPosition.Lerp(mousePosition, FollowsMouseAmount);
            }

            var cameraPosition = _camPos;
            var roundPos = cameraPosition.Round();
            PixelOffset = roundPos - cameraPosition;
            _offsetShader.SetShaderParameter("offset", PixelOffset);
            GlobalPosition = roundPos;
            
            Offset = _shakeOffset.Round();
            
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
    public async void CreateShake(Vector2 value, float time, bool decline = false)
    {
        if (time > 0)
        {
            value.X = Mathf.Abs(value.X);
            value.Y = Mathf.Abs(value.Y);
            var tempIndex = _index++;
            var sceneTreeTimer = GetTree().CreateTimer(time);
            if (decline)
            {
                _shakeMap[tempIndex] = new ShakeData(value, true, value.Length() / time);
            }
            else
            {
                _shakeMap[tempIndex] = new ShakeData(value, false, 0);
            }

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
            var distance = _CalculateDistanceSquared(delta);
            if (distance == Vector2.Zero)
            {
                _shakeOffset += _processDirection - Offset / 2f;
            }
            else
            {
                distance = new Vector2(Mathf.Sqrt(distance.X), Mathf.Sqrt(distance.Y));
                var offset = Offset;
                _shakeOffset += _processDirection + new Vector2(
                    (float)GD.RandRange(-distance.X, distance.X) - offset.X,
                    (float)GD.RandRange(-distance.Y, distance.Y) - offset.Y
                );
            }

            _processDistanceSquared = Vector2.Zero;
            _processDirection = _processDirection.Lerp(Vector2.Zero, RecoveryCoefficient * delta);
        }
        else
        {
            _shakeOffset = _shakeOffset.Lerp(Vector2.Zero, RecoveryCoefficient * delta);
        }
    }

    //计算相机需要抖动的值
    private Vector2 _CalculateDistanceSquared(float delta)
    {
        var temp = Vector2.Zero;
        float length = 0;

        foreach (var keyValuePair in _shakeMap)
        {
            var shakeData = keyValuePair.Value;
            var tempLength = shakeData.Value.LengthSquared();
            if (tempLength > length)
            {
                length = tempLength;
                temp = shakeData.Value;
                if (shakeData.Decline)
                {
                    shakeData.Value = shakeData.Value.MoveToward(Vector2.Zero, shakeData.DataDelta * delta);
                    //Debug.Log("shakeData.Value: " + shakeData.Value + ", _processDistanceSquared: " + _processDistanceSquared);
                }
            }
        }

        //return temp;
        return _processDistanceSquared.LengthSquared() > length ? _processDistanceSquared : temp;
    }
}
