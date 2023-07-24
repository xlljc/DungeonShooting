
using Godot;

/// <summary>
/// 道具基类
/// </summary>
public abstract partial class Prop : ActivityObject
{
    /// <summary>
    /// 道具所属角色
    /// </summary>
    public Role Master { get; set; }

    /// <summary>
    /// 当道具被拾起时调用 (在 Master 赋值之后调用)
    /// </summary>
    public abstract void OnPickUpItem();

    /// <summary>
    /// 当道具被移除时调用 (在 Master 置为 null 之前调用)
    /// </summary>
    public abstract void OnRemoveItem();

    /// <summary>
    /// 如果道具放入了角色背包中, 则每帧调用
    /// </summary>
    public virtual void PackProcess(float delta)
    {
    }

    /// <summary>
    /// 触发扔掉道具效果, 并不会管道具是否在道具背包中
    /// </summary>
    /// <param name="master">触发扔掉该道具的的角色</param>
    public void ThrowProp(Role master)
    {
        ThrowProp(master, master.GlobalPosition);
    }
    
    /// <summary>
    /// 触发扔掉道具效果, 并不会管道具是否在道具背包中
    /// </summary>
    /// <param name="master">触发扔掉该道具的的角色</param>
    /// <param name="startPosition">投抛起始位置</param>
    public void ThrowProp(Role master, Vector2 startPosition)
    {
        //阴影偏移
        ShadowOffset = new Vector2(0, 2);
        GlobalRotation = 0;
        var startHeight = -master.MountPoint.Position.Y;
        Throw(startPosition, startHeight, 0, Vector2.Zero, 0);

        //继承role的移动速度
        InheritVelocity(master);
    }
}