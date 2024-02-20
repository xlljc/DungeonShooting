
using Godot;

/// <summary>
/// 房间的门, 门有两种状态, 打开和关闭
/// </summary>
[Tool]
public partial class RoomDoor : ActivityObject
{
    /// <summary>
    /// 门的方向
    /// </summary>
    public DoorDirection Direction => _door.Direction;
    
    /// <summary>
    /// 门是否关闭
    /// </summary>
    public bool IsClose { get; private set; }
    
    private RoomDoorInfo _door;
    private bool waitDisabledCollision = false;
    private AnimatedSprite2D _animatedDown;

    public override void OnInit()
    {
        AnimatedSprite.AnimationFinished += OnAnimationFinished;
    }

    /// <summary>
    /// 初始化调用
    /// </summary>
    public void Init(RoomDoorInfo doorInfo)
    {
        _door = doorInfo;
        IsClose = false;
        if (doorInfo.Direction == DoorDirection.E || doorInfo.Direction == DoorDirection.W)
        {
            _animatedDown = GetNode<AnimatedSprite2D>("AnimatedSpriteDown");
        }
        OpenDoorHandler();
    }

    /// <summary>
    /// 打开当前的门
    /// </summary>
    public void OpenDoor()
    {
        IsClose = false;
        //Visible = false;
        waitDisabledCollision = true;
        if (AnimatedSprite.SpriteFrames.HasAnimation(AnimatorNames.OpenDoor))
        {
            AnimatedSprite.Play(AnimatorNames.OpenDoor);
        }
        
        if (_animatedDown != null && _animatedDown.SpriteFrames.HasAnimation(AnimatorNames.OpenDoor))
        {
            _animatedDown.Play(AnimatorNames.OpenDoor);
        }
    }

    /// <summary>
    /// 关闭当前的门
    /// </summary>
    public void CloseDoor()
    {
        IsClose = true;
        //Visible = true;
        Collision.Disabled = false;

        if (AnimatedSprite.SpriteFrames.HasAnimation(AnimatorNames.CloseDoor))
        {
            AnimatedSprite.Play(AnimatorNames.CloseDoor);
        }

        if (_animatedDown != null && _animatedDown.SpriteFrames.HasAnimation(AnimatorNames.CloseDoor))
        {
            _animatedDown.Play(AnimatorNames.CloseDoor);
        }

        //调整门的层级
        switch (Direction)
        {
            case DoorDirection.E:
                ZIndex = MapLayer.AutoTopLayer;
                if (_animatedDown != null)
                {
                    _animatedDown.ZIndex = MapLayer.AutoTopLayer;
                }

                break;
            case DoorDirection.W:
                ZIndex = MapLayer.AutoTopLayer;
                if (_animatedDown != null)
                {
                    _animatedDown.ZIndex = MapLayer.AutoTopLayer;
                }

                break;
            case DoorDirection.S:
                ZIndex = MapLayer.AutoTopLayer;
                break;
            case DoorDirection.N:
                ZIndex = MapLayer.AutoMiddleLayer;
                break;
        }
    }

    private void OnAnimationFinished()
    {
        if (!IsClose && waitDisabledCollision) //开门动画播放完成
        {
            waitDisabledCollision = false;
            OpenDoorHandler();
        }
    }

    private void OpenDoorHandler()
    {
        Collision.Disabled = true;
        //调整门的层级
        //ZIndex = MapLayer.AutoFloorLayer;
        
        //调整门的层级
        switch (Direction)
        {
            case DoorDirection.E:
                ZIndex = MapLayer.AutoMiddleLayer;
                if (_animatedDown != null)
                {
                    _animatedDown.ZIndex = MapLayer.AutoTopLayer;
                }

                break;
            case DoorDirection.W:
                ZIndex = MapLayer.AutoMiddleLayer;
                if (_animatedDown != null)
                {
                    _animatedDown.ZIndex = MapLayer.AutoTopLayer;
                }

                break;
            case DoorDirection.S:
                ZIndex = MapLayer.AutoTopLayer;
                break;
            case DoorDirection.N:
                ZIndex = MapLayer.AutoMiddleLayer;
                break;
        }
    }
}