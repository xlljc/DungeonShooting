namespace UI.EditorInput;

/// <summary>
/// Ui代码, 该类是根据ui场景自动生成的, 请不要手动编辑该类, 以免造成代码丢失
/// </summary>
public abstract partial class EditorInput : UiBase
{
    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: EditorInput.VBoxContainer
    /// </summary>
    public VBoxContainer L_VBoxContainer
    {
        get
        {
            if (_L_VBoxContainer == null) _L_VBoxContainer = new VBoxContainer((EditorInputPanel)this, GetNode<Godot.VBoxContainer>("VBoxContainer"));
            return _L_VBoxContainer;
        }
    }
    private VBoxContainer _L_VBoxContainer;


    public EditorInput() : base(nameof(EditorInput))
    {
    }

    public sealed override void OnInitNestedUi()
    {

    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: EditorInput.VBoxContainer.Label
    /// </summary>
    public class Label : UiNode<EditorInputPanel, Godot.Label, Label>
    {
        public Label(EditorInputPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Label Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.LineEdit"/>, 路径: EditorInput.VBoxContainer.LineEdit
    /// </summary>
    public class LineEdit : UiNode<EditorInputPanel, Godot.LineEdit, LineEdit>
    {
        public LineEdit(EditorInputPanel uiPanel, Godot.LineEdit node) : base(uiPanel, node) {  }
        public override LineEdit Clone() => new (UiPanel, (Godot.LineEdit)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.VBoxContainer"/>, 路径: EditorInput.VBoxContainer
    /// </summary>
    public class VBoxContainer : UiNode<EditorInputPanel, Godot.VBoxContainer, VBoxContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: EditorInput.Label
        /// </summary>
        public Label L_Label
        {
            get
            {
                if (_L_Label == null) _L_Label = new Label(UiPanel, Instance.GetNode<Godot.Label>("Label"));
                return _L_Label;
            }
        }
        private Label _L_Label;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.LineEdit"/>, 节点路径: EditorInput.LineEdit
        /// </summary>
        public LineEdit L_LineEdit
        {
            get
            {
                if (_L_LineEdit == null) _L_LineEdit = new LineEdit(UiPanel, Instance.GetNode<Godot.LineEdit>("LineEdit"));
                return _L_LineEdit;
            }
        }
        private LineEdit _L_LineEdit;

        public VBoxContainer(EditorInputPanel uiPanel, Godot.VBoxContainer node) : base(uiPanel, node) {  }
        public override VBoxContainer Clone() => new (UiPanel, (Godot.VBoxContainer)Instance.Duplicate());
    }


    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: EditorInput.VBoxContainer.Label
    /// </summary>
    public Label S_Label => L_VBoxContainer.L_Label;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.LineEdit"/>, 节点路径: EditorInput.VBoxContainer.LineEdit
    /// </summary>
    public LineEdit S_LineEdit => L_VBoxContainer.L_LineEdit;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: EditorInput.VBoxContainer
    /// </summary>
    public VBoxContainer S_VBoxContainer => L_VBoxContainer;

}
