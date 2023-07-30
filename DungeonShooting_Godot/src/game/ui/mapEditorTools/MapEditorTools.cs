namespace UI.MapEditorTools;

/// <summary>
/// Ui代码, 该类是根据ui场景自动生成的, 请不要手动编辑该类, 以免造成代码丢失
/// </summary>
public abstract partial class MapEditorTools : UiBase
{
    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Control"/>, 节点路径: MapEditorTools.DoorToolRoot
    /// </summary>
    public DoorToolRoot L_DoorToolRoot
    {
        get
        {
            if (_L_DoorToolRoot == null) _L_DoorToolRoot = new DoorToolRoot(this, GetNodeOrNull<Godot.Control>("DoorToolRoot"));
            return _L_DoorToolRoot;
        }
    }
    private DoorToolRoot _L_DoorToolRoot;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditorTools.HBoxContainer
    /// </summary>
    public HBoxContainer L_HBoxContainer
    {
        get
        {
            if (_L_HBoxContainer == null) _L_HBoxContainer = new HBoxContainer(this, GetNodeOrNull<Godot.HBoxContainer>("HBoxContainer"));
            return _L_HBoxContainer;
        }
    }
    private HBoxContainer _L_HBoxContainer;


    public MapEditorTools() : base(nameof(MapEditorTools))
    {
    }

    public sealed override void OnInitNestedUi()
    {
    }

