namespace UI.MapEditorMapLayer;

/// <summary>
/// Ui代码, 该类是根据ui场景自动生成的, 请不要手动编辑该类, 以免造成代码丢失
/// </summary>
public abstract partial class MapEditorMapLayer : UiBase
{
    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: MapEditorMapLayer.VBoxContainer
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


    public MapEditorMapLayer() : base(nameof(MapEditorMapLayer))
    {
    }

    public sealed override void OnInitNestedUi()
    {
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: MapEditorMapLayer.VBoxContainer.LayerLabel
    /// </summary>
    public class LayerLabel : UiNode<MapEditorMapLayer, Godot.Label, LayerLabel>
    {
        public LayerLabel(MapEditorMapLayer uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override LayerLabel Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureButton"/>, 路径: MapEditorMapLayer.VBoxContainer.ScrollContainer.LayerButton.VisibleButton
    /// </summary>
    public class VisibleButton : UiNode<MapEditorMapLayer, Godot.TextureButton, VisibleButton>
    {
        public VisibleButton(MapEditorMapLayer uiPanel, Godot.TextureButton node) : base(uiPanel, node) {  }
        public override VisibleButton Clone() => new (UiPanel, (Godot.TextureButton)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: MapEditorMapLayer.VBoxContainer.ScrollContainer.LayerButton
    /// </summary>
    public class LayerButton : UiNode<MapEditorMapLayer, Godot.Button, LayerButton>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureButton"/>, 节点路径: MapEditorMapLayer.VBoxContainer.ScrollContainer.VisibleButton
        /// </summary>
        public VisibleButton L_VisibleButton
        {
            get
            {
                if (_L_VisibleButton == null) _L_VisibleButton = new VisibleButton(UiPanel, Instance.GetNodeOrNull<Godot.TextureButton>("VisibleButton"));
                return _L_VisibleButton;
            }
        }
        private VisibleButton _L_VisibleButton;

        public LayerButton(MapEditorMapLayer uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override LayerButton Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.ScrollContainer"/>, 路径: MapEditorMapLayer.VBoxContainer.ScrollContainer
    /// </summary>
    public class ScrollContainer : UiNode<MapEditorMapLayer, Godot.ScrollContainer, ScrollContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorMapLayer.VBoxContainer.LayerButton
        /// </summary>
        public LayerButton L_LayerButton
        {
            get
            {
                if (_L_LayerButton == null) _L_LayerButton = new LayerButton(UiPanel, Instance.GetNodeOrNull<Godot.Button>("LayerButton"));
                return _L_LayerButton;
            }
        }
        private LayerButton _L_LayerButton;

        public ScrollContainer(MapEditorMapLayer uiPanel, Godot.ScrollContainer node) : base(uiPanel, node) {  }
        public override ScrollContainer Clone() => new (UiPanel, (Godot.ScrollContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.VBoxContainer"/>, 路径: MapEditorMapLayer.VBoxContainer
    /// </summary>
    public class VBoxContainer : UiNode<MapEditorMapLayer, Godot.VBoxContainer, VBoxContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorMapLayer.LayerLabel
        /// </summary>
        public LayerLabel L_LayerLabel
        {
            get
            {
                if (_L_LayerLabel == null) _L_LayerLabel = new LayerLabel(UiPanel, Instance.GetNodeOrNull<Godot.Label>("LayerLabel"));
                return _L_LayerLabel;
            }
        }
        private LayerLabel _L_LayerLabel;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ScrollContainer"/>, 节点路径: MapEditorMapLayer.ScrollContainer
        /// </summary>
        public ScrollContainer L_ScrollContainer
        {
            get
            {
                if (_L_ScrollContainer == null) _L_ScrollContainer = new ScrollContainer(UiPanel, Instance.GetNodeOrNull<Godot.ScrollContainer>("ScrollContainer"));
                return _L_ScrollContainer;
            }
        }
        private ScrollContainer _L_ScrollContainer;

        public VBoxContainer(MapEditorMapLayer uiPanel, Godot.VBoxContainer node) : base(uiPanel, node) {  }
        public override VBoxContainer Clone() => new (UiPanel, (Godot.VBoxContainer)Instance.Duplicate());
    }


    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorMapLayer.VBoxContainer.LayerLabel
    /// </summary>
    public LayerLabel S_LayerLabel => L_VBoxContainer.L_LayerLabel;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureButton"/>, 节点路径: MapEditorMapLayer.VBoxContainer.ScrollContainer.LayerButton.VisibleButton
    /// </summary>
    public VisibleButton S_VisibleButton => L_VBoxContainer.L_ScrollContainer.L_LayerButton.L_VisibleButton;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorMapLayer.VBoxContainer.ScrollContainer.LayerButton
    /// </summary>
    public LayerButton S_LayerButton => L_VBoxContainer.L_ScrollContainer.L_LayerButton;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.ScrollContainer"/>, 节点路径: MapEditorMapLayer.VBoxContainer.ScrollContainer
    /// </summary>
    public ScrollContainer S_ScrollContainer => L_VBoxContainer.L_ScrollContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: MapEditorMapLayer.VBoxContainer
    /// </summary>
    public VBoxContainer S_VBoxContainer => L_VBoxContainer;

}
