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
            if (_L_VBoxContainer == null) _L_VBoxContainer = new VBoxContainer((MapEditorMapLayerPanel)this, GetNode<Godot.VBoxContainer>("VBoxContainer"));
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
    /// 类型: <see cref="Godot.Label"/>, 路径: MapEditorMapLayer.VBoxContainer.HBoxContainer.LayerLabel
    /// </summary>
    public class LayerLabel : UiNode<MapEditorMapLayerPanel, Godot.Label, LayerLabel>
    {
        public LayerLabel(MapEditorMapLayerPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override LayerLabel Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: MapEditorMapLayer.VBoxContainer.HBoxContainer.AddButton
    /// </summary>
    public class AddButton : UiNode<MapEditorMapLayerPanel, Godot.Button, AddButton>
    {
        public AddButton(MapEditorMapLayerPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override AddButton Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: MapEditorMapLayer.VBoxContainer.HBoxContainer.EditButton
    /// </summary>
    public class EditButton : UiNode<MapEditorMapLayerPanel, Godot.Button, EditButton>
    {
        public EditButton(MapEditorMapLayerPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override EditButton Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: MapEditorMapLayer.VBoxContainer.HBoxContainer.DeleteButton
    /// </summary>
    public class DeleteButton : UiNode<MapEditorMapLayerPanel, Godot.Button, DeleteButton>
    {
        public DeleteButton(MapEditorMapLayerPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override DeleteButton Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: MapEditorMapLayer.VBoxContainer.HBoxContainer
    /// </summary>
    public class HBoxContainer : UiNode<MapEditorMapLayerPanel, Godot.HBoxContainer, HBoxContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorMapLayer.VBoxContainer.LayerLabel
        /// </summary>
        public LayerLabel L_LayerLabel
        {
            get
            {
                if (_L_LayerLabel == null) _L_LayerLabel = new LayerLabel(UiPanel, Instance.GetNode<Godot.Label>("LayerLabel"));
                return _L_LayerLabel;
            }
        }
        private LayerLabel _L_LayerLabel;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorMapLayer.VBoxContainer.AddButton
        /// </summary>
        public AddButton L_AddButton
        {
            get
            {
                if (_L_AddButton == null) _L_AddButton = new AddButton(UiPanel, Instance.GetNode<Godot.Button>("AddButton"));
                return _L_AddButton;
            }
        }
        private AddButton _L_AddButton;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorMapLayer.VBoxContainer.EditButton
        /// </summary>
        public EditButton L_EditButton
        {
            get
            {
                if (_L_EditButton == null) _L_EditButton = new EditButton(UiPanel, Instance.GetNode<Godot.Button>("EditButton"));
                return _L_EditButton;
            }
        }
        private EditButton _L_EditButton;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorMapLayer.VBoxContainer.DeleteButton
        /// </summary>
        public DeleteButton L_DeleteButton
        {
            get
            {
                if (_L_DeleteButton == null) _L_DeleteButton = new DeleteButton(UiPanel, Instance.GetNode<Godot.Button>("DeleteButton"));
                return _L_DeleteButton;
            }
        }
        private DeleteButton _L_DeleteButton;

        public HBoxContainer(MapEditorMapLayerPanel uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override HBoxContainer Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.NinePatchRect"/>, 路径: MapEditorMapLayer.VBoxContainer.ScrollContainer.LayerButton.SelectTexture
    /// </summary>
    public class SelectTexture : UiNode<MapEditorMapLayerPanel, Godot.NinePatchRect, SelectTexture>
    {
        public SelectTexture(MapEditorMapLayerPanel uiPanel, Godot.NinePatchRect node) : base(uiPanel, node) {  }
        public override SelectTexture Clone() => new (UiPanel, (Godot.NinePatchRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureButton"/>, 路径: MapEditorMapLayer.VBoxContainer.ScrollContainer.LayerButton.VisibleButton
    /// </summary>
    public class VisibleButton : UiNode<MapEditorMapLayerPanel, Godot.TextureButton, VisibleButton>
    {
        public VisibleButton(MapEditorMapLayerPanel uiPanel, Godot.TextureButton node) : base(uiPanel, node) {  }
        public override VisibleButton Clone() => new (UiPanel, (Godot.TextureButton)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: MapEditorMapLayer.VBoxContainer.ScrollContainer.LayerButton
    /// </summary>
    public class LayerButton : UiNode<MapEditorMapLayerPanel, Godot.Button, LayerButton>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.NinePatchRect"/>, 节点路径: MapEditorMapLayer.VBoxContainer.ScrollContainer.SelectTexture
        /// </summary>
        public SelectTexture L_SelectTexture
        {
            get
            {
                if (_L_SelectTexture == null) _L_SelectTexture = new SelectTexture(UiPanel, Instance.GetNode<Godot.NinePatchRect>("SelectTexture"));
                return _L_SelectTexture;
            }
        }
        private SelectTexture _L_SelectTexture;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureButton"/>, 节点路径: MapEditorMapLayer.VBoxContainer.ScrollContainer.VisibleButton
        /// </summary>
        public VisibleButton L_VisibleButton
        {
            get
            {
                if (_L_VisibleButton == null) _L_VisibleButton = new VisibleButton(UiPanel, Instance.GetNode<Godot.TextureButton>("VisibleButton"));
                return _L_VisibleButton;
            }
        }
        private VisibleButton _L_VisibleButton;

        public LayerButton(MapEditorMapLayerPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override LayerButton Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.ScrollContainer"/>, 路径: MapEditorMapLayer.VBoxContainer.ScrollContainer
    /// </summary>
    public class ScrollContainer : UiNode<MapEditorMapLayerPanel, Godot.ScrollContainer, ScrollContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorMapLayer.VBoxContainer.LayerButton
        /// </summary>
        public LayerButton L_LayerButton
        {
            get
            {
                if (_L_LayerButton == null) _L_LayerButton = new LayerButton(UiPanel, Instance.GetNode<Godot.Button>("LayerButton"));
                return _L_LayerButton;
            }
        }
        private LayerButton _L_LayerButton;

        public ScrollContainer(MapEditorMapLayerPanel uiPanel, Godot.ScrollContainer node) : base(uiPanel, node) {  }
        public override ScrollContainer Clone() => new (UiPanel, (Godot.ScrollContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.VBoxContainer"/>, 路径: MapEditorMapLayer.VBoxContainer
    /// </summary>
    public class VBoxContainer : UiNode<MapEditorMapLayerPanel, Godot.VBoxContainer, VBoxContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditorMapLayer.HBoxContainer
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ScrollContainer"/>, 节点路径: MapEditorMapLayer.ScrollContainer
        /// </summary>
        public ScrollContainer L_ScrollContainer
        {
            get
            {
                if (_L_ScrollContainer == null) _L_ScrollContainer = new ScrollContainer(UiPanel, Instance.GetNode<Godot.ScrollContainer>("ScrollContainer"));
                return _L_ScrollContainer;
            }
        }
        private ScrollContainer _L_ScrollContainer;

        public VBoxContainer(MapEditorMapLayerPanel uiPanel, Godot.VBoxContainer node) : base(uiPanel, node) {  }
        public override VBoxContainer Clone() => new (UiPanel, (Godot.VBoxContainer)Instance.Duplicate());
    }


    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorMapLayer.VBoxContainer.HBoxContainer.LayerLabel
    /// </summary>
    public LayerLabel S_LayerLabel => L_VBoxContainer.L_HBoxContainer.L_LayerLabel;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorMapLayer.VBoxContainer.HBoxContainer.AddButton
    /// </summary>
    public AddButton S_AddButton => L_VBoxContainer.L_HBoxContainer.L_AddButton;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorMapLayer.VBoxContainer.HBoxContainer.EditButton
    /// </summary>
    public EditButton S_EditButton => L_VBoxContainer.L_HBoxContainer.L_EditButton;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorMapLayer.VBoxContainer.HBoxContainer.DeleteButton
    /// </summary>
    public DeleteButton S_DeleteButton => L_VBoxContainer.L_HBoxContainer.L_DeleteButton;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditorMapLayer.VBoxContainer.HBoxContainer
    /// </summary>
    public HBoxContainer S_HBoxContainer => L_VBoxContainer.L_HBoxContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.NinePatchRect"/>, 节点路径: MapEditorMapLayer.VBoxContainer.ScrollContainer.LayerButton.SelectTexture
    /// </summary>
    public SelectTexture S_SelectTexture => L_VBoxContainer.L_ScrollContainer.L_LayerButton.L_SelectTexture;

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
