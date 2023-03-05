
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
    /// 获取物体Id
    /// </summary>
    /// <returns></returns>
    public string GetItemId()
    {
        return ActivityIdPrefix.GetNameByPrefixType(Type) + ItemId;
    }

    /// <summary>
    /// 调用该函数表示房间已经准备好了
    /// </summary>
    public virtual void BeReady(RoomInfo roomInfo)
    {
        var id = GetItemId();
        var instance = ActivityObject.Create(id);
        instance.PutDown(GlobalPosition, Layer);
        QueueFree();
    }

    public override void _Draw()
    {
        if (Engine.IsEditorHint() || GameApplication.Instance.Debug)
        {
            DrawLine(new Vector2(-5, -5), new Vector2(5, 5), new Color(0.4F,0.56078434F,0.8784314F), 2f);
            DrawLine(new Vector2(-5, 5), new Vector2(5, -5), new Color(0.4F,0.56078434F,0.8784314F), 2f);
        }
    }

}