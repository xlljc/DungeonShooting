
using Godot;

/// <summary>
/// 物体生成标记
/// </summary>
[Tool]
public partial class ActivityMark : Node2D
{
    public enum MarkActivityType
    {
        /// <summary>
        /// 没有前缀
        /// </summary>
        NonePrefix,

        /// <summary>
        /// 测试单位
        /// </summary>
        Test,

        /// <summary>
        /// 角色
        /// </summary>
        Role,

        /// <summary>
        /// 敌人
        /// </summary>
        Enemy,

        /// <summary>
        /// 武器
        /// </summary>
        Weapon,

        /// <summary>
        /// 子弹
        /// </summary>
        Bullet,

        /// <summary>
        /// 弹壳
        /// </summary>
        Shell,

        /// <summary>
        /// 其他类型
        /// </summary>
        Other,
    }

    /// <summary>
    /// 物体类型
    /// </summary>
    [Export]
    public MarkActivityType Type = MarkActivityType.NonePrefix;

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
        switch (Type)
        {
            case MarkActivityType.NonePrefix:
                return ItemId;
            case MarkActivityType.Test:
                return ActivityIdPrefix.Test + ItemId;
            case MarkActivityType.Role:
                return ActivityIdPrefix.Role + ItemId;
            case MarkActivityType.Enemy:
                return ActivityIdPrefix.Enemy + ItemId;
            case MarkActivityType.Weapon:
                return ActivityIdPrefix.Weapon + ItemId;
            case MarkActivityType.Bullet:
                return ActivityIdPrefix.Bullet + ItemId;
            case MarkActivityType.Shell:
                return ActivityIdPrefix.Shell + ItemId;
            case MarkActivityType.Other:
                return ActivityIdPrefix.Other + ItemId;
        }

        return ItemId;
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