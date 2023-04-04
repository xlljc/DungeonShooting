
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
    /// 物体初始海拔高度
    /// </summary>
    [ExportGroup("Vertical")]
    [Export(PropertyHint.Range, "0, 36")]
    public int Altitude = 0;

    /// <summary>
    /// 物体初始纵轴速度
    /// </summary>
    [Export(PropertyHint.Range, "-1000,1000,0.1")]
    public float VerticalSpeed = 0;

    /// <summary>
    /// 当前标记所在Tile节点
    /// </summary>
    public TileMap TileRoot;
    
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
#if TOOLS
        if (Engine.IsEditorHint())
        {
            return;
        }
#endif
        if (_isOver)
        {
            _overTimer += (float)delta;
            if (_overTimer >= 1)
            {
                SetActive(false);
            }
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
        SetActive(true);
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

        CreateActivityObject().PutDown(GlobalPosition, Layer);
    }

    /// <summary>
    /// 实例化ItemId指定的物体, 并返回对象实例
    /// </summary>
    protected ActivityObject CreateActivityObject()
    {
        var instance = ActivityObject.Create(GetItemId());
        instance.VerticalSpeed = VerticalSpeed;
        instance.Altitude = Altitude;
        return instance;
    }

#if TOOLS
    public override void _Draw()
    {
        if (Engine.IsEditorHint() || GameApplication.Instance.Debug)
        {
            DrawLine(new Vector2(-5, -5), new Vector2(5, 5), DrawColor, 2f);
            DrawLine(new Vector2(-5, 5), new Vector2(5, -5), DrawColor, 2f);
        }
    }
#endif

    /// <summary>
    /// 设置当前节点是否是活动状态
    /// </summary>
    public void SetActive(bool flag)
    {
        var parent = GetParent();
        if (flag)
        {
            if (parent == null)
            {
                TileRoot.AddChild(this);
            }
            else if (parent != TileRoot)
            {
                parent.RemoveChild(this);
                TileRoot.AddChild(this);
            }
        }
        else
        {
            if (parent != null)
            {
                parent.RemoveChild(this);
            }
        }
    }
}