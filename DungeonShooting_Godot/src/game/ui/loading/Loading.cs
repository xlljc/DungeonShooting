namespace UI.Loading;

/// <summary>
/// Ui代码, 该类是根据ui场景自动生成的, 请不要手动编辑该类, 以免造成代码丢失
/// </summary>
public abstract partial class Loading : UiBase
{
    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ColorRect"/>, 节点路径: Loading.ColorRect
    /// </summary>
    public Loading_ColorRect L_ColorRect
    {
        get
        {
            if (_L_ColorRect == null) _L_ColorRect = new Loading_ColorRect(this, GetNodeOrNull<Godot.ColorRect>("ColorRect"));
            return _L_ColorRect;
        }
    }
    private Loading_ColorRect _L_ColorRect;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: Loading.Label
    /// </summary>
    public Loading_Label L_Label
    {
        get
        {
            if (_L_Label == null) _L_Label = new Loading_Label(this, GetNodeOrNull<Godot.Label>("Label"));
            return _L_Label;
        }
    }
    private Loading_Label _L_Label;


    public Loading() : base(nameof(Loading))
    {
    }

    /// <summary>
    /// 类型: <see cref="Godot.ColorRect"/>, 路径: Loading.ColorRect
    /// </summary>
    public class Loading_ColorRect : UiNode<Loading, Godot.ColorRect, Loading_ColorRect>
    {
        public Loading_ColorRect(Loading uiPanel, Godot.ColorRect node) : base(uiPanel, node) {  }
        public override Loading_ColorRect Clone() => new (UiPanel, (Godot.ColorRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: Loading.Label
    /// </summary>
    public class Loading_Label : UiNode<Loading, Godot.Label, Loading_Label>
    {
        public Loading_Label(Loading uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Loading_Label Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }


    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="UI.Loading.LoadingPanel"/>, 节点路径: Loading.ColorRect
    /// </summary>
    public Loading_ColorRect S_ColorRect => L_ColorRect;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="UI.Loading.LoadingPanel"/>, 节点路径: Loading.Label
    /// </summary>
    public Loading_Label S_Label => L_Label;

}
