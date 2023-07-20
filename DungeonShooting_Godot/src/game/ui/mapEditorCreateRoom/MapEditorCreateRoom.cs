namespace UI.MapEditorCreateRoom;

/// <summary>
/// Ui代码, 该类是根据ui场景自动生成的, 请不要手动编辑该类, 以免造成代码丢失
/// </summary>
public abstract partial class MapEditorCreateRoom : UiBase
{
    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: MapEditorCreateRoom.VBoxContainer
    /// </summary>
    public VBoxContainer L_VBoxContainer
    {
        get
        {
            if (_L_VBoxContainer == null) _L_VBoxContainer = new VBoxContainer(this, GetNodeOrNull<Godot.VBoxContainer>("VBoxContainer"));
            return _L_VBoxContainer;
        }
    }
    private VBoxContainer _L_VBoxContainer;


    public MapEditorCreateRoom() : base(nameof(MapEditorCreateRoom))
    {
    }

    public sealed override void OnInitNestedUi()
    {
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: MapEditorCreateRoom.VBoxContainer.HBoxContainer.RoomNameLabel
    /// </summary>
    public class RoomNameLabel : UiNode<MapEditorCreateRoom, Godot.Label, RoomNameLabel>
    {
        public RoomNameLabel(MapEditorCreateRoom uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override RoomNameLabel Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.LineEdit"/>, 路径: MapEditorCreateRoom.VBoxContainer.HBoxContainer.RoomNameInput
    /// </summary>
    public class RoomNameInput : UiNode<MapEditorCreateRoom, Godot.LineEdit, RoomNameInput>
    {
        public RoomNameInput(MapEditorCreateRoom uiPanel, Godot.LineEdit node) : base(uiPanel, node) {  }
        public override RoomNameInput Clone() => new (UiPanel, (Godot.LineEdit)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: MapEditorCreateRoom.VBoxContainer.HBoxContainer
    /// </summary>
    public class HBoxContainer : UiNode<MapEditorCreateRoom, Godot.HBoxContainer, HBoxContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorCreateRoom.VBoxContainer.RoomNameLabel
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.LineEdit"/>, 节点路径: MapEditorCreateRoom.VBoxContainer.RoomNameInput
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
    /// 类型: <see cref="Godot.VBoxContainer"/>, 路径: MapEditorCreateRoom.VBoxContainer
    /// </summary>
    public class VBoxContainer : UiNode<MapEditorCreateRoom, Godot.VBoxContainer, VBoxContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditorCreateRoom.HBoxContainer
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

        public VBoxContainer(MapEditorCreateRoom uiPanel, Godot.VBoxContainer node) : base(uiPanel, node) {  }
        public override VBoxContainer Clone() => new (UiPanel, (Godot.VBoxContainer)Instance.Duplicate());
    }


    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorCreateRoom.VBoxContainer.HBoxContainer.RoomNameLabel
    /// </summary>
    public RoomNameLabel S_RoomNameLabel => L_VBoxContainer.L_HBoxContainer.L_RoomNameLabel;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.LineEdit"/>, 节点路径: MapEditorCreateRoom.VBoxContainer.HBoxContainer.RoomNameInput
    /// </summary>
    public RoomNameInput S_RoomNameInput => L_VBoxContainer.L_HBoxContainer.L_RoomNameInput;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditorCreateRoom.VBoxContainer.HBoxContainer
    /// </summary>
    public HBoxContainer S_HBoxContainer => L_VBoxContainer.L_HBoxContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: MapEditorCreateRoom.VBoxContainer
    /// </summary>
    public VBoxContainer S_VBoxContainer => L_VBoxContainer;

}
