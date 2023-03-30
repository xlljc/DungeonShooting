namespace UI.MyUi;

/// <summary>
/// Ui代码, 该类是根据ui场景自动生成的, 请不要手动编辑该类, 以免造成代码丢失
/// </summary>
public abstract partial class MyUi : UiBase
{
    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: MyUi.Button
    /// </summary>
    public UiNode_Button L_Button
    {
        get
        {
            if (_L_Button == null) _L_Button = new UiNode_Button(GetNodeOrNull<Godot.Button>("Button"));
            return _L_Button;
        }
    }
    private UiNode_Button _L_Button;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Control"/>, 节点路径: MyUi.Control
    /// </summary>
    public UiNode_Control L_Control
    {
        get
        {
            if (_L_Control == null) _L_Control = new UiNode_Control(GetNodeOrNull<Godot.Control>("Control"));
            return _L_Control;
        }
    }
    private UiNode_Control _L_Control;


    public MyUi() : base(nameof(MyUi))
    {
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: MyUi.Button
    /// </summary>
    public class UiNode_Button : IUiNode<Godot.Button, UiNode_Button>
    {
        public UiNode_Button(Godot.Button node) : base(node) {  }
        public override UiNode_Button Clone() => new ((Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: MyUi.Control.Label
    /// </summary>
    public class UiNode_Label : IUiNode<Godot.Label, UiNode_Label>
    {
        public UiNode_Label(Godot.Label node) : base(node) {  }
        public override UiNode_Label Clone() => new ((Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Control"/>, 路径: MyUi.Control
    /// </summary>
    public class UiNode_Control : IUiNode<Godot.Control, UiNode_Control>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: MyUi.Label
        /// </summary>
        public UiNode_Label L_Label
        {
            get
            {
                if (_L_Label == null) _L_Label = new UiNode_Label(Instance.GetNodeOrNull<Godot.Label>("Label"));
                return _L_Label;
            }
        }
        private UiNode_Label _L_Label;

        public UiNode_Control(Godot.Control node) : base(node) {  }
        public override UiNode_Control Clone() => new ((Godot.Control)Instance.Duplicate());
    }

}
