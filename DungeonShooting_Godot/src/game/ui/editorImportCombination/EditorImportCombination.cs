namespace UI.EditorImportCombination;

/// <summary>
/// Ui代码, 该类是根据ui场景自动生成的, 请不要手动编辑该类, 以免造成代码丢失
/// </summary>
public abstract partial class EditorImportCombination : UiBase
{
    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: EditorImportCombination.MarginContainer
    /// </summary>
    public MarginContainer L_MarginContainer
    {
        get
        {
            if (_L_MarginContainer == null) _L_MarginContainer = new MarginContainer((EditorImportCombinationPanel)this, GetNode<Godot.MarginContainer>("MarginContainer"));
            return _L_MarginContainer;
        }
    }
    private MarginContainer _L_MarginContainer;


    public EditorImportCombination() : base(nameof(EditorImportCombination))
    {
    }

    public sealed override void OnInitNestedUi()
    {

    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: EditorImportCombination.MarginContainer.VBoxContainer.HBoxContainer.NameLabel
    /// </summary>
    public class NameLabel : UiNode<EditorImportCombinationPanel, Godot.Label, NameLabel>
    {
        public NameLabel(EditorImportCombinationPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override NameLabel Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.LineEdit"/>, 路径: EditorImportCombination.MarginContainer.VBoxContainer.HBoxContainer.NameInput
    /// </summary>
    public class NameInput : UiNode<EditorImportCombinationPanel, Godot.LineEdit, NameInput>
    {
        public NameInput(EditorImportCombinationPanel uiPanel, Godot.LineEdit node) : base(uiPanel, node) {  }
        public override NameInput Clone() => new (UiPanel, (Godot.LineEdit)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: EditorImportCombination.MarginContainer.VBoxContainer.HBoxContainer
    /// </summary>
    public class HBoxContainer : UiNode<EditorImportCombinationPanel, Godot.HBoxContainer, HBoxContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: EditorImportCombination.MarginContainer.VBoxContainer.NameLabel
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.LineEdit"/>, 节点路径: EditorImportCombination.MarginContainer.VBoxContainer.NameInput
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

        public HBoxContainer(EditorImportCombinationPanel uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override HBoxContainer Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: EditorImportCombination.MarginContainer.VBoxContainer.HBoxContainer2.PreviewLabel
    /// </summary>
    public class PreviewLabel : UiNode<EditorImportCombinationPanel, Godot.Label, PreviewLabel>
    {
        public PreviewLabel(EditorImportCombinationPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override PreviewLabel Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: EditorImportCombination.MarginContainer.VBoxContainer.HBoxContainer2.PreviewTexture
    /// </summary>
    public class PreviewTexture : UiNode<EditorImportCombinationPanel, Godot.TextureRect, PreviewTexture>
    {
        public PreviewTexture(EditorImportCombinationPanel uiPanel, Godot.TextureRect node) : base(uiPanel, node) {  }
        public override PreviewTexture Clone() => new (UiPanel, (Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: EditorImportCombination.MarginContainer.VBoxContainer.HBoxContainer2
    /// </summary>
    public class HBoxContainer2 : UiNode<EditorImportCombinationPanel, Godot.HBoxContainer, HBoxContainer2>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: EditorImportCombination.MarginContainer.VBoxContainer.PreviewLabel
        /// </summary>
        public PreviewLabel L_PreviewLabel
        {
            get
            {
                if (_L_PreviewLabel == null) _L_PreviewLabel = new PreviewLabel(UiPanel, Instance.GetNode<Godot.Label>("PreviewLabel"));
                return _L_PreviewLabel;
            }
        }
        private PreviewLabel _L_PreviewLabel;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: EditorImportCombination.MarginContainer.VBoxContainer.PreviewTexture
        /// </summary>
        public PreviewTexture L_PreviewTexture
        {
            get
            {
                if (_L_PreviewTexture == null) _L_PreviewTexture = new PreviewTexture(UiPanel, Instance.GetNode<Godot.TextureRect>("PreviewTexture"));
                return _L_PreviewTexture;
            }
        }
        private PreviewTexture _L_PreviewTexture;

        public HBoxContainer2(EditorImportCombinationPanel uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override HBoxContainer2 Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.VBoxContainer"/>, 路径: EditorImportCombination.MarginContainer.VBoxContainer
    /// </summary>
    public class VBoxContainer : UiNode<EditorImportCombinationPanel, Godot.VBoxContainer, VBoxContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: EditorImportCombination.MarginContainer.HBoxContainer
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: EditorImportCombination.MarginContainer.HBoxContainer2
        /// </summary>
        public HBoxContainer2 L_HBoxContainer2
        {
            get
            {
                if (_L_HBoxContainer2 == null) _L_HBoxContainer2 = new HBoxContainer2(UiPanel, Instance.GetNode<Godot.HBoxContainer>("HBoxContainer2"));
                return _L_HBoxContainer2;
            }
        }
        private HBoxContainer2 _L_HBoxContainer2;

        public VBoxContainer(EditorImportCombinationPanel uiPanel, Godot.VBoxContainer node) : base(uiPanel, node) {  }
        public override VBoxContainer Clone() => new (UiPanel, (Godot.VBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.MarginContainer"/>, 路径: EditorImportCombination.MarginContainer
    /// </summary>
    public class MarginContainer : UiNode<EditorImportCombinationPanel, Godot.MarginContainer, MarginContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: EditorImportCombination.VBoxContainer
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

        public MarginContainer(EditorImportCombinationPanel uiPanel, Godot.MarginContainer node) : base(uiPanel, node) {  }
        public override MarginContainer Clone() => new (UiPanel, (Godot.MarginContainer)Instance.Duplicate());
    }


    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: EditorImportCombination.MarginContainer.VBoxContainer.HBoxContainer.NameLabel
    /// </summary>
    public NameLabel S_NameLabel => L_MarginContainer.L_VBoxContainer.L_HBoxContainer.L_NameLabel;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.LineEdit"/>, 节点路径: EditorImportCombination.MarginContainer.VBoxContainer.HBoxContainer.NameInput
    /// </summary>
    public NameInput S_NameInput => L_MarginContainer.L_VBoxContainer.L_HBoxContainer.L_NameInput;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: EditorImportCombination.MarginContainer.VBoxContainer.HBoxContainer
    /// </summary>
    public HBoxContainer S_HBoxContainer => L_MarginContainer.L_VBoxContainer.L_HBoxContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: EditorImportCombination.MarginContainer.VBoxContainer.HBoxContainer2.PreviewLabel
    /// </summary>
    public PreviewLabel S_PreviewLabel => L_MarginContainer.L_VBoxContainer.L_HBoxContainer2.L_PreviewLabel;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: EditorImportCombination.MarginContainer.VBoxContainer.HBoxContainer2.PreviewTexture
    /// </summary>
    public PreviewTexture S_PreviewTexture => L_MarginContainer.L_VBoxContainer.L_HBoxContainer2.L_PreviewTexture;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: EditorImportCombination.MarginContainer.VBoxContainer.HBoxContainer2
    /// </summary>
    public HBoxContainer2 S_HBoxContainer2 => L_MarginContainer.L_VBoxContainer.L_HBoxContainer2;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: EditorImportCombination.MarginContainer.VBoxContainer
    /// </summary>
    public VBoxContainer S_VBoxContainer => L_MarginContainer.L_VBoxContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: EditorImportCombination.MarginContainer
    /// </summary>
    public MarginContainer S_MarginContainer => L_MarginContainer;

}
