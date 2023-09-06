namespace UI.MapEditorCreatePreinstall;

/// <summary>
/// Ui代码, 该类是根据ui场景自动生成的, 请不要手动编辑该类, 以免造成代码丢失
/// </summary>
public abstract partial class MapEditorCreatePreinstall : UiBase
{
    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: MapEditorCreatePreinstall.MarginContainer
    /// </summary>
    public MarginContainer L_MarginContainer
    {
        get
        {
            if (_L_MarginContainer == null) _L_MarginContainer = new MarginContainer((MapEditorCreatePreinstallPanel)this, GetNodeOrNull<Godot.MarginContainer>("MarginContainer"));
            return _L_MarginContainer;
        }
    }
    private MarginContainer _L_MarginContainer;


    public MapEditorCreatePreinstall() : base(nameof(MapEditorCreatePreinstall))
    {
    }

    public sealed override void OnInitNestedUi()
    {
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: MapEditorCreatePreinstall.MarginContainer.VBoxContainer.HBoxContainer.PreinstallNameLabel
    /// </summary>
    public class PreinstallNameLabel : UiNode<MapEditorCreatePreinstallPanel, Godot.Label, PreinstallNameLabel>
    {
        public PreinstallNameLabel(MapEditorCreatePreinstallPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override PreinstallNameLabel Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.LineEdit"/>, 路径: MapEditorCreatePreinstall.MarginContainer.VBoxContainer.HBoxContainer.PreinstallNameInput
    /// </summary>
    public class PreinstallNameInput : UiNode<MapEditorCreatePreinstallPanel, Godot.LineEdit, PreinstallNameInput>
    {
        public PreinstallNameInput(MapEditorCreatePreinstallPanel uiPanel, Godot.LineEdit node) : base(uiPanel, node) {  }
        public override PreinstallNameInput Clone() => new (UiPanel, (Godot.LineEdit)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: MapEditorCreatePreinstall.MarginContainer.VBoxContainer.HBoxContainer
    /// </summary>
    public class HBoxContainer : UiNode<MapEditorCreatePreinstallPanel, Godot.HBoxContainer, HBoxContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorCreatePreinstall.MarginContainer.VBoxContainer.PreinstallNameLabel
        /// </summary>
        public PreinstallNameLabel L_PreinstallNameLabel
        {
            get
            {
                if (_L_PreinstallNameLabel == null) _L_PreinstallNameLabel = new PreinstallNameLabel(UiPanel, Instance.GetNodeOrNull<Godot.Label>("PreinstallNameLabel"));
                return _L_PreinstallNameLabel;
            }
        }
        private PreinstallNameLabel _L_PreinstallNameLabel;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.LineEdit"/>, 节点路径: MapEditorCreatePreinstall.MarginContainer.VBoxContainer.PreinstallNameInput
        /// </summary>
        public PreinstallNameInput L_PreinstallNameInput
        {
            get
            {
                if (_L_PreinstallNameInput == null) _L_PreinstallNameInput = new PreinstallNameInput(UiPanel, Instance.GetNodeOrNull<Godot.LineEdit>("PreinstallNameInput"));
                return _L_PreinstallNameInput;
            }
        }
        private PreinstallNameInput _L_PreinstallNameInput;

        public HBoxContainer(MapEditorCreatePreinstallPanel uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override HBoxContainer Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: MapEditorCreatePreinstall.MarginContainer.VBoxContainer.HBoxContainer4.WeightNameLabel
    /// </summary>
    public class WeightNameLabel : UiNode<MapEditorCreatePreinstallPanel, Godot.Label, WeightNameLabel>
    {
        public WeightNameLabel(MapEditorCreatePreinstallPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override WeightNameLabel Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.SpinBox"/>, 路径: MapEditorCreatePreinstall.MarginContainer.VBoxContainer.HBoxContainer4.WeightInput
    /// </summary>
    public class WeightInput : UiNode<MapEditorCreatePreinstallPanel, Godot.SpinBox, WeightInput>
    {
        public WeightInput(MapEditorCreatePreinstallPanel uiPanel, Godot.SpinBox node) : base(uiPanel, node) {  }
        public override WeightInput Clone() => new (UiPanel, (Godot.SpinBox)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: MapEditorCreatePreinstall.MarginContainer.VBoxContainer.HBoxContainer4
    /// </summary>
    public class HBoxContainer4 : UiNode<MapEditorCreatePreinstallPanel, Godot.HBoxContainer, HBoxContainer4>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorCreatePreinstall.MarginContainer.VBoxContainer.WeightNameLabel
        /// </summary>
        public WeightNameLabel L_WeightNameLabel
        {
            get
            {
                if (_L_WeightNameLabel == null) _L_WeightNameLabel = new WeightNameLabel(UiPanel, Instance.GetNodeOrNull<Godot.Label>("WeightNameLabel"));
                return _L_WeightNameLabel;
            }
        }
        private WeightNameLabel _L_WeightNameLabel;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.SpinBox"/>, 节点路径: MapEditorCreatePreinstall.MarginContainer.VBoxContainer.WeightInput
        /// </summary>
        public WeightInput L_WeightInput
        {
            get
            {
                if (_L_WeightInput == null) _L_WeightInput = new WeightInput(UiPanel, Instance.GetNodeOrNull<Godot.SpinBox>("WeightInput"));
                return _L_WeightInput;
            }
        }
        private WeightInput _L_WeightInput;

        public HBoxContainer4(MapEditorCreatePreinstallPanel uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override HBoxContainer4 Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: MapEditorCreatePreinstall.MarginContainer.VBoxContainer.HBoxContainer5.RemarkNameLabel
    /// </summary>
    public class RemarkNameLabel : UiNode<MapEditorCreatePreinstallPanel, Godot.Label, RemarkNameLabel>
    {
        public RemarkNameLabel(MapEditorCreatePreinstallPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override RemarkNameLabel Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextEdit"/>, 路径: MapEditorCreatePreinstall.MarginContainer.VBoxContainer.HBoxContainer5.RemarkInput
    /// </summary>
    public class RemarkInput : UiNode<MapEditorCreatePreinstallPanel, Godot.TextEdit, RemarkInput>
    {
        public RemarkInput(MapEditorCreatePreinstallPanel uiPanel, Godot.TextEdit node) : base(uiPanel, node) {  }
        public override RemarkInput Clone() => new (UiPanel, (Godot.TextEdit)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: MapEditorCreatePreinstall.MarginContainer.VBoxContainer.HBoxContainer5
    /// </summary>
    public class HBoxContainer5 : UiNode<MapEditorCreatePreinstallPanel, Godot.HBoxContainer, HBoxContainer5>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorCreatePreinstall.MarginContainer.VBoxContainer.RemarkNameLabel
        /// </summary>
        public RemarkNameLabel L_RemarkNameLabel
        {
            get
            {
                if (_L_RemarkNameLabel == null) _L_RemarkNameLabel = new RemarkNameLabel(UiPanel, Instance.GetNodeOrNull<Godot.Label>("RemarkNameLabel"));
                return _L_RemarkNameLabel;
            }
        }
        private RemarkNameLabel _L_RemarkNameLabel;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextEdit"/>, 节点路径: MapEditorCreatePreinstall.MarginContainer.VBoxContainer.RemarkInput
        /// </summary>
        public RemarkInput L_RemarkInput
        {
            get
            {
                if (_L_RemarkInput == null) _L_RemarkInput = new RemarkInput(UiPanel, Instance.GetNodeOrNull<Godot.TextEdit>("RemarkInput"));
                return _L_RemarkInput;
            }
        }
        private RemarkInput _L_RemarkInput;

        public HBoxContainer5(MapEditorCreatePreinstallPanel uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override HBoxContainer5 Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.VBoxContainer"/>, 路径: MapEditorCreatePreinstall.MarginContainer.VBoxContainer
    /// </summary>
    public class VBoxContainer : UiNode<MapEditorCreatePreinstallPanel, Godot.VBoxContainer, VBoxContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditorCreatePreinstall.MarginContainer.HBoxContainer
        /// </summary>
        public HBoxContainer L_HBoxContainer
        {
            get
            {
                if (_L_HBoxContainer == null) _L_HBoxContainer = new HBoxContainer(UiPanel, Instance.GetNodeOrNull<Godot.HBoxContainer>("HBoxContainer"));
                return _L_HBoxContainer;
            }
        }
        private HBoxContainer _L_HBoxContainer;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditorCreatePreinstall.MarginContainer.HBoxContainer4
        /// </summary>
        public HBoxContainer4 L_HBoxContainer4
        {
            get
            {
                if (_L_HBoxContainer4 == null) _L_HBoxContainer4 = new HBoxContainer4(UiPanel, Instance.GetNodeOrNull<Godot.HBoxContainer>("HBoxContainer4"));
                return _L_HBoxContainer4;
            }
        }
        private HBoxContainer4 _L_HBoxContainer4;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditorCreatePreinstall.MarginContainer.HBoxContainer5
        /// </summary>
        public HBoxContainer5 L_HBoxContainer5
        {
            get
            {
                if (_L_HBoxContainer5 == null) _L_HBoxContainer5 = new HBoxContainer5(UiPanel, Instance.GetNodeOrNull<Godot.HBoxContainer>("HBoxContainer5"));
                return _L_HBoxContainer5;
            }
        }
        private HBoxContainer5 _L_HBoxContainer5;

        public VBoxContainer(MapEditorCreatePreinstallPanel uiPanel, Godot.VBoxContainer node) : base(uiPanel, node) {  }
        public override VBoxContainer Clone() => new (UiPanel, (Godot.VBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.MarginContainer"/>, 路径: MapEditorCreatePreinstall.MarginContainer
    /// </summary>
    public class MarginContainer : UiNode<MapEditorCreatePreinstallPanel, Godot.MarginContainer, MarginContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: MapEditorCreatePreinstall.VBoxContainer
        /// </summary>
        public VBoxContainer L_VBoxContainer
        {
            get
            {
                if (_L_VBoxContainer == null) _L_VBoxContainer = new VBoxContainer(UiPanel, Instance.GetNodeOrNull<Godot.VBoxContainer>("VBoxContainer"));
                return _L_VBoxContainer;
            }
        }
        private VBoxContainer _L_VBoxContainer;

        public MarginContainer(MapEditorCreatePreinstallPanel uiPanel, Godot.MarginContainer node) : base(uiPanel, node) {  }
        public override MarginContainer Clone() => new (UiPanel, (Godot.MarginContainer)Instance.Duplicate());
    }


    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorCreatePreinstall.MarginContainer.VBoxContainer.HBoxContainer.PreinstallNameLabel
    /// </summary>
    public PreinstallNameLabel S_PreinstallNameLabel => L_MarginContainer.L_VBoxContainer.L_HBoxContainer.L_PreinstallNameLabel;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.LineEdit"/>, 节点路径: MapEditorCreatePreinstall.MarginContainer.VBoxContainer.HBoxContainer.PreinstallNameInput
    /// </summary>
    public PreinstallNameInput S_PreinstallNameInput => L_MarginContainer.L_VBoxContainer.L_HBoxContainer.L_PreinstallNameInput;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditorCreatePreinstall.MarginContainer.VBoxContainer.HBoxContainer
    /// </summary>
    public HBoxContainer S_HBoxContainer => L_MarginContainer.L_VBoxContainer.L_HBoxContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorCreatePreinstall.MarginContainer.VBoxContainer.HBoxContainer4.WeightNameLabel
    /// </summary>
    public WeightNameLabel S_WeightNameLabel => L_MarginContainer.L_VBoxContainer.L_HBoxContainer4.L_WeightNameLabel;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.SpinBox"/>, 节点路径: MapEditorCreatePreinstall.MarginContainer.VBoxContainer.HBoxContainer4.WeightInput
    /// </summary>
    public WeightInput S_WeightInput => L_MarginContainer.L_VBoxContainer.L_HBoxContainer4.L_WeightInput;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditorCreatePreinstall.MarginContainer.VBoxContainer.HBoxContainer4
    /// </summary>
    public HBoxContainer4 S_HBoxContainer4 => L_MarginContainer.L_VBoxContainer.L_HBoxContainer4;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorCreatePreinstall.MarginContainer.VBoxContainer.HBoxContainer5.RemarkNameLabel
    /// </summary>
    public RemarkNameLabel S_RemarkNameLabel => L_MarginContainer.L_VBoxContainer.L_HBoxContainer5.L_RemarkNameLabel;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextEdit"/>, 节点路径: MapEditorCreatePreinstall.MarginContainer.VBoxContainer.HBoxContainer5.RemarkInput
    /// </summary>
    public RemarkInput S_RemarkInput => L_MarginContainer.L_VBoxContainer.L_HBoxContainer5.L_RemarkInput;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditorCreatePreinstall.MarginContainer.VBoxContainer.HBoxContainer5
    /// </summary>
    public HBoxContainer5 S_HBoxContainer5 => L_MarginContainer.L_VBoxContainer.L_HBoxContainer5;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: MapEditorCreatePreinstall.MarginContainer.VBoxContainer
    /// </summary>
    public VBoxContainer S_VBoxContainer => L_MarginContainer.L_VBoxContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: MapEditorCreatePreinstall.MarginContainer
    /// </summary>
    public MarginContainer S_MarginContainer => L_MarginContainer;

}
