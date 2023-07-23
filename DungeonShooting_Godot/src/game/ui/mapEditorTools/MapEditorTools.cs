namespace UI.MapEditorTools;

/// <summary>
/// Ui代码, 该类是根据ui场景自动生成的, 请不要手动编辑该类, 以免造成代码丢失
/// </summary>
public abstract partial class MapEditorTools : UiBase
{
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

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="UI.MapEditorTools.DoorDragArea"/>, 节点路径: MapEditorTools.DoorToolTemplate
    /// </summary>
    public DoorToolTemplate L_DoorToolTemplate
    {
        get
        {
            if (_L_DoorToolTemplate == null) _L_DoorToolTemplate = new DoorToolTemplate(this, GetNodeOrNull<UI.MapEditorTools.DoorDragArea>("DoorToolTemplate"));
            return _L_DoorToolTemplate;
        }
    }
    private DoorToolTemplate _L_DoorToolTemplate;


    public MapEditorTools() : base(nameof(MapEditorTools))
    {
    }

    public sealed override void OnInitNestedUi()
    {
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureButton"/>, 路径: MapEditorTools.HBoxContainer.HandTool
    /// </summary>
    public class HandTool : UiNode<MapEditorTools, Godot.TextureButton, HandTool>
    {
        public HandTool(MapEditorTools uiPanel, Godot.TextureButton node) : base(uiPanel, node) {  }
        public override HandTool Clone() => new (UiPanel, (Godot.TextureButton)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureButton"/>, 路径: MapEditorTools.HBoxContainer.PenTool
    /// </summary>
    public class PenTool : UiNode<MapEditorTools, Godot.TextureButton, PenTool>
    {
        public PenTool(MapEditorTools uiPanel, Godot.TextureButton node) : base(uiPanel, node) {  }
        public override PenTool Clone() => new (UiPanel, (Godot.TextureButton)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureButton"/>, 路径: MapEditorTools.HBoxContainer.RectTool
    /// </summary>
    public class RectTool : UiNode<MapEditorTools, Godot.TextureButton, RectTool>
    {
        public RectTool(MapEditorTools uiPanel, Godot.TextureButton node) : base(uiPanel, node) {  }
        public override RectTool Clone() => new (UiPanel, (Godot.TextureButton)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureButton"/>, 路径: MapEditorTools.HBoxContainer.CenterTool
    /// </summary>
    public class CenterTool : UiNode<MapEditorTools, Godot.TextureButton, CenterTool>
    {
        public CenterTool(MapEditorTools uiPanel, Godot.TextureButton node) : base(uiPanel, node) {  }
        public override CenterTool Clone() => new (UiPanel, (Godot.TextureButton)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: MapEditorTools.HBoxContainer
    /// </summary>
    public class HBoxContainer : UiNode<MapEditorTools, Godot.HBoxContainer, HBoxContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureButton"/>, 节点路径: MapEditorTools.HandTool
        /// </summary>
        public HandTool L_HandTool
        {
            get
            {
                if (_L_HandTool == null) _L_HandTool = new HandTool(UiPanel, Instance.GetNodeOrNull<Godot.TextureButton>("HandTool"));
                return _L_HandTool;
            }
        }
        private HandTool _L_HandTool;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureButton"/>, 节点路径: MapEditorTools.PenTool
        /// </summary>
        public PenTool L_PenTool
        {
            get
            {
                if (_L_PenTool == null) _L_PenTool = new PenTool(UiPanel, Instance.GetNodeOrNull<Godot.TextureButton>("PenTool"));
                return _L_PenTool;
            }
        }
        private PenTool _L_PenTool;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureButton"/>, 节点路径: MapEditorTools.RectTool
        /// </summary>
        public RectTool L_RectTool
        {
            get
            {
                if (_L_RectTool == null) _L_RectTool = new RectTool(UiPanel, Instance.GetNodeOrNull<Godot.TextureButton>("RectTool"));
                return _L_RectTool;
            }
        }
        private RectTool _L_RectTool;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureButton"/>, 节点路径: MapEditorTools.CenterTool
        /// </summary>
        public CenterTool L_CenterTool
        {
            get
            {
                if (_L_CenterTool == null) _L_CenterTool = new CenterTool(UiPanel, Instance.GetNodeOrNull<Godot.TextureButton>("CenterTool"));
                return _L_CenterTool;
            }
        }
        private CenterTool _L_CenterTool;

        public HBoxContainer(MapEditorTools uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override HBoxContainer Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.ColorRect"/>, 路径: MapEditorTools.DoorToolTemplate.DoorArea
    /// </summary>
    public class DoorArea : UiNode<MapEditorTools, Godot.ColorRect, DoorArea>
    {
        public DoorArea(MapEditorTools uiPanel, Godot.ColorRect node) : base(uiPanel, node) {  }
        public override DoorArea Clone() => new (UiPanel, (Godot.ColorRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="UI.MapEditorTools.DoorDragButton"/>, 路径: MapEditorTools.DoorToolTemplate.StartBtn
    /// </summary>
    public class StartBtn : UiNode<MapEditorTools, UI.MapEditorTools.DoorDragButton, StartBtn>
    {
        public StartBtn(MapEditorTools uiPanel, UI.MapEditorTools.DoorDragButton node) : base(uiPanel, node) {  }
        public override StartBtn Clone() => new (UiPanel, (UI.MapEditorTools.DoorDragButton)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="UI.MapEditorTools.DoorDragButton"/>, 路径: MapEditorTools.DoorToolTemplate.EndBtn
    /// </summary>
    public class EndBtn : UiNode<MapEditorTools, UI.MapEditorTools.DoorDragButton, EndBtn>
    {
        public EndBtn(MapEditorTools uiPanel, UI.MapEditorTools.DoorDragButton node) : base(uiPanel, node) {  }
        public override EndBtn Clone() => new (UiPanel, (UI.MapEditorTools.DoorDragButton)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="UI.MapEditorTools.DoorDragArea"/>, 路径: MapEditorTools.DoorToolTemplate
    /// </summary>
    public class DoorToolTemplate : UiNode<MapEditorTools, UI.MapEditorTools.DoorDragArea, DoorToolTemplate>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ColorRect"/>, 节点路径: MapEditorTools.DoorArea
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="UI.MapEditorTools.DoorDragButton"/>, 节点路径: MapEditorTools.StartBtn
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="UI.MapEditorTools.DoorDragButton"/>, 节点路径: MapEditorTools.EndBtn
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
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureButton"/>, 节点路径: MapEditorTools.HBoxContainer.HandTool
    /// </summary>
    public HandTool S_HandTool => L_HBoxContainer.L_HandTool;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureButton"/>, 节点路径: MapEditorTools.HBoxContainer.PenTool
    /// </summary>
    public PenTool S_PenTool => L_HBoxContainer.L_PenTool;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureButton"/>, 节点路径: MapEditorTools.HBoxContainer.RectTool
    /// </summary>
    public RectTool S_RectTool => L_HBoxContainer.L_RectTool;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureButton"/>, 节点路径: MapEditorTools.HBoxContainer.CenterTool
    /// </summary>
    public CenterTool S_CenterTool => L_HBoxContainer.L_CenterTool;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditorTools.HBoxContainer
    /// </summary>
    public HBoxContainer S_HBoxContainer => L_HBoxContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.ColorRect"/>, 节点路径: MapEditorTools.DoorToolTemplate.DoorArea
    /// </summary>
    public DoorArea S_DoorArea => L_DoorToolTemplate.L_DoorArea;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="UI.MapEditorTools.DoorDragButton"/>, 节点路径: MapEditorTools.DoorToolTemplate.StartBtn
    /// </summary>
    public StartBtn S_StartBtn => L_DoorToolTemplate.L_StartBtn;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="UI.MapEditorTools.DoorDragButton"/>, 节点路径: MapEditorTools.DoorToolTemplate.EndBtn
    /// </summary>
    public EndBtn S_EndBtn => L_DoorToolTemplate.L_EndBtn;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="UI.MapEditorTools.DoorDragArea"/>, 节点路径: MapEditorTools.DoorToolTemplate
    /// </summary>
    public DoorToolTemplate S_DoorToolTemplate => L_DoorToolTemplate;

}