    /// <summary>
    /// 类型: <see cref="DoorHoverArea"/>, 路径: MapEditorTools.DoorToolRoot.N_HoverRoot.N_HoverArea
    /// </summary>
    public class N_HoverArea : UiNode<MapEditorTools, DoorHoverArea, N_HoverArea>
    {
        public N_HoverArea(MapEditorTools uiPanel, DoorHoverArea node) : base(uiPanel, node) {  }
        public override N_HoverArea Clone() => new (UiPanel, (DoorHoverArea)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Control"/>, 路径: MapEditorTools.DoorToolRoot.N_HoverRoot
    /// </summary>
    public class N_HoverRoot : UiNode<MapEditorTools, Godot.Control, N_HoverRoot>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="DoorHoverArea"/>, 节点路径: MapEditorTools.DoorToolRoot.N_HoverArea
        /// </summary>
        public N_HoverArea L_N_HoverArea
        {
            get
            {
                if (_L_N_HoverArea == null) _L_N_HoverArea = new N_HoverArea(UiPanel, Instance.GetNodeOrNull<DoorHoverArea>("N_HoverArea"));
                return _L_N_HoverArea;
            }
        }
        private N_HoverArea _L_N_HoverArea;

        public N_HoverRoot(MapEditorTools uiPanel, Godot.Control node) : base(uiPanel, node) {  }
        public override N_HoverRoot Clone() => new (UiPanel, (Godot.Control)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="DoorHoverArea"/>, 路径: MapEditorTools.DoorToolRoot.E_HoverRoot.E_HoverArea
    /// </summary>
    public class E_HoverArea : UiNode<MapEditorTools, DoorHoverArea, E_HoverArea>
    {
        public E_HoverArea(MapEditorTools uiPanel, DoorHoverArea node) : base(uiPanel, node) {  }
        public override E_HoverArea Clone() => new (UiPanel, (DoorHoverArea)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Control"/>, 路径: MapEditorTools.DoorToolRoot.E_HoverRoot
    /// </summary>
    public class E_HoverRoot : UiNode<MapEditorTools, Godot.Control, E_HoverRoot>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="DoorHoverArea"/>, 节点路径: MapEditorTools.DoorToolRoot.E_HoverArea
        /// </summary>
        public E_HoverArea L_E_HoverArea
        {
            get
            {
                if (_L_E_HoverArea == null) _L_E_HoverArea = new E_HoverArea(UiPanel, Instance.GetNodeOrNull<DoorHoverArea>("E_HoverArea"));
                return _L_E_HoverArea;
            }
        }
        private E_HoverArea _L_E_HoverArea;

        public E_HoverRoot(MapEditorTools uiPanel, Godot.Control node) : base(uiPanel, node) {  }
        public override E_HoverRoot Clone() => new (UiPanel, (Godot.Control)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="DoorHoverArea"/>, 路径: MapEditorTools.DoorToolRoot.S_HoverRoot.S_HoverArea
    /// </summary>
    public class S_HoverArea : UiNode<MapEditorTools, DoorHoverArea, S_HoverArea>
    {
        public S_HoverArea(MapEditorTools uiPanel, DoorHoverArea node) : base(uiPanel, node) {  }
        public override S_HoverArea Clone() => new (UiPanel, (DoorHoverArea)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Control"/>, 路径: MapEditorTools.DoorToolRoot.S_HoverRoot
    /// </summary>
    public class S_HoverRoot : UiNode<MapEditorTools, Godot.Control, S_HoverRoot>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="DoorHoverArea"/>, 节点路径: MapEditorTools.DoorToolRoot.S_HoverArea
        /// </summary>
        public S_HoverArea L_S_HoverArea
        {
            get
            {
                if (_L_S_HoverArea == null) _L_S_HoverArea = new S_HoverArea(UiPanel, Instance.GetNodeOrNull<DoorHoverArea>("S_HoverArea"));
                return _L_S_HoverArea;
            }
        }
        private S_HoverArea _L_S_HoverArea;

        public S_HoverRoot(MapEditorTools uiPanel, Godot.Control node) : base(uiPanel, node) {  }
        public override S_HoverRoot Clone() => new (UiPanel, (Godot.Control)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="DoorHoverArea"/>, 路径: MapEditorTools.DoorToolRoot.W_HoverRoot.W_HoverArea
    /// </summary>
    public class W_HoverArea : UiNode<MapEditorTools, DoorHoverArea, W_HoverArea>
    {
        public W_HoverArea(MapEditorTools uiPanel, DoorHoverArea node) : base(uiPanel, node) {  }
        public override W_HoverArea Clone() => new (UiPanel, (DoorHoverArea)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Control"/>, 路径: MapEditorTools.DoorToolRoot.W_HoverRoot
    /// </summary>
    public class W_HoverRoot : UiNode<MapEditorTools, Godot.Control, W_HoverRoot>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="DoorHoverArea"/>, 节点路径: MapEditorTools.DoorToolRoot.W_HoverArea
        /// </summary>
        public W_HoverArea L_W_HoverArea
        {
            get
            {
                if (_L_W_HoverArea == null) _L_W_HoverArea = new W_HoverArea(UiPanel, Instance.GetNodeOrNull<DoorHoverArea>("W_HoverArea"));
                return _L_W_HoverArea;
            }
        }
        private W_HoverArea _L_W_HoverArea;

        public W_HoverRoot(MapEditorTools uiPanel, Godot.Control node) : base(uiPanel, node) {  }
        public override W_HoverRoot Clone() => new (UiPanel, (Godot.Control)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: MapEditorTools.DoorToolRoot.HoverPreviewRoot.HoverPreview
    /// </summary>
    public class HoverPreview : UiNode<MapEditorTools, Godot.TextureRect, HoverPreview>
    {
        public HoverPreview(MapEditorTools uiPanel, Godot.TextureRect node) : base(uiPanel, node) {  }
        public override HoverPreview Clone() => new (UiPanel, (Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Control"/>, 路径: MapEditorTools.DoorToolRoot.HoverPreviewRoot
    /// </summary>
    public class HoverPreviewRoot : UiNode<MapEditorTools, Godot.Control, HoverPreviewRoot>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: MapEditorTools.DoorToolRoot.HoverPreview
        /// </summary>
        public HoverPreview L_HoverPreview
        {
            get
            {
                if (_L_HoverPreview == null) _L_HoverPreview = new HoverPreview(UiPanel, Instance.GetNodeOrNull<Godot.TextureRect>("HoverPreview"));
                return _L_HoverPreview;
            }
        }
        private HoverPreview _L_HoverPreview;

        public HoverPreviewRoot(MapEditorTools uiPanel, Godot.Control node) : base(uiPanel, node) {  }
        public override HoverPreviewRoot Clone() => new (UiPanel, (Godot.Control)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.ColorRect"/>, 路径: MapEditorTools.DoorToolRoot.DoorToolTemplate.DoorArea
    /// </summary>
    public class DoorArea : UiNode<MapEditorTools, Godot.ColorRect, DoorArea>
    {
        public DoorArea(MapEditorTools uiPanel, Godot.ColorRect node) : base(uiPanel, node) {  }
        public override DoorArea Clone() => new (UiPanel, (Godot.ColorRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="UI.MapEditorTools.DoorDragButton"/>, 路径: MapEditorTools.DoorToolRoot.DoorToolTemplate.StartBtn
    /// </summary>
    public class StartBtn : UiNode<MapEditorTools, UI.MapEditorTools.DoorDragButton, StartBtn>
    {
        public StartBtn(MapEditorTools uiPanel, UI.MapEditorTools.DoorDragButton node) : base(uiPanel, node) {  }
        public override StartBtn Clone() => new (UiPanel, (UI.MapEditorTools.DoorDragButton)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="UI.MapEditorTools.DoorDragButton"/>, 路径: MapEditorTools.DoorToolRoot.DoorToolTemplate.EndBtn
    /// </summary>
    public class EndBtn : UiNode<MapEditorTools, UI.MapEditorTools.DoorDragButton, EndBtn>
    {
        public EndBtn(MapEditorTools uiPanel, UI.MapEditorTools.DoorDragButton node) : base(uiPanel, node) {  }
        public override EndBtn Clone() => new (UiPanel, (UI.MapEditorTools.DoorDragButton)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="UI.MapEditorTools.DoorDragArea"/>, 路径: MapEditorTools.DoorToolRoot.DoorToolTemplate
    /// </summary>
    public class DoorToolTemplate : UiNode<MapEditorTools, UI.MapEditorTools.DoorDragArea, DoorToolTemplate>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ColorRect"/>, 节点路径: MapEditorTools.DoorToolRoot.DoorArea
        /// </summary>
        public DoorArea L_DoorArea
        {
            get
            {
                if (_L_DoorArea == null) _L_DoorArea = new DoorArea(UiPanel, Instance.GetNodeOrNull<Godot.ColorRect>("DoorArea"));
                return _L_DoorArea;
            }
        }
        private DoorArea _L_DoorArea;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="UI.MapEditorTools.DoorDragButton"/>, 节点路径: MapEditorTools.DoorToolRoot.StartBtn
        /// </summary>
        public StartBtn L_StartBtn
        {
            get
            {
                if (_L_StartBtn == null) _L_StartBtn = new StartBtn(UiPanel, Instance.GetNodeOrNull<UI.MapEditorTools.DoorDragButton>("StartBtn"));
                return _L_StartBtn;
            }
        }
        private StartBtn _L_StartBtn;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="UI.MapEditorTools.DoorDragButton"/>, 节点路径: MapEditorTools.DoorToolRoot.EndBtn
        /// </summary>
        public EndBtn L_EndBtn
        {
            get
            {
                if (_L_EndBtn == null) _L_EndBtn = new EndBtn(UiPanel, Instance.GetNodeOrNull<UI.MapEditorTools.DoorDragButton>("EndBtn"));
                return _L_EndBtn;
            }
        }
        private EndBtn _L_EndBtn;

        public DoorToolTemplate(MapEditorTools uiPanel, UI.MapEditorTools.DoorDragArea node) : base(uiPanel, node) {  }
        public override DoorToolTemplate Clone() => new (UiPanel, (UI.MapEditorTools.DoorDragArea)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Control"/>, 路径: MapEditorTools.DoorToolRoot
    /// </summary>
    public class DoorToolRoot : UiNode<MapEditorTools, Godot.Control, DoorToolRoot>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Control"/>, 节点路径: MapEditorTools.N_HoverRoot
        /// </summary>
        public N_HoverRoot L_N_HoverRoot
        {
            get
            {
                if (_L_N_HoverRoot == null) _L_N_HoverRoot = new N_HoverRoot(UiPanel, Instance.GetNodeOrNull<Godot.Control>("N_HoverRoot"));
                return _L_N_HoverRoot;
            }
        }
        private N_HoverRoot _L_N_HoverRoot;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Control"/>, 节点路径: MapEditorTools.E_HoverRoot
        /// </summary>
        public E_HoverRoot L_E_HoverRoot
        {
            get
            {
                if (_L_E_HoverRoot == null) _L_E_HoverRoot = new E_HoverRoot(UiPanel, Instance.GetNodeOrNull<Godot.Control>("E_HoverRoot"));
                return _L_E_HoverRoot;
            }
        }
        private E_HoverRoot _L_E_HoverRoot;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Control"/>, 节点路径: MapEditorTools.S_HoverRoot
        /// </summary>
        public S_HoverRoot L_S_HoverRoot
        {
            get
            {
                if (_L_S_HoverRoot == null) _L_S_HoverRoot = new S_HoverRoot(UiPanel, Instance.GetNodeOrNull<Godot.Control>("S_HoverRoot"));
                return _L_S_HoverRoot;
            }
        }
        private S_HoverRoot _L_S_HoverRoot;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Control"/>, 节点路径: MapEditorTools.W_HoverRoot
        /// </summary>
        public W_HoverRoot L_W_HoverRoot
        {
            get
            {
                if (_L_W_HoverRoot == null) _L_W_HoverRoot = new W_HoverRoot(UiPanel, Instance.GetNodeOrNull<Godot.Control>("W_HoverRoot"));
                return _L_W_HoverRoot;
            }
        }
        private W_HoverRoot _L_W_HoverRoot;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Control"/>, 节点路径: MapEditorTools.HoverPreviewRoot
        /// </summary>
        public HoverPreviewRoot L_HoverPreviewRoot
        {
            get
            {
                if (_L_HoverPreviewRoot == null) _L_HoverPreviewRoot = new HoverPreviewRoot(UiPanel, Instance.GetNodeOrNull<Godot.Control>("HoverPreviewRoot"));
                return _L_HoverPreviewRoot;
            }
        }
        private HoverPreviewRoot _L_HoverPreviewRoot;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="UI.MapEditorTools.DoorDragArea"/>, 节点路径: MapEditorTools.DoorToolTemplate
        /// </summary>
        public DoorToolTemplate L_DoorToolTemplate
        {
            get
            {
                if (_L_DoorToolTemplate == null) _L_DoorToolTemplate = new DoorToolTemplate(UiPanel, Instance.GetNodeOrNull<UI.MapEditorTools.DoorDragArea>("DoorToolTemplate"));
                return _L_DoorToolTemplate;
            }
        }
        private DoorToolTemplate _L_DoorToolTemplate;

        public DoorToolRoot(MapEditorTools uiPanel, Godot.Control node) : base(uiPanel, node) {  }
        public override DoorToolRoot Clone() => new (UiPanel, (Godot.Control)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: MapEditorTools.HBoxContainer.ToolButton.Select
    /// </summary>
    public class Select : UiNode<MapEditorTools, Godot.TextureRect, Select>
    {
        public Select(MapEditorTools uiPanel, Godot.TextureRect node) : base(uiPanel, node) {  }
        public override Select Clone() => new (UiPanel, (Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureButton"/>, 路径: MapEditorTools.HBoxContainer.ToolButton
    /// </summary>
    public class ToolButton : UiNode<MapEditorTools, Godot.TextureButton, ToolButton>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: MapEditorTools.HBoxContainer.Select
        /// </summary>
        public Select L_Select
        {
            get
            {
                if (_L_Select == null) _L_Select = new Select(UiPanel, Instance.GetNodeOrNull<Godot.TextureRect>("Select"));
                return _L_Select;
            }
        }
        private Select _L_Select;

        public ToolButton(MapEditorTools uiPanel, Godot.TextureButton node) : base(uiPanel, node) {  }
        public override ToolButton Clone() => new (UiPanel, (Godot.TextureButton)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: MapEditorTools.HBoxContainer
    /// </summary>
    public class HBoxContainer : UiNode<MapEditorTools, Godot.HBoxContainer, HBoxContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureButton"/>, 节点路径: MapEditorTools.ToolButton
        /// </summary>
        public ToolButton L_ToolButton
        {
            get
            {
                if (_L_ToolButton == null) _L_ToolButton = new ToolButton(UiPanel, Instance.GetNodeOrNull<Godot.TextureButton>("ToolButton"));
                return _L_ToolButton;
            }
        }
        private ToolButton _L_ToolButton;

        public HBoxContainer(MapEditorTools uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override HBoxContainer Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }


    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="DoorHoverArea"/>, 节点路径: MapEditorTools.DoorToolRoot.N_HoverRoot.N_HoverArea
    /// </summary>
    public N_HoverArea S_N_HoverArea => L_DoorToolRoot.L_N_HoverRoot.L_N_HoverArea;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Control"/>, 节点路径: MapEditorTools.DoorToolRoot.N_HoverRoot
    /// </summary>
    public N_HoverRoot S_N_HoverRoot => L_DoorToolRoot.L_N_HoverRoot;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="DoorHoverArea"/>, 节点路径: MapEditorTools.DoorToolRoot.E_HoverRoot.E_HoverArea
    /// </summary>
    public E_HoverArea S_E_HoverArea => L_DoorToolRoot.L_E_HoverRoot.L_E_HoverArea;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Control"/>, 节点路径: MapEditorTools.DoorToolRoot.E_HoverRoot
    /// </summary>
    public E_HoverRoot S_E_HoverRoot => L_DoorToolRoot.L_E_HoverRoot;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="DoorHoverArea"/>, 节点路径: MapEditorTools.DoorToolRoot.S_HoverRoot.S_HoverArea
    /// </summary>
    public S_HoverArea S_S_HoverArea => L_DoorToolRoot.L_S_HoverRoot.L_S_HoverArea;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Control"/>, 节点路径: MapEditorTools.DoorToolRoot.S_HoverRoot
    /// </summary>
    public S_HoverRoot S_S_HoverRoot => L_DoorToolRoot.L_S_HoverRoot;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="DoorHoverArea"/>, 节点路径: MapEditorTools.DoorToolRoot.W_HoverRoot.W_HoverArea
    /// </summary>
    public W_HoverArea S_W_HoverArea => L_DoorToolRoot.L_W_HoverRoot.L_W_HoverArea;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Control"/>, 节点路径: MapEditorTools.DoorToolRoot.W_HoverRoot
    /// </summary>
    public W_HoverRoot S_W_HoverRoot => L_DoorToolRoot.L_W_HoverRoot;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: MapEditorTools.DoorToolRoot.HoverPreviewRoot.HoverPreview
    /// </summary>
    public HoverPreview S_HoverPreview => L_DoorToolRoot.L_HoverPreviewRoot.L_HoverPreview;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Control"/>, 节点路径: MapEditorTools.DoorToolRoot.HoverPreviewRoot
    /// </summary>
    public HoverPreviewRoot S_HoverPreviewRoot => L_DoorToolRoot.L_HoverPreviewRoot;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.ColorRect"/>, 节点路径: MapEditorTools.DoorToolRoot.DoorToolTemplate.DoorArea
    /// </summary>
    public DoorArea S_DoorArea => L_DoorToolRoot.L_DoorToolTemplate.L_DoorArea;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="UI.MapEditorTools.DoorDragButton"/>, 节点路径: MapEditorTools.DoorToolRoot.DoorToolTemplate.StartBtn
    /// </summary>
    public StartBtn S_StartBtn => L_DoorToolRoot.L_DoorToolTemplate.L_StartBtn;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="UI.MapEditorTools.DoorDragButton"/>, 节点路径: MapEditorTools.DoorToolRoot.DoorToolTemplate.EndBtn
    /// </summary>
    public EndBtn S_EndBtn => L_DoorToolRoot.L_DoorToolTemplate.L_EndBtn;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="UI.MapEditorTools.DoorDragArea"/>, 节点路径: MapEditorTools.DoorToolRoot.DoorToolTemplate
    /// </summary>
    public DoorToolTemplate S_DoorToolTemplate => L_DoorToolRoot.L_DoorToolTemplate;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Control"/>, 节点路径: MapEditorTools.DoorToolRoot
    /// </summary>
    public DoorToolRoot S_DoorToolRoot => L_DoorToolRoot;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: MapEditorTools.HBoxContainer.ToolButton.Select
    /// </summary>
    public Select S_Select => L_HBoxContainer.L_ToolButton.L_Select;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureButton"/>, 节点路径: MapEditorTools.HBoxContainer.ToolButton
    /// </summary>
    public ToolButton S_ToolButton => L_HBoxContainer.L_ToolButton;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditorTools.HBoxContainer
    /// </summary>
    public HBoxContainer S_HBoxContainer => L_HBoxContainer;

}
