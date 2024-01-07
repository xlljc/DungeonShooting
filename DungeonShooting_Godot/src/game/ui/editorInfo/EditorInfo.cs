namespace UI.EditorInfo;

/// <summary>
/// Ui代码, 该类是根据ui场景自动生成的, 请不要手动编辑该类, 以免造成代码丢失
/// </summary>
public abstract partial class EditorInfo : UiBase
{
    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: EditorInfo.MarginContainer
    /// </summary>
    public MarginContainer L_MarginContainer
    {
        get
        {
            if (_L_MarginContainer == null) _L_MarginContainer = new MarginContainer((EditorInfoPanel)this, GetNode<Godot.MarginContainer>("MarginContainer"));
            return _L_MarginContainer;
        }
    }
    private MarginContainer _L_MarginContainer;


    public EditorInfo() : base(nameof(EditorInfo))
    {
    }

    public sealed override void OnInitNestedUi()
    {

    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: EditorInfo.MarginContainer.VBoxContainer.HBoxContainer.NameLabel
    /// </summary>
    public class NameLabel : UiNode<EditorInfoPanel, Godot.Label, NameLabel>
    {
        public NameLabel(EditorInfoPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override NameLabel Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.LineEdit"/>, 路径: EditorInfo.MarginContainer.VBoxContainer.HBoxContainer.NameInput
    /// </summary>
    public class NameInput : UiNode<EditorInfoPanel, Godot.LineEdit, NameInput>
    {
        public NameInput(EditorInfoPanel uiPanel, Godot.LineEdit node) : base(uiPanel, node) {  }
        public override NameInput Clone() => new (UiPanel, (Godot.LineEdit)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: EditorInfo.MarginContainer.VBoxContainer.HBoxContainer
    /// </summary>
    public class HBoxContainer : UiNode<EditorInfoPanel, Godot.HBoxContainer, HBoxContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: EditorInfo.MarginContainer.VBoxContainer.NameLabel
        /// </summary>
        public NameLabel L_NameLabel
        {
            get
            {
                if (_L_NameLabel == null) _L_NameLabel = new NameLabel(UiPanel, Instance.GetNode<Godot.Label>("NameLabel"));
                return _L_NameLabel;
            }
        }
        private NameLabel _L_NameLabel;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.LineEdit"/>, 节点路径: EditorInfo.MarginContainer.VBoxContainer.NameInput
        /// </summary>
        public NameInput L_NameInput
        {
            get
            {
                if (_L_NameInput == null) _L_NameInput = new NameInput(UiPanel, Instance.GetNode<Godot.LineEdit>("NameInput"));
                return _L_NameInput;
            }
        }
        private NameInput _L_NameInput;

        public HBoxContainer(EditorInfoPanel uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override HBoxContainer Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: EditorInfo.MarginContainer.VBoxContainer.HBoxContainer5.RemarkNameLabel
    /// </summary>
    public class RemarkNameLabel : UiNode<EditorInfoPanel, Godot.Label, RemarkNameLabel>
    {
        public RemarkNameLabel(EditorInfoPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override RemarkNameLabel Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextEdit"/>, 路径: EditorInfo.MarginContainer.VBoxContainer.HBoxContainer5.RemarkInput
    /// </summary>
    public class RemarkInput : UiNode<EditorInfoPanel, Godot.TextEdit, RemarkInput>
    {
        public RemarkInput(EditorInfoPanel uiPanel, Godot.TextEdit node) : base(uiPanel, node) {  }
        public override RemarkInput Clone() => new (UiPanel, (Godot.TextEdit)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: EditorInfo.MarginContainer.VBoxContainer.HBoxContainer5
    /// </summary>
    public class HBoxContainer5 : UiNode<EditorInfoPanel, Godot.HBoxContainer, HBoxContainer5>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: EditorInfo.MarginContainer.VBoxContainer.RemarkNameLabel
        /// </summary>
        public RemarkNameLabel L_RemarkNameLabel
        {
            get
            {
                if (_L_RemarkNameLabel == null) _L_RemarkNameLabel = new RemarkNameLabel(UiPanel, Instance.GetNode<Godot.Label>("RemarkNameLabel"));
                return _L_RemarkNameLabel;
            }
        }
        private RemarkNameLabel _L_RemarkNameLabel;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextEdit"/>, 节点路径: EditorInfo.MarginContainer.VBoxContainer.RemarkInput
        /// </summary>
        public RemarkInput L_RemarkInput
        {
            get
            {
                if (_L_RemarkInput == null) _L_RemarkInput = new RemarkInput(UiPanel, Instance.GetNode<Godot.TextEdit>("RemarkInput"));
                return _L_RemarkInput;
            }
        }
        private RemarkInput _L_RemarkInput;

        public HBoxContainer5(EditorInfoPanel uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override HBoxContainer5 Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.VBoxContainer"/>, 路径: EditorInfo.MarginContainer.VBoxContainer
    /// </summary>
    public class VBoxContainer : UiNode<EditorInfoPanel, Godot.VBoxContainer, VBoxContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: EditorInfo.MarginContainer.HBoxContainer
        /// </summary>
        public HBoxContainer L_HBoxContainer
        {
            get
            {
                if (_L_HBoxContainer == null) _L_HBoxContainer = new HBoxContainer(UiPanel, Instance.GetNode<Godot.HBoxContainer>("HBoxContainer"));
                return _L_HBoxContainer;
            }
        }
        private HBoxContainer _L_HBoxContainer;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: EditorInfo.MarginContainer.HBoxContainer5
        /// </summary>
        public HBoxContainer5 L_HBoxContainer5
        {
            get
            {
                if (_L_HBoxContainer5 == null) _L_HBoxContainer5 = new HBoxContainer5(UiPanel, Instance.GetNode<Godot.HBoxContainer>("HBoxContainer5"));
                return _L_HBoxContainer5;
            }
        }
        private HBoxContainer5 _L_HBoxContainer5;

        public VBoxContainer(EditorInfoPanel uiPanel, Godot.VBoxContainer node) : base(uiPanel, node) {  }
        public override VBoxContainer Clone() => new (UiPanel, (Godot.VBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.MarginContainer"/>, 路径: EditorInfo.MarginContainer
    /// </summary>
    public class MarginContainer : UiNode<EditorInfoPanel, Godot.MarginContainer, MarginContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: EditorInfo.VBoxContainer
        /// </summary>
        public VBoxContainer L_VBoxContainer
        {
            get
            {
                if (_L_VBoxContainer == null) _L_VBoxContainer = new VBoxContainer(UiPanel, Instance.GetNode<Godot.VBoxContainer>("VBoxContainer"));
                return _L_VBoxContainer;
            }
        }
        private VBoxContainer _L_VBoxContainer;

        public MarginContainer(EditorInfoPanel uiPanel, Godot.MarginContainer node) : base(uiPanel, node) {  }
        public override MarginContainer Clone() => new (UiPanel, (Godot.MarginContainer)Instance.Duplicate());
    }


    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: EditorInfo.MarginContainer.VBoxContainer.HBoxContainer.NameLabel
    /// </summary>
    public NameLabel S_NameLabel => L_MarginContainer.L_VBoxContainer.L_HBoxContainer.L_NameLabel;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.LineEdit"/>, 节点路径: EditorInfo.MarginContainer.VBoxContainer.HBoxContainer.NameInput
    /// </summary>
    public NameInput S_NameInput => L_MarginContainer.L_VBoxContainer.L_HBoxContainer.L_NameInput;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: EditorInfo.MarginContainer.VBoxContainer.HBoxContainer
    /// </summary>
    public HBoxContainer S_HBoxContainer => L_MarginContainer.L_VBoxContainer.L_HBoxContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: EditorInfo.MarginContainer.VBoxContainer.HBoxContainer5.RemarkNameLabel
    /// </summary>
    public RemarkNameLabel S_RemarkNameLabel => L_MarginContainer.L_VBoxContainer.L_HBoxContainer5.L_RemarkNameLabel;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextEdit"/>, 节点路径: EditorInfo.MarginContainer.VBoxContainer.HBoxContainer5.RemarkInput
    /// </summary>
    public RemarkInput S_RemarkInput => L_MarginContainer.L_VBoxContainer.L_HBoxContainer5.L_RemarkInput;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: EditorInfo.MarginContainer.VBoxContainer.HBoxContainer5
    /// </summary>
    public HBoxContainer5 S_HBoxContainer5 => L_MarginContainer.L_VBoxContainer.L_HBoxContainer5;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: EditorInfo.MarginContainer.VBoxContainer
    /// </summary>
    public VBoxContainer S_VBoxContainer => L_MarginContainer.L_VBoxContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: EditorInfo.MarginContainer
    /// </summary>
    public MarginContainer S_MarginContainer => L_MarginContainer;

}
