
using Godot;

/// <summary>
/// 物体生成标记
/// </summary>
[Tool]
public partial class ActivityMark : Node2D
{
    /// <summary>
    /// 物体类型
    /// </summary>
    [Export]
    public ActivityIdPrefix.ActivityPrefixType Type = ActivityIdPrefix.ActivityPrefixType.NonePrefix;

    /// <summary>
    /// 物体id
    /// </summary>
    [Export]
    public string ItemId;

    /// <summary>
    /// 所在层级
    /// </summary>
    [Export]
    public RoomLayerEnum Layer = RoomLayerEnum.NormalLayer;

    /// <summary>
    /// 该标记在第几波调用 BeReady，
    /// 一个房间内所以敌人清完即可进入下一波
    /// </summary>
    [Export]
    public int WaveNumber = 1;

    /// <summary>
    /// 延时执行时间，单位：秒
    /// </summary>
    [Export]
    public float DelayTime = 0;
    
    /// <summary>
    /// 绘制的颜色
    /// </summary>
    protected Color DrawColor = new Color(0.4F, 0.56078434F, 0.8784314F);

    //是否已经结束
    private bool _isOver = true;
    private float _overTimer = 0;
    private float _timer = 0;
    private RoomInfo _tempRoom;

    /// <summary>
    /// 获取物体Id
    /// </summary>
    public string GetItemId()
    {
        return ActivityIdPrefix.GetNameByPrefixType(Type) + ItemId;
    }

    public override void _Process(double delta)
    {
        if (_isOver)
        {
            _overTimer += (float)delta;
        }
        else
        {
            if (DelayTime > 0)
            {
                _timer += (float)delta;
                if (_timer >= DelayTime)
                {
                    Doing(_tempRoom);
                    _tempRoom = null;
                    _isOver = true;
                }
            }
        }
    }

    /// <summary>
    /// 标记准备好了
    /// </summary>
    public void BeReady(RoomInfo roomInfo)
    {
        _isOver = false;
        _overTimer = 0;
        if (DelayTime <= 0)
        {
            Doing(roomInfo);
            _isOver = true;
        }
        else
        {
            _timer = 0;
            _tempRoom = roomInfo;
        }
    }

    /// <summary>
    /// 是否已经结束
    /// </summary>
    public bool IsOver()
    {
        return _isOver && _overTimer >= 1;
    }

    /// <summary>
    /// 调用该函数表示该标记可以生成物体了
    /// </summary>
    public virtual void Doing(RoomInfo roomInfo)
    {
        var instance = ActivityObject.Create(GetItemId());
        instance.PutDown(GlobalPosition, Layer);
        Visible = false;
    }

    public override void _Draw()
    {
        if (Engine.IsEditorHint() || GameApplication.Instance.Debug)
        {
            DrawLine(new Vector2(-5, -5), new Vector2(5, 5), DrawColor, 2f);
            DrawLine(new Vector2(-5, 5), new Vector2(5, -5), DrawColor, 2f);
        }
    }

}