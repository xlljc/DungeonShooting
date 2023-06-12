namespace UI.Loading;

/// <summary>
/// Ui代码, 该类是根据ui场景自动生成的, 请不要手动编辑该类, 以免造成代码丢失
/// </summary>
public abstract partial class Loading : UiBase
{
    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ColorRect"/>, 节点路径: Loading.ColorRect
    /// </summary>
    public UiNode_ColorRect L_ColorRect
    {
        get
        {
            if (_L_ColorRect == null) _L_ColorRect = new UiNode_ColorRect(GetNodeOrNull<Godot.ColorRect>("ColorRect"));
            return _L_ColorRect;
        }
    }
    private UiNode_ColorRect _L_ColorRect;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: Loading.Label
    /// </summary>
    public UiNode_Label L_Label
    {
        get
        {
            if (_L_Label == null) _L_Label = new UiNode_Label(GetNodeOrNull<Godot.Label>("Label"));
            return _L_Label;
        }
    }
    private UiNode_Label _L_Label;


    public Loading() : base(nameof(Loading))
    {
    }

    /// <summary>
    /// 类型: <see cref="Godot.ColorRect"/>, 路径: Loading.ColorRect
    /// </summary>
    public class UiNode_ColorRect : IUiNode<Godot.ColorRect, UiNode_ColorRect>
    {
        public UiNode_ColorRect(Godot.ColorRect node) : base(node) {  }
        public override UiNode_ColorRect Clone() => new ((Godot.ColorRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: Loading.Label
    /// </summary>
    public class UiNode_Label : IUiNode<Godot.Label, UiNode_Label>
    {
        public UiNode_Label(Godot.Label node) : base(node) {  }
        public override UiNode_Label Clone() => new ((Godot.Label)Instance.Duplicate());
    }

}
