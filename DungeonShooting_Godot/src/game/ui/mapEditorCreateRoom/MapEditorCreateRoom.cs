namespace UI.MapEditorCreateRoom;

/// <summary>
/// Ui代码, 该类是根据ui场景自动生成的, 请不要手动编辑该类, 以免造成代码丢失
/// </summary>
public abstract partial class MapEditorCreateRoom : UiBase
{
    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: MapEditorCreateRoom.MarginContainer
    /// </summary>
    public MarginContainer L_MarginContainer
    {
        get
        {
            if (_L_MarginContainer == null) _L_MarginContainer = new MarginContainer(this, GetNodeOrNull<Godot.MarginContainer>("MarginContainer"));
            return _L_MarginContainer;
        }
    }
    private MarginContainer _L_MarginContainer;


    public MapEditorCreateRoom() : base(nameof(MapEditorCreateRoom))
    {
    }

    public sealed override void OnInitNestedUi()
    {
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: MapEditorCreateRoom.MarginContainer.VBoxContainer.HBoxContainer.RoomNameLabel
    /// </summary>
    public class RoomNameLabel : UiNode<MapEditorCreateRoom, Godot.Label, RoomNameLabel>
    {
        public RoomNameLabel(MapEditorCreateRoom uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override RoomNameLabel Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.LineEdit"/>, 路径: MapEditorCreateRoom.MarginContainer.VBoxContainer.HBoxContainer.RoomNameInput
    /// </summary>
    public class RoomNameInput : UiNode<MapEditorCreateRoom, Godot.LineEdit, RoomNameInput>
    {
        public RoomNameInput(MapEditorCreateRoom uiPanel, Godot.LineEdit node) : base(uiPanel, node) {  }
        public override RoomNameInput Clone() => new (UiPanel, (Godot.LineEdit)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: MapEditorCreateRoom.MarginContainer.VBoxContainer.HBoxContainer
    /// </summary>
    public class HBoxContainer : UiNode<MapEditorCreateRoom, Godot.HBoxContainer, HBoxContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorCreateRoom.MarginContainer.VBoxContainer.RoomNameLabel
        /// </summary>
        public RoomNameLabel L_RoomNameLabel
        {
            get
            {
                if (_L_RoomNameLabel == null) _L_RoomNameLabel = new RoomNameLabel(UiPanel, Instance.GetNodeOrNull<Godot.Label>("RoomNameLabel"));
                return _L_RoomNameLabel;
            }
        }
        private RoomNameLabel _L_RoomNameLabel;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.LineEdit"/>, 节点路径: MapEditorCreateRoom.MarginContainer.VBoxContainer.RoomNameInput
        /// </summary>
        public RoomNameInput L_RoomNameInput
        {
            get
            {
                if (_L_RoomNameInput == null) _L_RoomNameInput = new RoomNameInput(UiPanel, Instance.GetNodeOrNull<Godot.LineEdit>("RoomNameInput"));
                return _L_RoomNameInput;
            }
        }
        private RoomNameInput _L_RoomNameInput;

        public HBoxContainer(MapEditorCreateRoom uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override HBoxContainer Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: MapEditorCreateRoom.MarginContainer.VBoxContainer.HBoxContainer2.GroupNameLabel
    /// </summary>
    public class GroupNameLabel : UiNode<MapEditorCreateRoom, Godot.Label, GroupNameLabel>
    {
        public GroupNameLabel(MapEditorCreateRoom uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override GroupNameLabel Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.OptionButton"/>, 路径: MapEditorCreateRoom.MarginContainer.VBoxContainer.HBoxContainer2.GroupSelect
    /// </summary>
    public class GroupSelect : UiNode<MapEditorCreateRoom, Godot.OptionButton, GroupSelect>
    {
        public GroupSelect(MapEditorCreateRoom uiPanel, Godot.OptionButton node) : base(uiPanel, node) {  }
        public override GroupSelect Clone() => new (UiPanel, (Godot.OptionButton)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: MapEditorCreateRoom.MarginContainer.VBoxContainer.HBoxContainer2
    /// </summary>
    public class HBoxContainer2 : UiNode<MapEditorCreateRoom, Godot.HBoxContainer, HBoxContainer2>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorCreateRoom.MarginContainer.VBoxContainer.GroupNameLabel
        /// </summary>
        public GroupNameLabel L_GroupNameLabel
        {
            get
            {
                if (_L_GroupNameLabel == null) _L_GroupNameLabel = new GroupNameLabel(UiPanel, Instance.GetNodeOrNull<Godot.Label>("GroupNameLabel"));
                return _L_GroupNameLabel;
            }
        }
        private GroupNameLabel _L_GroupNameLabel;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.OptionButton"/>, 节点路径: MapEditorCreateRoom.MarginContainer.VBoxContainer.GroupSelect
        /// </summary>
        public GroupSelect L_GroupSelect
        {
            get
            {
                if (_L_GroupSelect == null) _L_GroupSelect = new GroupSelect(UiPanel, Instance.GetNodeOrNull<Godot.OptionButton>("GroupSelect"));
                return _L_GroupSelect;
            }
        }
        private GroupSelect _L_GroupSelect;

        public HBoxContainer2(MapEditorCreateRoom uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override HBoxContainer2 Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: MapEditorCreateRoom.MarginContainer.VBoxContainer.HBoxContainer3.TypeNameLabel
    /// </summary>
    public class TypeNameLabel : UiNode<MapEditorCreateRoom, Godot.Label, TypeNameLabel>
    {
        public TypeNameLabel(MapEditorCreateRoom uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override TypeNameLabel Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.OptionButton"/>, 路径: MapEditorCreateRoom.MarginContainer.VBoxContainer.HBoxContainer3.TypeSelect
    /// </summary>
    public class TypeSelect : UiNode<MapEditorCreateRoom, Godot.OptionButton, TypeSelect>
    {
        public TypeSelect(MapEditorCreateRoom uiPanel, Godot.OptionButton node) : base(uiPanel, node) {  }
        public override TypeSelect Clone() => new (UiPanel, (Godot.OptionButton)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: MapEditorCreateRoom.MarginContainer.VBoxContainer.HBoxContainer3
    /// </summary>
    public class HBoxContainer3 : UiNode<MapEditorCreateRoom, Godot.HBoxContainer, HBoxContainer3>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorCreateRoom.MarginContainer.VBoxContainer.TypeNameLabel
        /// </summary>
        public TypeNameLabel L_TypeNameLabel
        {
            get
            {
                if (_L_TypeNameLabel == null) _L_TypeNameLabel = new TypeNameLabel(UiPanel, Instance.GetNodeOrNull<Godot.Label>("TypeNameLabel"));
                return _L_TypeNameLabel;
            }
        }
        private TypeNameLabel _L_TypeNameLabel;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.OptionButton"/>, 节点路径: MapEditorCreateRoom.MarginContainer.VBoxContainer.TypeSelect
        /// </summary>
        public TypeSelect L_TypeSelect
        {
            get
            {
                if (_L_TypeSelect == null) _L_TypeSelect = new TypeSelect(UiPanel, Instance.GetNodeOrNull<Godot.OptionButton>("TypeSelect"));
                return _L_TypeSelect;
            }
        }
        private TypeSelect _L_TypeSelect;

        public HBoxContainer3(MapEditorCreateRoom uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override HBoxContainer3 Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: MapEditorCreateRoom.MarginContainer.VBoxContainer.HBoxContainer4.WeightNameLabel
    /// </summary>
    public class WeightNameLabel : UiNode<MapEditorCreateRoom, Godot.Label, WeightNameLabel>
    {
        public WeightNameLabel(MapEditorCreateRoom uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override WeightNameLabel Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.SpinBox"/>, 路径: MapEditorCreateRoom.MarginContainer.VBoxContainer.HBoxContainer4.WeightInput
    /// </summary>
    public class WeightInput : UiNode<MapEditorCreateRoom, Godot.SpinBox, WeightInput>
    {
        public WeightInput(MapEditorCreateRoom uiPanel, Godot.SpinBox node) : base(uiPanel, node) {  }
        public override WeightInput Clone() => new (UiPanel, (Godot.SpinBox)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: MapEditorCreateRoom.MarginContainer.VBoxContainer.HBoxContainer4
    /// </summary>
    public class HBoxContainer4 : UiNode<MapEditorCreateRoom, Godot.HBoxContainer, HBoxContainer4>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorCreateRoom.MarginContainer.VBoxContainer.WeightNameLabel
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.SpinBox"/>, 节点路径: MapEditorCreateRoom.MarginContainer.VBoxContainer.WeightInput
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

        public HBoxContainer4(MapEditorCreateRoom uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override HBoxContainer4 Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: MapEditorCreateRoom.MarginContainer.VBoxContainer.HBoxContainer5.RemarkNameLabel
    /// </summary>
    public class RemarkNameLabel : UiNode<MapEditorCreateRoom, Godot.Label, RemarkNameLabel>
    {
        public RemarkNameLabel(MapEditorCreateRoom uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override RemarkNameLabel Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextEdit"/>, 路径: MapEditorCreateRoom.MarginContainer.VBoxContainer.HBoxContainer5.RemarkInput
    /// </summary>
    public class RemarkInput : UiNode<MapEditorCreateRoom, Godot.TextEdit, RemarkInput>
    {
        public RemarkInput(MapEditorCreateRoom uiPanel, Godot.TextEdit node) : base(uiPanel, node) {  }
        public override RemarkInput Clone() => new (UiPanel, (Godot.TextEdit)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: MapEditorCreateRoom.MarginContainer.VBoxContainer.HBoxContainer5
    /// </summary>
    public class HBoxContainer5 : UiNode<MapEditorCreateRoom, Godot.HBoxContainer, HBoxContainer5>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorCreateRoom.MarginContainer.VBoxContainer.RemarkNameLabel
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextEdit"/>, 节点路径: MapEditorCreateRoom.MarginContainer.VBoxContainer.RemarkInput
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

        public HBoxContainer5(MapEditorCreateRoom uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override HBoxContainer5 Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.VBoxContainer"/>, 路径: MapEditorCreateRoom.MarginContainer.VBoxContainer
    /// </summary>
    public class VBoxContainer : UiNode<MapEditorCreateRoom, Godot.VBoxContainer, VBoxContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditorCreateRoom.MarginContainer.HBoxContainer
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditorCreateRoom.MarginContainer.HBoxContainer2
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditorCreateRoom.MarginContainer.HBoxContainer3
        /// </summary>
        public HBoxContainer3 L_HBoxContainer3
        {
            get
            {
                if (_L_HBoxContainer3 == null) _L_HBoxContainer3 = new HBoxContainer3(UiPanel, Instance.GetNodeOrNull<Godot.HBoxContainer>("HBoxContainer3"));
                return _L_HBoxContainer3;
            }
        }
        private HBoxContainer3 _L_HBoxContainer3;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditorCreateRoom.MarginContainer.HBoxContainer4
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditorCreateRoom.MarginContainer.HBoxContainer5
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

        public VBoxContainer(MapEditorCreateRoom uiPanel, Godot.VBoxContainer node) : base(uiPanel, node) {  }
        public override VBoxContainer Clone() => new (UiPanel, (Godot.VBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.MarginContainer"/>, 路径: MapEditorCreateRoom.MarginContainer
    /// </summary>
    public class MarginContainer : UiNode<MapEditorCreateRoom, Godot.MarginContainer, MarginContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: MapEditorCreateRoom.VBoxContainer
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

        public MarginContainer(MapEditorCreateRoom uiPanel, Godot.MarginContainer node) : base(uiPanel, node) {  }
        public override MarginContainer Clone() => new (UiPanel, (Godot.MarginContainer)Instance.Duplicate());
    }


    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorCreateRoom.MarginContainer.VBoxContainer.HBoxContainer.RoomNameLabel
    /// </summary>
    public RoomNameLabel S_RoomNameLabel => L_MarginContainer.L_VBoxContainer.L_HBoxContainer.L_RoomNameLabel;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.LineEdit"/>, 节点路径: MapEditorCreateRoom.MarginContainer.VBoxContainer.HBoxContainer.RoomNameInput
    /// </summary>
    public RoomNameInput S_RoomNameInput => L_MarginContainer.L_VBoxContainer.L_HBoxContainer.L_RoomNameInput;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditorCreateRoom.MarginContainer.VBoxContainer.HBoxContainer
    /// </summary>
    public HBoxContainer S_HBoxContainer => L_MarginContainer.L_VBoxContainer.L_HBoxContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorCreateRoom.MarginContainer.VBoxContainer.HBoxContainer2.GroupNameLabel
    /// </summary>
    public GroupNameLabel S_GroupNameLabel => L_MarginContainer.L_VBoxContainer.L_HBoxContainer2.L_GroupNameLabel;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.OptionButton"/>, 节点路径: MapEditorCreateRoom.MarginContainer.VBoxContainer.HBoxContainer2.GroupSelect
    /// </summary>
    public GroupSelect S_GroupSelect => L_MarginContainer.L_VBoxContainer.L_HBoxContainer2.L_GroupSelect;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditorCreateRoom.MarginContainer.VBoxContainer.HBoxContainer2
    /// </summary>
    public HBoxContainer2 S_HBoxContainer2 => L_MarginContainer.L_VBoxContainer.L_HBoxContainer2;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorCreateRoom.MarginContainer.VBoxContainer.HBoxContainer3.TypeNameLabel
    /// </summary>
    public TypeNameLabel S_TypeNameLabel => L_MarginContainer.L_VBoxContainer.L_HBoxContainer3.L_TypeNameLabel;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.OptionButton"/>, 节点路径: MapEditorCreateRoom.MarginContainer.VBoxContainer.HBoxContainer3.TypeSelect
    /// </summary>
    public TypeSelect S_TypeSelect => L_MarginContainer.L_VBoxContainer.L_HBoxContainer3.L_TypeSelect;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditorCreateRoom.MarginContainer.VBoxContainer.HBoxContainer3
    /// </summary>
    public HBoxContainer3 S_HBoxContainer3 => L_MarginContainer.L_VBoxContainer.L_HBoxContainer3;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorCreateRoom.MarginContainer.VBoxContainer.HBoxContainer4.WeightNameLabel
    /// </summary>
    public WeightNameLabel S_WeightNameLabel => L_MarginContainer.L_VBoxContainer.L_HBoxContainer4.L_WeightNameLabel;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.SpinBox"/>, 节点路径: MapEditorCreateRoom.MarginContainer.VBoxContainer.HBoxContainer4.WeightInput
    /// </summary>
    public WeightInput S_WeightInput => L_MarginContainer.L_VBoxContainer.L_HBoxContainer4.L_WeightInput;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditorCreateRoom.MarginContainer.VBoxContainer.HBoxContainer4
    /// </summary>
    public HBoxContainer4 S_HBoxContainer4 => L_MarginContainer.L_VBoxContainer.L_HBoxContainer4;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorCreateRoom.MarginContainer.VBoxContainer.HBoxContainer5.RemarkNameLabel
    /// </summary>
    public RemarkNameLabel S_RemarkNameLabel => L_MarginContainer.L_VBoxContainer.L_HBoxContainer5.L_RemarkNameLabel;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextEdit"/>, 节点路径: MapEditorCreateRoom.MarginContainer.VBoxContainer.HBoxContainer5.RemarkInput
    /// </summary>
    public RemarkInput S_RemarkInput => L_MarginContainer.L_VBoxContainer.L_HBoxContainer5.L_RemarkInput;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditorCreateRoom.MarginContainer.VBoxContainer.HBoxContainer5
    /// </summary>
    public HBoxContainer5 S_HBoxContainer5 => L_MarginContainer.L_VBoxContainer.L_HBoxContainer5;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: MapEditorCreateRoom.MarginContainer.VBoxContainer
    /// </summary>
    public VBoxContainer S_VBoxContainer => L_MarginContainer.L_VBoxContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: MapEditorCreateRoom.MarginContainer
    /// </summary>
    public MarginContainer S_MarginContainer => L_MarginContainer;

}
