namespace UI.EditorTips;

/// <summary>
/// Ui代码, 该类是根据ui场景自动生成的, 请不要手动编辑该类, 以免造成代码丢失
/// </summary>
public abstract partial class EditorTips : UiBase
{
    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: EditorTips.Label
    /// </summary>
    public Label L_Label
    {
        get
        {
            if (_L_Label == null) _L_Label = new Label(this, GetNodeOrNull<Godot.Label>("Label"));
            return _L_Label;
        }
    }
    private Label _L_Label;


    public EditorTips() : base(nameof(EditorTips))
    {
    }

    public sealed override void OnInitNestedUi()
    {
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: EditorTips.Label
    /// </summary>
    public class Label : UiNode<EditorTips, Godot.Label, Label>
    {
        public Label(EditorTips uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Label Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }


    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: EditorTips.Label
    /// </summary>
    public Label S_Label => L_Label;

}
