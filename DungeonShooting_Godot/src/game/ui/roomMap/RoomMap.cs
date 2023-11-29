namespace UI.RoomMap;

/// <summary>
/// Ui代码, 该类是根据ui场景自动生成的, 请不要手动编辑该类, 以免造成代码丢失
/// </summary>
public abstract partial class RoomMap : UiBase
{
    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ColorRect"/>, 节点路径: RoomMap.Bg
    /// </summary>
    public Bg L_Bg
    {
        get
        {
            if (_L_Bg == null) _L_Bg = new Bg((RoomMapPanel)this, GetNode<Godot.ColorRect>("Bg"));
            return _L_Bg;
        }
    }
    private Bg _L_Bg;


    public RoomMap() : base(nameof(RoomMap))
    {
    }

    public sealed override void OnInitNestedUi()
    {

    }

    /// <summary>
    /// 类型: <see cref="Godot.ColorRect"/>, 路径: RoomMap.Bg
    /// </summary>
    public class Bg : UiNode<RoomMapPanel, Godot.ColorRect, Bg>
    {
        public Bg(RoomMapPanel uiPanel, Godot.ColorRect node) : base(uiPanel, node) {  }
        public override Bg Clone() => new (UiPanel, (Godot.ColorRect)Instance.Duplicate());
    }


    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.ColorRect"/>, 节点路径: RoomMap.Bg
    /// </summary>
    public Bg S_Bg => L_Bg;

}
