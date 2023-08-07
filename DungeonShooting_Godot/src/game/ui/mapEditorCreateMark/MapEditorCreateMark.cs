namespace UI.MapEditorCreateMark;

/// <summary>
/// Ui代码, 该类是根据ui场景自动生成的, 请不要手动编辑该类, 以免造成代码丢失
/// </summary>
public abstract partial class MapEditorCreateMark : UiBase
{
    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: MapEditorCreateMark.MarginContainer
    /// </summary>
    public MarginContainer L_MarginContainer
    {
        get
        {
            if (_L_MarginContainer == null) _L_MarginContainer = new MarginContainer((MapEditorCreateMarkPanel)this, GetNodeOrNull<Godot.MarginContainer>("MarginContainer"));
            return _L_MarginContainer;
        }
    }
    private MarginContainer _L_MarginContainer;


    public MapEditorCreateMark() : base(nameof(MapEditorCreateMark))
    {
    }

    public sealed override void OnInitNestedUi()
    {
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: MapEditorCreateMark.MarginContainer.VBoxContainer.HBoxContainer2.WaveNameLabel
    /// </summary>
    public class WaveNameLabel : UiNode<MapEditorCreateMarkPanel, Godot.Label, WaveNameLabel>
    {
        public WaveNameLabel(MapEditorCreateMarkPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override WaveNameLabel Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.OptionButton"/>, 路径: MapEditorCreateMark.MarginContainer.VBoxContainer.HBoxContainer2.WaveOption
    /// </summary>
    public class WaveOption : UiNode<MapEditorCreateMarkPanel, Godot.OptionButton, WaveOption>
    {
        public WaveOption(MapEditorCreateMarkPanel uiPanel, Godot.OptionButton node) : base(uiPanel, node) {  }
        public override WaveOption Clone() => new (UiPanel, (Godot.OptionButton)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: MapEditorCreateMark.MarginContainer.VBoxContainer.HBoxContainer2
    /// </summary>
    public class HBoxContainer2 : UiNode<MapEditorCreateMarkPanel, Godot.HBoxContainer, HBoxContainer2>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.WaveNameLabel
        /// </summary>
        public WaveNameLabel L_WaveNameLabel
        {
            get
            {
                if (_L_WaveNameLabel == null) _L_WaveNameLabel = new WaveNameLabel(UiPanel, Instance.GetNodeOrNull<Godot.Label>("WaveNameLabel"));
                return _L_WaveNameLabel;
            }
        }
        private WaveNameLabel _L_WaveNameLabel;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.OptionButton"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.WaveOption
        /// </summary>
        public WaveOption L_WaveOption
        {
            get
            {
                if (_L_WaveOption == null) _L_WaveOption = new WaveOption(UiPanel, Instance.GetNodeOrNull<Godot.OptionButton>("WaveOption"));
                return _L_WaveOption;
            }
        }
        private WaveOption _L_WaveOption;

        public HBoxContainer2(MapEditorCreateMarkPanel uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override HBoxContainer2 Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: MapEditorCreateMark.MarginContainer.VBoxContainer.HBoxContainer.DelayNameLabel
    /// </summary>
    public class DelayNameLabel : UiNode<MapEditorCreateMarkPanel, Godot.Label, DelayNameLabel>
    {
        public DelayNameLabel(MapEditorCreateMarkPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override DelayNameLabel Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.SpinBox"/>, 路径: MapEditorCreateMark.MarginContainer.VBoxContainer.HBoxContainer.DelayInput
    /// </summary>
    public class DelayInput : UiNode<MapEditorCreateMarkPanel, Godot.SpinBox, DelayInput>
    {
        public DelayInput(MapEditorCreateMarkPanel uiPanel, Godot.SpinBox node) : base(uiPanel, node) {  }
        public override DelayInput Clone() => new (UiPanel, (Godot.SpinBox)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: MapEditorCreateMark.MarginContainer.VBoxContainer.HBoxContainer
    /// </summary>
    public class HBoxContainer : UiNode<MapEditorCreateMarkPanel, Godot.HBoxContainer, HBoxContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.DelayNameLabel
        /// </summary>
        public DelayNameLabel L_DelayNameLabel
        {
            get
            {
                if (_L_DelayNameLabel == null) _L_DelayNameLabel = new DelayNameLabel(UiPanel, Instance.GetNodeOrNull<Godot.Label>("DelayNameLabel"));
                return _L_DelayNameLabel;
            }
        }
        private DelayNameLabel _L_DelayNameLabel;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.SpinBox"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.DelayInput
        /// </summary>
        public DelayInput L_DelayInput
        {
            get
            {
                if (_L_DelayInput == null) _L_DelayInput = new DelayInput(UiPanel, Instance.GetNodeOrNull<Godot.SpinBox>("DelayInput"));
                return _L_DelayInput;
            }
        }
        private DelayInput _L_DelayInput;

        public HBoxContainer(MapEditorCreateMarkPanel uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override HBoxContainer Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: MapEditorCreateMark.MarginContainer.VBoxContainer.AddMark
    /// </summary>
    public class AddMark : UiNode<MapEditorCreateMarkPanel, Godot.Button, AddMark>
    {
        public AddMark(MapEditorCreateMarkPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override AddMark Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.VBoxContainer"/>, 路径: MapEditorCreateMark.MarginContainer.VBoxContainer
    /// </summary>
    public class VBoxContainer : UiNode<MapEditorCreateMarkPanel, Godot.VBoxContainer, VBoxContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditorCreateMark.MarginContainer.HBoxContainer2
        /// </summary>
        public HBoxContainer2 L_HBoxContainer2
        {
            get
            {
                if (_L_HBoxContainer2 == null) _L_HBoxContainer2 = new HBoxContainer2(UiPanel, Instance.GetNodeOrNull<Godot.HBoxContainer>("HBoxContainer2"));
                return _L_HBoxContainer2;
            }
        }
        private HBoxContainer2 _L_HBoxContainer2;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditorCreateMark.MarginContainer.HBoxContainer
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorCreateMark.MarginContainer.AddMark
        /// </summary>
        public AddMark L_AddMark
        {
            get
            {
                if (_L_AddMark == null) _L_AddMark = new AddMark(UiPanel, Instance.GetNodeOrNull<Godot.Button>("AddMark"));
                return _L_AddMark;
            }
        }
        private AddMark _L_AddMark;

        public VBoxContainer(MapEditorCreateMarkPanel uiPanel, Godot.VBoxContainer node) : base(uiPanel, node) {  }
        public override VBoxContainer Clone() => new (UiPanel, (Godot.VBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.MarginContainer"/>, 路径: MapEditorCreateMark.MarginContainer
    /// </summary>
    public class MarginContainer : UiNode<MapEditorCreateMarkPanel, Godot.MarginContainer, MarginContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: MapEditorCreateMark.VBoxContainer
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

        public MarginContainer(MapEditorCreateMarkPanel uiPanel, Godot.MarginContainer node) : base(uiPanel, node) {  }
        public override MarginContainer Clone() => new (UiPanel, (Godot.MarginContainer)Instance.Duplicate());
    }


    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.HBoxContainer2.WaveNameLabel
    /// </summary>
    public WaveNameLabel S_WaveNameLabel => L_MarginContainer.L_VBoxContainer.L_HBoxContainer2.L_WaveNameLabel;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.OptionButton"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.HBoxContainer2.WaveOption
    /// </summary>
    public WaveOption S_WaveOption => L_MarginContainer.L_VBoxContainer.L_HBoxContainer2.L_WaveOption;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.HBoxContainer2
    /// </summary>
    public HBoxContainer2 S_HBoxContainer2 => L_MarginContainer.L_VBoxContainer.L_HBoxContainer2;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.HBoxContainer.DelayNameLabel
    /// </summary>
    public DelayNameLabel S_DelayNameLabel => L_MarginContainer.L_VBoxContainer.L_HBoxContainer.L_DelayNameLabel;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.SpinBox"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.HBoxContainer.DelayInput
    /// </summary>
    public DelayInput S_DelayInput => L_MarginContainer.L_VBoxContainer.L_HBoxContainer.L_DelayInput;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.HBoxContainer
    /// </summary>
    public HBoxContainer S_HBoxContainer => L_MarginContainer.L_VBoxContainer.L_HBoxContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.AddMark
    /// </summary>
    public AddMark S_AddMark => L_MarginContainer.L_VBoxContainer.L_AddMark;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer
    /// </summary>
    public VBoxContainer S_VBoxContainer => L_MarginContainer.L_VBoxContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: MapEditorCreateMark.MarginContainer
    /// </summary>
    public MarginContainer S_MarginContainer => L_MarginContainer;

}
